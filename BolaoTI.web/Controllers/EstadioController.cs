using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BolaoTI.web.Models;
using BolaoTI.web.DAL;

namespace BolaoTI.web.Controllers
{
    [Authorize(Roles="Admin")]
    public class EstadioController : Controller
    {
        private BolaoTIContext db = new BolaoTIContext();

        //
        // GET: /Estadio/

        public ActionResult Index()
        {
            return View(db.Estadios.ToList());
        }

        //
        // GET: /Estadio/Details/5

        public ActionResult Details(int id = 0)
        {
            Estadio estadio = db.Estadios.Find(id);
            if (estadio == null)
            {
                return HttpNotFound();
            }
            return View(estadio);
        }

        //
        // GET: /Estadio/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Estadio/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estadio estadio)
        {
            if (ModelState.IsValid)
            {
                db.Estadios.Add(estadio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadio);
        }

        //
        // GET: /Estadio/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Estadio estadio = db.Estadios.Find(id);
            if (estadio == null)
            {
                return HttpNotFound();
            }
            return View(estadio);
        }

        //
        // POST: /Estadio/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estadio estadio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadio);
        }

        //
        // GET: /Estadio/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Estadio estadio = db.Estadios.Find(id);
            if (estadio == null)
            {
                return HttpNotFound();
            }
            return View(estadio);
        }

        //
        // POST: /Estadio/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estadio estadio = db.Estadios.Find(id);
            db.Estadios.Remove(estadio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}