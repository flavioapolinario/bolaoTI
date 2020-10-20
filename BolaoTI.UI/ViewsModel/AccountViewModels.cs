using System.ComponentModel.DataAnnotations;

namespace BolaoTI.UI.ViewsModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [DataType(DataType.Password)]        
        [Display(Name = "Usuario_OldPassword_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [StringLength(100, ErrorMessageResourceName = "ErrorMessage_Account_LocalPasswordModel_LengthPassword", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Usuario_NewPassword_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Usuario_ConfimNewPassword_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [Compare("NewPassword", ErrorMessageResourceName = "ErrorMessage_Account_LocalPasswordModel_DiferenceBetweenPasswordAndConfirmPassword", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {        
        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Usuario_Email_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Comum_EmailInvalidoField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages), ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]        
        [DataType(DataType.Password)]
        [Display(Name = "Usuario_Password_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Usuario_Nome_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(256, ErrorMessageResourceName = "ErrorMessage_Time_TamanhoNome", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Usuario_Email_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Comum_EmailInvalidoField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages), ErrorMessage = null)]
        public string Email { get; set; }

        [Display(Name = "Usuario_Telefone_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string Telefone { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [StringLength(100, ErrorMessageResourceName = "ErrorMessage_Account_LocalPasswordModel_LengthPassword", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Usuario_Password_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Usuario_ConfimPassword_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [Compare("Password", ErrorMessageResourceName = "ErrorMessage_Account_LocalPasswordModel_DiferenceBetweenPasswordAndConfirmPassword", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        public string ConfirmPassword { get; set; }
        
    }
}
