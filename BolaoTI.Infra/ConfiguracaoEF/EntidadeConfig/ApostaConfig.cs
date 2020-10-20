
using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class ApostaConfig : EntityTypeConfiguration<Aposta>
    {
        public ApostaConfig()
        {
            ToTable(Tables.Aposta_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Aposta_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos

            HasRequired(x => x.Usuario)
                .WithMany(x => x.Apostas)
                .HasForeignKey(x => x.UsuarioId);

            HasRequired(x => x.PartidaApostada)
                .WithMany(x => x.Apostas)
                .HasForeignKey(x => x.PartidaId);

            #endregion

            #region Colunas

            Property(x => x.UsuarioId)
                .HasColumnName(Tables.Aposta_UsuarioId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_UniqueIdentifier)
                .IsRequired();

            Property(x => x.PartidaId)
                .HasColumnName(Tables.Aposta_PartidaId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.GolsTimeHome)
                .HasColumnName(Tables.Aposta_GolsTimeHome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.GolsTimeAway)
                .HasColumnName(Tables.Aposta_GolsTimeAway_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.GolsTimeAway)
                .HasColumnName(Tables.Aposta_GolsTimeAway_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.PontosAposta)
                .HasColumnName(Tables.Aposta_PontosAposta_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsOptional();


            #endregion
        }
    }
}
