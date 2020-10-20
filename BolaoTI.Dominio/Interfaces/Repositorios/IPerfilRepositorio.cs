using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Threading;
using System.Threading.Tasks;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IPerfilRepositorio : IRepositorioGenerico<Perfil>
    {
        Perfil FindByName(string nome);
        Task<Perfil> FindByNameAsync(string nome);
        Task<Perfil> FindByNameAsync(CancellationToken cancellationToken, string nome);
    }
}
