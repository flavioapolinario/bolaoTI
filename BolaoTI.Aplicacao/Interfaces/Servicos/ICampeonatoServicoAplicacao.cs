using BolaoTI.Dominio;
using System;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface ICampeonatoServicoAplicacao
    {
        void CadastrarCampeonato(Campeonato Campeonato);

        Campeonato RecuperarPorId(int id);

        IList<Campeonato> RecuperarPorFiltro(string nome, DateTime? dataInicio, DateTime? dataFim);

        IList<Campeonato> RecuperarTodosOsCampeonatos();

        void Remover(Campeonato campeonato);

        void Remover(int id);

        IList<Campeonato> RecuperarPorOrganizacao(int idOrganizacao);
    }
}
