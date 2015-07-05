using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Service
{
    public interface ICrudService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(object id);        
        T Insert(T entity);
        T Update(T entity);
        void Delete(int? id);
    }
}
