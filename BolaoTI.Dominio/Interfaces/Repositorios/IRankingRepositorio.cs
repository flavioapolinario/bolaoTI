using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IRankingRepositorio : IRepositorioGenerico<Ranking>
    {
        Ranking FindRanking(int organizacaoId, int campeonatoId, System.Guid usuarioId);

        List<Ranking> FindRankingByFilter(int organizacaoId, int? campeonatoId, int? faseId, System.Guid? usuarioId);

        List<Ranking> FindRankingByFase(int organizacaoId, int campeonatoId, int? faseId);

        List<Ranking> FindRankingByUsuario(int organizacaoId, Guid? usuarioId);

        List<Ranking> FindRankingGeral(int organizacaoId, int campeonatoId);
        
    }
}
