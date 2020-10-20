using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class RodadaConfig : EntityTypeConfiguration<Rodada>
    {
        public RodadaConfig()
        {
            ToTable(Tables.Rodada_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Rodada_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos

            HasMany(x => x.Partidas)
                .WithRequired(x => x.Rodada)
                .HasForeignKey(x => x.RodadaId);

            HasRequired(x => x.Grupo)
                .WithMany(x => x.Rodadas)
                .HasForeignKey(x => x.GrupoId);

            #endregion

            #region Colunas

            Property(x => x.GrupoId)
               .HasColumnName(Tables.Rodada_GrupoId_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            Property(x => x.Nome)
                .HasMaxLength(200)
                .HasColumnName(Tables.Rodada_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            Property(x => x.Ordem)                
                .HasColumnName(Tables.Rodada_Ordem_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired(); 

            #endregion
        }
    }
}