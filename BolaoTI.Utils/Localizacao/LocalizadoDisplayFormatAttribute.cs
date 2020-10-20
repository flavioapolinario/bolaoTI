using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BolaoTI.Utils.Localizacao
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class LocalizadoDisplayFormatAttribute : DisplayFormatAttribute
    {
        public LocalizadoDisplayFormatAttribute(Type resourceType)
            : base()
        {
            this._resourceType = resourceType;
        }

        private Type _resourceType;      

        public new string NullDisplayText
        {
            get
            {
                return base.NullDisplayText;
            }
            set
            {
                base.NullDisplayText = Utils.FieldUtil.GetResource(value, _resourceType);
            }
        }

        public new string DataFormatString
        {
            get
            {
                return base.DataFormatString;
            }
            set
            {
                base.DataFormatString = Utils.FieldUtil.GetResource(value, _resourceType);
            }
        }
    }
}
