
using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class GrupoConfig : EntityTypeConfiguration<Grupo>
    {
        public GrupoConfig()
        {
            ToTable(Tables.Grupo_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Grupo_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos

            HasRequired(x => x.Fase)
                .WithMany(x => x.Grupos)
                .HasForeignKey(x => x.FaseId);

            HasMany(x => x.Rodadas)
                .WithRequired(x => x.Grupo)
                .HasForeignKey(x => x.GrupoId);

            #endregion

            #region Colunas

            Property(x => x.FaseId)
                .HasColumnName(Tables.Grupo_FaseId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.Nome)
                .HasMaxLength(100)
                .HasColumnName(Tables.Grupo_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            #endregion          
        }
    }
}
