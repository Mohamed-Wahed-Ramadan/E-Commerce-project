using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Repository
{
    public interface ICategoryRepository
    {
        public IQueryable<Category> GetAll();
        public void Create(Category cat);
        public void Update(Category cat);
        public void Delete(Category cat);
        public int Save();

    }
}
