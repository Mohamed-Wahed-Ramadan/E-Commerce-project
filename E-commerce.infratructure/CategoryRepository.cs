using E_Commerce.application.Repository;
using E_Commerce.context;
using E_Commerce_project.models;

namespace E_commerce.infratructure
{
    public class CategoryRepository : ICategoryRepository
    {
        E_commerceContext _context;//= new();
        public CategoryRepository(E_commerceContext context)
        {
            _context = context;
        }
        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }
        public void Create(Category cat)
        {
            _context.Categories.Add(cat);
        }
        public void Update(Category cat)
        {
            _context.Categories.Update(cat);
        }


        public void Delete(Category cat)
        {
            _context.Categories.Remove(cat);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
