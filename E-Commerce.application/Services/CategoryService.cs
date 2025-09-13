using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce.DTOs.Category;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.application.Services
{
    public class CategoryService : ICategoryServices
    {
        ICategoryRepository _context;
        public CategoryService(ICategoryRepository context)
        {
            _context = context;
        }
        public List<CategotyDto> GetAllCategory()
        {
            IQueryable<Category> AllCategoty =  _context.GetAll();
            var AllCats= AllCategoty.Where(a=> a.Products.Count > 0).Select(a=> 
            new CategotyDto
            {
                CatId = a.Id,
                CatName = a.Name,
                CatDescription = a.Description,
            }
            ).ToList();
            return AllCats;
        }
        public void AddCategory(Category category) 
        {
            _context.Create(category);

        }
        public void UpdateCategory(Category category) 
        {
            _context.Update(category);
        }
        public void DeleteCategory(Category category) 
        {
            _context.Delete(category);
        }
        public int saveCategory() 
        {
            return _context.Save();
        }

    }
}
