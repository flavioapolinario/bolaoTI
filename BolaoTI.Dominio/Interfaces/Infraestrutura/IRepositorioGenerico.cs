using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Infraestrutura
{
    public interface IRepositorioGenerico<TEntidade> where TEntidade : class
    {
        TEntidade Get(object id);

        IList<TEntidade> FindAll();

        void Insert(TEntidade entidade);

        void Update(TEntidade entidade);

        void Delete(TEntidade entidade);

        void Delete(object id);
    }
}
