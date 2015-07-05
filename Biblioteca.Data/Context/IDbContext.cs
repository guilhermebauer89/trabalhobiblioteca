using System.Data.Entity;

namespace Biblioteca.Data.Context
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }

}