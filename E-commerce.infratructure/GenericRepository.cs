using E_Commerce.application.Contracts;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class GenericRepository<T,TID> : IGenericRepository<T, TID> where T : BaseModel<TID>
    {

    }
}
