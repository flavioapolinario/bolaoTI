using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace BolaoTI.web.Models
{
    //public class UsersContext : DbContext
    //{
    //    public UsersContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public DbSet<UserProfile> UserProfiles { get; set; }
    //}

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Display(Name = "Participante")]
        public string UserName { get; set; }
        [Display(Name = "Nome para exibição")]
        public string ExhibitionName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        [StringLength(100, ErrorMessage = "A {0} deve ter, pelo menos, {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a nova senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Alterar o nome para exibição")]
        public string ExhibitionName { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Esqueceu sua senha?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Nome para exibição")]
        public string ExhibitionName { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        [StringLength(100, ErrorMessage = "A {0} deve ter, pelo menos, {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não corresponderem.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Nome para exibição")]
        public string ExhibitionName { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
