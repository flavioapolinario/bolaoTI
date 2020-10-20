using BolaoTI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BolaoTI.Utils
{
    public static class FieldUtil
    {
        /// <summary>
        /// Obtem a data atual de acordo com o fuso informado.
        /// </summary>
        /// <returns>
        /// Data Atual com o fuso horario informado
        /// </returns>
        public static DateTime DataAtualFusoHorario(string IdTimeZone = "E. South America Standard Time")
        {
            // Mesmo estando o servidor configurado para qualquer fuso horário, o código abaixo obtém o horário de Brasília
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById(IdTimeZone);
            DateTime dateTimeBrasilia = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            return dateTimeBrasilia;
        }

        /// <summary>
        /// Obtem o Nome do Display do valor do Enum
        /// </summary>
        /// <param name="value">valor do Enum</param>
        /// <returns>DisplayName</returns>
        public static string GetEnumDescricao(Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(String.Format(Messages.ErrorMessage_FieldUtil_TipoObjeto_DiferenteEnum, type));

            var members = type.GetMember(value.ToString());
            if (members.Length == 0) throw new ArgumentException(String.Format(Messages.ErrorMessage_FieldUtil_MembroNaoEncontrado_NoTipoInformado, value, type.Name));

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0) throw new ArgumentException(String.Format(Messages.ErrorMessage_FieldUtil_ObjetoNaoPossuiDisplayAttribute, type.Name, value));

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }

        /// <summary>
        /// Obtem o 'DisplayName' da propriedade da classe informada
        /// </summary>
        /// <typeparam name="T">Classe</typeparam>
        /// <param name="propertyExpression">Propriedade</param>
        /// <returns>DisplayName</returns>        
        public static string GetPropriedadeDisplayValue<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
                throw new ArgumentException(Messages.ErrorMessage_Field_Propriedade_Invalida_Objeto, "propertyExpression");

            var attributes = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0)
                throw new ArgumentException(String.Format(Messages.ErrorMessage_FieldUtil_ObjetoNaoPossuiDisplayAttribute, propertyExpression.Parameters[0].Type.Name, memberInfo.Name));

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }       

        /// <summary>
        /// Obtem o 'MemberInfo' da expressao informada
        /// </summary>
        /// <param name="propertyExpression">Expressão</param>
        /// <returns>MemberInfo</returns>
        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        /// <summary>
        /// Obtem a String corresponde ao Resource Informado
        /// </summary>
        /// <param name="resourceKey">String Resource Chave</param>
        /// <param name="resourceType">Resource</param>
        /// <returns>String correspondente a chave do Resource informado</returns>
        public static string GetResource(string resourceKey, Type resourceType)
        {
            PropertyInfo _propertyInfo;
            _propertyInfo = resourceType.GetProperty(resourceKey, BindingFlags.Static | BindingFlags.Public);
            if (_propertyInfo == null) return string.Empty;

            return (string)_propertyInfo.GetValue(_propertyInfo.DeclaringType, null);
        }
    }
}
