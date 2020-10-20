using Microsoft.AspNet.Identity;
using System;

namespace BolaoTI.UI.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid();
        }

        public IdentityUser(string userName, string email, string telefone)
            : this()
        {
            this.UserName = userName;
            this.Email = email;
            this.Telefone = telefone;            
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
                
    }
}