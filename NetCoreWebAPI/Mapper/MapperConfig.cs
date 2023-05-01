using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetCoreWebAPI.DTOs;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ProductsDTO, Products>().ReverseMap();
        }
    }
}
