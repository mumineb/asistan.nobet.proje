using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Controllers; 
using PediatriAsistanNöbet1.Models.Model;

namespace PediatriAsistanNöbet1.Controllers
{
    public class AsistanController : BaseController
    {
        public ActionResult Index() { return View(); }

        public JsonResult GetAsistanlar()
        {

            var list = db.Asistanlar.Select(x => new
            {
                x.AsistanID,
                x.Ad,
                x.Soyad,
                x.Telefon,
                x.Email,
                x.Adres
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kaydet(Asistan asistan)
        {
            try
            {
                if (asistan.AsistanID == 0)
                {
                    db.Asistanlar.Add(asistan);
                }
                else
                {
                    var a = db.Asistanlar.Find(asistan.AsistanID);
                    if (a == null) return Json(new { success = false, message = "Bulunamadı" });

                    a.Ad = asistan.Ad; a.Soyad = asistan.Soyad;
                    a.Telefon = asistan.Telefon; a.Email = asistan.Email; a.Adres = asistan.Adres;
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult Sil(int id)
        {
            try
            {
                var a = db.Asistanlar.Find(id);
                if (a != null) { db.Asistanlar.Remove(a); db.SaveChanges(); }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            var data = db.Asistanlar.Find(id);
            return Json(new { data.AsistanID, data.Ad, data.Soyad, data.Telefon, data.Email, data.Adres }, JsonRequestBehavior.AllowGet);
        }
    }
}