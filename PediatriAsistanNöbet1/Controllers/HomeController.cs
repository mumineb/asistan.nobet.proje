using PediatriAsistanNöbet1.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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




    }
}