using PediatriAsistanNöbet1.Models.DataContext;
using PediatriAsistanNöbet1.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
        public ActionResult Edit(int id, Asistan asistan)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asistan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asistan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
