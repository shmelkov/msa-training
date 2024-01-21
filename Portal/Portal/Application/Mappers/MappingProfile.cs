using AutoMapper;
using Portal.Application.CQRS.News.DTOs;

namespace Portal.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.News, NewsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        }
    }
}
