namespace WinForms.pressentation
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCart = new Button();
            btnOrder = new Button();
            btnMGR = new Button();
            dataGridView1 = new DataGridView();
            img = new DataGridViewImageColumn();
            btnAddToCart = new DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnCart
            // 
            btnCart.Location = new Point(1008, 405);
            btnCart.Name = "btnCart";
            btnCart.Size = new Size(98, 33);
            btnCart.TabIndex = 0;
            btnCart.Text = "Go To Cart";
            btnCart.UseVisualStyleBackColor = true;
            // 
            // btnOrder
            // 
            btnOrder.Location = new Point(905, 405);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(98, 33);
            btnOrder.TabIndex = 1;
            btnOrder.Text = "Go To Order";
            btnOrder.UseVisualStyleBackColor = true;
            // 
            // btnMGR
            // 
            btnMGR.Location = new Point(12, 405);
            btnMGR.Name = "btnMGR";
            btnMGR.Size = new Size(175, 33);
            btnMGR.TabIndex = 2;
            btnMGR.Text = "Manage Products";
            btnMGR.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { img, btnAddToCart });
            dataGridView1.Location = new Point(9, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1097, 369);
            dataGridView1.TabIndex = 3;
            // 
            // img
            // 
            img.HeaderText = "Product Img";
            img.Name = "img";
            // 
            // btnAddToCart
            // 
            btnAddToCart.HeaderText = "Add To Cart";
            btnAddToCart.Name = "btnAddToCart";
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(1118, 450);
            Controls.Add(dataGridView1);
            Controls.Add(btnMGR);
            Controls.Add(btnOrder);
            Controls.Add(btnCart);
            Name = "HomeForm";
            Text = "HomeForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCart;
        private Button btnOrder;
        private Button btnMGR;
        private DataGridView dataGridView1;
        private DataGridViewImageColumn img;
        private DataGridViewImageColumn btnAddToCart;
    }
}