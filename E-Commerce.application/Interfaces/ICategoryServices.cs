using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.DTOs.Category;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce_project.models;


namespace E_Commerce.application.Interfaces
{
    public interface ICategoryServices
    {
        List<CategoryReadDto>  GetAllCategory();

        void AddCategory(CategoryCreateDto category);

        void UpdateCategory(CategoryUpdateDto category);


        void DeleteCategory(Category category);
        int SaveCategory();

    }
}
