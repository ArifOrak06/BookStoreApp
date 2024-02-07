using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Entities.RequestFeatures;

namespace BookStoreApp.Core.Services
{
    public interface IBookService
    {
        Task<(IEnumerable<BookDto> bookDtos, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
        Task<BookDto> GetOneBookByIdAsync(int bookId, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForInsertion);
        Task DeleteOneBookAsync(int bookId,bool trackChanges);
        Task<BookDtoForUpdate> UpdateOneBookAsync(int bookId, BookDtoForUpdate bookDtoForUpdate,bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);// birden fazla nesnenin method dönüşü için Tuplle kullandık.
        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);

    }
}
