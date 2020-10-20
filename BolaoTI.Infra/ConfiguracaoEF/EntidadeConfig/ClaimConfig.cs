
using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class ClaimConfig : EntityTypeConfiguration<Claim>
    {
        public ClaimConfig()
        {
            ToTable(Tables.Claim_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName(Tables.Claim_ClaimId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            #endregion

            #region Relacionamentos

            HasRequired(x => x.Usuario)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId);

            #endregion

            #region Colunas
            
            Property(x => x.UserId)
                .HasColumnName(Tables.Claim_UsuarioId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_UniqueIdentifier)
                .IsRequired();

            Property(x => x.Type)
                .HasColumnName(Tables.Claim_ClaimType_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsMaxLength()
                .IsOptional();

            Property(x => x.Value)
                .HasColumnName(Tables.Claim_ClaimValue_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsMaxLength()
                .IsOptional();

            #endregion


        }

    }
}
