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
    public class HomeController : Controller
    {
        private PediatriDBContext db = new PediatriDBContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Asistan()
        {
            return View(db.Asistanlar.ToList());
        }

        public ActionResult OgretimUyesi()

        {
            return View(db.OgretimUyeleri.Include("Bolum").ToList());

        }

        public ActionResult Bolum()

        {
            return View(db.Bolumler.ToList());

        }

        public ActionResult AcilDurum()
        {
            return View(db.AcilDurumlar.ToList());
        }


        public ActionResult Nobet()
        {
            return View();
        }
        public JsonResult GetNobetler()
        {
            var nobetler = db.Nobetler
                             .Include(n => n.Asistan)    // Asistan tablosunu dahil et
                             .Include(n => n.Bolum)      // Bolum tablosunu dahil et
                             .ToList()                   // Verileri çekiyoruz
                             .Select(n => new
                             {
                                 title = $"{n.Asistan.Ad} {n.Asistan.Soyad} - {n.Bolum.BolumAdi}", // string concatenation
                                 start = n.NobetTarihi.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO 8601 formatında
                                 end = n.NobetTarihi.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss") // End date
                             })
                             .ToList();

            return Json(nobetler, JsonRequestBehavior.AllowGet);
        }



    }
}