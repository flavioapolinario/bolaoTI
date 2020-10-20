
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class PerfilServicoAplicacao : IPerfilServicoAplicacao
    {
        private readonly IPerfilRepositorio _perfilRepositorio;        
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public PerfilServicoAplicacao(IPerfilRepositorio PerfilRepositorio,                                      
                                      IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _perfilRepositorio = PerfilRepositorio;            
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }


        public void CadastrarPerfil(Perfil perfil)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Insert(perfil);

                unidadeDeTrabalho.Completar();
            }
        }

        public Perfil RecuperarPorId(Guid id)
        {
            return _perfilRepositorio.Get(id);
        }

        public Perfil RecuperarPorNome(string nome)
        {
            return _perfilRepositorio.FindByName(nome);
        }        

        public void Remover(Perfil perfil)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Delete(perfil);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(Guid id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Perfil perfil)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Update(perfil);

                unidadeDeTrabalho.Completar();
            }
        }

        #region Metodos Async
        
        public Task<int> CadastrarPerfilAsync(Perfil perfil)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Insert(perfil);

                return unidadeDeTrabalho.CompletarAsync();
            }
        }

        public Task<int> AtualizarAsync(Perfil perfil)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Update(perfil);

                return unidadeDeTrabalho.CompletarAsync();
            }
        }

        public Task<int> RemoverAsync(Perfil perfil)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _perfilRepositorio.Delete(perfil);

                return unidadeDeTrabalho.CompletarAsync();
            }
        }

        #endregion
    }
}
