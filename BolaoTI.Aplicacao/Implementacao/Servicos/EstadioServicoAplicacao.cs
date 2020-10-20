using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class EstadioServicoAplicacao : IEstadioServicoAplicacao
    {
        private readonly IEstadioRepositorio _estadioRepositorio;
        private readonly IEstadioServicoCadastro _estadioServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public EstadioServicoAplicacao(IEstadioRepositorio estadioRepositorio,
                                       IEstadioServicoCadastro estadioServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _estadioRepositorio = estadioRepositorio;
            _estadioServicoCadastro = estadioServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }



        public virtual void CadastrarEstadio(Estadio Estadio)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _estadioServicoCadastro.CadastrarEstadio(Estadio);

                unidadeDeTrabalho.Completar();
            }
        }

        public Estadio RecuperarPorId(int id)
        {
            return _estadioRepositorio.Get(id);
        }

        public virtual IList<Estadio> RecuperarPorFiltro(string nome, string cidade, string uf)
        {
            return _estadioRepositorio.FindByFilter(nome, cidade, uf);
        }

        public virtual IList<Estadio> RecuperarTodosOsEstadios()
        {
            return _estadioRepositorio.FindAll();
        }

        public virtual void Remover(Estadio estadio)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _estadioRepositorio.Delete(estadio);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _estadioRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Estadio estadio)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _estadioRepositorio.Update(estadio);

                unidadeDeTrabalho.Completar();
            }
        }

    }
}
