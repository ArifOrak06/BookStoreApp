using BookStoreApp.Core.Repositories;
using BookStoreApp.Persistence.Contexts;

namespace BookStoreApp.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly Lazy<IBookRepository> _bookRepository;
        public RepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_appDbContext));
        }

        public IBookRepository BookRepository => _bookRepository.Value;

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
