namespace BolaoTI.web.Migrations
{
    using BolaoTI.web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using BolaoTI.web.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<BolaoTIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BolaoTI.web.DAL.BolaoTIContext context)
        {
            CriarPermissoesUsuarios();

            if (context.Times.Count() == 0)
                CriarTimes(context);

            if (context.Estadios.Count() == 0)
                CriarEstadios(context);

            if (context.Fases.Count() == 0)
                CriarFases(context);

            if (context.Grupos.Count() == 0)
                CriarGrupos(context.Fases.ToList(), context);

            if (context.Rodadas.Count() == 0)
                CriarRodadas(context.Grupos.ToList(), context);

            if (context.Partidas.Count() == 0)
                CriarPartidas(context.Rodadas.ToList(), context.Estadios.ToList(), context.Times.ToList(), context);
        }

        private void CriarPermissoesUsuarios()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("BolaoTIContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            if (!WebSecurity.UserExists("flavio"))
                WebSecurity.CreateUserAndAccount(
                    "flavio",
                    "123456");
            if (!WebSecurity.UserExists("marcelino"))
                WebSecurity.CreateUserAndAccount(
                    "marcelino",
                    "123456");

            if (!Roles.GetRolesForUser("flavio").Contains("Admin"))
                Roles.AddUsersToRoles(new[] { "flavio" }, new[] { "Admin" });

            if (!Roles.GetRolesForUser("marcelino").Contains("Admin"))
                Roles.AddUsersToRoles(new[] { "marcelino" }, new[] { "Admin" });
        }

        private void CriarGrupos(List<Fase> fases, DAL.BolaoTIContext context)
        {
            Fase faseDeGrupos = fases.Find(f => f.Nome.Equals("FASE DE GRUPOS"));

            var grupoA = new string[] { "BRA", "CRO", "MEX", "CAM" };
            var grupoB = new string[] { "ESP", "HOL", "CHI", "AUS" };
            var grupoC = new string[] { "COL", "GRE", "CDM", "JAP" };
            var grupoD = new string[] { "URU", "COS", "ING", "ITA" };
            var grupoE = new string[] { "SUI", "EQU", "FRA", "HON" };
            var grupoF = new string[] { "ARG", "BOS", "IRA", "NGA" };
            var grupoG = new string[] { "ALE", "POR", "GAN", "EUA" };
            var grupoH = new string[] { "BEL", "AGL", "RUS", "COR" };

            var grupos = new List<Grupo>
            {
                new Grupo { Nome = "GRUPO A", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO B", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO C", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO D", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO E", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO F", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO G", Fase=faseDeGrupos, },
                new Grupo { Nome = "GRUPO H", Fase=faseDeGrupos, },                              
            };
            
            grupos.ForEach(s => context.Grupos.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarPartidas(List<Rodada> rodadas, List<Estadio> estadios, List<Time> times, DAL.BolaoTIContext context)
        {
            string grupo = string.Empty;
            string rodada = string.Empty;

            var partidas = new List<Partida>();

            #region Grupo A

            grupo = "GRUPO A";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 12, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Corinthians")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CRO")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 13, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio das Dunas")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("MEX")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CAM")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 17, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Castelão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("MEX")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 18, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Amazônia")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("CAM")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CRO")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 23, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Mané Garrincha")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("CAM")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("BRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 23, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pernambuco")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("CRO")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("MEX")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo B

            grupo = "GRUPO B";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 13, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Fonte Nova")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ESP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("HOL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 13, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pantanal")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("CHI")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("AUS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 18, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Beira-Rio")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("AUS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("HOL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 18, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Maracanã")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ESP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CHI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 23, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena da Baixada")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("AUS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ESP")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 23, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Corinthians")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("HOL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CHI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo C

            grupo = "GRUPO C";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 14, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Mineirão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("GRE")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 14, 22, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pernambuco")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("CDM")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 19, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Mané Garrincha")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CDM")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 19, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio das Dunas")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("GRE")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 24, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Castelão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("GRE")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CDM")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 24, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pantanal")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo D

            grupo = "GRUPO D";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 14, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Castelão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("URU")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 14, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Amazônia")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ING")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ITA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 19, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Corinthians")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("URU")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ING")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 20, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pernambuco")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ITA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 24, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Mineirão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ING")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 24, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio das Dunas")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ITA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("URU")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo E

            grupo = "GRUPO E";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 15, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Mané Garrincha")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SUI")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("EQU")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 15, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Beira-Rio")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("FRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("HON")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 20, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Fonte Nova")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SUI")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("FRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 20, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena da Baixada")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("HON")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("EQU")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 25, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Maracanã")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("EQU")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("FRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 25, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Amazônia")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("HON")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SUI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo F

            grupo = "GRUPO F";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 15, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Maracanã")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ARG")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("BOS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 16, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena da Baixada")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("IRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("NGA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 21, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Mineirão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ARG")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("IRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 21, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pantanal")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("NGA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("BOS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 25, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Fonte Nova")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BOS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("IRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 25, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Beira-Rio")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("NGA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ARG")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo G

            grupo = "GRUPO G";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 16, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Fonte Nova")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ALE")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("POR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 16, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio das Dunas")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("GAN")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("EUA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 21, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Castelão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ALE")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("GAN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 22, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Amazônia")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("EUA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("POR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 26, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Estádio Mané Garrincha")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("POR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("GAN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 26, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pernambuco")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("EUA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ALE")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo H

            grupo = "GRUPO H";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 17, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Mineirão")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BEL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("AGL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 17, 19, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Pantanal")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("RUS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 22, 13, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Maracanã")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BEL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("RUS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 22, 16, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Beira-Rio")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("AGL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 26, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena da Baixada")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("AGL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("RUS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(2014, 06, 26, 17, 0, 0),
                EstadioJogo = estadios.Find(e => e.Nome.Equals("Arena Corinthians")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("BEL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.RodadaGrupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            partidas.ForEach(s => context.Partidas.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();
        }

        private void CriarRodadas(List<Grupo> grupos, DAL.BolaoTIContext context)
        {
            var rodadas = new List<Rodada>();

            grupos.ForEach(g =>
            {
                for (int i = 1; i <= 3; i++)
                {
                    rodadas.Add(new Rodada
                    {
                        Nome = "Rodada 0" + i.ToString(),
                        RodadaGrupo = g
                    });
                }
            });

            rodadas.ForEach(s => context.Rodadas.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarFases(DAL.BolaoTIContext context)
        {
            var fases = new List<Fase>
            {
                new Fase { Nome = "FASE DE GRUPOS" },
                new Fase { Nome = "OITAVAS DE FINAL" },
                new Fase { Nome = "QUARTAS DE FINAL" },
                new Fase { Nome = "SEMIFINAL" },                
                new Fase { Nome = "FINAL" },                
            };

            fases.ForEach(s => context.Fases.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarEstadios(DAL.BolaoTIContext context)
        {
            var estadios = new List<Estadio>
            {
                new Estadio { Nome = "Arena Fonte Nova", Cidade="Salvador", Uf="BA", Capacidade=54610 },
                new Estadio { Nome = "Estádio das Dunas", Cidade="Natal", Uf="RN", Capacidade=44070 },
                new Estadio { Nome = "Estádio Castelão", Cidade="Fortaleza ", Uf="CE", Capacidade=63819 },
                new Estadio { Nome = "Arena Pernambuco", Cidade="São Lourenço da Mata", Uf="PE", Capacidade=45425 },
                new Estadio { Nome = "Arena Amazônia", Cidade="Manaus", Uf="AM", Capacidade=44351 },
                new Estadio { Nome = "Estádio Mané Garrincha", Cidade="Brasília", Uf="DF", Capacidade=72741 },
                new Estadio { Nome = "Arena Pantanal", Cidade="Cuiabá", Uf="MT", Capacidade=44335 },
                new Estadio { Nome = "Arena Corinthians", Cidade="São Paulo", Uf="SP", Capacidade=67349 },
                new Estadio { Nome = "Maracanã", Cidade="Rio de Janeiro", Uf="RJ", Capacidade=78448 },
                new Estadio { Nome = "Mineirão", Cidade="Belo Horizonte", Uf="MG", Capacidade=62329 },
                new Estadio { Nome = "Arena da Baixada", Cidade="Curitiba", Uf="PR", Capacidade=42247 },
                new Estadio { Nome = "Beira-Rio", Cidade="Porto Alegre", Uf="RS", Capacidade=47110 },
            };

            estadios.ForEach(s => context.Estadios.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarTimes(DAL.BolaoTIContext context)
        {
            var times = new List<Time>
            {
                // Grupo A
                new Time { Nome="Brasil", NomeAbreviado="BRA", ImagemBandeira="~/Content/themes/base/images/Bandeiras/brasil_30x30.png" },
                new Time { Nome="Croacia", NomeAbreviado="CRO", ImagemBandeira="~/Content/themes/base/images/Bandeiras/croacia_30x30.png" },
                new Time { Nome="Mexico", NomeAbreviado="MEX", ImagemBandeira="~/Content/themes/base/images/Bandeiras/mexico_30x30.png" },
                new Time { Nome="Camarões", NomeAbreviado="CAM", ImagemBandeira="~/Content/themes/base/images/Bandeiras/camaroes_30x30.png" },

                // Grupo B
                new Time { Nome="Espanha", NomeAbreviado="ESP", ImagemBandeira="~/Content/themes/base/images/Bandeiras/espanha_30x30.png" },
                new Time { Nome="Holanda", NomeAbreviado="HOL", ImagemBandeira="~/Content/themes/base/images/Bandeiras/holanda_30x30.png" },
                new Time { Nome="Chile", NomeAbreviado="CHI", ImagemBandeira="~/Content/themes/base/images/Bandeiras/chile_30x30.png" },
                new Time { Nome="Austrália", NomeAbreviado="AUS", ImagemBandeira="~/Content/themes/base/images/Bandeiras/australia_30x30.png" },
                
                // Grupo C
                new Time { Nome="Colômbia", NomeAbreviado="COL", ImagemBandeira="~/Content/themes/base/images/Bandeiras/colombia_30x30.png" },
                new Time { Nome="Grécia", NomeAbreviado="GRE", ImagemBandeira="~/Content/themes/base/images/Bandeiras/grecia_30x30.png" },
                new Time { Nome="Costa do Marfim", NomeAbreviado="CDM", ImagemBandeira="~/Content/themes/base/images/Bandeiras/costa_do_marfim_30x30.png" },
                new Time { Nome="Japão", NomeAbreviado="JAP", ImagemBandeira="~/Content/themes/base/images/Bandeiras/japao_30x30.png" },
                
                // Grupo D
                new Time { Nome="Uruguai", NomeAbreviado="URU", ImagemBandeira="~/Content/themes/base/images/Bandeiras/uruguai_30x30.png" },
                new Time { Nome="Costa Rica", NomeAbreviado="COS", ImagemBandeira="~/Content/themes/base/images/Bandeiras/costa_rica_30x30.png" },
                new Time { Nome="Inglaterra", NomeAbreviado="ING", ImagemBandeira="~/Content/themes/base/images/Bandeiras/inglaterra_30x30.png" },
                new Time { Nome="Itália", NomeAbreviado="ITA", ImagemBandeira="~/Content/themes/base/images/Bandeiras/italia_30x30.png" },
                
                // Grupo E
                new Time { Nome="Suíça", NomeAbreviado="SUI", ImagemBandeira="~/Content/themes/base/images/Bandeiras/suica_30x30.png" },
                new Time { Nome="Equador", NomeAbreviado="EQU", ImagemBandeira="~/Content/themes/base/images/Bandeiras/equador_30x30.png" },
                new Time { Nome="França", NomeAbreviado="FRA", ImagemBandeira="~/Content/themes/base/images/Bandeiras/franca_30x30.png" },
                new Time { Nome="Honduras", NomeAbreviado="HON", ImagemBandeira="~/Content/themes/base/images/Bandeiras/honduras_30x30.png" },
                
                // Grupo F
                new Time { Nome="Argentina", NomeAbreviado="ARG", ImagemBandeira="~/Content/themes/base/images/Bandeiras/argentina_30x30.png" },
                new Time { Nome="Bósnia-Herzegovina", NomeAbreviado="BOS", ImagemBandeira="~/Content/themes/base/images/Bandeiras/bosnia_30x30.png" },
                new Time { Nome="Irã", NomeAbreviado="IRA", ImagemBandeira="~/Content/themes/base/images/Bandeiras/ira_30x30.png" },
                new Time { Nome="Nigéria", NomeAbreviado="NGA", ImagemBandeira="~/Content/themes/base/images/Bandeiras/nigeria_30x30.png" },
                
                // Grupo G
                new Time { Nome="Alemanha", NomeAbreviado="ALE", ImagemBandeira="~/Content/themes/base/images/Bandeiras/alemanha_30x30.png" },
                new Time { Nome="Portugal", NomeAbreviado="POR", ImagemBandeira="~/Content/themes/base/images/Bandeiras/portugal_30x30.png" },
                new Time { Nome="Gana", NomeAbreviado="GAN", ImagemBandeira="~/Content/themes/base/images/Bandeiras/gana_30x30.png" },
                new Time { Nome="Estados Unidos", NomeAbreviado="EUA", ImagemBandeira="~/Content/themes/base/images/Bandeiras/estados_unidos_30x30.png" },
                
                // Grupo H
                new Time { Nome="Bélgica", NomeAbreviado="BEL", ImagemBandeira="~/Content/themes/base/images/Bandeiras/belgica_30x30.png" },
                new Time { Nome="Argélia", NomeAbreviado="AGL", ImagemBandeira="~/Content/themes/base/images/Bandeiras/argelia_30x30.png" },
                new Time { Nome="Rússia", NomeAbreviado="RUS", ImagemBandeira="~/Content/themes/base/images/Bandeiras/russia_30x30.png" },
                new Time { Nome="Coreia do Sul", NomeAbreviado="COR", ImagemBandeira="~/Content/themes/base/images/Bandeiras/coreia_do_sul_30x30.png" },
                
            };

            times.ForEach(s => context.Times.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

    }
}
