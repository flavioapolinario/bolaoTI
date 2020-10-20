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
    public class RodadaController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Rodada/

        public ActionResult Index(int? SelectedGrupo)
        {
            var grupos = unitOfWork.GrupoRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedGrupo != null ? SelectedGrupo : grupos.FirstOrDefault().Id;
            ViewBag.SelectedGrupo = new SelectList(grupos, "Id", "Nome", selecionado);

            int IdGrupo = selecionado.GetValueOrDefault();
            var rodadas = unitOfWork.RodadaRepository.Get(filter: r => r.RodadaGrupo.Id == IdGrupo, includeProperties: "RodadaGrupo, Partidas.TimeHome, Partidas.TimeAway");

            return View(rodadas.ToList());
        }

        //
        // GET: /Rodada/Details/5

        public ActionResult Details(int id = 0)
        {
            Rodada rodada = unitOfWork.RodadaRepository.GetByID(id);
            if (rodada == null)
            {
                return HttpNotFound();
            }
            return View(rodada);
        }

        //
        // GET: /Rodada/Create

        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        //
        // POST: /Rodada/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rodada rodada)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RodadaRepository.Insert(rodada);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(rodada);
        }

        //
        // GET: /Rodada/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Rodada rodada = unitOfWork.RodadaRepository.GetByID(id);
            if (rodada == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(rodada.RodadaGrupo.Id);
            return View(rodada);
        }

        //
        // POST: /Rodada/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rodada rodada)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RodadaRepository.Update(rodada);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            PopulateDropDownList(rodada.RodadaGrupo.Id);
            return View(rodada);
        }

        //
        // GET: /Rodada/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Rodada rodada = unitOfWork.RodadaRepository.GetByID(id);
            if (rodada == null)
            {
                return HttpNotFound();
            }
            return View(rodada);
        }

        //
        // POST: /Rodada/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.RodadaRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void PopulateDropDownList(object selectedGrupo = null)
        {
            var GrupoQuery = unitOfWork.GrupoRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));

            ViewBag.GrupoID = new SelectList(GrupoQuery, "Id", "Nome", selectedGrupo);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}