using BolaoTI.Dominio;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IEstadioServicoAplicacao
    {
        void CadastrarEstadio(Estadio Estadio);

        Estadio RecuperarPorId(int id);

        IList<Estadio> RecuperarPorFiltro(string nome, string cidade, string uf);

        IList<Estadio> RecuperarTodosOsEstadios();

        void Remover(Estadio estadio);

        void Remover(int id);

        void Atualizar(Estadio estadio);
    }
}
