

using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class ExternalLoginConfig : EntityTypeConfiguration<ExternalLogin>
    {
        public ExternalLoginConfig()
        {
            ToTable(Tables.ExternalLogin);

            #region Chvae Primaria

            HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            #endregion

            #region Relacionamentos

            HasRequired(x => x.Usuario)
                .WithMany(x => x.ExternalLogins)
                .HasForeignKey(x => x.UserId);

            #endregion

            #region Colunas
                        
            Property(x => x.LoginProvider)
                .HasColumnName(Tables.ExternalLogin_LoginProvider_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.ProviderKey)
                .HasColumnName(Tables.ExternalLogin_ProviderKey_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(128)
                .IsRequired();

            Property(x => x.UserId)
                .HasColumnName(Tables.ExternalLogin_UserId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_UniqueIdentifier)
                .IsRequired();

            #endregion
            
        }
    }
}
