using BookStoreApp.Core.Entities;

namespace BookStoreApp.Core.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int bookId, bool trackChanges);
        Book CreateOneBook(Book book);
        void DeleteOneBook(int bookId,bool trackChanges);
        Book UpdateOneBook(int bookId, Book book,bool trackChanges);

    }
}
