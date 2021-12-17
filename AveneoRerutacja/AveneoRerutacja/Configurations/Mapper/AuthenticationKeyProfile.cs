using AutoMapper;
using AveneoRerutacja.KeyGenerator;
using AveneoRerutacja.Models;

namespace AveneoRerutacja.Configurations.Mapper
{
    public class AuthenticationKeyProfile : Profile
    {
        public AuthenticationKeyProfile()
        {
            CreateMap<AuthenticationKey, CreateAuthenticationKeyDto>().ReverseMap();
            CreateMap<AuthenticationKey, UpdateAuthenticationKeyDto>().ReverseMap();
            CreateMap<AuthenticationKey, AuthenticationKeyDto>().ReverseMap();
        }
    }
}