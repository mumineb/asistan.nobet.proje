using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.Model;

namespace PediatriAsistanNöbet1.Controllers
{
    public class MusaitlikController : BaseController
    {
        public ActionResult Index() { return View(); }

        public JsonResult GetMusaitlikler()
        {
            var list = db.Musaitlikler.Select(x => new
            {
                x.MusaitlikID,
                HocaAd = x.OgretimUyesi.Ad + " " + x.OgretimUyesi.Soyad,
                Tarih = x.Tarih,
                Saat = x.Saat
            }).ToList()
            .Select(x => new {
                x.MusaitlikID,
                x.HocaAd,
                Tarih = x.Tarih.ToString("dd.MM.yyyy"),
                Saat = x.Saat.ToString(@"hh\:mm") // TimeSpan formatı
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHocalar()
        {
            var data = db.OgretimUyeleri.Select(x => new { x.OgretimUyesiID, AdSoyad = x.Ad + " " + x.Soyad }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kaydet(Musaitlik m)
        {
            try
            {
                if (m.MusaitlikID == 0) db.Musaitlikler.Add(m);
                else
                {
                    var mevcut = db.Musaitlikler.Find(m.MusaitlikID);
                    mevcut.OgretimUyesiID = m.OgretimUyesiID;
                    mevcut.Tarih = m.Tarih;
                    mevcut.Saat = m.Saat;
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult Sil(int id)
        {
            var k = db.Musaitlikler.Find(id);
            if (k != null) { db.Musaitlikler.Remove(k); db.SaveChanges(); return Json(new { success = true }); }
            return Json(new { success = false });
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            var d = db.Musaitlikler.Find(id);
            return Json(new
            {
                d.MusaitlikID,
                d.OgretimUyesiID,
                Tarih = d.Tarih.ToString("yyyy-MM-dd"),
                Saat = d.Saat.ToString(@"hh\:mm")
            }, JsonRequestBehavior.AllowGet);
        }
    }
}