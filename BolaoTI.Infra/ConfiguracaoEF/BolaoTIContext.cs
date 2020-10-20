using BolaoTI.Dominio;
using System.Data.Entity;
using BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig;
using System.Data.Entity.SqlServer;

namespace BolaoTI.Infra.ConfiguracaoEF
{
    public class BolaoTIContext : DbContext
    {
        public BolaoTIContext()
            : base(BolaoTI.Resources.Configuration.BolaoTIContext)
        {
            Database.SetInitializer(new BolaoTIInitializer());
        }
        
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Organizacao> Organizacoes { get; set; }
        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<Fase> Fases { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Rodada> Rodadas { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Estadio> Estadios { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Aposta> Apostas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfils { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            var dbSpatialServices = SqlSpatialServices.Default;

            modelBuilder.Configurations.Add(new RankingConfig());
            modelBuilder.Configurations.Add(new OrganizacaoConfig());
            modelBuilder.Configurations.Add(new CampeonatoConfig());
            modelBuilder.Configurations.Add(new FaseConfig());
            modelBuilder.Configurations.Add(new GrupoConfig());
            modelBuilder.Configurations.Add(new RodadaConfig());
            modelBuilder.Configurations.Add(new TimeConfig());
            modelBuilder.Configurations.Add(new EstadioConfig());
            modelBuilder.Configurations.Add(new PartidaConfig());
            modelBuilder.Configurations.Add(new ApostaConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new PerfilConfig());
            modelBuilder.Configurations.Add(new ExternalLoginConfig());
            modelBuilder.Configurations.Add(new ClaimConfig());
            
        }
    }
}