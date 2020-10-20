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
    [Authorize(Roles = "Admin")]
    public class GrupoController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Grupo/

        public ActionResult Index(int? SelectedFase)
        {
            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedFase != null ? SelectedFase : fases.FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);

            int IdFase = selecionado.GetValueOrDefault();
            var grupos = unitOfWork.GrupoRepository.Get(filter: g => g.Fase.Id == IdFase, includeProperties: "Fase,Rodadas");

            return View(grupos.ToList());
        }

        //
        // GET: /Grupo/Details/5

        public ActionResult Details(int id = 0)
        {
            Grupo grupo = unitOfWork.GrupoRepository.GetByID(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        //
        // GET: /Grupo/Create

        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        //
        // POST: /Grupo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GrupoRepository.Insert(grupo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        //
        // GET: /Grupo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Grupo grupo = unitOfWork.GrupoRepository.GetByID(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(grupo.Fase.Id);
            return View(grupo);
        }

        //
        // POST: /Grupo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GrupoRepository.Update(grupo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            PopulateDropDownList(grupo.Fase.Id);
            return View(grupo);
        }

        //
        // GET: /Grupo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Grupo grupo = unitOfWork.GrupoRepository.GetByID(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        //
        // POST: /Grupo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.GrupoRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void PopulateDropDownList(object selectedFase = null)
        {
            var FaseQuery = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));

            ViewBag.FaseID = new SelectList(FaseQuery, "Id", "Nome", selectedFase);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}