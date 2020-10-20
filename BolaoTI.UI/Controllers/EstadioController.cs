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
    public class EstadioController : Controller
    {
        private readonly IEstadioServicoAplicacao _estadioServicoAplicacao;

        public EstadioController(IEstadioServicoAplicacao estadioServicoAplicacao)
        {
            _estadioServicoAplicacao = estadioServicoAplicacao;
        }

        [HttpPost]
        public ActionResult Recuperar(string nome, string cidade, string uf)
        {
            var estadios = _estadioServicoAplicacao.RecuperarPorFiltro(nome, cidade, uf);
            if (estadios == null)
            {
                return new EmptyResult();
            }
            return Json(estadios);
        }

        public ActionResult Listar()
        {
            ViewBag.Estadios = ObterEstadios();

            return View();
        }

        private List<Estadio> ObterEstadios()
        {
            return _estadioServicoAplicacao.RecuperarTodosOsEstadios().ToList();
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            var estadio = new EstadioViewModel();
            return View(estadio);
        }

        [HttpPost]
        public ActionResult Adicionar(EstadioViewModel estadioViewModel)
        {
            if (ModelState.IsValid)
            {
                Estadio estadio = new Estadio()
                {
                    Nome = estadioViewModel.Nome,
                    Cidade = estadioViewModel.Cidade,
                    Uf = estadioViewModel.Uf,
                    Capacidade = estadioViewModel.Capacidade
                };

                try
                {
                    _estadioServicoAplicacao.CadastrarEstadio(estadio);
                    return RedirectToAction("Listar");
                }
                catch (BolaoTIException exception)
                {
                    ModelState.AddModelError("Validação", exception.Message);
                    return View(estadioViewModel);
                }
            }

            return View(estadioViewModel);
        }

        public ActionResult Deletar(int id)
        {
            _estadioServicoAplicacao.Remover(id);
            return PartialView("_ListaDeEstadios", ObterEstadios());
        }
    }
}