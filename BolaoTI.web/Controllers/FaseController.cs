using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BolaoTI.web.Models;
using BolaoTI.web.DAL;
using BolaoTI.web.BLL;

namespace BolaoTI.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FaseController : Controller
    {        
        private FaseService faseService = new FaseService();
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Fechar()
        {
            return View();
        }

        public ActionResult Fechar(int id = 0)
        {
            Fase fase = unitOfWork.FaseRepository.GetByID(id);
            if (fase == null)
            {
                return HttpNotFound();
            }
            return View(fase);
        }

        //
        // POST: /Fase/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fechar(Fase fase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    faseService.Fechar(fase.Id);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(fase);
        }


        //
        // GET: /Fase/

        public ActionResult Index()
        {
            return View(unitOfWork.FaseRepository.Get().ToList());
        }

        //
        // GET: /Fase/Details/5

        public ActionResult Details(int id = 0)
        {
            Fase fase = unitOfWork.FaseRepository.GetByID(id);
            if (fase == null)
            {
                return HttpNotFound();
            }
            return View(fase);
        }

        //
        // GET: /Fase/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Fase/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fase fase)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.FaseRepository.Insert(fase);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(fase);
        }

        //
        // GET: /Fase/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Fase fase = unitOfWork.FaseRepository.GetByID(id);
            if (fase == null)
            {
                return HttpNotFound();
            }
            return View(fase);
        }

        //
        // POST: /Fase/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fase fase)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.FaseRepository.Update(fase);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(fase);
        }

        //
        // GET: /Fase/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Fase fase = unitOfWork.FaseRepository.GetByID(id);
            if (fase == null)
            {
                return HttpNotFound();
            }
            return View(fase);
        }

        //
        // POST: /Fase/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.FaseRepository.Delete(id);
            unitOfWork.Save();            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}