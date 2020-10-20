using BolaoTI.Resources;
using BolaoTI.Dominio.Interfaces.Repositorios;
﻿using BolaoTI.Aplicacao.Interfaces.Servicos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dominio = BolaoTI.Dominio;

namespace BolaoTI.UI.Identity
{
    public class UserStore : IUserLoginStore<IdentityUser, Guid>, IUserClaimStore<IdentityUser, Guid>, IUserRoleStore<IdentityUser, Guid>, IUserPasswordStore<IdentityUser, Guid>, IUserSecurityStampStore<IdentityUser, Guid>, IUserStore<IdentityUser, Guid>, IDisposable
    {        
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IUsuarioServicoAplicacao _usuarioServicoAplicacao;

        public UserStore(IUsuarioRepositorio usuarioRepositorio, IPerfilRepositorio perfilRepositorio, IUsuarioServicoAplicacao usuarioServicoAplicacao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioServicoAplicacao = usuarioServicoAplicacao;

            _perfilRepositorio = perfilRepositorio;
        }

        #region IUserStore<IdentityUser, Guid> Members
        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            var u = getUser(user);
            
            return _usuarioServicoAplicacao.CadastrarUsuarioAsync(u);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            var u = getUser(user);

            return _usuarioServicoAplicacao.RemoverAsync(u);
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            var user = _usuarioRepositorio.Get(userId);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public Task<IdentityUser> FindByNameAsync(string email)
        {
            var user = _usuarioRepositorio.FindByEmail(email);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentException(Classes.Usuario_Class);

            var u = _usuarioRepositorio.Get(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            populateUser(u, user);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IUserClaimStore<IdentityUser, Guid> Members
        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);
            if (claim == null)
                throw new ArgumentNullException(Classes.Claim_Class);

            var u = _usuarioRepositorio.Get(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            var c = new Dominio.Claim
            {
                Type = claim.Type,
                Value = claim.Value,
                Usuario = u
            };
            u.Claims.Add(c);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            return Task.FromResult<IList<Claim>>(u.Claims.Select(x => new Claim(x.Type, x.Value)).ToList());
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);
            if (claim == null)
                throw new ArgumentNullException(Classes.Claim_Class);

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            var c = u.Claims.FirstOrDefault(x => x.Type == claim.Type && x.Value == claim.Value);
            u.Claims.Remove(c);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }
        #endregion

        #region IUserLoginStore<IdentityUser, Guid> Members
        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            if (login == null)
                throw new ArgumentNullException(Classes.ExternalLogin_Class);

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            var l = new Dominio.ExternalLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                Usuario = u
            };
            u.ExternalLogins.Add(l);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException(Classes.ExternalLogin_Class);

            var identityUser = default(IdentityUser);

            //var l = _unitOfWork.ExternalLoginRepository.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);

            //if (l != null)
            //    identityUser = getIdentityUser(l.User);

            return Task.FromResult<IdentityUser>(identityUser);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            return Task.FromResult<IList<UserLoginInfo>>(u.ExternalLogins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            if (login == null)
                throw new ArgumentNullException(Classes.ExternalLogin_Class);

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            var l = u.ExternalLogins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            u.ExternalLogins.Remove(l);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }
        #endregion

        #region IUserRoleStore<IdentityUser, Guid> Members

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Parametro_Vazio, Field.Perfil_Nome_Field));

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            var r = _perfilRepositorio.FindByName(roleName);
            if (r == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            u.Perfis.Add(r);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            return Task.FromResult<IList<string>>(u.Perfis.Select(x => x.Nome).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Parametro_Vazio, Field.Perfil_Nome_Field));

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            return Task.FromResult<bool>(u.Perfis.Any(x => x.Nome == roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Parametro_Vazio, Field.Perfil_Nome_Field));

            var u = _usuarioRepositorio.FindById(user.Id);
            if (u == null)
                throw new ArgumentException(string.Format(Messages.ErrorMessage_Classe_Nao_Corresponde_Entidade, Classes.IdentityUser_Class, Classes.Usuario_Class), Classes.Usuario_Class);

            var r = u.Perfis.FirstOrDefault(x => x.Nome == roleName);
            u.Perfis.Remove(r);

            return _usuarioServicoAplicacao.AtualizarAsync(u);
        }

        #endregion

        #region IUserPasswordStore<IdentityUser, Guid> Members
        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore<IdentityUser, Guid> Members
        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException(Classes.Usuario_Class);
            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
        private Dominio.Usuario getUser(IdentityUser identityUser)
        {
            if (identityUser == null)
                return null;

            var user = new Dominio.Usuario();
            populateUser(user, identityUser);

            return user;
        }

        private void populateUser(Dominio.Usuario user, IdentityUser identityUser)
        {
            user.Id = identityUser.Id;
            user.Email = identityUser.Email;
            user.Telefone = identityUser.Telefone;
            user.Nome = identityUser.UserName;
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
        }

        private IdentityUser getIdentityUser(Dominio.Usuario user)
        {
            if (user == null)
                return null;

            var identityUser = new IdentityUser();
            populateIdentityUser(identityUser, user);

            return identityUser;
        }

        private void populateIdentityUser(IdentityUser identityUser, Dominio.Usuario user)
        {
            identityUser.Id = user.Id;
            identityUser.UserName = user.Nome;            
            identityUser.Email = user.Email;
            identityUser.Telefone = user.Telefone;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SecurityStamp = user.SecurityStamp;
        }
        #endregion
    }
}