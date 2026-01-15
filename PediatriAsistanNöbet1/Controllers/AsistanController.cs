using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PediatriAsistanNöbet1.Controllers
{
    public class AsistanController : Controller
    {
        PediatriDBContext db = new PediatriDBContext(); 
        // GET: Asistan
        public ActionResult Index()
        {
            return View(db.Asistanlar.ToList());
        }

        // GET: Asistan/Details/5


        // GET: Asistan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asistan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind (Include = "AsistanID, Ad, Soyad, Teleon, Email, Adres ")] Asistan asistan)
        {
            if (ModelState.IsValid)
            {
                db.Asistanlar.Add(asistan);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            {
                return View(asistan);
            }
        }

        // GET: Asistan/Edit/5
        public ActionResult Edit(int id)
        {
            var asistan=db.Asistanlar.Where(x => x.AsistanID == id ).SingleOrDefault();
            return View(asistan);
        }

        // POST: Asistan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Asistan asistan)
        {
            
            if (ModelState.IsValid)
            {
                var a =db.Asistanlar.Where(x => x.AsistanID==id).SingleOrDefault();

                a.Adres = asistan.Adres;   
                a.Ad =  asistan.Ad;
                a.Email = asistan.Email;
                a.Soyad = asistan.Soyad;
                a.Telefon   = asistan.Telefon;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(asistan);
        }

        // GET: Asistan/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistan asistan = db.Asistanlar.Find(id);
            if (asistan == null)
            {
                return HttpNotFound();
            }
            return View(asistan);
        }

        // POST: Asistan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asistan asistan = db.Asistanlar.Find(id);
            db.Asistanlar.Remove(asistan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
 
            protected override void Dispose (bool disposing)
        {
            if (!disposing) {
                db.Dispose();
        }
            base.Dispose(disposing);
        }
    }
}
