using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BolaoTI.UI.Identity
{
    public class RoleStore : IRoleStore<IdentityRole, Guid>, IQueryableRoleStore<IdentityRole, Guid>, IDisposable
    {
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IPerfilServicoAplicacao _perfilServicoAplicacao;

        public RoleStore(IPerfilRepositorio perfilRepositorio, IPerfilServicoAplicacao perfilServicoAplicacao)
        {
            _perfilServicoAplicacao = perfilServicoAplicacao;
            _perfilRepositorio = perfilRepositorio;
        }

        #region IRoleStore<IdentityRole, Guid> Members

        public System.Threading.Tasks.Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException(Classes.Perfil_Class);

            var r = getRole(role);           
            return _perfilServicoAplicacao.CadastrarPerfilAsync(r);
        }

        public System.Threading.Tasks.Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException(Classes.Perfil_Class);

            var r = getRole(role);

            return _perfilServicoAplicacao.RemoverAsync(r);
        }

        public System.Threading.Tasks.Task<IdentityRole> FindByIdAsync(Guid roleId)
        {
            var role = _perfilRepositorio.Get(roleId);
            return Task.FromResult<IdentityRole>(getIdentityRole(role));
        }

        public System.Threading.Tasks.Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var role = _perfilRepositorio.FindByName(roleName);
            return Task.FromResult<IdentityRole>(getIdentityRole(role));
        }

        public System.Threading.Tasks.Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException(Classes.Perfil_Class);

            var r = getRole(role);            
            return _perfilServicoAplicacao.AtualizarAsync(r);
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IQueryableRoleStore<IdentityRole, Guid> Members
        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _perfilRepositorio
                    .FindAll()
                    .Select(x => getIdentityRole(x))
                    .AsQueryable();
            }
        }
        #endregion

        #region Private Methods

        private Perfil getRole(IdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            return new Perfil
            {
                Id = identityRole.Id,
                Nome = identityRole.Name
            };
        }

        private IdentityRole getIdentityRole(Perfil role)
        {
            if (role == null)
                return null;
            return new IdentityRole
            {
                Id = role.Id,
                Name = role.Nome
            };
        }
        #endregion
    }
}