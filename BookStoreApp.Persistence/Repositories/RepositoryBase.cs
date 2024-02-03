using BookStoreApp.Core.Repositories;
using BookStoreApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreApp.Persistence.Repositories
{
    // bu yapıdan kalıtılanlar context'e erişebilecekler.
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public IQueryable<T> GetAll(bool trackChanges) => !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public IQueryable<T> GetByFilter(bool trackChanges, Expression<Func<T, bool>> predicate) => !trackChanges ?  _context.Set<T>().Where(predicate).AsNoTracking() : _context.Set<T>().Where(predicate);

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
