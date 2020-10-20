using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using BolaoTI.Dominio.Interfaces.Infraestrutura;
using BolaoTI.Infra.ConfiguracaoEF;

namespace BolaoTI.Infra.Implementacao.InfraEstrutura
{
    public class RepositorioGenerico<TEntidade> : IRepositorioGenerico<TEntidade> where TEntidade : class
    {
        internal readonly BolaoTIContext _contexto;
        internal DbSet<TEntidade> _dbSet;

        public RepositorioGenerico(BolaoTIContext contexto)
        {
            this._contexto = contexto;
            this._dbSet = contexto.Set<TEntidade>();
        }

        public virtual IEnumerable<TEntidade> Get(
           System.Linq.Expressions.Expression<Func<TEntidade, bool>> filter = null,
           Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntidade> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntidade Get(object id)
        {
            return _dbSet.Find(id);
        }

        public IList<TEntidade> FindAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(TEntidade entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntidade entity)
        {               
            _dbSet.Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntidade entity)
        {
            if (_contexto.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            TEntidade entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        internal object Get(object filter, string includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
