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
    public class BolumController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();

        // GET: Bolum
        public ActionResult Index()
        {
            return View(db.Bolumler.ToList());
        }

        // GET: Bolum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bolum/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BolumID,BolumAdi,HastaSayisi,BosYatakSayisi,Aciklama")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Bolumler.Add(bolum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bolum);
        }

        // GET: Bolum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolumler.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: Bolum/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BolumID,BolumAdi,HastaSayisi,BosYatakSayisi,Aciklama")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bolum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bolum);
        }

        // GET: Bolum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolumler.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: Bolum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bolum bolum = db.Bolumler.Find(id);
            db.Bolumler.Remove(bolum);
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
