using Autofac;
using E_commerce.infratructure;
using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce.application.Services;
using E_Commerce.Context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.pressentation
{
    public class Autofac
    {
        public static IContainer Inject()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AppDbContext>().As<AppDbContext>();

            builder.RegisterGeneric(typeof(GenericRepository<,>))
                   .As(typeof(IGenaricRepository<,>));

            #region UserServices
            //builder.RegisterType<IUserServices>().As<UserService>();
            //builder.RegisterType<IUserRepository>().As<UserRepository>();

            #endregion

            #region AutofacCategory
            builder.RegisterType<ICategoryServices>().As<CategoryService>();
            builder.RegisterType<ICategoryRepository>().As<CategoryRepository>();
            #endregion

            #region AutofacProduct
            builder.RegisterType<IProductServices>().As<ProductService>();
            builder.RegisterType<IProductRepository>().As<ProductRepository>();
            #endregion

            return builder.Build();

        }
    }
}
