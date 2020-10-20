
using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class OrganizacaoConfig : EntityTypeConfiguration<Organizacao>
    {
        public OrganizacaoConfig() {

            ToTable(Tables.Organizacao_Table);
            
            #region Chave Primaria

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName(Tables.Organizacao_Id_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_Int)
               .IsRequired();

            #endregion
        
            #region Relacionamentos
            
             HasMany(x => x.Usuarios)
                .WithMany(x => x.Organizacoes)
                .Map(x =>
                {
                    x.ToTable(Tables.UsuarioOrganizacao_Table);
                    x.MapLeftKey(Tables.UsuarioOrganizacao_UsuarioId_FK_Column);
                    x.MapRightKey(Tables.UsuarioOrganizacao_OrganizacaoId_FK_Column);
                });

            HasMany(x => x.Campeonatos)
                .WithMany(x => x.Organizacoes)
                .Map(x =>
                {
                    x.ToTable(Tables.CampeonatoOrganizacao_Table);
                    x.MapLeftKey(Tables.CampeonatoOrganizacao_OrganizacaoId_FK_Column);
                    x.MapRightKey(Tables.CampeonatoOrganizacao_CampeonatoId_FK_Column);
                });

            HasMany(x => x.Rankings)
                .WithRequired(x => x.Organizacao)
                .HasForeignKey(x => x.OrganizacaoId);

            #endregion

            #region Colunas

            Property(x => x.Nome)
                .HasMaxLength(100)
                .HasColumnName(Tables.Organizacao_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsRequired();

            #endregion  
        }

    }
}
