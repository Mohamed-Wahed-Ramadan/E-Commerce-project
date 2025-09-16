using Autofac;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Mapper;
using E_Commerce.application.Services;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ProductDtos;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.pressentation
{
    public partial class HomeForm : Form
    {

        private readonly ProductService _productService;
        private readonly CartProServices _cartService;

        public HomeForm()
        {
            InitializeComponent();
            MapsterConfigCategory.RegisterMapsterConfiguration();
            //AppDbContext appDbContext = new AppDbContext();
            //ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
            //IGenaricRepository<Category, int> categoryRepositories = new GenericRepository<Category, int>(appDbContext);
            //_icategoryService = new CategoryService(categoryRepositories);
            var builder = Autofac.Inject();
            _icategoryService = builder.Resolve<ICategoryServices>();
            _productServices = builder.Resolve<IProductServices>();
        }

        ICategoryServices _icategoryService;
        IProductServices _productServices;
        List<Product> ProductsList;
        List<Category> CategoriesList;
        BindingSource bindingSource;
        private void btnCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm();
            this.Hide();
            cartForm.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            this.Hide();
            orderForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ProductsList = _productServices.GetAllProduct();
            bindingSource = new BindingSource(ProductsList, "");
            dataGridView1.DataSource = bindingSource;
            dataGridView1.Columns[0].ReadOnly = true;
            bindingSource.AddingNew += (sender, e) =>
            {
                Product newProductDTO = new Product();
                e.NewObject = newProductDTO;
                _productServices.AddProduct(newProductDTO);

            };
            dataGridView1.UserDeletingRow += (sender, e) =>
            {
                if (e.Row.DataBoundItem is Product deletedPro)
                {
                    _productServices.DeleteProduct(deletedPro);
                }
            };

        }
       

    }
}
