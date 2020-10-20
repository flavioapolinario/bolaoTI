using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class UsuarioServicoAplicacao : IUsuarioServicoAplicacao
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUsuarioServicoCadastro _usuarioServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public UsuarioServicoAplicacao(IUsuarioRepositorio usuarioRepositorio,
                                       IUsuarioServicoCadastro usuarioServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioServicoCadastro = usuarioServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public virtual void CadastrarUsuario(Usuario Usuario)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioServicoCadastro.CadastrarUsuario(Usuario);

                unidadeDeTrabalho.Completar();
            }
        }

        public Usuario RecuperarPorId(Guid id)
        {
            return _usuarioRepositorio.FindById(id);
        }

        public Usuario RecuperarPorEmail(string email)
        {
            return _usuarioRepositorio.FindByEmail(email);
        }

        public IList<Usuario> RecuperarPorFiltro(string nome, string email, Guid[] perfilId)
        {
            return _usuarioRepositorio.FindByFilter(nome, email, perfilId);
        }

        public virtual IList<Usuario> RecuperarTodosOsUsuarios()
        {
            return _usuarioRepositorio.FindAll();
        }

        public virtual void Remover(Usuario Usuario)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioRepositorio.Delete(Usuario);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(Guid id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Usuario Usuario)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioRepositorio.Update(Usuario);

                unidadeDeTrabalho.Completar();
            }
        }

        #region Metodo Async

        public Task<int> CadastrarUsuarioAsync(Usuario usuario)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioRepositorio.Insert(usuario);

                return unidadeDeTrabalho.CompletarAsync();
            }
        }

        public Task<int> AtualizarAsync(Usuario usuario)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioRepositorio.Update(usuario);

                return unidadeDeTrabalho.CompletarAsync();
            }
        }

        public Task<int> RemoverAsync(Usuario usuario)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _usuarioRepositorio.Delete(usuario);

                return unidadeDeTrabalho.CompletarAsync();
            }
        }


        #endregion
    }
}