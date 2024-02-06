using BookStoreApp.Core.Entities;

namespace BookStoreApp.Core.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
        Task<Book> GetOneBookByIdAsync(int bookId, bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
      

    }
}
