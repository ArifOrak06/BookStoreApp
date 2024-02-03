using System.Linq.Expressions;

namespace BookStoreApp.Core.Repositories
{
    public interface IRepositoryBase<T>
    {
        // Filtresiz, Sonradan filter uygulanabilir.
        IQueryable<T> GetAll(bool trackChanges);
        // Filter zorunlu
        IQueryable<T> GetByFilter(bool trackChanges, Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        
    }
}
