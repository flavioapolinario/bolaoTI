using BolaoTI.Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IUsuarioServicoAplicacao
    {
        void CadastrarUsuario(Usuario Usuario);

        Usuario RecuperarPorId(Guid id);

        Usuario RecuperarPorEmail(string email);

        IList<Usuario> RecuperarPorFiltro(string nome, string email, Guid[] perfilId);
        
        void Remover(Usuario Usuario);

        void Remover(Guid id);

        void Atualizar(Usuario Usuario);

        #region Metodos Async
        
        Task<int> CadastrarUsuarioAsync(Usuario Usuario);
        Task<int> AtualizarAsync(Usuario Usuario);
        Task<int> RemoverAsync(Usuario Usuario);

        #endregion
    }
}