using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;

namespace PediatriAsistanNöbet1.Controllers
{
    public class RandevuController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();

        // GET: Randevu
        public ActionResult Index()
        {
            var randevular = db.Randevular.Include(r => r.Asistan).Include(r => r.Musaitlik).Include(r => r.OgretimUyesi).Include(r => r.RandevuDurum);
            return View(randevular.ToList());
        }


        // GET: Randevu/Create
        public ActionResult Create()
        {
            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad");
            ViewBag.MusaitlikID = new SelectList(db.Musaitlikler, "MusaitlikID", "MusaitlikID");
            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri, "OgretimUyesiID", "Ad");
            ViewBag.RandevuDurumID = new SelectList(db.RandevuDurumlari, "RandevuDurumID", "DurumAdi");
            return View();
        }

        // POST: Randevu/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RandevuID,AsistanID,OgretimUyesiID,RandevuDurumID,RandevuTarihi,Aciklama,MusaitlikID")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                // Varsayılan olarak aktif durumu ekleniyor
                randevu.RandevuDurumID = db.RandevuDurumlari.FirstOrDefault(d => d.DurumAdi == "Aktif").RandevuDurumID;

                db.Randevular.Add(randevu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", randevu.AsistanID);
            ViewBag.MusaitlikID = new SelectList(db.Musaitlikler
                .Include(m => m.OgretimUyesi)
                .Select(m => new {
                    m.MusaitlikID,
                    DisplayText = m.Tarih.ToString("dd/MM/yyyy") + " - " + m.Saat + " - " + m.OgretimUyesi.Ad + " " + m.OgretimUyesi.Soyad
                }),
                "MusaitlikID", "DisplayText", randevu.MusaitlikID);

            return View(randevu);
        }
    

        // GET: Randevu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Randevu randevu = db.Randevular.Find(id);
            if (randevu == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", randevu.AsistanID);
            ViewBag.MusaitlikID = new SelectList(db.Musaitlikler, "MusaitlikID", "MusaitlikID", randevu.MusaitlikID);
            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri, "OgretimUyesiID", "Ad", randevu.OgretimUyesiID);
            ViewBag.RandevuDurumID = new SelectList(db.RandevuDurumlari, "RandevuDurumID", "DurumAdi", randevu.RandevuDurumID);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RandevuID,AsistanID,OgretimUyesiID,RandevuDurumID,RandevuTarihi,Aciklama,MusaitlikID")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(randevu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", randevu.AsistanID);
            ViewBag.MusaitlikID = new SelectList(db.Musaitlikler, "MusaitlikID", "MusaitlikID", randevu.MusaitlikID);
            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri, "OgretimUyesiID", "Ad", randevu.OgretimUyesiID);
            ViewBag.RandevuDurumID = new SelectList(db.RandevuDurumlari, "RandevuDurumID", "DurumAdi", randevu.RandevuDurumID);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Randevu randevu = db.Randevular.Find(id);
            if (randevu == null)
            {
                return HttpNotFound();
            }
            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Randevu randevu = db.Randevular.Find(id);
            db.Randevular.Remove(randevu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
