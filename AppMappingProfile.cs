using AutoMapper;
using MinimalAPISample.Dtos;
using MinimalAPISample.Models;

namespace MinimalAPISample
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<News, NewsResponseModel>().ReverseMap();
            CreateMap<News, NewsRequestModel>().ReverseMap();

            CreateMap<Tag, TagResponseModel>()
                .ForMember(x => x.NewsTitle, y => y.MapFrom(m => m.News.Title))
                .ForMember(x => x.NewsDescription, y => y.MapFrom(x => x.News.Description));

        }
    }
}
