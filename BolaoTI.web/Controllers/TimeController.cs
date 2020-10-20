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
    public class TimeController : Controller
    {
        private BolaoTIContext db = new BolaoTIContext();

        //
        // GET: /Time/

        public ActionResult Index()
        {
            return View(db.Times.ToList());
        }

        //
        // GET: /Time/Details/5

        public ActionResult Details(int id = 0)
        {
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        //
        // GET: /Time/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Time/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Time time)
        {
            if (ModelState.IsValid)
            {
                db.Times.Add(time);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(time);
        }

        //
        // GET: /Time/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        //
        // POST: /Time/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Time time)
        {
            if (ModelState.IsValid)
            {
                db.Entry(time).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(time);
        }

        //
        // GET: /Time/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        //
        // POST: /Time/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Time time = db.Times.Find(id);
            db.Times.Remove(time);
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