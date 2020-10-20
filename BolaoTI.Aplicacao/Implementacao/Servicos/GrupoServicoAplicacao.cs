using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class GrupoServicoAplicacao : IGrupoServicoAplicacao
    {
        private readonly IGrupoRepositorio _GrupoRepositorio;
        private readonly IGrupoServicoCadastro _GrupoServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public GrupoServicoAplicacao(IGrupoRepositorio GrupoRepositorio,
                                       IGrupoServicoCadastro GrupoServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _GrupoRepositorio = GrupoRepositorio;
            _GrupoServicoCadastro = GrupoServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public virtual void CadastrarGrupo(Grupo Grupo)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _GrupoServicoCadastro.CadastrarGrupo(Grupo);

                unidadeDeTrabalho.Completar();
            }
        }

        public Grupo RecuperarPorId(int id)
        {
            return _GrupoRepositorio.Get(id);
        }

        public virtual IList<Grupo> RecuperarPorFiltro(string nome, int? idFase)
        {
            return _GrupoRepositorio.FindByFilter(nome, idFase);
        }

        public virtual IList<Grupo> RecuperarTodosOsGrupos()
        {
            return _GrupoRepositorio.FindAll();
        }

        public virtual void Remover(Grupo Grupo)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _GrupoRepositorio.Delete(Grupo);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _GrupoRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Grupo Grupo)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _GrupoRepositorio.Update(Grupo);

                unidadeDeTrabalho.Completar();
            }
        }
    }
}
