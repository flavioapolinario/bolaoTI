
using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Text;
namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IOrganizacaoRepositorio : IRepositorioGenerico<Organizacao>
    {
        bool OrganizacaoExistente(string nome);

        bool CampeonatoAssociado(ICollection<Campeonato> campeonatos, out StringBuilder mensagem);

        bool UsuarioAssociado(ICollection<Usuario> usuarios, out StringBuilder mensagem);

        IList<Organizacao> FindByFilter(string nome, IList<int> campeonatoIds, IList<Guid> usuariosIds);

        IList<Organizacao> FindByCampeonato(int idCampeonato);
    }
}
