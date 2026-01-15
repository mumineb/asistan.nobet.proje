using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;
using PediatriAsistanNöbet1.ViewModels;

namespace PediatriAsistanNöbet1.Controllers
{
    public class NobetsController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();

        // GET: Nobets
        public ActionResult Index()
        {


            var nobetler = db.Nobetler.Include(n => n.Asistan).Include(n => n.Bolum)
    .Select(n => new NobetViewModel
    {
        NobetID = n.NobetID,
        AsistanAdiSoyadi = n.Asistan.Ad + " " + n.Asistan.Soyad, // Ad + Soyad birleşimi
        BolumAdi = n.Bolum.BolumAdi,
        NobetTarihi = n.NobetTarihi
    }).ToList();

            return View(nobetler);
        }


        // GET: Nobets/Create
        public ActionResult Create()
        {
            ViewBag.AsistanID = new SelectList(db.Asistanlar.Select(a => new
            {
                a.AsistanID,
                FullName = a.Ad + " " + a.Soyad // Ad + Soyad birleşimi
            }), "AsistanID", "FullName");
            ViewBag.BolumID = new SelectList(db.Bolumler, "BolumID", "BolumAdi");
            return View();
        }

        // POST: Nobets/Create
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

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            ViewBag.AsistanID = new SelectList(db.Asistanlar, "AsistanID", "Ad", nobet.AsistanID);
            ViewBag.BolumID = new SelectList(db.Bolumler, "BolumID", "BolumAdi", nobet.BolumID);
            return View(nobet);
        }

        // GET: Nobets/Edit/5
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

        // POST: Nobets/Edit/5
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

        // GET: Nobets/Delete/5
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

        // POST: Nobets/Delete/5
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
