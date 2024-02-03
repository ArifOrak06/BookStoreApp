namespace BookStoreApp.Core.ResponseResultPattern.Exceptions
{
    public abstract class NotMatchedException : Exception
    {
        protected NotMatchedException(string message) : base(message) { }
    }
}
