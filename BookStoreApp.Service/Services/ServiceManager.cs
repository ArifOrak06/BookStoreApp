using BookStoreApp.Core.Repositories;
using BookStoreApp.Core.Services;


namespace BookStoreApp.Service.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        
        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService loggerService)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager,loggerService));
        }

        public IBookService BookService => _bookService.Value;
    }
}
