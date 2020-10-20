using System;
using System.ComponentModel.DataAnnotations;

namespace BolaoTI.Utils.Localizacao
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class LocalizadoValidationAttribute : RegularExpressionAttribute
    {
        public LocalizadoValidationAttribute(string pattern, Type ResourceType)
            : base(pattern)
        {
            pattern = FieldUtil.GetResource(pattern, ResourceType);
        }
    }
}
