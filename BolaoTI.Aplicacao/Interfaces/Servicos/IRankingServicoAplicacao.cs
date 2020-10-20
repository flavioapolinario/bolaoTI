using BolaoTI.Dominio;
using System;
using System.Collections.Generic;
namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IRankingServicoAplicacao
    {
        void RealizaRanking(Campeonato campeonato);

        void AtualizaColocaoRanking(Campeonato campeonato);

        IList<Ranking> RecuperarPorFiltro(int organizacaoId, int? campeonatoId, int? faseId, Guid? usuarioId);        
    }
}
