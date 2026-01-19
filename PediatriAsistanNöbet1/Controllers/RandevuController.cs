using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.Model;

namespace PediatriAsistanNöbet1.Controllers
{
    public class RandevuController : BaseController
    {
        public ActionResult Index() { return View(); }

        public JsonResult GetRandevular()
        {
            var list = db.Randevular.Select(x => new
            {
                x.RandevuID,
                Hoca = x.OgretimUyesi.Ad + " " + x.OgretimUyesi.Soyad,
                Asistan = x.Asistan.Ad + " " + x.Asistan.Soyad,
                Tarih = x.RandevuTarihi,
                Durum = x.RandevuDurum.DurumAdi,
                Aciklama = x.Aciklama
            }).ToList()
            .Select(x => new {
                x.RandevuID,
                x.Hoca,
                x.Asistan,
                Tarih = x.Tarih.ToString("dd.MM.yyyy HH:mm"),
                x.Durum,
                x.Aciklama
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

 
        [HttpPost]
        public JsonResult Sil(int id)
        {
            var r = db.Randevular.Find(id);
            if (r != null) { db.Randevular.Remove(r); db.SaveChanges(); return Json(new { success = true }); }
            return Json(new { success = false });
        }
    }
}