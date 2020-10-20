using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class PerfilRepositorio : RepositorioGenerico<Perfil>, IPerfilRepositorio
    {
        public PerfilRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public Perfil FindByName(string nome)
        {
            return _dbSet.FirstOrDefault(x => x.Nome.Equals(nome));
        }

        public Task<Perfil> FindByNameAsync(string nome)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Nome.Equals(nome));
        }

        public Task<Perfil> FindByNameAsync(CancellationToken cancellationToken, string nome)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Nome.Equals(nome), cancellationToken);
        }
    }
}
