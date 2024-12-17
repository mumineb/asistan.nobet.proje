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
    public class AcilDurumController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();

        // GET: AcilDurum
        public ActionResult Index()
        {
            return View(db.AcilDurumlar.ToList());
        }

        // GET: AcilDurum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcilDurum acilDurum = db.AcilDurumlar.Find(id);
            if (acilDurum == null)
            {
                return HttpNotFound();
            }
            return View(acilDurum);
        }

        // GET: AcilDurum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AcilDurum/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AcilDurumID,Baslik,Icerik,Tarih")] AcilDurum acilDurum)
        {
            if (ModelState.IsValid)
            {
                db.AcilDurumlar.Add(acilDurum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acilDurum);
        }

        // GET: AcilDurum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcilDurum acilDurum = db.AcilDurumlar.Find(id);
            if (acilDurum == null)
            {
                return HttpNotFound();
            }
            return View(acilDurum);
        }

        // POST: AcilDurum/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AcilDurumID,Baslik,Icerik,Tarih")] AcilDurum acilDurum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acilDurum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acilDurum);
        }

        // GET: AcilDurum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcilDurum acilDurum = db.AcilDurumlar.Find(id);
            if (acilDurum == null)
            {
                return HttpNotFound();
            }
            return View(acilDurum);
        }

        // POST: AcilDurum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcilDurum acilDurum = db.AcilDurumlar.Find(id);
            db.AcilDurumlar.Remove(acilDurum);
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
