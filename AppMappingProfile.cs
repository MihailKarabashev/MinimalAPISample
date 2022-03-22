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

        }
    }
}
