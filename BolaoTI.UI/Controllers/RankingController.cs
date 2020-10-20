using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using BolaoTI.Dominio.Interfaces.Repositorios;


namespace BolaoTI.UI.Controllers
{
    [Authorize]
    public class RankingController : Controller
    {
        private readonly IRankingServicoAplicacao _rankingServicoAplicacao;
        private readonly IUsuarioServicoAplicacao _usuarioServicoAplicacao;
        private readonly ICampeonatoRepositorio _campeonatoRepositorio;

        public RankingController(IRankingServicoAplicacao rankingServicoAplicacao,
                                IUsuarioServicoAplicacao usuarioServicoAplicacao,
                                ICampeonatoRepositorio campeonatoRepositorio)
        {
            _rankingServicoAplicacao = rankingServicoAplicacao;
            _usuarioServicoAplicacao = usuarioServicoAplicacao;
            _campeonatoRepositorio = campeonatoRepositorio;
        }

        public ActionResult Listar()
        {
            Usuario usuario = _usuarioServicoAplicacao.RecuperarPorId(new Guid(User.Identity.GetUserId()));
            Campeonato campeonato = _campeonatoRepositorio.FindAll().FirstOrDefault();
            Organizacao organizacao = usuario.Organizacoes.FirstOrDefault();

            ViewBag.CampeonatoNome = campeonato != null ? campeonato.Nome : null;
            ViewBag.isParticipante = usuario.EhParticipante;
            ViewBag.UserEmail = usuario.Email;
            if (usuario.EhParticipante)
                ViewBag.Rankings = ObterRankings(organizacao.Id, campeonato.Id);

            return View();
        }

        private List<Ranking> ObterRankings(int organizacaoId, int campeonatoId)
        {
            return _rankingServicoAplicacao.RecuperarPorFiltro(organizacaoId, campeonatoId, null, null).ToList();
        }

    }
}