using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class CampeonatoConfig : EntityTypeConfiguration<Campeonato>
    {
        public CampeonatoConfig()
        {
            ToTable(Tables.Campeonato_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Campeonato_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion

            #region Relacionamentos

            HasMany(x => x.Fases)
                .WithRequired(x => x.Campeonato)
                .HasForeignKey(x => x.CampeonatoId);
            
            HasMany(x => x.Rankings)
                .WithRequired(x => x.Campeonato)
                .HasForeignKey(x => x.CampeonatoId);

            HasMany(x => x.Organizacoes)
               .WithMany(x => x.Campeonatos)
               .Map(x =>
               {
                   x.ToTable(Tables.CampeonatoOrganizacao_Table);
                   x.MapLeftKey(Tables.CampeonatoOrganizacao_OrganizacaoId_FK_Column);
                   x.MapRightKey(Tables.CampeonatoOrganizacao_CampeonatoId_FK_Column);
               });
           
            #endregion

            #region Colunas

            Property(x => x.Nome)
                .HasColumnName(Tables.Campeoanto_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.NomeAbreviado)
                .HasColumnName(Tables.Campeonato_NomeAbreviado_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.Inicio)
                .HasColumnName(Tables.Campeonato_Inicio_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_DateTime)
                .IsRequired();

            Property(x => x.Fim)
                .HasColumnName(Tables.Campeonato_Fim_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_DateTime)
                .IsRequired();

            #endregion            
        }
    }
}
