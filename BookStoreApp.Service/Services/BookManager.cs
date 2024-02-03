using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
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
            {
                _loggerService.LogInfo($"The book with id : {bookId} could not found.");
                throw new Exception($"Book with id : {bookId} could not found.");
            }
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
            {
                string message = $"Book with id : {bookId} could not found.";
                _loggerService.LogInfo(message);
                throw new Exception(message);
            }

            return currentEntity;
        }

        public Book UpdateOneBook(int bookId, Book book, bool trackChanges)
        {
            var currentEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if (currentEntity == null)
            {
                string logMessage = $"The book with id : {bookId} could not found.";
                _loggerService.LogInfo(logMessage);
                throw new Exception(logMessage);

            }

            if (bookId != book.Id)
            {
                string message = $"Parametre olarak gönderilen varlığa ait id bilgisi ile rotadan gönderilen id'ler eşleşmemektedir.";
                _loggerService.LogInfo(message);
                throw new Exception(message);
            }

            _repositoryManager.BookRepository.UpdateOneBook(book);
            //currentEntity.Title = book.Title;
            //currentEntity.Price = book.Price;
            _repositoryManager.Save();
            return book;
        }
    }
}
