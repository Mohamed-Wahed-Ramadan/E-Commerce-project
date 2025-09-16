using Autofac;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Mapper;
using E_Commerce.DTOs.CategoryDtos;
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
            #region MyRegion
            //bindingSourceCategory.AddingNew += (sender, e) =>
            //{
            //    CategoryReadDto newCategoryDTO = new CategoryReadDto();

            //    if (e.NewObject is CategoryReadDto newCategoryDTOS)
            //    {
            //        _icategoryService.AddCategory(newCategoryDTOS);
            //    }
            //}; 
            #endregion

            bindingSourceCategory.AddingNew += (sender, e) =>
            {
                Category newCategoryDTO = new Category();
                e.NewObject = newCategoryDTO;
                //_icategoryService.AddCategory(newCategoryDTO);

            };
            dataGridViewCat.CellValueChanged += (sender, e) =>
            {
                var catObj = dataGridViewCat.Rows[e.RowIndex].DataBoundItem;

                if (catObj is Category category) 
                {
                    if (category.Id > 0) 
                    {
                        _icategoryService.UpdateCategory(category);
                    }
                   
                }
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
            #region MyRegion
            //bindingSourceProduct.AddingNew += (sender, e) =>
            //{
            //    ProductReadDto newProductDTO = new ProductReadDto();

            //    if (e.NewObject is ProductReadDto newProductDTOs)
            //    {
            //        _productServices.AddProduct(newProductDTOs);
            //    }
            //}; 
            #endregion
            bindingSourceProduct.AddingNew += (sender, e) =>
            {
                Product  newProductDTO = new Product();
                e.NewObject = newProductDTO;
                //_productServices.AddProduct(newProductDTO);

            };
            dataGridViewPro.CellValueChanged += (sender, e) =>
            {
                var proObj = dataGridViewPro.Rows[e.RowIndex].DataBoundItem;

                if (proObj is Product product)
                {
                    if (product.Id > 0)
                    {
                        _productServices.UpdateProduct(product);
                    }
                    
                }
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
            #region MyRegion
            //AppDbContext appDbContext = new AppDbContext();
            //ICategoryRepository categoryRepository = new CategoryRepository(appDbContext);
            //IGenaricRepository<Category, int> categoryRepositories = new GenericRepository<Category, int>(appDbContext);
            //_icategoryService = new CategoryService(categoryRepositories); 
            #endregion
            var builder = Autofac.Inject();
            _icategoryService = builder.Resolve<ICategoryServices>();
            _productServices = builder.Resolve<IProductServices>();
            LoadCategory();
            LoadProduct();


        }

        ICategoryServices _icategoryService;
        IProductServices _productServices;
        List<Product> ProductsList;
        List<Category> CategoriesList;
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
            foreach (Category cat in bindingSourceCategory.List)
            {
                if (string.IsNullOrWhiteSpace(cat.Name))
                {
                    MessageBox.Show("Category Name cannot be empty.");
                    continue;
                }

                if (cat.Id > 0)
                    _icategoryService.UpdateCategory(cat);
                else
                    _icategoryService.AddCategory(cat);
            }
            foreach (Product pro in bindingSourceProduct.List)
            {
                if (string.IsNullOrWhiteSpace(pro.Name))
                {
                    MessageBox.Show("Category Name cannot be empty.");
                    continue;
                }

                if (pro.Id > 0)
                    _productServices.UpdateProduct(pro);
                else
                    _productServices.AddProduct(pro);
            }


            int C = _icategoryService.SaveCategory();
            int P = _productServices.saveProduct();

        }
    }
}