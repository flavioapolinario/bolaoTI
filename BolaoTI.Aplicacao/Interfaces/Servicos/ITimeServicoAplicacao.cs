using BolaoTI.Dominio;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface ITimeServicoAplicacao
    {
        void CadastrarTime(Time time);

        Time RecuperarPorId(int id);

        IList<Time> RecuperarPorFiltro(string nome, string nomeAbreviado);

        IList<Time> RecuperarTodosOsTimes();

        void Remover(Time time);

        void Remover(int id);

        void Atualizar(Time time);
    }
}
