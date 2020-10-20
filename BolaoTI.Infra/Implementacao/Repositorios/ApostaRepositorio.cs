using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Resources;
using System;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class ApostaRepositorio : RepositorioGenerico<Aposta>, IApostaRepositorio
    {
        public ApostaRepositorio(BolaoTIContext _contexto)
            : base(_contexto)
        {
        }

        public List<Aposta> FindApostasByFilter(int IdOrganizacao, int CampeonatoId, int IdFase, Guid? IdUsuario)
        {
            string propriedades = string.Format("{0},{1},{2},{3},{4}", Classes.Organizacao_Class, Classes.Campeonato_Class, Classes.Fase_Class, Classes.Grupo_Class, Classes.Rodada_Class, Classes.Partida_Class);

            var apostas = base.Get(filter: a => a.PartidaApostada.Rodada.Grupo.Fase.Campeonato.Id == CampeonatoId &&
                                                       a.PartidaApostada.Rodada.Grupo.Fase.Campeonato.Organizacoes.Any(p => p.Id == IdOrganizacao) &&
                                                       a.PartidaApostada.Rodada.Grupo.Fase.Id == IdFase,
                                          includeProperties: propriedades);
            if (IdUsuario.HasValue)
                apostas.Where(a => a.Usuario.Id == IdUsuario.Value);

            return apostas.ToList();
        }

        public List<Aposta> FindApostasByFase(Guid idUsuario, int idFase)
        {
            string propriedades = "PartidaApostada.Rodada.Grupo.Fase";

            return (from a in _dbSet.Include(propriedades)
                    where a.UsuarioId.Equals(idUsuario) &&
                          a.PartidaApostada.Rodada.Grupo.Fase.Id == idFase
                    select a).ToList();
        }

        public void RealizarAposta(IList<Aposta> apostas)
        {
            apostas.ToList().ForEach(base.Insert);
        }

        public List<Aposta> FindApostasByPartida(int idPartida)
        {
            string propriedades = "Usuario.Organizacoes";
            return (from a in _dbSet.Include(propriedades)
                    where a.PartidaId == idPartida
                    select a).ToList();
        }


        public List<Aposta> FindApostasByUsuario(int IdOrganizacao, int CampeonatoId, Guid IdUsuario)
        {           
            string propriedades = string.Format("PartidaApostada.{0}.{1}.{2}.{3}", Classes.Rodada_Class, Classes.Grupo_Class, Classes.Fase_Class, Classes.Campeonato_Class);

            var apostas = base.Get(filter: a => a.PartidaApostada.Rodada.Grupo.Fase.Campeonato.Id == CampeonatoId &&
                                                a.PartidaApostada.Rodada.Grupo.Fase.Campeonato.Organizacoes.Any(p => p.Id == IdOrganizacao) &&
                                                a.UsuarioId == IdUsuario,
                                          includeProperties: propriedades);
            return apostas.ToList();
        }
    }
}
