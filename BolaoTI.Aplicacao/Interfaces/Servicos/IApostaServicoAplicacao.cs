using BolaoTI.Dominio;
using System;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IApostaServicoAplicacao
    {
        List<Aposta> RecuperarApostasPorFiltro(int IdOrganizacao, int IdCampeonato, int IdFase, Guid? IdUsuario);

        void RealizarAposta(IList<Aposta> apostas);

        void CalculaPontos(Partida partida);
    }
}
