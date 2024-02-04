using AutoMapper;
using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Core.ResponseResultPattern.Exceptions;
using BookStoreApp.Core.Services;

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

        public BookDtoForInsertion CreateOneBook(BookDtoForInsertion bookDtoForInsertion)
        {
            _repositoryManager.BookRepository.CreateOneBook(_mapper.Map<Book>(bookDtoForInsertion));
            _repositoryManager.Save();
            return bookDtoForInsertion;
        }

        public void DeleteOneBook(int bookId, bool trackChanges)
        {
            var deletedEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (deletedEntity == null)
                throw new BookNotFoundException(bookId);
            
            _repositoryManager.BookRepository.DeleteOneBook(deletedEntity);
            _repositoryManager.Save();
        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            return _mapper.Map<IEnumerable<BookDto>>(_repositoryManager.BookRepository.GetAllBooks(trackChanges));
        }

        public BookDto GetOneBookById(int bookId, bool trackChanges)
        {
            var currentEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (currentEntity == null)
                // loglama mekanizması global Exception Handler yapısı içerisinde çalıştırıldığı için burada tekrardan mekanızma çalıştırmaya gerek yok.!
                throw new BookNotFoundException(bookId);
            

            return _mapper.Map<BookDto>(currentEntity);
        }

        public BookDtoForUpdate UpdateOneBook(int bookId, BookDtoForUpdate bookDtoForUpdate, bool trackChanges)
        {
            var currentEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (currentEntity == null)
                throw new BookNotFoundException(bookId);

            if (bookId != bookDtoForUpdate.Id)
                throw new BookNotMatchedException(bookId);

            _repositoryManager.BookRepository.UpdateOneBook(_mapper.Map<Book>(bookDtoForUpdate));
            _repositoryManager.Save();
            return bookDtoForUpdate;
        }
    }
}
