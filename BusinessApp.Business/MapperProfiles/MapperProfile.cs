using AutoMapper;
using BusinessApp.Business.ViewModels;
using BusinessApp.Core.Entities;

namespace BusinessApp.Business.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<CreateBlogVm, Blog>();
            CreateMap<UpdateBlogVm, Blog>();
            CreateMap<UpdateBlogVm, Blog>().ReverseMap();
        }
    }
}
