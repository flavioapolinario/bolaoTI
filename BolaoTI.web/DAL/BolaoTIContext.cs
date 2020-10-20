using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BolaoTI.web.DAL
{
    public class BolaoTIContext : DbContext
    {
        public BolaoTIContext()
            : base("BolaoTIContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Fase> Fases { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Rodada> Rodadas { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Estadio> Estadios { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Aposta> Apostas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Partida>().HasRequired(c => c.TimeAway).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Partida>().HasRequired(c => c.TimeHome).WithMany().WillCascadeOnDelete(false);
        }
    }
}