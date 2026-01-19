using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;


namespace PediatriAsistanNöbet1.Controllers
{
    public class HomeController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();
        // GET: Home
       

        public ActionResult Index()
        {
            // 1. Bugünün tarihini al (Saat kısmını sıfırlayarak)
            var bugun = DateTime.Today;

            // 2. Sadece BUGÜNÜN Nöbetlerini Çek
            // DbFunctions.TruncateTime kullanıyoruz ki saat farkından dolayı veri kaçmasın.
            ViewBag.BugununNobetleri = db.Nobetler
                                         .Include(n => n.Asistan)
                                         .Include(n => n.Bolum)
                                         .Where(x => DbFunctions.TruncateTime(x.NobetTarihi) == bugun)
                                         .ToList();

            // 3. Son Eklenen 5 Acil Durumu Çek
            ViewBag.Duyurular = db.AcilDurumlar
                                  .OrderByDescending(x => x.Tarih)
                                  .Take(5)
                                  .ToList();

            return View();
        }

        public ActionResult Asistan()
        {
            return View(db.Asistanlar.ToList());
        }

        public ActionResult OgretimUyesi()

        {
            return View(db.OgretimUyeleri.Include("Bolum").ToList());

        }

        public ActionResult Bolum()

        {
            return View(db.Bolumler.ToList());

        }

        public ActionResult AcilDurum()
        {
            return View(db.AcilDurumlar.ToList());
        }


        public ActionResult Nobet()
        {
            return View();
        }
        public JsonResult GetNobetler()
        {
            var nobetler = db.Nobetler
                             .Include(n => n.Asistan)
                             .Include(n => n.Bolum)
                             .ToList()
                             .Select(n => new
                             {
                                 title = $"{n.Bolum.BolumAdi} - {n.Asistan.Ad} {n.Asistan.Soyad}",
                                 start = n.NobetTarihi.ToString("yyyy-MM-dd"),
                                                                               
                                 allDay = true, 
                                 color = "#0d6efd",
                                 textColor = "#ffffff"
                             })
                             .ToList();

            return Json(nobetler, JsonRequestBehavior.AllowGet);
        }


        // HomeController.cs içine eklenecek

        // Müsaitlikleri listele (Henüz randevu alınmamış olanlar)
        public ActionResult Randevu()
        {
            // Dolu olan müsaitlik ID'lerini bul
            var doluMusaitlikler = db.Randevular.Select(r => r.MusaitlikID).ToList();

            // Sadece boş olanları ve tarihi geçmemişleri getir
            var musaitlikler = db.Musaitlikler
                                 .Include("OgretimUyesi")
                                 .Where(m => !doluMusaitlikler.Contains(m.MusaitlikID) && m.Tarih >= DateTime.Today)
                                 .OrderBy(m => m.Tarih)
                                 .ThenBy(m => m.Saat)
                                 .ToList();

            return View(musaitlikler);
        }


        [HttpPost]
        public JsonResult RandevuOlustur(int musaitlikId, string aciklama, string ogrenciNo)
        {
            try
            {
                // 1. Müsaitlik Kontrolü
                var musaitlik = db.Musaitlikler.Find(musaitlikId);
                if (musaitlik == null)
                    return Json(new { success = false, message = "Seçilen saat bulunamadı." });

                var doluMu = db.Randevular.Any(r => r.MusaitlikID == musaitlikId);
                if (doluMu)
                    return Json(new { success = false, message = "Bu randevu az önce başkası tarafından alındı." });

                // 2. Otomatik Asistan Atama (Geçici Çözüm)
                // Eğer giriş sistemi yoksa, veritabanındaki İLK asistanı seçelim ki hata vermesin.
                var varsayilanAsistan = db.Asistanlar.FirstOrDefault();
                if (varsayilanAsistan == null)
                    return Json(new { success = false, message = "Sistemde kayıtlı hiç asistan yok! Lütfen önce Admin panelinden asistan ekleyin." });

                // 3. Randevu Durumu Bulma
                // 'Aktif' adında bir durum arar, bulamazsa ilk bulduğu durumu atar.
                var durum = db.RandevuDurumlari.FirstOrDefault(x => x.DurumAdi == "Aktif")
                            ?? db.RandevuDurumlari.FirstOrDefault();

                if (durum == null)
                {
                    // Hiç durum yoksa kodla geçici bir tane oluşturalım
                    var yeniDurum = new PediatriAsistanNöbet1.Models.Model.RandevuDurum { DurumAdi = "Aktif" };
                    db.RandevuDurumlari.Add(yeniDurum);
                    db.SaveChanges(); // ID oluşsun diye kaydet
                    durum = yeniDurum;
                }

                // 4. Kayıt İşlemi
                PediatriAsistanNöbet1.Models.Model.Randevu yeniRandevu = new PediatriAsistanNöbet1.Models.Model.Randevu();
                yeniRandevu.MusaitlikID = musaitlikId;
                yeniRandevu.OgretimUyesiID = musaitlik.OgretimUyesiID ?? 0;
                yeniRandevu.RandevuTarihi = musaitlik.Tarih; // Saati de içeren DateTime olmalı

                yeniRandevu.Aciklama = $"Öğrenci No: {ogrenciNo} - {aciklama}";
                yeniRandevu.AsistanID = varsayilanAsistan.AsistanID; // Bulduğumuz ilk asistanı atadık
                yeniRandevu.RandevuDurumID = durum.RandevuDurumID; // Bulduğumuz durumu atadık

                db.Randevular.Add(yeniRandevu);
                db.SaveChanges();

                return Json(new { success = true, message = "Randevu başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                // Hatanın detayını görelim (InnerException varsa onu da yazdıralım)
                string hataMesaji = ex.Message;
                if (ex.InnerException != null) hataMesaji += " | " + ex.InnerException.Message;

                return Json(new { success = false, message = "Hata oluştu: " + hataMesaji });
            }
        }
    }
}