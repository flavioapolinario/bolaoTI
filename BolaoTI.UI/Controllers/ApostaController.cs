using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources.View;
using BolaoTI.UI.ViewsModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BolaoTI.UI.Controllers
{
    [Authorize]
    public class ApostaController : Controller
    {
        private readonly IApostaServicoAplicacao _apostaServicoAplicacao;
        private readonly IApostaRepositorio _apostaRepositorio;
        private readonly ICampeonatoRepositorio _campeonatoRepositorio;
        private readonly IFaseRepositorio _faseRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ApostaController(IApostaServicoAplicacao apostaServicoAplicacao,
                                IApostaRepositorio apostaRepositorio,
                                ICampeonatoRepositorio campeonatoRepositorio,
                                IFaseRepositorio faseRepositorio,
                                IUsuarioRepositorio usuarioRepositorio)
        {
            _apostaServicoAplicacao = apostaServicoAplicacao;
            _apostaRepositorio = apostaRepositorio;
            _campeonatoRepositorio = campeonatoRepositorio;
            _faseRepositorio = faseRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
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

        private void VerificarRealizouAposta(Fase fase)
        {
            if (fase == null) return;

            var apostas = _apostaRepositorio.FindApostasByFase(new Guid(User.Identity.GetUserId()), fase.Id);

            fase.Grupos.ForEach(g =>
            {
                g.Rodadas.ForEach(r =>
                {
                    r.Partidas.ForEach(p =>
                    {
                        p.GolsTimeAway = null;
                        p.GolsTimeHome = null;
                        var aposta = apostas.Find(a => a.PartidaId == p.Id);
                        if (aposta != null)
                        {
                            ViewBag.Apostada = true;
                            p.GolsTimeAway = aposta.GolsTimeAway;
                            p.GolsTimeHome = aposta.GolsTimeHome;
                        }
                    });
                });
            });
        }

        private List<Fase> ObterFases(int idCampeonato)
        {
            return _faseRepositorio.FindByFilter(string.Empty, idCampeonato, null).ToList();
        }

        private Usuario ObterUsuario()
        {
            return _usuarioRepositorio.FindById(new Guid(User.Identity.GetUserId()));
        }

        private Aposta ConverterPartidaApostadaToAposta(Partida partida)
        {
            if (partida == null) throw new ArgumentException(BolaoTI.Resources.Classes.Partida_Class);

            Aposta aposta = new Aposta();

            aposta.PartidaId = partida.Id;
            aposta.GolsTimeAway = partida.GolsTimeAway.Value;
            aposta.GolsTimeHome = partida.GolsTimeHome.Value;
            aposta.UsuarioId = new Guid(User.Identity.GetUserId());

            return aposta;
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

            if (!fase.EstaAberta)
                throw new BolaoTIException(String.Format(Messages.Alert_Fase_PeriodoInvalido,
                                            fase.DataInicio.ToString(BolaoTI.Resources.RegularExpression.FormatString_DateTime),
                                            fase.DataFim.ToString(BolaoTI.Resources.RegularExpression.FormatString_DateTime)));

            VerificarRealizouAposta(fase);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var usuario = ObterUsuario();
            ViewBag.UserEmail = usuario != null ? usuario.Email : null;
            ViewBag.isParticipante = usuario != null ? usuario.EhParticipante : false;

            FaseViewModel model = new FaseViewModel();
            model.Campeonato = _campeonatoRepositorio.FindAll().FirstOrDefault();
            model.Fases = BindSelectListFases(ObterFases(model.Campeonato.Id));

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FaseViewModel faseViewModel, string FaseId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = ObterUsuario();
                    ViewBag.UserEmail = usuario != null ? usuario.Email : null;
                    ViewBag.isParticipante = usuario != null ? usuario.EhParticipante : false;
     
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
        public ActionResult Apostar(FaseViewModel faseViewModel, Fase fase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    faseViewModel.Campeonato = fase.Campeonato;
                    faseViewModel.Fases = BindSelectListFases(ObterFases(fase.CampeonatoId), fase);
                    faseViewModel.Fase = fase;

                    List<Aposta> apostas = new List<Aposta>();
                    fase.Grupos.ForEach(g =>
                    {
                        g.Rodadas.ForEach(r =>
                        {
                            r.Partidas.ForEach(p =>
                            {
                                apostas.Add(ConverterPartidaApostadaToAposta(p));
                            });
                        });
                    });

                    _apostaServicoAplicacao.RealizarAposta(apostas);

                    TempData["tipo"] = Resources.Configuration.Message_Tipo_Sucesso;
                    TempData["mensagem"] = Messages.Alert_ApostaRealiza_Sucesso;
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

            return View("Index", fase);
        }

    }
}