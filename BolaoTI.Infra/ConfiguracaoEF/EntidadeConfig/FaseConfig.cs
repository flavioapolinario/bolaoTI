using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class FaseConfig : EntityTypeConfiguration<Fase>
    {
        public FaseConfig()
        {
            ToTable(Tables.Fase_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Fase_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos

            HasRequired(x => x.Campeonato)
                .WithMany(x => x.Fases)
                .HasForeignKey(x => x.CampeonatoId);

            HasMany(x => x.Grupos)
                .WithRequired(x => x.Fase)
                .HasForeignKey(x => x.FaseId);

            #endregion

            #region Colunas

            Property(x => x.CampeonatoId)
                .HasColumnName(Tables.Fase_CampeonatoId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.Nome)
                .HasMaxLength(200)
                .HasColumnName(Tables.Fase_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            Property(x => x.DataInicio)
                .HasColumnName(Tables.Fase_DataInicio_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_DateTime)
                .IsRequired();

            Property(x => x.DataFim)
                .HasColumnName(Tables.Fase_DataFim_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_DateTime)
                .IsRequired();
            
            #endregion
        }
    }
}