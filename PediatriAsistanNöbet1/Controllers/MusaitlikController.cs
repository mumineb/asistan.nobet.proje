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
    public class MusaitlikController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();

        // GET: Musaitlik
        public ActionResult Index()
        {
            var musaitlikler = db.Musaitlikler.Include(m => m.OgretimUyesi).ToList();
            return View(musaitlikler);
        }

        public ActionResult Create()
        {
            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri.Select(u => new {
                u.OgretimUyesiID,
                FullName = u.Ad + " " + u.Soyad
            }), "OgretimUyesiID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MusaitlikID,Tarih,Saat,OgretimUyesiID")] Musaitlik musaitlik)
        {
            if (ModelState.IsValid)
            {
                db.Musaitlikler.Add(musaitlik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri.Select(u => new {
                u.OgretimUyesiID,
                FullName = u.Ad + " " + u.Soyad
            }), "OgretimUyesiID", "FullName", musaitlik.OgretimUyesiID);
            return View(musaitlik);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musaitlik musaitlik = db.Musaitlikler.Find(id);
            if (musaitlik == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri.Select(u => new {
                u.OgretimUyesiID,
                FullName = u.Ad + " " + u.Soyad
            }), "OgretimUyesiID", "FullName", musaitlik.OgretimUyesiID);
            return View(musaitlik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MusaitlikID,Tarih,Saat,OgretimUyesiID")] Musaitlik musaitlik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musaitlik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgretimUyesiID = new SelectList(db.OgretimUyeleri.Select(u => new {
                u.OgretimUyesiID,
                FullName = u.Ad + " " + u.Soyad
            }), "OgretimUyesiID", "FullName", musaitlik.OgretimUyesiID);
            return View(musaitlik);
        }


        // GET: Musaitlik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musaitlik musaitlik = db.Musaitlikler.Find(id);
            if (musaitlik == null)
            {
                return HttpNotFound();
            }
            return View(musaitlik);
        }

        // POST: Musaitlik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musaitlik musaitlik = db.Musaitlikler.Find(id);
            db.Musaitlikler.Remove(musaitlik);
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
