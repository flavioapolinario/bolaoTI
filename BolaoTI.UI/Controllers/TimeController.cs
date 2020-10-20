using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using BolaoTI.UI.ViewsModel;

namespace BolaoTI.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TimeController : Controller
    {
        private readonly ITimeServicoAplicacao _timeServicoAplicacao;

        public TimeController(ITimeServicoAplicacao timeServicoAplicacao)
        {
            _timeServicoAplicacao = timeServicoAplicacao;
        }

        [HttpPost]
        public ActionResult Recuperar(string nome, string nomeAbreviado)
        {
            var Times = _timeServicoAplicacao.RecuperarPorFiltro(nome, nomeAbreviado);
            if (Times == null)
            {
                return new EmptyResult();
            }
            return Json(Times);
        }

        public ActionResult Listar()
        {
            ViewBag.Times = ObterTimes();

            return View();
        }

        private List<Time> ObterTimes()
        {
            return _timeServicoAplicacao.RecuperarTodosOsTimes().ToList();
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            var Time = new TimeViewModel();
            return View(Time);
        }

        [HttpPost]
        public ActionResult Adicionar(TimeViewModel timeViewModel)
        {
            if (ModelState.IsValid)
            {
                Time time = new Time()
                {
                    Nome = timeViewModel.Nome,
                    NomeAbreviado = timeViewModel.NomeAbreviado,
                    ImagemBandeira = timeViewModel.BandeiraCaminho,                     
                };

                try
                {
                    _timeServicoAplicacao.CadastrarTime(time);
                    return RedirectToAction("Listar");
                }
                catch (BolaoTIException exception)
                {
                    ModelState.AddModelError("Validação", exception.Message);
                    return View(timeViewModel);
                }
            }

            return View(timeViewModel);
        }

        public ActionResult Deletar(int id)
        {
            _timeServicoAplicacao.Remover(id);
            return PartialView("_ListaDeTimes", ObterTimes());
        }
    }
}