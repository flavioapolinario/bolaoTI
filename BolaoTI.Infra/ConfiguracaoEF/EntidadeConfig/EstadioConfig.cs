using BolaoTI.Resources;
using BolaoTI.Dominio;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class EstadioConfig : EntityTypeConfiguration<Estadio>
    {
        public EstadioConfig()
        {
            ToTable(Tables.Estadio_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Estadio_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos
            
            HasMany(x => x.Partidas)
                .WithRequired(x => x.Estadio)
                .HasForeignKey(x => x.EstadioId);

            #endregion

            #region Colunas

            Property(x => x.Nome)
                .HasMaxLength(200)
                .HasColumnName(Tables.Estadio_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            Property(x => x.Cidade)
                .HasMaxLength(200)
                .HasColumnName(Tables.Estadio_Cidade_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar);

            Property(x => x.Uf)
                .HasMaxLength(2)
                .HasColumnName(Tables.Estadio_UF_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar);

            Property(x => x.Capacidade)
                .HasColumnName(Tables.Estadio_Capacidade_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsOptional();

            #endregion            
        }
    }
}
