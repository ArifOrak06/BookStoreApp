using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Entities;

namespace BookStoreApp.Core.Services
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        BookDto GetOneBookById(int bookId, bool trackChanges);
        BookDtoForInsertion CreateOneBook(BookDtoForInsertion bookDtoForInsertion);
        void DeleteOneBook(int bookId,bool trackChanges);
        BookDtoForUpdate UpdateOneBook(int bookId, BookDtoForUpdate bookDtoForUpdate,bool trackChanges);

    }
}
