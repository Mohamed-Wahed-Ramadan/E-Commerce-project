using E_Commerce.application.Repository;
using E_Commerce.Context;
using E_Commerce_project.models;

namespace E_commerce.infratructure
{
    public class CategoryRepository :GenericRepository<Category,int>, ICategoryRepository
    {
        AppDbContext _context;//= new();
        public CategoryRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
        #region CategoryRepo
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
        #endregion

    }
}
