using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace ParserWebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Repositories.Models.Product, ViewModels.ProductDetailsViewModel>();
            CreateMap<Repositories.Models.Product, ViewModels.ProductViewModel>();
        }
    }
}