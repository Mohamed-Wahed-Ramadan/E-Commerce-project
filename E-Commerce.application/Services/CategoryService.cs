using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce.DTOs.Category;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce_project.models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.application.Services
{
    public class CategoryService : ICategoryServices
    {
        //ICategoryRepository _context;
        IGenaricRepository<Category,int> _context;
        public CategoryService(IGenaricRepository<Category,int> context)
        {
            _context = context;
        }
        public List<CategoryReadDto> GetAllCategory()
        {
            IQueryable<Category> AllCategoty = _context.GetAll();
            var AllCats= AllCategoty.Where(a=> a.Products.Count > 0).ToList().Adapt<List<CategoryReadDto>>();
            
            return AllCats;
        }
        public void AddCategory(CategoryReadDto Creatcategory) 
        {
            //Category category = new Category()
            //{
            //    Name = Creatcategory.Name
            //};
            Category category = Creatcategory.Adapt<Category>();

            _context.Add(category);

        }
        public void UpdateCategory(CategoryUpdateDto Updatecategory) 
        {
            //Category category = new Category()
            //{
            //    Id=Updatecategory.Id,
            //    Name = Updatecategory.Name,
            //};
            Category category = Updatecategory.Adapt<Category>();
            _context.Update(category);
        }
        public void DeleteCategory(Category category) 
        {
            _context.Delete(category);
        }
        public int SaveCategory() 
        {
            return _context.CompleteAsync();
        }

        
    }
}
