using System.Web.Mvc;
using PediatriAsistanNöbet1.Models.DataContext;

namespace PediatriAsistanNöbet1.Controllers
{
    // Tüm Controller'lar bu sınıftan türeyecek.
    public class BaseController : Controller
    {
        protected PediatriDBContext db;

        public BaseController()
        {
            db = new PediatriDBContext();
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