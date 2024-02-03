namespace BookStoreApp.Core.ResponseResultPattern.Exceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id) : base($"The book with id : {id} could not found")
        {
            
        }
    }
}
