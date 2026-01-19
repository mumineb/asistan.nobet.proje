using System;
using System.Linq;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Controllers;
using PediatriAsistanNöbet1.Models.Model;

namespace PediatriAsistanNöbet1.Controllers
{
    public class BolumController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBolumler()
        {
            var list = db.Bolumler.Select(x => new
            {
                x.BolumID,
                x.BolumAdi,
                x.HastaSayisi,
                x.BosYatakSayisi,
                x.Aciklama
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kaydet(Bolum bolum)
        {
            try
            {
                if (bolum.BolumID == 0)
                {
                    db.Bolumler.Add(bolum);
                }
                else
                {
                    var mevcut = db.Bolumler.Find(bolum.BolumID);
                    if (mevcut == null) return Json(new { success = false, message = "Kayıt bulunamadı" });

                    mevcut.BolumAdi = bolum.BolumAdi;
                    mevcut.HastaSayisi = bolum.HastaSayisi;
                    mevcut.BosYatakSayisi = bolum.BosYatakSayisi;
                    mevcut.Aciklama = bolum.Aciklama;
                }
                db.SaveChanges();
                return Json(new { success = true, message = "İşlem Başarılı" });
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
                var kayit = db.Bolumler.Find(id);
                if (kayit != null)
                {
                    db.Bolumler.Remove(kayit);
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
            var data = db.Bolumler.Find(id);
            return Json(new
            {
                data.BolumID,
                data.BolumAdi,
                data.HastaSayisi,
                data.BosYatakSayisi,
                data.Aciklama
            }, JsonRequestBehavior.AllowGet);
        }
    }
}