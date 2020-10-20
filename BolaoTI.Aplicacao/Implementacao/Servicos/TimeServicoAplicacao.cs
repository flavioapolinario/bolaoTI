using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class TimeServicoAplicacao : ITimeServicoAplicacao
    {
        private readonly ITimeRepositorio _timeRepositorio;
        private readonly ITimeServicoCadastro _timeServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public TimeServicoAplicacao(ITimeRepositorio TimeRepositorio,
                                       ITimeServicoCadastro TimeServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _timeRepositorio = TimeRepositorio;
            _timeServicoCadastro = TimeServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public virtual void CadastrarTime(Time Time)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _timeServicoCadastro.CadastrarTime(Time);

                unidadeDeTrabalho.Completar();
            }
        }

        public Time RecuperarPorId(int id)
        {
            return _timeRepositorio.Get(id);
        }

        public virtual IList<Time> RecuperarPorFiltro(string nome, string nomeAbreviado)
        {
            return _timeRepositorio.FindByFilter(nome, nomeAbreviado);
        }

        public virtual IList<Time> RecuperarTodosOsTimes()
        {
            return _timeRepositorio.FindAll();
        }

        public virtual void Remover(Time Time)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _timeRepositorio.Delete(Time);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _timeRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Time Time)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _timeRepositorio.Update(Time);

                unidadeDeTrabalho.Completar();
            }
        }

    }
}
