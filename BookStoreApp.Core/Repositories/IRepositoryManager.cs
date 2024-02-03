namespace BookStoreApp.Core.Repositories
{
    public interface IRepositoryManager
    {
        IBookRepository BookRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
