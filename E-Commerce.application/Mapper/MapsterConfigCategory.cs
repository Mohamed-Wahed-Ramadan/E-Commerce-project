using E_Commerce.DTOs.Category;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce_project.models;
using Mapster;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Mapper
{
    public static class MapsterConfigCategory
    {
        public static void RegisterMapsterConfiguration()
        {
            TypeAdapterConfig<Category, CategoryReadDto>.NewConfig();
            //Map(des => des.Id, src => src.Id).
            //Map(des => des.Name, src => src.Name);
            TypeAdapterConfig<CategoryCreateDto, Category>.NewConfig();
            //Map(des=> des.Name, src => src.Name) ;
            TypeAdapterConfig<CategoryUpdateDto, Category>.NewConfig();
            //Map(des => des.Id, src => src.Id).
            //Map(des => des.Name, src => src.Name);
            TypeAdapterConfig<CategoryReadDto, Category>.NewConfig();





        }
    }
}
