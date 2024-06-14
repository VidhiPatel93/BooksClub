using AutoMapper;
using BookClub.API.Models.Domain;
using BookClub.API.Models.DTO;

namespace BookClub.API.Mappings
{
    public class BooksProfile : Profile 
    {
        public BooksProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<AddBookRequestDTO,Book>().ReverseMap();
            CreateMap<UpdateBookRequestDTO, Book>().ReverseMap();
        }
    }
}
