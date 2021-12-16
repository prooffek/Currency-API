using AutoMapper;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Models;

namespace AveneoRerutacja.Configurations.Mapper
{
    public class DailyRateProfile : Profile
    {
        public DailyRateProfile()
        {
            CreateMap<DailyRate, CreateDailyRateDto>().ReverseMap();
            CreateMap<DailyRate, UpdateDailyRateDto>().ReverseMap();
            CreateMap<DailyRate, DailyRateDto>().ReverseMap();
        }
    }
}