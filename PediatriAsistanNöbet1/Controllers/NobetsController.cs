using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.Model;


namespace PediatriAsistanNöbet1.Controllers
{
    public class NobetsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNobetler()
        {
            var list = db.Nobetler.Select(x => new
            {
                x.NobetID,
                AsistanAd = x.Asistan.Ad + " " + x.Asistan.Soyad,
                BolumAd = x.Bolum.BolumAdi,
                Tarih = x.NobetTarihi
            }).ToList()
            .Select(x => new {
                x.NobetID,
                x.AsistanAd,
                x.BolumAd,
                Tarih = x.Tarih.ToString("dd.MM.yyyy") // Tarih formatı
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // Dropdownlar için Asistan ve Bölümleri getir
        public JsonResult GetVeriler()
        {
            var asistanlar = db.Asistanlar.Select(x => new { x.AsistanID, AdSoyad = x.Ad + " " + x.Soyad }).ToList();
            var bolumler = db.Bolumler.Select(x => new { x.BolumID, x.BolumAdi }).ToList();

            return Json(new { asistanlar, bolumler }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kaydet(Nobet nobet)
        {
            try
            {
                if (nobet.NobetID == 0)
                {
                    db.Nobetler.Add(nobet);
                }
                else
                {
                    var mevcut = db.Nobetler.Find(nobet.NobetID);
                    if (mevcut == null) return Json(new { success = false, message = "Kayıt bulunamadı" });

                    mevcut.AsistanID = nobet.AsistanID;
                    mevcut.BolumID = nobet.BolumID;
                    mevcut.NobetTarihi = nobet.NobetTarihi;
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Sil(int id)
        {
            try
            {
                var kayit = db.Nobetler.Find(id);
                if (kayit != null)
                {
                    db.Nobetler.Remove(kayit);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Bulunamadı" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            var data = db.Nobetler.Find(id);
            return Json(new
            {
                data.NobetID,
                data.AsistanID,
                data.BolumID,
                Tarih = data.NobetTarihi.ToString("yyyy-MM-dd") // Input type=date için format
            }, JsonRequestBehavior.AllowGet);
        }
    }
}