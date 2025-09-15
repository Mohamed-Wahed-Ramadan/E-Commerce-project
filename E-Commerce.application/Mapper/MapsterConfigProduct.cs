using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ProductDtos;
using E_Commerce_project.models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Mapper
{
    public class MapsterConfigProduct
    {
        public static void RegisterMapsterConfiguration()
        {
            TypeAdapterConfig<Product, ProductReadDto>.NewConfig();
                //Map(des=>des.Id, src=>src.Id);
            TypeAdapterConfig<ProductCreateDto,Product>.NewConfig();
            TypeAdapterConfig<ProductUpdateDto, Product>.NewConfig();
            TypeAdapterConfig<ProductReadDto, Product>.NewConfig();




        }

    }
}
