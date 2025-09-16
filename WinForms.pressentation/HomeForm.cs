using Autofac;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Mapper;
using E_Commerce.application.Services;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ProductDtos;
using E_Commerce.DTOs.User;
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


        public HomeForm(UserResponse? user)
        {
            InitializeComponent();
            MapsterConfigCategory.RegisterMapsterConfiguration();
            //AppDbContext appDbContext = new AppDbContext();
            //ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
            //IGenaricRepository<Category, int> categoryRepositories = new GenericRepository<Category, int>(appDbContext);
            //_icategoryService = new CategoryService(categoryRepositories);
            var builder = Autofac.Inject();
            _productServices = builder.Resolve<IProductServices>();
            LoadProduct();
            _user = user;
        }
        IProductServices _productServices;
        List<ProductReadDto> ProductsList;
        BindingSource bindingSourceProduct;
        private readonly UserResponse? _user;

        private void btnCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm(_user);
            this.Hide();
            cartForm.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm(_user);
            this.Hide();
            orderForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadProduct()
        {
            ProductsList = _productServices.GetAllProduct();
            bindingSourceProduct = new BindingSource(ProductsList, "");
            dataGridView1.DataSource = bindingSourceProduct;
            //dataGridView1.Columns[0].ReadOnly = true;
            //bindingSourceProduct.AddingNew += (sender, e) =>
            //{
            //    ProductReadDto newProductDTO = new ProductReadDto();
            //    e.NewObject = newProductDTO;
            //    _productServices.AddProduct(newProductDTO);

            //};
            //dataGridView1.UserDeletingRow += (sender, e) =>
            //{
            //    if (e.Row.DataBoundItem is Product deletedPro)
            //    {
            //        _productServices.DeleteProduct(deletedPro);
            //    }
            //};

        }

    }
}
