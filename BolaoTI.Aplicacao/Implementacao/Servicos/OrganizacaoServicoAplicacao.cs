using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class OrganizacaoServicoAplicacao : IOrganizacaoServicoAplicacao
    {
        private readonly IOrganizacaoRepositorio _OrganizacaoRepositorio;
        private readonly IOrganizacaoServicoCadastro _OrganizacaoServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public OrganizacaoServicoAplicacao(IOrganizacaoRepositorio OrganizacaoRepositorio,
                                       IOrganizacaoServicoCadastro OrganizacaoServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _OrganizacaoRepositorio = OrganizacaoRepositorio;
            _OrganizacaoServicoCadastro = OrganizacaoServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }



        public virtual void CadastrarOrganizacao(Organizacao organizacao)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _OrganizacaoServicoCadastro.CadastrarOrganizacao(organizacao);

                unidadeDeTrabalho.Completar();
            }
        }

        public Organizacao RecuperarPorId(int id)
        {
            return _OrganizacaoRepositorio.Get(id);
        }

        public IList<Organizacao> RecuperarPorFiltro(string nome, List<int> campeonatosId, List<Guid> usuariosId)
        {
            return _OrganizacaoRepositorio.FindByFilter(nome, campeonatosId, usuariosId);
        }

        public IList<Organizacao> RecuperarTodosOrganizacoes()
        {
            return _OrganizacaoRepositorio.FindAll();
        }

        public void Remover(Organizacao organizacao)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _OrganizacaoRepositorio.Delete(organizacao);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _OrganizacaoRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Organizacao organizacao)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _OrganizacaoRepositorio.Update(organizacao);

                unidadeDeTrabalho.Completar();
            }
        }
       
    }
}
