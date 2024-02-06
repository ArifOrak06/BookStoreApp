using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Core.DTOs.Concretes.ValidationsForBookDTOs
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage ="Title zorunlu bir alandır.")]
        [MinLength(2,ErrorMessage ="Title en az 2 karakterden oluşturulmalıdır.")]
        [MaxLength(50, ErrorMessage = "Title en az 2 karakterden oluşturulmalıdır.")]
        public String Title { get; init; }
        [Required(ErrorMessage ="Price alanı zorunlu bir alandır.")]
        [Range(10,1000)]
        public decimal Price { get; init; }
    }
}
