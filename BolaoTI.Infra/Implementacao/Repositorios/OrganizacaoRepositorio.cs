using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using System;
using System.Linq;
using System.Collections.Generic;
using BolaoTI.Resources;
using System.Text;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class OrganizacaoRepositorio : RepositorioGenerico<Organizacao>, IOrganizacaoRepositorio
    {
        public OrganizacaoRepositorio(BolaoTIContext _contexto)
            : base(_contexto)
        {
        }


        public bool OrganizacaoExistente(string nome)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool CampeonatoAssociado(ICollection<Campeonato> campeonatos, out StringBuilder mensagem)
        {
            mensagem = new StringBuilder();

            foreach (var campeonato in campeonatos.ToList())
            {
                if (_dbSet.Include(Classes.Campeonato_Class).Any(o => o.Campeonatos.Any(u => u.Id == campeonato.Id)))
                {
                    if (mensagem == null) mensagem = new StringBuilder();
                    mensagem.AppendLine(string.Format(Messages.AlertMessage_Organizacao_Registro_Associado, Field.Organizacao_Campeonato_Field, campeonato.Nome));
                }
            };

            return !mensagem.ToString().Equals(string.Empty);
        }

        public bool UsuarioAssociado(ICollection<Usuario> usuarios, out StringBuilder mensagem)
        {
            mensagem = new StringBuilder();

            foreach (var usuario in usuarios.ToList())
            {
                if (_dbSet.Include(Classes.Usuario_Class).Any(o => o.Usuarios.Any(u => u.Id == usuario.Id)))
                {
                    if (mensagem == null) mensagem = new StringBuilder();
                    mensagem.AppendLine(string.Format(Messages.AlertMessage_Organizacao_Registro_Associado, Field.Organizacao_Usuario_Field, usuario.Email));
                }
            };

            return !mensagem.ToString().Equals(string.Empty);
        }

        public IList<Organizacao> FindByFilter(string nome, IList<int> campeonatoIds, IList<Guid> usuariosIds)
        {
            var query = (from c in _dbSet.Include(Field.Organizacao_Campeonato_Field).Include(Field.Organizacao_Usuario_Field) select c);

            if (!String.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));

            if (campeonatoIds != null)
                query = query.Where(p => p.Campeonatos.Any(c => campeonatoIds.Contains(c.Id)));

            if (usuariosIds != null)
                query = query.Where(p => p.Usuarios.Any(c => usuariosIds.Contains(c.Id)));

            return query.ToList();
        }

        public IList<Organizacao> FindByCampeonato(int idCampeonato)
        {
            var query = (from o in _dbSet.Include(Field.Organizacao_Campeonato_Field)
                                         .Include(Field.Organizacao_Usuario_Field)
                         where o.Campeonatos.Any(c => c.Id == idCampeonato)
                         select o);
           
            return query.ToList();
        }
    }
}
