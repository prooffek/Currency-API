using AutoMapper;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Models;

namespace AveneoRerutacja.Configurations.Mapper
{
    public class DateClassProfile : Profile
    {
        public DateClassProfile()
        {
            CreateMap<DateClass, CreateDateClassDto>().ReverseMap();
            CreateMap<DateClass, UpdateDateClassDto>().ReverseMap();
            CreateMap<DateClass, DateClassDto>().ReverseMap();
        }
    }
}