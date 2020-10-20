using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BolaoTI.web.Models;
using BolaoTI.web.DAL;
using System.Transactions;
using WebMatrix.WebData;

namespace BolaoTI.web.Controllers
{
    [Authorize]
    public class ApostaController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private const string DATA_INDEFINIDA = "Data não Definida.";

        //
        // GET: /Aposta/
        [Authorize]
        public ActionResult Index(int? SelectedFase, string Message)
        {
            ViewBag.StatusMessage = Message;


            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedFase != null ? SelectedFase : fases.Where(p => p.Nome.Equals("FASE DE GRUPOS")).FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);

            int IdFase = selecionado.GetValueOrDefault();
            var partidas = unitOfWork.ApostaRepository.GetPartidasPorFase(IdFase, WebSecurity.CurrentUserId);

            ViewBag.NumeroPartidas = partidas != null ? partidas.Count() : 0;
            ViewBag.PalpitesRealizados = partidas != null ? partidas.Count(a => a.IdAposta.HasValue) : 0;
            ViewBag.EstaAberta = true;
            ViewBag.DataEncerramento = DATA_INDEFINIDA;
            var faseSelecionada = fases.Where(f => f.Id == IdFase);
            if (faseSelecionada != null)
            {
                ViewBag.EstaAberta = faseSelecionada.FirstOrDefault().EstaAberta;
                ViewBag.DataEncerramento = faseSelecionada.FirstOrDefault().DataEncerramento.HasValue ?
                                            faseSelecionada.FirstOrDefault().DataEncerramento.Value.ToString("dd/MM/yyyy HH:mm") : DATA_INDEFINIDA;
            }

            return View(partidas.ToList());
        }

        [Authorize]
        public ActionResult Partidas(int? SelectedFase)
        {
            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedFase != null ? SelectedFase : fases.Where(p => p.Nome.Equals("FASE DE GRUPOS")).FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);

            ViewBag.Apostas = unitOfWork.ApostaRepository.Get(filter: a => a.Usuario.UserId == WebSecurity.CurrentUserId,
                                                              includeProperties: "Usuario,PartidaApostada");

            int IdFase = selecionado.GetValueOrDefault();
            var grupos = unitOfWork.GrupoRepository.Get(filter: f => f.Fase.Id == IdFase,
                includeProperties: "Fase, Rodadas.Partidas.EstadioJogo, Rodadas.Partidas.TimeHome, Rodadas.Partidas.TimeAway, Rodadas.Partidas.Apostas,");
            return View(grupos.ToList());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarAposta(IEnumerable<BolaoTI.web.ViewModels.ApostaPartidaData> apostas)
        {
            if (apostas == null)
                ModelState.AddModelError("", "Erro ao salvar as apostas");
            try
            {
                unitOfWork.ApostaRepository.SalvarAposta(apostas);
                unitOfWork.Save();
                return RedirectToAction("Index", new { SelectedFase = ViewBag.SelectedFase, Message = "Apostas Atualizadas!" });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return RedirectToAction("Index", new { SelectedFase = ViewBag.SelectedFase, Message = string.Empty });
        }

        [Authorize]
        public ActionResult ExibirAposta(int? SelectedFase, int? IdUsuario, string NomeUsuario)
        {
            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = SelectedFase != null ? SelectedFase : fases.Where(p => p.Nome.Equals("FASE DE GRUPOS")).FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);
            ViewBag.IdUsuario = IdUsuario;
            ViewBag.NomeUsuario = NomeUsuario;

            int IdFase = selecionado.GetValueOrDefault();
            var apostas = unitOfWork.ApostaRepository.GetPartidasPorFase(IdFase, IdUsuario.Value);

            return View(apostas.ToList());
        }

        public ActionResult ObtemApostas() 
        {
            int? SelectedFase;
            var fases = unitOfWork.FaseRepository.Get(orderBy: q => q.OrderBy(d => d.Nome));
            var selecionado = fases.Where(p => p.Nome.Equals("FASE DE GRUPOS")).FirstOrDefault().Id;
            ViewBag.SelectedFase = new SelectList(fases, "Id", "Nome", selecionado);
                        
            var users = unitOfWork.UsuarioRepository.Get();            

            var apostaPartidaList = new List<ViewModels.ApostaPartidaData>();

            var apostas = unitOfWork.ApostaRepository.GetPartidasPorFase(selecionado, users.First().UserId);

            return View(apostas.ToList());  
        }

        public ActionResult generatePDF(int? SelectedFase, int? IdUsuario, string NomeUsuario) 
        {
            return new Rotativa.ActionAsPdf("ExibirAposta",
                                            new {
                                                SelectedFase = SelectedFase,
                                                IdUsuario = IdUsuario,
                                                NomeUsuario = NomeUsuario
                                                });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}