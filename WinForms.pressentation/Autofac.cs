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

            #region AutofacCategory
            builder.RegisterType<ICategoryServices>().As<CategoryService>();
            builder.RegisterType<ICategoryRepository>().As<CategoryRepository>();
            builder.RegisterType<IGenaricRepository<Category, int>>().As<GenericRepository<Category, int>>();
            #endregion

            #region AutofacProduct
            builder.RegisterType<IProductServices>().As<ProductService>();
            builder.RegisterType<IProductRepository>().As<ProductRepository>();
            builder.RegisterType<IGenaricRepository<Product, int>>().As<GenericRepository<Product, int>>();
            #endregion

            return builder.Build();

        }
    }
}
