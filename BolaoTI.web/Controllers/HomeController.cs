using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BolaoTI.web.ViewModels;
using BolaoTI.web.DAL;
using BolaoTI.web.BLL;

namespace BolaoTI.web.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            ViewBag.Message = "O Bolão definitivo da Copa do Mundo FIFA 2014.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Pagina dedica aos administradores do site.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult ExibirTabela()
        {
            ViewBag.Message = "Tabela da Copa do Mundo 2014";
            ViewBag.Link = "http://globoesporte.globo.com/futebol/copa-do-mundo/classificacao.html";

            return View();
        }

        [Authorize]
        public ActionResult Ranking()
        {

            var viewModel = unitOfWork.RankingRepository.GetRanking();
            var fases = unitOfWork.FaseRepository.Get(f => f.DataEncerramento.HasValue);
            var faseAberta = fases.Where(f => !(BolaoTI.web.BLL.Utils.DataAtualFusoHorario().CompareTo(f.DataEncerramento.Value) > 0));
            ViewBag.VerApostas = (faseAberta == null || (faseAberta != null && faseAberta.Count() == 0));

            return View(viewModel.OrderBy(r => r.Usuario)
                                 .OrderByDescending(r => r.DoisTotalPontos)
                                 .OrderByDescending(r => r.CincoTotalPontos)
                                 .OrderByDescending(r => r.SeteTotalPontos)
                                 .OrderByDescending(r => r.DezTotalPontos)
                                 .OrderByDescending(r => r.TotalPontos));
        }

        public ActionResult Regra()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ExibirParticipantes(string message)
        {
            ViewBag.message = message;
            return View(unitOfWork.UsuarioRepository.Get());
        }

    }
}

