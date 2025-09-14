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
            dataGridView1 = new DataGridView();
            img = new DataGridViewImageColumn();
            btnAddToCart = new DataGridViewImageColumn();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnCart
            // 
            btnCart.Location = new Point(1152, 540);
            btnCart.Margin = new Padding(3, 4, 3, 4);
            btnCart.Name = "btnCart";
            btnCart.Size = new Size(112, 44);
            btnCart.TabIndex = 0;
            btnCart.Text = "Go To Cart";
            btnCart.UseVisualStyleBackColor = true;
            btnCart.Click += btnCart_Click;
            // 
            // btnOrder
            // 
            btnOrder.Location = new Point(14, 540);
            btnOrder.Margin = new Padding(3, 4, 3, 4);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(112, 44);
            btnOrder.TabIndex = 1;
            btnOrder.Text = "Go To Order";
            btnOrder.UseVisualStyleBackColor = true;
            btnOrder.Click += btnOrder_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { img, btnAddToCart });
            dataGridView1.Location = new Point(10, 71);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1254, 437);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // img
            // 
            img.HeaderText = "Product Img";
            img.MinimumWidth = 6;
            img.Name = "img";
            img.Width = 125;
            // 
            // btnAddToCart
            // 
            btnAddToCart.HeaderText = "Add To Cart";
            btnAddToCart.MinimumWidth = 6;
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Width = 125;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(597, 25);
            label1.Name = "label1";
            label1.Size = new Size(88, 20);
            label1.TabIndex = 4;
            label1.Text = "Home Bage";
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(1278, 600);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(btnOrder);
            Controls.Add(btnCart);
            Margin = new Padding(3, 4, 3, 4);
            Name = "HomeForm";
            Text = "HomeForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCart;
        private Button btnOrder;
        private DataGridView dataGridView1;
        private DataGridViewImageColumn img;
        private DataGridViewImageColumn btnAddToCart;
        private Label label1;
    }
}