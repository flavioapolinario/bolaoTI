using BolaoTI.Resources;
using BolaoTI.Dominio;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class TimeConfig : EntityTypeConfiguration<Time>
    {
        public TimeConfig()
        {
            ToTable(Tables.Time_Table);
            
            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Time_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos
            
            HasMany(x => x.PartidasHome)
                .WithRequired(x => x.TimeHome)
                .HasForeignKey(x => x.TimeHomeId);

            HasMany(x => x.PartidasAway)
                .WithRequired(x => x.TimeAway)
                .HasForeignKey(x => x.TimeAwayId);

            #endregion

            #region Colunas

            Property(x => x.Nome)
                .HasMaxLength(200)
                .HasColumnName(Tables.Time_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            Property(x => x.NomeAbreviado)
                .HasMaxLength(200)
                .HasColumnName(Tables.Time_NomeAbreviado_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            Property(x => x.ImagemBandeira)
                .HasMaxLength(300)
                .HasColumnName(Tables.Time_ImagemBandeira_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            #endregion
        }
    }
}