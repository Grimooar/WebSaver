using AutoMapper;
using DTOs;
using WebApplication1.DTOs;
using WebApplication1.Models;

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
