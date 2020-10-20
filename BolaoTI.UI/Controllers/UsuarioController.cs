using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using BolaoTI.UI.ViewsModel;
using System;
using System.Net;
using BolaoTI.Dominio.Interfaces.Repositorios;
using Microsoft.AspNet.Identity;
using BolaoTI.Resources;

namespace BolaoTI.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicoAplicacao _usuarioServicoAplicacao;
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IOrganizacaoRepositorio _organizacaoRepositorio;

        public UsuarioController(IUsuarioServicoAplicacao usuarioServicoAplicacao,
                                 IPerfilRepositorio perfilRepositorio,
                                 IOrganizacaoRepositorio organizacaoRepositorio)
        {
            _usuarioServicoAplicacao = usuarioServicoAplicacao;
            _perfilRepositorio = perfilRepositorio;
            _organizacaoRepositorio = organizacaoRepositorio;
        }

        [HttpPost]
        public ActionResult Recuperar(string nome, string email, Guid[] guids)
        {
            var Usuarios = _usuarioServicoAplicacao.RecuperarPorFiltro(nome, email, guids);
            if (Usuarios == null)
            {
                return new EmptyResult();
            }
            return Json(Usuarios);
        }

        public ActionResult Listar()
        {
            ViewBag.Usuarios = ObterUsuarios();

            return View();
        }

        private List<Usuario> ObterUsuarios()
        {
            return _usuarioServicoAplicacao.RecuperarPorFiltro(string.Empty, string.Empty, new Guid[] { }).ToList();
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            var usuario = new RegisterViewModel();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Adicionar(RegisterViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = new PasswordHasher();

                Usuario usuario = new Usuario()
                {
                    Id = Guid.NewGuid(),
                    Nome = usuarioViewModel.Nome,
                    Email = usuarioViewModel.Email,
                    Telefone = usuarioViewModel.Telefone,
                    PasswordHash = passwordHash.HashPassword(usuarioViewModel.Password),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                try
                {
                    _usuarioServicoAplicacao.CadastrarUsuario(usuario);
                    return RedirectToAction("Listar");
                }
                catch (BolaoTIException exception)
                {
                    ModelState.AddModelError("Validação", exception.Message);
                    return View(usuarioViewModel);
                }
            }

            return View(usuarioViewModel);
        }

        public ActionResult Deletar(Guid id)
        {
            _usuarioServicoAplicacao.Remover(id);
            return PartialView("_ListaDeUsuarios", ObterUsuarios());
        }

        //
        // GET: /Users/Edit/1
        public ActionResult Editar(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = _usuarioServicoAplicacao.RecuperarPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(new UsuarioViewModel()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Telefone = usuario.Telefone,
                PerfilList = _perfilRepositorio.FindAll().Select(x => new SelectListItem()
                {
                    Selected = usuario.Perfis.Select(p => p.Nome).Contains(x.Nome),
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                OrganizacoesList = _organizacaoRepositorio.FindAll().Select(x => new SelectListItem()
                {
                    Selected = usuario.Organizacoes.Select(p => p.Nome).Contains(x.Nome),
                    Text = x.Nome,
                    Value = x.Id.ToString()
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Email,Telefone")] UsuarioViewModel editUser, string[] selectedPerfil, params string[] selectedOrganizacao)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioServicoAplicacao.RecuperarPorId(editUser.Id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }

                usuario.Nome = editUser.Nome;
                usuario.Email = editUser.Email;
                usuario.Telefone = editUser.Telefone;
                usuario.Perfis = _perfilRepositorio.FindAll().Where(p => selectedPerfil.Contains(p.Id.ToString())).ToList();
                usuario.Organizacoes = _organizacaoRepositorio.FindAll().Where(p => selectedOrganizacao.Contains(p.Id.ToString())).ToList();

                try
                {
                    _usuarioServicoAplicacao.Atualizar(usuario);
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                    return View();
                }
            }
            ModelState.AddModelError("", "Erro ao editar usuario.");
            return View();
        }

        public ActionResult Reset(Guid id)
        {
            try
            {
                Usuario usuario = _usuarioServicoAplicacao.RecuperarPorId(id);
                var passwordHash = new PasswordHasher();
                usuario.PasswordHash = passwordHash.HashPassword(BolaoTI.Resources.Configuration.Usuario_Padrao_Participante_Senha);
                _usuarioServicoAplicacao.Atualizar(usuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return PartialView("_ListaDeUsuarios", ObterUsuarios());
        }

        public ActionResult ConfirmaParticipante(Guid id)
        {
            try
            {
                Usuario usuario = _usuarioServicoAplicacao.RecuperarPorId(id);

                if (!usuario.Perfis.Any(p => p.Nome.Equals(Configuration.Role_Padrao_Participante)))
                {
                    var organizacaoPadrao = _organizacaoRepositorio.FindByFilter(BolaoTI.Resources.Configuration.Usuario_Padrao_Organizacao, null, null).FirstOrDefault();
                    var perfilParticipante = _perfilRepositorio.FindByName(Configuration.Role_Padrao_Participante);
                    if (perfilParticipante != null)
                    {
                        usuario.Organizacoes.Add(organizacaoPadrao);
                        usuario.Perfis.Add(perfilParticipante);
                        _usuarioServicoAplicacao.Atualizar(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return PartialView("_ListaDeUsuarios", ObterUsuarios());
        }

    }
}