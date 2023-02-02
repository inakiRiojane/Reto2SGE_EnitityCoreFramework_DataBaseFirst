using AutoMapper;
using Reto2eSge_3__.Core.Entities;
using Reto2eSge_3__.Core.Models;
using Reto2eSgeG3.Core.Entitis;

namespace MyERPVv2.Profiles
{
    public class AutoMappedProfiles : Profile
    {
        public AutoMappedProfiles()
        {
            CreateMap<Category, CategoryGetModel>();
            CreateMap<Category, CategoryModel>().ReverseMap();

            //CreateMap<Category, CategoryViewModel>()
            //    .ForMember(m => m.CategoryName, e => e.MapFrom(prop => prop.Description));

        }
    }
}
