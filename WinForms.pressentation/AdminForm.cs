using E_commerce.infratructure;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce.Context;
using E_Commerce_project.models;
namespace WinForms.pressentation
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            AppDbContext
            ICategoryRepository categoryRepository = new CategoryRepository(); 
            _icategoryService = new ICategoryServices( );

        }

        ICategoryServices _icategoryService;
        List<Category> CategoriesList;
        BindingSource bindingSource;
        private void btnBack_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            this.Hide();
            homeForm.Show();
        }

        private void dataGridViewCat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CategoriesList = _icategoryService.GetAllCategory();
            bindingSource = new BindingSource(CategoriesList, "");
            dataGridViewCat.DataSource = bindingSource;
            dataGridViewCat.Columns[0].ReadOnly = true;

        }
    }
}
