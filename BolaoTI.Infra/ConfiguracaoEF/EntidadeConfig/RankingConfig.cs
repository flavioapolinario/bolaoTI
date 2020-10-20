using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class RankingConfig : EntityTypeConfiguration<Ranking>
    {
        public RankingConfig()
        {
            ToTable(Tables.Ranking_Table);

            #region Chave Primaria

            //HasKey(x => x.Id)
            //   .Property(x => x.Id)
            //   .HasColumnName(Tables.Ranking_Id_ColumnName)
            //   .HasColumnType(Tables.Tipo_Coluna_Int)
            //   .IsRequired();            

            HasKey(r => new { r.OrganizacaoId, r.CampeonatoId, r.UsuarioId});

            #endregion

            #region Relacionamentos

            HasRequired(x => x.Organizacao)
                .WithMany(x => x.Rankings)
                .HasForeignKey(x => x.OrganizacaoId);

            HasRequired(x => x.Campeonato)
                .WithMany(x => x.Rankings)
                .HasForeignKey(x => x.CampeonatoId);

            HasRequired(x => x.Usuario)
                .WithMany(x => x.Rankings)
                .HasForeignKey(x => x.UsuarioId);                
            
            #endregion

            #region Colunas

            Property(x => x.UsuarioId)
                .HasColumnName(Tables.Ranking_UsuarioId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_UniqueIdentifier)
                .IsRequired();

            Property(x => x.CampeonatoId)
                .HasColumnName(Tables.Ranking_CampeonatoId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.OrganizacaoId)
                .HasColumnName(Tables.Ranking_OrganizacaoId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.NumeroApostas)
                .HasColumnName(Tables.Ranking_NumeroApostas_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.NumeroPartidas)
                .HasColumnName(Tables.Ranking_NumeroPartidas_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.TotalPontos)
                .HasColumnName(Tables.Ranking_TotalPontos_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.DezTotalPontos)
                .HasColumnName(Tables.Ranking_DezTotalPontos_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.SeteTotalPontos)
                .HasColumnName(Tables.Ranking_SeteTotalPontos_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.CincoTotalPontos)
                .HasColumnName(Tables.Ranking_CincoTotalPontos_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.DoisTotalPontos)
                .HasColumnName(Tables.Ranking_DoisTotalPontos_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.Colocacao)
                .HasColumnName(Tables.Ranking_Colocacao_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            #endregion
        }
    }
}
