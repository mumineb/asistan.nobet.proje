using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;

namespace PediatriAsistanNöbet1.Controllers
{
    public class AcilDurumController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAcilDurumlar()
        {
            var list = db.AcilDurumlar
                         .OrderByDescending(x => x.Tarih)
                         .Select(x => new {
                             x.AcilDurumID,
                             x.Baslik,
                             x.Icerik,
                             Tarih = x.Tarih.ToString() // 
                         }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    
        [HttpPost]
        public JsonResult Kaydet(AcilDurum acilDurum)
        {
            string mesaj = "";
            try
            {
                if (acilDurum.AcilDurumID == 0) // ID 0 ise Yeni Kayıt
                {
                    acilDurum.Tarih = DateTime.Now;
                    db.AcilDurumlar.Add(acilDurum);
                    mesaj = "Başarıyla eklendi.";
                }
                else // ID varsa Güncelleme
                {
                    var mevcut = db.AcilDurumlar.Find(acilDurum.AcilDurumID);
                    if (mevcut == null) return Json(new { success = false, message = "Kayıt bulunamadı" });

                    mevcut.Baslik = acilDurum.Baslik;
                    mevcut.Icerik = acilDurum.Icerik;

                    mesaj = "Başarıyla güncellendi.";
                }

                db.SaveChanges();
                return Json(new { success = true, message = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }

        // SİLME
        [HttpPost]
        public JsonResult Sil(int id)
        {
            try
            {
                var kayit = db.AcilDurumlar.Find(id);
                if (kayit != null)
                {
                    db.AcilDurumlar.Remove(kayit);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Kayıt silindi." });
                }
                return Json(new { success = false, message = "Kayıt bulunamadı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Silinemedi: " + ex.Message });
            }
        }

        // DÜZENLEME İÇİN VERİ GETİRME
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var kayit = db.AcilDurumlar.Find(id);

            var result = new
            {
                kayit.AcilDurumID,
                kayit.Baslik,
                kayit.Icerik
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}