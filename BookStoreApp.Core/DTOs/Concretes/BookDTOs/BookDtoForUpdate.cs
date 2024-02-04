namespace BookStoreApp.Core.DTOs.Concretes.BookDTOs
{
    public record BookDtoForUpdate
    {
        public int Id { get; init; }
        public String  Title  { get; init; }
        public decimal Price { get; init; }
    }
}
