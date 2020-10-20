using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        bool UsuarioExistente(string email);

        Usuario FindById(Guid id);

        Usuario FindByEmail(string email);

        Task<Usuario> FindByEmailAsync(string email);
        Task<Usuario> FindByEmailAsync(CancellationToken cancellationToken, string email);
  
        IList<Usuario> FindByFilter(string nome, string email, Guid[] perfilId);
    }
}
