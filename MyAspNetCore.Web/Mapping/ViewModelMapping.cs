using AutoMapper;
using MyAspNetCore.Web.Models;
using MyAspNetCore.Web.ViewModel;

namespace MyAspNetCore.Web.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();

            CreateMap<Visitor, VIsitorViewModel>().ReverseMap();

            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();
        }
    }
}
