using System.Linq;

namespace Biblioteca.Data.Repository
{
    public interface IRepository<T>
    {
        T GetById(object id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(int? id);
        IQueryable<T> Table { get; }
    }
}