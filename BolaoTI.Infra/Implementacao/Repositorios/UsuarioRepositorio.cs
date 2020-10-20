using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Resources;
using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {

        }

        public bool UsuarioExistente(string nome, string email)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase) ||
                                   p.Email.Equals(email));
        }

        public bool UsuarioExistente(string email)
        {
            return _dbSet.Any(p => p.Email.Equals(email));
        }

        public Usuario FindByEmail(string email)
        {
            return _dbSet.Include(Field.Usuario_Perfils_Field)
                        .Include(Field.Usuario_Organizacoes_Field)
                        .Include(Field.Usuario_Claims_Field)
                        .Include(Field.Usuario_ExternalLogins_Field)
                        .Where(p => p.Email.Equals(email)).FirstOrDefault();
        }

        public Usuario FindById(Guid id)
        {
            return _dbSet.Include(Field.Usuario_Perfils_Field)
                        .Include(Field.Usuario_Organizacoes_Field)
                        .Include(Field.Usuario_Claims_Field)
                        .Where(p => p.Id.Equals(id)).FirstOrDefault();
        }

        public IList<Usuario> FindByFilter(string nome, string email, Guid[] perfilId)
        {
            var query = (from u in _dbSet.Include(Field.Usuario_Perfils_Field)
                                         .Include(Field.Usuario_Organizacoes_Field)
                         where
                            (string.IsNullOrEmpty(nome) || u.Nome.Equals(nome)) &&
                            (string.IsNullOrEmpty(email) || u.Email.Equals(email)) &&
                            ((perfilId.Count() == 0) || u.Perfis.Any(p => perfilId.Contains(p.Id)))
                         select u);

            return query.ToList();
        }

        public Task<Usuario> FindByEmailAsync(string email)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public Task<Usuario> FindByEmailAsync(System.Threading.CancellationToken cancellationToken, string email)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email), cancellationToken);
        }
    }
}