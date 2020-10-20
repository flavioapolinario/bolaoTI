using BolaoTI.Dominio;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IGrupoServicoAplicacao
    {
        void CadastrarGrupo(Grupo grupo);

        Grupo RecuperarPorId(int id);

        IList<Grupo> RecuperarPorFiltro(string nome, int? idFase);

        IList<Grupo> RecuperarTodosOsGrupos();

        void Remover(Grupo grupo);

        void Remover(int id);

        void Atualizar(Grupo grupo);
    }
}
