using Autofac;
using E_commerce.infratructure;
using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Mapper;
using E_Commerce.application.Repository;
using E_Commerce.application.Services;
using E_Commerce.Context;
using E_Commerce.DTOs.Cart;
using E_Commerce.DTOs.Category;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ProductDtos;
using E_Commerce_project.models;
using System.Windows.Forms;
namespace WinForms.pressentation
{
    public partial class AdminForm : Form
    {
        private void LoadCategory()
        {
            CategoriesList = _icategoryService.GetAllCategory();
            bindingSourceCategory = new BindingSource(CategoriesList, "");
            dataGridViewCat.DataSource = bindingSourceCategory;
            dataGridViewCat.Columns[0].ReadOnly = true;
            bindingSourceCategory.AddingNew += (sender, e) =>
            {
                CategoryReadDto newCategoryDTO = new CategoryReadDto();
                e.NewObject = newCategoryDTO;
                _icategoryService.AddCategory(newCategoryDTO);

            };
            dataGridViewCat.UserDeletingRow += (sender, e) =>
            {
                if (e.Row.DataBoundItem is Category deletedCat)
                {
                    _icategoryService.DeleteCategory(deletedCat);
                }
            };

        }
        private void LoadProduct()
        {
            ProductsList = _productServices.GetAllProduct();
            bindingSourceProduct = new BindingSource(ProductsList, "");
            dataGridViewPro.DataSource = bindingSourceProduct;
            dataGridViewPro.Columns[0].ReadOnly = true;
            bindingSourceProduct.AddingNew += (sender, e) =>
            {
                ProductReadDto newProductDTO = new ProductReadDto();
                e.NewObject = newProductDTO;
                _productServices.AddProduct(newProductDTO);

            };
            dataGridViewPro.UserDeletingRow += (sender, e) =>
            {
                if (e.Row.DataBoundItem is Product deletedPro)
                {
                    _productServices.DeleteProduct(deletedPro);
                }
            };

        }
        public AdminForm()
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
            LoadCategory();
            LoadProduct();


        }

        ICategoryServices _icategoryService;
        IProductServices _productServices;
        List<ProductReadDto> ProductsList;
        List<CategoryReadDto> CategoriesList;
        BindingSource bindingSourceProduct;
        BindingSource bindingSourceCategory;
        private void btnBack_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            this.Hide();
            homeForm.Show();
        }

        private void dataGridViewCat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            #region MyRegion
            //CategoriesList = _icategoryService.GetAllCategory();
            //bindingSource = new BindingSource(CategoriesList, "");
            //dataGridViewCat.DataSource = bindingSource;
            //dataGridViewCat.Columns[0].ReadOnly = true;
            //bindingSource.AddingNew += (sender, e) =>
            //{
            //    CategoryCreateDto newCategoryDTO = new CategoryCreateDto();
            //    e.NewObject = newCategoryDTO;
            //    _icategoryService.AddCategory(newCategoryDTO);

            //};
            //dataGridViewCat.UserDeletingRow += (sender, e) =>
            //{
            //    if (e.Row.DataBoundItem is Category deletedCat)
            //    {
            //        _icategoryService.DeleteCategory(deletedCat);
            //    }
            //};

            #endregion

        }

        private void dataGridViewPro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            #region MyRegion

            //ProductsList = _productServices.GetAllProduct();
            //bindingSource = new BindingSource(ProductsList, "");
            //dataGridViewPro.DataSource = bindingSource;
            //dataGridViewPro.Columns[0].ReadOnly = true;
            //bindingSource.AddingNew += (sender, e) =>
            //{
            //    ProductCreateDto newProductDTO = new ProductCreateDto();
            //    e.NewObject = newProductDTO;
            //    _productServices.AddProduct(newProductDTO);

            //};
            //dataGridViewPro.UserDeletingRow += (sender, e) =>
            //{
            //    if (e.Row.DataBoundItem is Product deletedPro)
            //    {
            //        _productServices.DeleteProduct(deletedPro);
            //    }
            //}; 
            #endregion

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int C = _icategoryService.SaveCategory();
            int P = _productServices.saveProduct();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}