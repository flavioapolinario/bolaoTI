using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable(Tables.Usuario_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName(Tables.Usuario_UsuarioId_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_UniqueIdentifier)
                .IsRequired();

            #endregion

            #region Relacionamentos

            HasMany(x => x.Perfis)
                .WithMany(x => x.Usuarios)
                .Map(x =>
                {
                    x.ToTable(Tables.UsuarioPerfil_Table);
                    x.MapLeftKey(Tables.UsuarioPerfil_UsuarioId_FK_Column);
                    x.MapRightKey(Tables.UsuarioPerfil_PerfilId_FK_Column);
                });

            HasMany(x => x.Claims)
                .WithRequired(x => x.Usuario)
                .HasForeignKey(x => x.UserId);

            HasMany(x => x.ExternalLogins)
                .WithRequired(x => x.Usuario)
                .HasForeignKey(x => x.UserId);
            
            HasMany(x => x.Rankings)
                .WithRequired(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);

            HasMany(x => x.Apostas)
                .WithRequired(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);

            HasMany(x => x.Organizacoes)
                .WithMany(x => x.Usuarios)
                .Map(x =>
                {
                    x.ToTable(Tables.UsuarioOrganizacao_Table);
                    x.MapLeftKey(Tables.UsuarioOrganizacao_UsuarioId_FK_Column);
                    x.MapRightKey(Tables.UsuarioOrganizacao_OrganizacaoId_FK_Column);
                });

            #endregion

            #region Colunas
            
            Property(x => x.PasswordHash)
                .HasColumnName(Tables.Usuario_PasswordHash_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsMaxLength()
                .IsOptional();

            Property(x => x.SecurityStamp)
                .HasColumnName(Tables.Usuario_SecurityStamp_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .IsMaxLength()
                .IsOptional();

            Property(x => x.Nome)
                .HasColumnName(Tables.Usuario_Nome_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.Email)
                .HasColumnName(Tables.Usuario_Email_ColumnName)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.Telefone)
               .HasColumnName(Tables.Usuario_Telefone_ColumnName)
               .HasColumnType(Tables.Tipo_Coluna_NVarchar)
               .HasMaxLength(256);

            #endregion
        }
    }
}
