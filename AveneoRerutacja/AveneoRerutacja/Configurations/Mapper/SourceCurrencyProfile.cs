using AutoMapper;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Models;

namespace AveneoRerutacja.Configurations.Mapper
{
    public class SourceCurrencyProfile : Profile
    {
        public SourceCurrencyProfile()
        {
            CreateMap<Currency, CreateSourceCurrencyDto>().ReverseMap();
            CreateMap<Currency, UpdateSourceCurrencyDto>().ReverseMap();
            CreateMap<Currency, SourceCurrencyDto>().ReverseMap();
        }
    }
}