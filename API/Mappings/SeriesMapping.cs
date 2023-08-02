using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mappings;

public class SeriesMapping : Profile
{
    public SeriesMapping()
    {
        CreateMap<Series, SeriesDto>().ReverseMap();
        CreateMap<Series, SeriesCreateDto>().ReverseMap();
        CreateMap<Series, SeriesUpdateDto>().ReverseMap();
    }
}
