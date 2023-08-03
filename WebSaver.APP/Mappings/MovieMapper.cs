using AutoMapper;
using Domain;
using DTOs;

namespace WebApplication1.Mappings;

public class MovieMapper : Profile
{
    public MovieMapper()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<Movie, MoviesCreateDto>().ReverseMap();
        CreateMap<Movie, MoviesUpdateDto>().ReverseMap();
    }
}
