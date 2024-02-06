using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Persistence.Repositories
{
    // Abstract olarak tanımlanan baseRepository'deki temel Crudları customRepository yapımızda kullandık.
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);
        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) => await GetAll(trackChanges).OrderBy(x => x.Id).ToListAsync();

        public async Task<Book> GetOneBookByIdAsync(int bookId, bool trackChanges) => await GetByFilter(trackChanges, x => x.Id == bookId).SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
