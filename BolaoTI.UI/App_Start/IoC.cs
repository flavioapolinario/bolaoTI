using BolaoTI.Aplicacao.Implementacao.Servicos;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Infra.Implementacao.Repositorios;
using BolaoTI.UI.Identity;
using Microsoft.AspNet.Identity;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Web.Mvc;

namespace BolaoTI.UI
{
    public static class IoC
    {
        private static Container container;

        public static void Start()
        {
            container = new Container();

            // Serviços de Aplicação
            container.Register<IApostaServicoAplicacao, ApostaServicoAplicacao>();
            container.Register<ICampeonatoServicoAplicacao, CampeonatoServicoAplicacao>();
            container.Register<IEstadioServicoAplicacao, EstadioServicoAplicacao>();
            container.Register<IFaseServicoAplicacao, FaseServicoAplicacao>();
            container.Register<IGrupoServicoAplicacao, GrupoServicoAplicacao>();
            container.Register<IPartidaServicoAplicacao, PartidaServicoAplicacao>();
            container.Register<IRegraServicoAplicacao, RegraServicoAplicacao>();
            container.Register<IRodadaServicoAplicacao, RodadaServicoAplicacao>();
            container.Register<ITimeServicoAplicacao, TimeServicoAplicacao>();
            container.Register<IUsuarioServicoAplicacao, UsuarioServicoAplicacao>();
            container.Register<IOrganizacaoServicoAplicacao, OrganizacaoServicoAplicacao>();
            container.Register<IRankingServicoAplicacao, RankingServicoAplicacao>();

            // Serviços de Cadastro
            container.Register<ICampeonatoServicoCadastro, CampeonatoServicoCadastro>();
            container.Register<IEstadioServicoCadastro, EstadioServicoCadastro>();
            container.Register<IFaseServicoCadastro, FaseServicoCadastro>();
            container.Register<IGrupoServicoCadastro, GrupoServicoCadastro>();
            container.Register<IPartidaServicoCadastro, PartidaServicoCadastro>();
            container.Register<IRodadaServicoCadastro, RodadaServicoCadastro>();
            container.Register<ITimeServicoCadastro, TimeServicoCadastro>();
            container.Register<IUsuarioServicoCadastro, UsuarioServicoCadastro>();
            container.Register<IOrganizacaoServicoCadastro, OrganizacaoServicoCadastro>();
            
            // Repositorios
            container.Register<IApostaRepositorio, ApostaRepositorio>();
            container.Register<ICampeonatoRepositorio, CampeonatoRepositorio>();
            container.Register<IEstadioRepositorio, EstadioRepositorio>();
            container.Register<IFaseRepositorio, FaseRepositorio>();
            container.Register<IGrupoRepositorio, GrupoRepositorio>();
            container.Register<IPartidaRepositorio, PartidaRepositorio>();
            container.Register<IRankingRepositorio, RankingRepositorio>();
            container.Register<IRodadaRepositorio, RodadaRepositorio>();
            container.Register<ITimeRepositorio, TimeRepositorio>();
            container.Register<IUsuarioRepositorio, UsuarioRepositorio>();
            container.Register<IPerfilRepositorio, PerfilRepositorio>();
            container.Register<IOrganizacaoRepositorio, OrganizacaoRepositorio>();
            
            // Repositorio Usuario e Perfil
            container.Register<IUserStore<IdentityUser, Guid>, UserStore>();
            container.Register<IRoleStore<IdentityRole, Guid>, RoleStore>();
                        
            container.Register<BolaoTIContext>(() => new GerenciadorDeContextoBancoHttp().Contexto);
            container.Register<IFabricaDeUnidadeDeTrabalho, FabricaDeUnidadeDeTrabalhoEF>();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public static TServico Obter<TServico>() where TServico : class
        {
            return container.GetInstance<TServico>();
        }
    }
}