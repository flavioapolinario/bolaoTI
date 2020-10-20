
using BolaoTI.Dominio;
using System;
using System.Collections.Generic;
namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IFaseServicoAplicacao
    {
        void Fechar(Fase fase);

        void CadastrarFase(Fase fase);

        Fase RecuperarPorId(int id);

        IList<Fase> RecuperarPorFiltro(string nome, int? idCampeonato, DateTime? dataReferencia);

        IList<Fase> RecuperarTodosOsFases();

        void Remover(Fase fase);

        void Remover(int id);

        void Atualizar(Fase fase);
    }
}
