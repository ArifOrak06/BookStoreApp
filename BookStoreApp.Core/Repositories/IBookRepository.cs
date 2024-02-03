using BookStoreApp.Core.Entities;

namespace BookStoreApp.Core.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        IQueryable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int bookId, bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
      

    }
}
