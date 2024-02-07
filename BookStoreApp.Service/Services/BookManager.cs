using AutoMapper;
using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Core.ResponseResultPattern.Exceptions;
using BookStoreApp.Core.Services;
using System.Net;

namespace BookStoreApp.Service.Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForInsertion)
        {
            Book? newEntity = _mapper.Map<Book>(bookDtoForInsertion);
            _repositoryManager.BookRepository.CreateOneBook(newEntity);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<BookDto>(newEntity);
        }

        public async Task DeleteOneBookAsync(int bookId, bool trackChanges)
        {
            var deletedEntity = await GetOneBookByIdAndCheckExitsAsync(bookId, true);

            _repositoryManager.BookRepository.DeleteOneBook(deletedEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _repositoryManager.BookRepository.GetAllBooksAsync(trackChanges));
        }

        public async Task<BookDto> GetOneBookByIdAsync(int bookId, bool trackChanges)
        {
            var currentEntity = await GetOneBookByIdAndCheckExitsAsync(bookId, true);


            return _mapper.Map<BookDto>(currentEntity);
        } 

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            Book? currentBook =  await GetOneBookByIdAndCheckExitsAsync(id, true);

            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(currentBook);

            return (bookDtoForUpdate, currentBook);


        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate,book);
            await _repositoryManager.SaveAsync();
        }

        public async Task<BookDtoForUpdate> UpdateOneBookAsync(int bookId, BookDtoForUpdate bookDtoForUpdate, bool trackChanges)
        {
            await GetOneBookByIdAndCheckExitsAsync(bookId, true);

            if (bookId != bookDtoForUpdate.Id)
                throw new BookNotMatchedException(bookId);

            _repositoryManager.BookRepository.UpdateOneBook(_mapper.Map<Book>(bookDtoForUpdate));
            await _repositoryManager.SaveAsync();
            return bookDtoForUpdate;
        }

        private async Task<Book> GetOneBookByIdAndCheckExitsAsync (int id, bool trackChanges)
        {
            var entity =await  _repositoryManager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
            if(entity == null)
                throw new BookNotFoundException(id);

            return entity;
        }
    }
}
