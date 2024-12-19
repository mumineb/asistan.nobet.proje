using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PediatriAsistanNöbet1.Controllers
{
    public class OgretimUyesiController : Controller
    {
        PediatriDBContext db = new PediatriDBContext();
        // GET: OgretimUyesi
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.OgretimUyeleri.Include("Bolum").ToList());
           
        }

        // GET: OgretimUyesi/Details/5
 

        // GET: OgretimUyesi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OgretimUyesi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgretimUyesiID, Ad, Soyad, Teleon, Email, Adres ")] OgretimUyesi ogretimuyesi)
        {
            if (ModelState.IsValid)
            {
                db.OgretimUyeleri.Add(ogretimuyesi);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            {
                return View(ogretimuyesi);
            }
            
        }

        // GET: OgretimUyesi/Edit/5
        public ActionResult Edit(int id)
        {
            var ogretimuyesi=db.OgretimUyeleri.Where(x => x.OgretimUyesiID == id).SingleOrDefault();
            return View(ogretimuyesi);
        }

        // POST: OgretimUyesi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OgretimUyesi ogretimuyesi)
        {
   
            if (ModelState.IsValid)
            {
                
                var o =db.OgretimUyeleri.Where(x => x.OgretimUyesiID == id).SingleOrDefault();
                
                o.Adres = ogretimuyesi.Adres;
                o.Ad= ogretimuyesi.Ad;
                o.Email= ogretimuyesi.Email;
                o.Soyad = ogretimuyesi.Soyad;
                o.Bolum = ogretimuyesi.Bolum;
                o.Telefon = ogretimuyesi.Telefon;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
                return View(ogretimuyesi);
            
        }

        // GET: OgretimUyesi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgretimUyesi ogretimuyesi = db.OgretimUyeleri.Find(id);
            if (ogretimuyesi == null)
            {
                return HttpNotFound();
            }
            return View(ogretimuyesi);
        }

        // POST: OgretimUyesi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OgretimUyesi ogretimuyesi = db.OgretimUyeleri.Find(id);
            db.OgretimUyeleri.Remove(ogretimuyesi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
