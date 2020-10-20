using BolaoTI.Resources;
using BolaoTI.Dominio;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class PartidaConfig : EntityTypeConfiguration<Partida>
    {
        public PartidaConfig()
        {
            ToTable(Tables.Partida_Table);            

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Partida_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos
            
            HasRequired(x => x.TimeAway)
                .WithMany(x => x.PartidasAway)
                .HasForeignKey(x => x.TimeAwayId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.TimeHome)
                .WithMany(x => x.PartidasHome)
                .HasForeignKey(x => x.TimeHomeId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Estadio)
                .WithMany(x => x.Partidas)
                .HasForeignKey(x => x.EstadioId);

            HasRequired(x => x.Rodada)
                .WithMany(x => x.Partidas)
                .HasForeignKey(x => x.RodadaId);

            HasMany(x => x.Apostas)
                .WithRequired(x => x.PartidaApostada)
                .HasForeignKey(x => x.PartidaId);
           
            #endregion

            #region Colunas

            Property(x => x.EstadioId)
                .HasColumnName(Tables.Partida_EstadioId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.TimeHomeId)
                .HasColumnName(Tables.Partida_TimeHomeId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.TimeAwayId)
                .HasColumnName(Tables.Partida_TimeAwayId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.RodadaId)
                .HasColumnName(Tables.Partida_RodadaId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int)
                .IsRequired();

            Property(x => x.DataPartida)
                .HasColumnName(Tables.Partida_DataPartida_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_DateTime)
                .IsRequired();

            Property(x => x.GolsTimeHome)
                .HasColumnName(Tables.Partida_GolsTimeHome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int);

            Property(x => x.GolsTimeAway)
                .HasColumnName(Tables.Partida_GolsTimeAway_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_Int);

            #endregion            
        }
    }
}