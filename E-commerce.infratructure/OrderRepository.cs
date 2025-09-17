using E_Commerce.application.Repository;
using E_Commerce.Context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            
            _context = context;
        }

        

        
    }
}
