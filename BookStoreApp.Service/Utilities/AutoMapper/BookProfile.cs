using AutoMapper;
using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Entities;

namespace BookStoreApp.Service.Utilities.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, BookDtoForInsertion>().ReverseMap();
            CreateMap<Book, BookDtoForUpdate>().ReverseMap();
        }
    }
}
