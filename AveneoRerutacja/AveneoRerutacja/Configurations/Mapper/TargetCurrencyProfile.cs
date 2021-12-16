using AutoMapper;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Models;

namespace AveneoRerutacja.Configurations.Mapper
{
    public class CurrencyProfile : Profile
    
    {
        public CurrencyProfile()
        {
            CreateMap<Currency, CreateTargetCurrencyDto>().ReverseMap();
            CreateMap<Currency, UpdateTargetCurrencyDto>().ReverseMap();
            CreateMap<Currency, TargetCurrencyDto>().ReverseMap();
        }
    }
}