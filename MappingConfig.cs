using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductsChona.Models;
using ProductsChona.Models.Dto;

namespace ProductsChona
// hereda del llamado Profile (proviene del paquete automapper)
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            // es lo mismo que de arriba pero en una sola linea
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();


        }
    }
}