using BolaoTI.Dominio;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IRodadaServicoAplicacao
    {
        void CadastrarRodada(Rodada rodada);

        Rodada RecuperarPorId(int id);

        IList<Rodada> RecuperarPorFiltro(string nome, int? idFase, int? idGrupo);

        IList<Rodada> RecuperarTodosOsRodadas();

        void Remover(Rodada rodada);

        void Remover(int id);

        void Atualizar(Rodada rodada);
    }
}
