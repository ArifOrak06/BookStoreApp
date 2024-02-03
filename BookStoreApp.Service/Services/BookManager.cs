using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Core.Services;

namespace BookStoreApp.Service.Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Book CreateOneBook(Book book)
        {
             if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
             _repositoryManager.BookRepository.CreateOneBook(book);
            _repositoryManager.Save();
            return book;    
        }

        public void DeleteOneBook(int bookId, bool trackChanges)
        {
            var deletedEntity = _repositoryManager.BookRepository.GetOneBookById(bookId,trackChanges);
            if (deletedEntity == null)
                throw new Exception($"Book with id : {bookId} could not found.");
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
                throw new Exception($"Book with id : {bookId} could not found.");

            return currentEntity;
        }

        public Book UpdateOneBook(int bookId,Book book,bool trackChanges)
        {
            var currentEntity = _repositoryManager.BookRepository.GetOneBookById(bookId, trackChanges);
            if(currentEntity == null)
                throw new Exception($"Book with id : {bookId} could not found.");
            if(book is null)
                throw new ArgumentNullException(nameof(book));
            if (bookId != book.Id)
                throw new Exception($"Parametre olarak gönderilen varlığa ait id bilgisi ile rotadan gönderilen id'ler eşleşmemektedir.");

            _repositoryManager.BookRepository.UpdateOneBook(book);
            //currentEntity.Title = book.Title;
            //currentEntity.Price = book.Price;
            _repositoryManager.Save();
            return book;
        }
    }
}
