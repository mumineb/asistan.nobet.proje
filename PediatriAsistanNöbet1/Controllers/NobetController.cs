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
    public class NobetController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();

        // GET: Nobet
        public ActionResult Index()
        {
            var nobetler = db.Nobetler.Include(n => n.Asistan).Include(n => n.Bolum);
            return View(nobetler.ToList());
        }

        // GET: Nobet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nobet nobet = db.Nobetler.Find(id);
            if (nobet == null)
            {
                return HttpNotFound();
            }
            return View(nobet);
        }

        // GET: Nobet/Create
        public ActionResult Create()
        {
            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad");
            ViewBag.BolumID = new SelectList(db.Bolumler, "BolumID", "BolumAdi");
            return View();
        }

        // POST: Nobet/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NobetID,AsistanID,BolumID,NobetTarihi")] Nobet nobet)
        {
            if (ModelState.IsValid)
            {
                db.Nobetler.Add(nobet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", nobet.AsistanID);
            ViewBag.BolumID = new SelectList(db.Bolumler, "BolumID", "BolumAdi", nobet.BolumID);
            return View(nobet);
        }

        // GET: Nobet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nobet nobet = db.Nobetler.Find(id);
            if (nobet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", nobet.AsistanID);
            ViewBag.BolumID = new SelectList(db.Bolumler, "BolumID", "BolumAdi", nobet.BolumID);
            return View(nobet);
        }

        // POST: Nobet/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NobetID,AsistanID,BolumID,NobetTarihi")] Nobet nobet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nobet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", nobet.AsistanID);
            ViewBag.BolumID = new SelectList(db.Bolumler, "BolumID", "BolumAdi", nobet.BolumID);
            return View(nobet);
        }

        // GET: Nobet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nobet nobet = db.Nobetler.Find(id);
            if (nobet == null)
            {
                return HttpNotFound();
            }
            return View(nobet);
        }

        // POST: Nobet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nobet nobet = db.Nobetler.Find(id);
            db.Nobetler.Remove(nobet);
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
