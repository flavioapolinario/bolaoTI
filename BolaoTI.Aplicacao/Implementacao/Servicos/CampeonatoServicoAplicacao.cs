using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class CampeonatoServicoAplicacao : ICampeonatoServicoAplicacao
    {
        private readonly ICampeonatoRepositorio _campeonatoRepositorio;
        private readonly ICampeonatoServicoCadastro _campeonatoServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public CampeonatoServicoAplicacao(ICampeonatoRepositorio CampeonatoRepositorio,
                                       ICampeonatoServicoCadastro CampeonatoServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _campeonatoRepositorio = CampeonatoRepositorio;
            _campeonatoServicoCadastro = CampeonatoServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public virtual void CadastrarCampeonato(Campeonato Campeonato)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _campeonatoServicoCadastro.CadastrarCampeonato(Campeonato);

                unidadeDeTrabalho.Completar();
            }
        }

        public Campeonato RecuperarPorId(int id)
        {
            return _campeonatoRepositorio.Get(id);
        }

        public virtual IList<Campeonato> RecuperarPorFiltro(string nome, System.DateTime? dataInicio, System.DateTime? dataFim)
        {
            return _campeonatoRepositorio.FindByFilter(nome, dataInicio, dataFim);
        }

        public virtual IList<Campeonato> RecuperarTodosOsCampeonatos()
        {
            return _campeonatoRepositorio.FindAll();
        }

        public virtual void Remover(Campeonato Campeonato)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _campeonatoRepositorio.Delete(Campeonato);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _campeonatoRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Campeonato Campeonato)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _campeonatoRepositorio.Update(Campeonato);

                unidadeDeTrabalho.Completar();
            }
        }

        public IList<Campeonato> RecuperarPorOrganizacao(int idOrganizacao)
        {
            return _campeonatoRepositorio.FindByOrganizacao(idOrganizacao);
        }
    }
}
