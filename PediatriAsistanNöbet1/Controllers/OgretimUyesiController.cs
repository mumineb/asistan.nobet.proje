using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.Model;


namespace PediatriAsistanNöbet1.Controllers
{
    public class OgretimUyesiController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetOgretimUyeleri()
        {
            var list = db.OgretimUyeleri.Select(x => new
            {
                x.OgretimUyesiID,
                x.Ad,
                x.Soyad,
                BolumAdi = x.Bolum.BolumAdi, 
                x.Telefon,
                x.Email,
                x.Adres
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBolumListesi()
        {
            var bolumler = db.Bolumler.Select(x => new
            {
                x.BolumID,
                x.BolumAdi
            }).OrderBy(x => x.BolumAdi).ToList();

            return Json(bolumler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kaydet(OgretimUyesi hoca)
        {
            try
            {
                if (hoca.OgretimUyesiID == 0) // Yeni Kayıt
                {
                    db.OgretimUyeleri.Add(hoca);
                }
                else // Güncelleme
                {
                    var mevcut = db.OgretimUyeleri.Find(hoca.OgretimUyesiID);
                    if (mevcut == null) return Json(new { success = false, message = "Kayıt bulunamadı" });

                    mevcut.Ad = hoca.Ad;
                    mevcut.Soyad = hoca.Soyad;
                    mevcut.BolumID = hoca.BolumID; // Bölüm değişikliği
                    mevcut.Telefon = hoca.Telefon;
                    mevcut.Email = hoca.Email;
                    mevcut.Adres = hoca.Adres;
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
                var kayit = db.OgretimUyeleri.Find(id);
                if (kayit != null)
                {
                    db.OgretimUyeleri.Remove(kayit);
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
            var data = db.OgretimUyeleri.Find(id);

            return Json(new
            {
                data.OgretimUyesiID,
                data.Ad,
                data.Soyad,
                data.BolumID, 
                data.Telefon,
                data.Email,
                data.Adres
            }, JsonRequestBehavior.AllowGet);
        }
    }
}