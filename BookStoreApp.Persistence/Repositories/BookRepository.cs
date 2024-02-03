using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Persistence.Contexts;

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
        public IQueryable<Book> GetAllBooks(bool trackChanges) => GetAll(trackChanges).OrderBy(x => x.Id);

        public Book GetOneBookById(int bookId, bool trackChanges) => GetByFilter(trackChanges, x => x.Id == bookId).SingleOrDefault();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
