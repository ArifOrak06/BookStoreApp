namespace BookStoreApp.Core.DTOs.Concretes.BookDTOs
{
    public record BookDtoForInsertion
    {
        public String Title { get; init; }
        public decimal Price { get; init; }
    }
}
