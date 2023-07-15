using Application.Dto;
using AutoMapper;
using Domain;

namespace Application.MapperProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
    }
}

