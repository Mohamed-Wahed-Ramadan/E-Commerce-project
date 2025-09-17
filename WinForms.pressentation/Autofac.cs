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
            builder.RegisterType<UserService>().As<IUserServices>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            #endregion

            #region AutofacCategory
            builder.RegisterType<CategoryService>().As<ICategoryServices>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            #endregion

            #region AutofacProduct
            builder.RegisterType<ProductService>().As<IProductServices>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            #endregion

            #region AtofacCart
            builder.RegisterType<CartService>().As<ICartServices>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();
            #endregion

            #region AtofacOrder
            builder.RegisterType<OrderService>().As<IOrderServices>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            #endregion


            #region AtofacOrderProduct
            builder.RegisterType<ProductsOrderSarvices>().As<IProductsOrderServices>();
            builder.RegisterType<ProdsOrderRepository>().As<IProdsOrderRepository>();
            #endregion

            //#region AtofacOrderCart
            //builder.RegisterType<CartProductServices>().As<ICartProServices>();
            //builder.RegisterType<CartProRepository>().As<ICartProRepository>();
            //#endregion
            return builder.Build();

        }
    }
}
