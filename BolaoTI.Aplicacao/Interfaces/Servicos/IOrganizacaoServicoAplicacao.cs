
using BolaoTI.Dominio;
using System;
using System.Collections.Generic;
namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IOrganizacaoServicoAplicacao
    {
        void CadastrarOrganizacao(Organizacao organizacao);

        Organizacao RecuperarPorId(int id);

        IList<Organizacao> RecuperarPorFiltro(string nome, List<int> campeonatosId, List<Guid> usuariosId);

        IList<Organizacao> RecuperarTodosOrganizacoes();

        void Remover(Organizacao organizacao);

        void Remover(int id);

        void Atualizar(Organizacao organizacao);
    }
}
