using BookStoreApp.Core.DTOs.Concretes.ValidationsForBookDTOs;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Core.DTOs.Concretes.BookDTOs
{
    public record BookDtoForUpdate : BookDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
     
    }
}
