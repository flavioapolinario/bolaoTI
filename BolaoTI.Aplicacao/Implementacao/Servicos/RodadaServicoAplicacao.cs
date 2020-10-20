using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class RodadaServicoAplicacao : IRodadaServicoAplicacao
    {
        private readonly IRodadaRepositorio _RodadaRepositorio;
        private readonly IRodadaServicoCadastro _RodadaServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public RodadaServicoAplicacao(IRodadaRepositorio RodadaRepositorio,
                                       IRodadaServicoCadastro RodadaServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _RodadaRepositorio = RodadaRepositorio;
            _RodadaServicoCadastro = RodadaServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }



        public virtual void CadastrarRodada(Rodada Rodada)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _RodadaServicoCadastro.CadastrarRodada(Rodada);

                unidadeDeTrabalho.Completar();
            }
        }

        public Rodada RecuperarPorId(int id)
        {
            return _RodadaRepositorio.Get(id);
        }

        public virtual IList<Rodada> RecuperarPorFiltro(string nome, int? idFase, int? idGrupo)
        {
            return _RodadaRepositorio.FindByFilter(nome, idFase, idGrupo);
        }

        public virtual IList<Rodada> RecuperarTodosOsRodadas()
        {
            return _RodadaRepositorio.FindAll();
        }

        public virtual void Remover(Rodada Rodada)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _RodadaRepositorio.Delete(Rodada);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _RodadaRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Rodada Rodada)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _RodadaRepositorio.Update(Rodada);

                unidadeDeTrabalho.Completar();
            }
        }
    }
}
