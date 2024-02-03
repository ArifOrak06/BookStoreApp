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

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
        }

        public Book CreateOneBook(Book book)
        {
            _repositoryManager.BookRepository.CreateOneBook(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int bookId, bool trackChanges)
        {
            var deletedEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (deletedEntity == null)
                throw new BookNotFoundException(bookId);
            
            _repositoryManager.BookRepository.DeleteOneBook(deletedEntity);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _repositoryManager.BookRepository.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int bookId, bool trackChanges)
        {
            var currentEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (currentEntity == null)
                // loglama mekanizması global Exception Handler yapısı içerisinde çalıştırıldığı için burada tekrardan mekanızma çalıştırmaya gerek yok.!
                throw new BookNotFoundException(bookId);
            

            return currentEntity;
        }

        public Book UpdateOneBook(int bookId, Book book, bool trackChanges)
        {
            var currentEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (currentEntity == null)
                throw new BookNotFoundException(bookId);

            if (bookId != book.Id)
                throw new BookNotMatchedException(bookId);
            

            _repositoryManager.BookRepository.UpdateOneBook(book);
            //currentEntity.Title = book.Title;
            //currentEntity.Price = book.Price;
            _repositoryManager.Save();
            return book;
        }
    }
}
