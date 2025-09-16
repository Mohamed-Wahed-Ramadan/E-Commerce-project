<<<<<<< HEAD
﻿using Autofac;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Services;
using E_Commerce_project.models;
=======
﻿using E_Commerce.DTOs.User;
>>>>>>> 8eb9597a3720c8c9881c5e005aeebf1ccd7c8ce7
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
    public partial class CartForm : Form

    {
<<<<<<< HEAD
        private void LoadCard()
        {
            CartList = _CartServices.GetAllCarts();
            bindingSourceCart = new BindingSource(CartList, "");
            dataGridView1.DataSource = bindingSourceCart;
            dataGridView1.Columns[0].ReadOnly = true;
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
            bindingSourceCart.AddingNew += (sender, e) =>
            {
                Product newProductDTO = new Product();
                e.NewObject = newProductDTO;
                //_productServices.AddProduct(newProductDTO);

            };
            dataGridView1.CellValueChanged += (sender, e) =>
            {
                var proObj = dataGridView1.Rows[e.RowIndex].DataBoundItem;

                if (proObj is Cart cart)
                {
                    if (cart.Id > 0)
                    {
                        _CartServices.UpdateCard(cart);
                    }

                }
            };

            dataGridView1.UserDeletingRow += (sender, e) =>
            {
                if (e.Row.DataBoundItem is Cart deletedCard)
                {
                    _CartServices.DelectCart(deletedCard);
                }
            };

        }
        public CartForm()
        {
            InitializeComponent();
            var builder = Autofac.Inject();
            _CartServices = builder.Resolve<ICartServices>();
            LoadCard();

=======
        private readonly UserResponse _user;

        public CartForm(UserResponse user)
        {
            InitializeComponent();
            this._user = user;
>>>>>>> 8eb9597a3720c8c9881c5e005aeebf1ccd7c8ce7
        }
        ICartServices _CartServices;
        List<Cart> CartList;
        BindingSource bindingSourceCart;

        private void btnBack_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(_user);
            this.Hide();
            homeForm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            foreach (Cart pro in bindingSourceCart.List)
            {
                if (string.IsNullOrWhiteSpace(pro.User.FullName))
                {
                    MessageBox.Show("Category Name cannot be empty.");
                    continue;
                }

                if (pro.Id > 0)
                {

                }

                else
                    _CartServices.AddCart(pro);
            }
            _CartServices.Save();


=======
            
>>>>>>> 8eb9597a3720c8c9881c5e005aeebf1ccd7c8ce7
        }

        private void btnConf_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
