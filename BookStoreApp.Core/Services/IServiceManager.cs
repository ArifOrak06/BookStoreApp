namespace BookStoreApp.Core.Services
{
    public interface IServiceManager
    {
        IBookService BookService { get; }
    }
}
