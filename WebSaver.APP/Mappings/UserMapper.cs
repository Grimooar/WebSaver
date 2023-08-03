using AutoMapper;
using Domain;
using DTOs;

namespace WebApplication1.Mappings;

public class UserMapper : Profile
{
    public UserMapper() {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}