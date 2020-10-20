using BolaoTI.Dominio;
using BolaoTI.Resources;
using System.Data.Entity.ModelConfiguration;

namespace BolaoTI.Infra.ConfiguracaoEF.EntidadeConfig
{
    public class PerfilConfig : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfig()
        {
            ToTable(Tables.Perfil_Table);

            #region Chave Primaria

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName(Tables.Perfil_Id_Field)
                .HasColumnType(Tables.Tipo_Coluna_UniqueIdentifier)
                .IsRequired();

            #endregion

            #region Relacionamentos

            HasMany(x => x.Usuarios)
             .WithMany(x => x.Perfis)
             .Map(x =>
             {
                 x.ToTable(Tables.UsuarioPerfil_Table);
                 x.MapLeftKey(Tables.UsuarioPerfil_PerfilId_FK_Column);
                 x.MapRightKey(Tables.UsuarioPerfil_UsuarioId_FK_Column);
             });

            #endregion

            #region Colunas

            Property(x => x.Nome)
                .HasColumnName(Tables.Perfil_Nome_Field)
                .HasColumnType(Tables.Tipo_Coluna_NVarchar)
                .HasMaxLength(256)
                .IsRequired();

            #endregion
           
        }
    }
}
