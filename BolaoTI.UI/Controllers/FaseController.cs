using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.UI.ViewsModel;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using BolaoTI.Dominio.Exceptions;
using BolaoTI.Resources.View;
using System;

namespace BolaoTI.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FaseController : Controller
    {
        private readonly IPartidaServicoAplicacao _partidaServicoAplicacao;
        private readonly ICampeonatoRepositorio _campeonatoRepositorio;
        private readonly IFaseRepositorio _faseRepositorio;
        private readonly IFaseServicoAplicacao _faseServicoAplicacao;
        private readonly IRankingServicoAplicacao _rankingServicoAplicacao;

        public FaseController(IPartidaServicoAplicacao partidaServicoAplicacao,
                            ICampeonatoRepositorio campeonatoRepositorio,
                            IFaseRepositorio faseRepositorio,
                            IFaseServicoAplicacao faseServicoAplicacao,
                            IRankingServicoAplicacao rankingServicoAplicacao)
        {
            _partidaServicoAplicacao = partidaServicoAplicacao;
            _campeonatoRepositorio = campeonatoRepositorio;
            _faseRepositorio = faseRepositorio;
            _faseServicoAplicacao = faseServicoAplicacao;
            _rankingServicoAplicacao = rankingServicoAplicacao;
        }

        private IList<SelectListItem> BindSelectListFases(List<Fase> fases, Fase faseSelecionada = null)
        {
            if (fases == null) return null;

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });

            foreach (var fase in fases)
            {
                SelectListItem item = new SelectListItem();
                item.Text = fase.Nome;
                item.Value = fase.Id.ToString();
                if (faseSelecionada != null)
                    item.Selected = (faseSelecionada.Id == fase.Id);

                list.Add(item);
            }
            return list;
        }

        private List<Fase> ObterFases(int idCampeonato)
        {
            return _faseRepositorio.FindByFilter(string.Empty, idCampeonato, null).ToList();
        }

        private void ValidarFase(Fase fase)
        {
            if (fase.Grupos == null || fase.Grupos.Count == 0)
                throw new BolaoTIException(Messages.Alert_Fase_SemPartidas);

            if (fase.Grupos.FirstOrDefault().Rodadas == null || (fase.Grupos.FirstOrDefault().Rodadas.Count == 0))
                throw new BolaoTIException(Messages.Alert_Fase_SemPartidas);

            if (fase.Grupos.FirstOrDefault().Rodadas.FirstOrDefault().Partidas == null ||
               (fase.Grupos.FirstOrDefault().Rodadas.FirstOrDefault().Partidas.Count == 0))
                throw new BolaoTIException(Messages.Alert_Fase_SemPartidas);
        }

        private List<Fase> ObterFases()
        {
            return _faseServicoAplicacao.RecuperarTodosOsFases().ToList();
        }

        [HttpGet]
        public ActionResult Index()
        {
            FaseViewModel faseViewModel = new FaseViewModel();
            faseViewModel.Campeonato = _campeonatoRepositorio.FindAll().FirstOrDefault();
            faseViewModel.Fases = BindSelectListFases(ObterFases(faseViewModel.Campeonato.Id));

            return View(faseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FaseViewModel faseViewModel, string FaseId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int idFase = 0;
                    int.TryParse(FaseId, out idFase);
                    ViewBag.Apostada = false;

                    var fase = _faseRepositorio.FindById(idFase);

                    faseViewModel.Campeonato = fase.Campeonato;
                    faseViewModel.Fases = BindSelectListFases(ObterFases(fase.CampeonatoId), fase);

                    ValidarFase(fase);

                    faseViewModel.Fase = fase;
                }
                catch (BolaoTIException bex)
                {
                    TempData["tipo"] = Resources.Configuration.Message_Tipo_Alerta;
                    TempData["Mensagem"] = bex.Message;
                }
                catch (Exception ex)
                {
                    TempData["tipo"] = Resources.Configuration.Message_Tipo_Erro;
                    TempData["Mensagem"] = ex.Message;
                }
            }

            return View(faseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fechar(FaseViewModel faseViewModel, Fase fase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    faseViewModel.Campeonato = fase.Campeonato;
                    faseViewModel.Fases = BindSelectListFases(ObterFases(fase.CampeonatoId), fase);
                    faseViewModel.Fase = fase;

                    _faseServicoAplicacao.Fechar(fase);
                    _rankingServicoAplicacao.AtualizaColocaoRanking(fase.Campeonato);

                    TempData["tipo"] = Resources.Configuration.Message_Tipo_Sucesso;
                    TempData["mensagem"] = Messages.Alert_Fase_Fechada_Sucesso;
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("Validação", Messages.Alert_ApostaRealiza_Invalida);
            }
            catch (BolaoTIException bex)
            {
                TempData["tipo"] = Resources.Configuration.Message_Tipo_Alerta;
                TempData["Mensagem"] = bex.Message;
            }
            catch (Exception ex)
            {
                TempData["tipo"] = Resources.Configuration.Message_Tipo_Erro;
                TempData["Mensagem"] = ex.Message;
            }

            return View("Index", faseViewModel);
        }


        public ActionResult Listar()
        {
            ViewBag.Fases = ObterFases();

            return View();
        }
    }
}