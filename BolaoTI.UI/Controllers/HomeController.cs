using BolaoTI.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BolaoTI.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFaseRepositorio _faseRepositorio;

        public HomeController(IFaseRepositorio faseRepositorio)
        {            
            _faseRepositorio = faseRepositorio;         
        }

        public ActionResult Index()
        {
            ViewBag.Fase = _faseRepositorio.FindById(1);
            return View();
        }

        public ActionResult CalendarioJogo()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Regras()
        {
            return View();
        }
    }
}