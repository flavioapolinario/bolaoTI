using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Migrations;
using BolaoTI.Resources;
using Microsoft.AspNet.Identity;

namespace BolaoTI.Infra.ConfiguracaoEF
{
    public class BolaoTIInitializer : DropCreateDatabaseIfModelChanges<BolaoTIContext>
    {
        protected override void Seed(BolaoTIContext context)
        {
            try
            {
                if (!context.Perfils.Any())
                    CriarPerfis(context);

                if (!context.Usuarios.Any())
                    CriarUsuarios(context.Perfils.ToList(), context);

                if (!context.Times.Any())
                    CriarTimes(context);

                if (!context.Estadios.Any())
                    CriarEstadios(context);

                if (!context.Organizacoes.Any())
                    CriarOrganizacoes(context.Usuarios.ToList(), context);

                if (!context.Campeonatos.Any())
                    CriarCampeonato(context.Organizacoes.ToList(), context);

                if (!context.Fases.Any())
                    CriarFases(context.Campeonatos.ToList(), context);

                if (!context.Grupos.Any())
                    CriarGrupos(context.Fases.ToList(), context);

                if (!context.Rodadas.Any())
                    CriarRodadas(context.Grupos.ToList(), context);

                if (!context.Partidas.Any())
                    CriarPartidas(context.Rodadas.ToList(), context.Estadios.ToList(), context.Times.ToList(), context);

                //if (!context.Rankings.Any())
                //    CriarRankings(context.Campeonatos.ToList(), context);

                base.Seed(context);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void CriarPerfis(BolaoTIContext context)
        {
            List<Perfil> perfils = new List<Perfil>()
            {
                new Perfil { Id = Guid.NewGuid(), Nome = Configuration.Role_Padrao_Admin },
                new Perfil { Id = Guid.NewGuid(), Nome = Configuration.Role_Padrao_Participante },
            };
            perfils.ForEach(s => context.Perfils.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarUsuarios(List<Perfil> perfis, BolaoTIContext context)
        {
            var passwordHash = new PasswordHasher();

            List<Usuario> usuarios = new List<Usuario>()
            {
                new Usuario {
                    Id = Guid.NewGuid(),
                    Nome = Configuration.Usuario_Padrao_Admin,
                    PasswordHash = passwordHash.HashPassword(Configuration.Usuario_Padrao_Admin_Senha),
                    Telefone=Configuration.Usuario_Padrao_Admin_Telefone,
                    Email = Configuration.Usuario_Padrao_Admin_Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Perfis = perfis.Where(p=>p.Nome.Equals(Configuration.Role_Padrao_Admin)).ToList()
                },
                 new Usuario {
                    Id = Guid.NewGuid(),
                    Nome = Configuration.Usuario_Padrao_Participante,
                    PasswordHash = passwordHash.HashPassword(Configuration.Usuario_Padrao_Participante_Senha),
                    Telefone=Configuration.Usuario_Padrao_Participante_Telefone,
                    Email = Configuration.Usuario_Padrao_Participante_Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Perfis = perfis.Where(p=>p.Nome.Equals(Configuration.Role_Padrao_Participante)).ToList()
                },
            };
            usuarios.ForEach(s => context.Usuarios.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();
        }

        private void CriarEstadios(BolaoTIContext context)
        {
            var estadios = new List<Estadio>
            {
                new Estadio { Nome = "Lujniki", Cidade="Moscou", Uf="RS", Capacidade=80000 },
                new Estadio { Nome = "Spartak", Cidade="Moscou", Uf="RS", Capacidade=45000 },
                new Estadio { Nome = "São Petersburgo", Cidade="São Petersburgo", Uf="RS", Capacidade=67000 },
                new Estadio { Nome = "Nizhny Novgorod", Cidade="Nizhny Novgorod", Uf="RS", Capacidade=48000 },
                new Estadio { Nome = "Sochi", Cidade="Sochi", Uf="RS", Capacidade=48000 },
                new Estadio { Nome = "Kazan", Cidade="Kazan", Uf="RS", Capacidade=45000 },
                new Estadio { Nome = "Samara", Cidade="Samara", Uf="RS", Capacidade=60000 },
                new Estadio { Nome = "Rostov", Cidade="Rostov", Uf="RS", Capacidade=60000 },
                new Estadio { Nome = "Ecaterimburgo", Cidade="Ecaterimburgo", Uf="RS", Capacidade=60000 },
                new Estadio { Nome = "Kaliningrado", Cidade="Kaliningrado", Uf="RS", Capacidade=60000 },
                new Estadio { Nome = "Saransk", Cidade="Saransk", Uf="RS", Capacidade=60000 },
                new Estadio { Nome = "Volgogrado", Cidade="Volgogrado", Uf="RS", Capacidade=60000 },
            };

            estadios.ForEach(s => context.Estadios.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarTimes(BolaoTIContext context)
        {
            var times = new List<Time>
            {
                // Grupo A
                new Time { Nome="Arábia Saudita", NomeAbreviado="ARA", ImagemBandeira="~/Content/Imagens/Bandeiras/ArabiaSaudita-65.png" },
                new Time { Nome="Egito", NomeAbreviado="EGI", ImagemBandeira="~/Content/Imagens/Bandeiras/egito_65.png" },
                new Time { Nome="Rússia", NomeAbreviado="RUS", ImagemBandeira="~/Content/Imagens/Bandeiras/Russia_65.png" },
                new Time { Nome="Uruguai", NomeAbreviado="URU", ImagemBandeira="~/Content/Imagens/Bandeiras/Uruguai_65.png" },

                // Grupo B
                new Time { Nome="Espanha", NomeAbreviado="ESP", ImagemBandeira="~/Content/Imagens/Bandeiras/ESPANHA-65.png" },
                new Time { Nome="Irã", NomeAbreviado="IRA", ImagemBandeira="~/Content/Imagens/Bandeiras/Ira-65.png" },
                new Time { Nome="Marrocos", NomeAbreviado="MAR", ImagemBandeira="~/Content/Imagens/Bandeiras/marrocos_65.png" },
                new Time { Nome="Portugal", NomeAbreviado="POR", ImagemBandeira="~/Content/Imagens/Bandeiras/Portugal_65.png" },
                
                // Grupo C
                new Time { Nome="Austrália", NomeAbreviado="AUS", ImagemBandeira="~/Content/Imagens/Bandeiras/Australia-65.png" },
                new Time { Nome="Dinamarca", NomeAbreviado="DIN", ImagemBandeira="~/Content/Imagens/Bandeiras/Dinamarca_65.png" },
                new Time { Nome="França", NomeAbreviado="FRA", ImagemBandeira="~/Content/Imagens/Bandeiras/FRANCA-65.png" },
                new Time { Nome="Peru", NomeAbreviado="PER", ImagemBandeira="~/Content/Imagens/Bandeiras/Peru_65.png" },
 
                // Grupo D
                new Time { Nome="Argentina", NomeAbreviado="ARG", ImagemBandeira="~/Content/Imagens/Bandeiras/Argentina_65.png" },
                new Time { Nome="Croácia", NomeAbreviado="CRO", ImagemBandeira="~/Content/Imagens/Bandeiras/Croacia_65.png" },
                new Time { Nome="Islândia", NomeAbreviado="ISL", ImagemBandeira="~/Content/Imagens/Bandeiras/Islandia_65.png" },
                new Time { Nome="Nigéria", NomeAbreviado="NIG", ImagemBandeira="~/Content/Imagens/Bandeiras/Nigeria_65.png" },

                // Grupo E
                new Time { Nome="Brasil", NomeAbreviado="BRA", ImagemBandeira="~/Content/Imagens/Bandeiras/Brasil_65.png" },
                new Time { Nome="Costa Rica", NomeAbreviado="COS", ImagemBandeira="~/Content/Imagens/Bandeiras/CostaRica_65.png" },
                new Time { Nome="Sérvia", NomeAbreviado="SER", ImagemBandeira="~/Content/Imagens/Bandeiras/servia_65.png" },
                new Time { Nome="Suíça", NomeAbreviado="SUI", ImagemBandeira="~/Content/Imagens/Bandeiras/suica_65.png" },

                // Grupo F
                new Time { Nome="Alemanha", NomeAbreviado="ALE", ImagemBandeira="~/Content/Imagens/Bandeiras/Alemanha_65.png" },
                new Time { Nome="Coreia do Sul", NomeAbreviado="COR", ImagemBandeira="~/Content/Imagens/Bandeiras/Coreia_Sul_65.png" },
                new Time { Nome="México", NomeAbreviado="MEX", ImagemBandeira="~/Content/Imagens/Bandeiras/Mexico_65.png" },
                new Time { Nome="Suécia", NomeAbreviado="SUE", ImagemBandeira="~/Content/Imagens/Bandeiras/Suecia_65.png" },

                // Grupo G
                new Time { Nome="Bélgica", NomeAbreviado="BEL", ImagemBandeira="~/Content/Imagens/Bandeiras/Belgica_65.png" },
                new Time { Nome="Inglaterra", NomeAbreviado="ING", ImagemBandeira="~/Content/Imagens/Bandeiras/Inglaterra_65x65.png" },
                new Time { Nome="Panamá", NomeAbreviado="PAN", ImagemBandeira="~/Content/Imagens/Bandeiras/Panama_65.png" },
                new Time { Nome="Tunísia", NomeAbreviado="TUN", ImagemBandeira="~/Content/Imagens/Bandeiras/tunisia_65.png" },

                // Grupo H
                new Time { Nome="Colômbia", NomeAbreviado="COL", ImagemBandeira="~/Content/Imagens/Bandeiras/Colombia_65.png" },
                new Time { Nome="Japão", NomeAbreviado="JAP", ImagemBandeira="~/Content/Imagens/Bandeiras/Japao_65.png" },
                new Time { Nome="Polônia", NomeAbreviado="POL", ImagemBandeira="~/Content/Imagens/Bandeiras/Polonia_65.png" },
                new Time { Nome="Senegal", NomeAbreviado="SEN", ImagemBandeira="~/Content/Imagens/Bandeiras/Senegal-65.png" },
            };

            times.ForEach(s => context.Times.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarOrganizacoes(List<Usuario> usuarios, BolaoTIContext context)
        {
            List<Organizacao> organizacoes = new List<Organizacao>()
            {
                new Organizacao { Nome = "CSN - TI", Usuarios = usuarios },
            };

            organizacoes.ForEach(o => context.Organizacoes.AddOrUpdate(p => p.Nome, o));
            context.SaveChanges();
        }

        private void CriarCampeonato(List<Organizacao> organizacoes, BolaoTIContext context)
        {
            List<Campeonato> campeoantos = new List<Campeonato>()
            {
                new Campeonato {
                    Nome = "COPA RUSSIA 2018",
                    NomeAbreviado = "COPA2018",
                    Inicio= new DateTime(2018,6,14),
                    Fim = new DateTime(2018,7,30,23,59,59),
                    Organizacoes = organizacoes
                },
            };
            campeoantos.ForEach(c => context.Campeonatos.AddOrUpdate(p => p.Nome, c));
            context.SaveChanges();
        }

        private void CriarFases(List<Campeonato> campeonatos, BolaoTIContext context)
        {
            Campeonato campeonato = campeonatos.Find(f => f.Nome.Equals("COPA RUSSIA 2018"));
            int ano = 2018;
            int mes = 6;
            DateTime data = new DateTime(ano, mes, 4);

            var fases = new List<Fase>
            {
                new Fase { Nome = "FASE DE GRUPOS", DataInicio = new DateTime(ano,mes,1), DataFim =new DateTime(ano,mes,13,23,59,59), Campeonato = campeonato, CampeonatoId = campeonato.Id },
                new Fase { Nome = "OITAVAS DE FINAL", DataInicio = new DateTime(ano,mes, 25), DataFim =new DateTime(ano,mes,29,23,59,59), Campeonato = campeonato, CampeonatoId = campeonato.Id },
                new Fase { Nome = "QUARTAS DE FINAL", DataInicio = new DateTime(ano,mes+1,4), DataFim=new DateTime(ano,mes+1,5,23,59,59), Campeonato = campeonato, CampeonatoId = campeonato.Id  },
                new Fase { Nome = "SEMIFINAL", DataInicio = new DateTime(ano,mes+1,8), DataFim=new DateTime(ano,mes+1,9,23,59,59), Campeonato = campeonato, CampeonatoId = campeonato.Id  },
                new Fase { Nome = "FINAL", DataInicio = new DateTime(ano,mes+1,12), DataFim=new DateTime(ano,mes+1,13, 23,59,59), Campeonato = campeonato, CampeonatoId = campeonato.Id  },
            };

            fases.ForEach(s => context.Fases.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarGrupos(List<Fase> fases, BolaoTIContext context)
        {
            Fase faseDeGrupos = fases.Find(f => f.Nome.Equals("FASE DE GRUPOS"));

            var grupos = new List<Grupo>
            {
                new Grupo { Nome = "GRUPO A", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO B", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO C", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO D", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO E", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO F", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO G", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
                new Grupo { Nome = "GRUPO H", Fase=faseDeGrupos, FaseId = faseDeGrupos.Id },
            };

            grupos.ForEach(s => context.Grupos.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarPartidas(List<Rodada> rodadas, List<Estadio> estadios, List<Time> times, BolaoTIContext context)
        {
            string grupo = string.Empty;
            string rodada = string.Empty;

            int ano = 2018;
            int mes = 6;

            var partidas = new List<Partida>();

            #region Grupo A

            grupo = "GRUPO A";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 14, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Lujniki")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("RUS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ARA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 15, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Ecaterimburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("EGI")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("URU")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 19, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("São Petersburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("RUS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("EGI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 20, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Rostov")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("URU")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ARA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 25, 11, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Samara")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("URU")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("RUS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 25, 11, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Volgogrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ARA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("EGI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo B

            grupo = "GRUPO B";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 15, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("São Petersburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("MAR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("IRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 15, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Sochi")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("POR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ESP")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 20, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Lujniki")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("POR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("MAR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 20, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kazan")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 25, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Saransk")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("IRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("POR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 25, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kaliningrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ESP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("MAR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo C

            grupo = "GRUPO C";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 16, 7, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kazan")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("FRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("AUS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 16, 13, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Saransk")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("PER")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("DIN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 21, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Samara")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("DIN")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("AUS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 21, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Ecaterimburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("FRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("PER")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 10, 16, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Lujniki")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("DIN")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("FRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 10, 16, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Sochi")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("AUS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("PER")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo D

            grupo = "GRUPO D";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 16, 10, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Spartak")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ARG")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ISL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 16, 16, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kaliningrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("CRO")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("NIG")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 21, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Nizhny Novgorod")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ARG")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CRO")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 22, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Volgogrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("NIG")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ISL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 26, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("São Petersburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("NIG")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ARG")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 26, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Rostov")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ISL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("CRO")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo E

            grupo = "GRUPO E";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 17, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Samara")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COS")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SER")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 17, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Rostov")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SUI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 22, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("São Petersburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BRA")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 22, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kaliningrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SER")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SUI")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 27, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Spartak")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SER")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("BRA")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 27, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Nizhny Novgorod")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SUI")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COS")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo F

            grupo = "GRUPO F";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 17, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Lujniki")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ALE")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("MEX")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 18, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Nizhny Novgorod")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SUE")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COR")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 23, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Rostov")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("MEX")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 23, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Sochi")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ALE")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SUE")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 27, 11, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kazan")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COR")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ALE")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 27, 11, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Ecaterimburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("MEX")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SUE")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo G

            grupo = "GRUPO G";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 18, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Sochi")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BEL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("PAN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 18, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Volgogrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("TUN")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("ING")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 23, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Spartak")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("BEL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("TUN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 24, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Nizhny Novgorod")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ING")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("PAN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 28, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kaliningrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("ING")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("BEL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 28, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Saransk")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("PAN")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("TUN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            #region Grupo H

            grupo = "GRUPO H";

            #region Rodada 01

            rodada = "Rodada 01";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 19, 9, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Saransk")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("COL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 19, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Spartak")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("POL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SEN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 02

            rodada = "Rodada 02";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 24, 12, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Ecaterimburgo")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("SEN")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 24, 15, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Kazan")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("POL")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #region Rodada 03

            rodada = "Rodada 03";

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 28, 11, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Volgogrado")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("JAP")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("POL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            partidas.Add(new Partida()
            {
                DataPartida = new DateTime(ano, mes, 28, 11, 0, 0),
                Estadio = estadios.Find(e => e.Nome.Equals("Samara")),
                TimeHome = times.Find(t => t.NomeAbreviado.Equals("SEN")),
                TimeAway = times.Find(t => t.NomeAbreviado.Equals("COL")),
                Rodada = rodadas.Find(x => x.Nome.Equals(rodada) && x.Grupo.Nome.Equals(grupo))
            });

            #endregion

            #endregion

            partidas.ForEach(s => context.Partidas.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();
        }

        private void CriarRodadas(List<Grupo> grupos, BolaoTIContext context)
        {
            var rodadas = new List<Rodada>();

            grupos.ForEach(g =>
            {
                for (int i = 1; i <= 3; i++)
                {
                    rodadas.Add(new Rodada
                    {
                        Nome = "Rodada 0" + i.ToString(),
                        Grupo = g,
                        GrupoId = g.Id,
                        Ordem = i
                    });
                }
            });

            rodadas.ForEach(s => context.Rodadas.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();
        }

        private void CriarRankings(List<Campeonato> campeonatos, BolaoTIContext context)
        {
            List<Ranking> rankings = new List<Ranking>();
            string _propriedadePartidas = string.Format(@"{0}s.{1}s.{2}s", Classes.Grupo_Class, Classes.Rodada_Class, Classes.Partida_Class);

            campeonatos.ForEach(c =>
            {
                var numeroPartidas = (from p in context.Partidas
                                      join r in context.Rodadas on p.RodadaId equals r.Id
                                      join g in context.Grupos on r.GrupoId equals g.Id
                                      join f in context.Fases on g.FaseId equals f.Id
                                      join ca in context.Campeonatos on f.CampeonatoId equals ca.Id
                                      where ca.Id == c.Id
                                      select p).Count();

                c.Organizacoes.ForEach(o =>
                {
                    o.Usuarios.ForEach(u =>
                    {
                        rankings.Add(new Ranking()
                        {
                            CampeonatoId = c.Id,
                            OrganizacaoId = o.Id,
                            UsuarioId = u.Id,
                            NumeroPartidas = numeroPartidas,
                            NumeroApostas = 0,
                            TotalPontos = 0,
                            DezTotalPontos = 0,
                            SeteTotalPontos = 0,
                            CincoTotalPontos = 0,
                            DoisTotalPontos = 0,
                            Colocacao = 1,
                        });
                    });
                });
            });
            context.Rankings.AddOrUpdate(a => new { a.CampeonatoId, a.OrganizacaoId, a.UsuarioId }, rankings.ToArray());
            context.SaveChanges();
        }

    }
}