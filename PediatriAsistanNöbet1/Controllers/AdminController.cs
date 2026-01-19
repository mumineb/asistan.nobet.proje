using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PediatriAsistanNöbet1.Controllers
{
    public class AdminController : BaseController
    {

        public ActionResult Index()
        {
            // 1. KARTLAR İÇİN İSTATİSTİKLER
            ViewBag.AsistanSayisi = db.Asistanlar.Count();
            ViewBag.HocaSayisi = db.OgretimUyeleri.Count();

            // Bugünün tarihi (Saat bilgisini sıfırlayarak)
            var bugun = DateTime.Today;
            ViewBag.BugunkuNobetciSayisi = db.Nobetler.Count(x => x.NobetTarihi == bugun);

            // Gelecek tarihli randevular
            ViewBag.AktifRandevuSayisi = db.Randevular.Count(x => x.RandevuTarihi >= DateTime.Now);

            // 2. SON EKLENEN 5 NÖBET (Hızlı Bakış Tablosu İçin)
            var sonNobetler = db.Nobetler
                                .Include("Asistan")
                                .Include("Bolum")
                                .OrderByDescending(x => x.NobetTarihi)
                                .Take(5)
                                .ToList();

            return View(sonNobetler);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            // Kullanıcının girdiği şifreyi şifrele
            string sifreliGiris = PediatriAsistanNöbet1.Helpers.CryptoHelper.ComputeSha256Hash(admin.Sifre);

            // Veritabanında eşleşen e-posta var mı?
            var login = db.Adminler.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();

            // Kullanıcı bulunduysa VE veritabanındaki şifreli haliyle, girilenin şifreli hali tutuyorsa
            if (login != null && login.Sifre == sifreliGiris)
            {
                Session["kullaniciID"] = login.KullaniciID;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Uyari = "E-posta ya da Şifre yanlış!";
            return View(admin);
        }

        public ActionResult Logout()
        {
            Session["kullaniciID"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}



