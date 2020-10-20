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
    public class PartidaController : Controller
    {
        public enum ManageMessageId
        {
            Success,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private BolaoTIContext db = new BolaoTIContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private PartidaService partidaService = new PartidaService();

        [Authorize(Roles = "Admin")]
        public ActionResult Fechar(int? SelectedFase, string Message)
        {
            ViewBag.StatusMessage = Message;

            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedFase != null ? SelectedFase : fases.Where(p => p.Nome.Equals("FASE DE GRUPOS")).FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);

            int IdFase = selecionado.GetValueOrDefault();
            var partidas = unitOfWork.PartidaRepository.Get(filter: f => f.Rodada.RodadaGrupo.Fase.Id == IdFase, includeProperties: "TimeHome, TimeAway, EstadioJogo, Rodada.RodadaGrupo.Fase");

            return View(partidas.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FecharPartida(IEnumerable<BolaoTI.web.Models.Partida> partidas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (partidas != null)
                    {
                        partidaService.FecharPartida(partidas.ToList());
                        partidaService.Save();
                        return RedirectToAction("Fechar", new { SelectedFase = ViewBag.SelectedFase, Message = "Partidas Atualizadas!" });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return RedirectToAction("Fechar", new { SelectedFase = ViewBag.SelectedFase, Message = string.Empty });
        }


        //
        // GET: /Partida/
        public ActionResult Index(int? SelectedFase)
        {
            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedFase != null ? SelectedFase : fases.FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);

            int IdFase = selecionado.GetValueOrDefault();
            var partidas = unitOfWork.PartidaRepository.Get(filter: g => g.Rodada.RodadaGrupo.FaseID == IdFase, includeProperties: "TimeHome, TimeAway, EstadioJogo, Rodada.RodadaGrupo");

            return View(partidas.ToList());
        }

        //
        // GET: /Partida/Details/5

        public ActionResult Details(int id = 0)
        {
            Partida partida = unitOfWork.PartidaRepository.GetByID(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        //
        // GET: /Partida/Create

        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        //
        // POST: /Partida/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Partida partida)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.PartidaRepository.Insert(partida);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            PopulateDropDownList(partida.TimeHomeID, partida.TimeAwayID, partida.EstadioID, partida.RodadaID);
            return View(partida);
        }

        //
        // GET: /Partida/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Partida partida = unitOfWork.PartidaRepository.Get(filter: p => p.Id == id,
             includeProperties: "TimeHome,TimeAway,EstadioJogo,Rodada.RodadaGrupo").FirstOrDefault();
            if (partida == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(partida.TimeHome.Id, partida.TimeAway.Id, partida.EstadioJogo.Id, partida.Rodada.Id);
            return View(partida);
        }

        //
        // POST: /Partida/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Partida partida)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    partidaService.FecharPartida(partida);
                    partidaService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            PopulateDropDownList(partida.TimeHome.Id, partida.TimeAway.Id, partida.EstadioJogo.Id, partida.Rodada.Id);
            return View(partida);
        }

        //
        // GET: /Partida/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Partida partida = unitOfWork.PartidaRepository.GetByID(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        //
        // POST: /Partida/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.PartidaRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void PopulateDropDownList(object selectedTimeHome = null, object selectedTimeAway = null,
                                                     object selectedEstadio = null, object selectedRodada = null)
        {
            var TimeQuery = unitOfWork.TimeRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var EstadioQuery = unitOfWork.EstadioRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var RodadaQuery = unitOfWork.RodadaRepository.Get(orderBy: q => q.OrderBy(d => d.Nome), includeProperties: "RodadaGrupo");

            ViewBag.TimeHomeID = new SelectList(TimeQuery, "Id", "Nome", selectedTimeHome);
            ViewBag.TimeAwayID = new SelectList(TimeQuery, "Id", "Nome", selectedTimeAway);
            ViewBag.EstadioID = new SelectList(EstadioQuery, "Id", "Nome", selectedEstadio);
            ViewBag.RodadaID = new SelectList(RodadaQuery, "Id", "NomeGrupo", selectedRodada);
        }
    }
}