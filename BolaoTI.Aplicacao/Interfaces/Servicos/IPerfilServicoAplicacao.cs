
using BolaoTI.Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IPerfilServicoAplicacao
    {
        void CadastrarPerfil(Perfil perfil);

        Perfil RecuperarPorId(Guid id);

        Perfil RecuperarPorNome(string nome);
        
        void Remover(Perfil Perfil);

        void Remover(Guid id);


        void Atualizar(Perfil Perfil);

        #region Métodos Async
        
        Task<int> CadastrarPerfilAsync(Perfil perfil);

        Task<int> AtualizarAsync(Perfil Perfil);
        
        Task<int> RemoverAsync(Perfil r);

        #endregion

        
    }
}
