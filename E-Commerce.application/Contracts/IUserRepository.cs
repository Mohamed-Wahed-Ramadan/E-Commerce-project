using E_Commerce.application.Contracts;
using E_Commerce_project.models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Repository
{
    public interface IUserRepository : IGenericRepository<User,int>
    {

    }
}
