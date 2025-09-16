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
            btnCart.BackColor = SystemColors.ButtonHighlight;
            btnCart.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnCart.Location = new Point(695, 331);
            btnCart.Name = "btnCart";
            btnCart.Size = new Size(166, 33);
            btnCart.TabIndex = 0;
            btnCart.Text = "Go To Cart";
            btnCart.UseVisualStyleBackColor = false;
            btnCart.Click += btnCart_Click;
            // 
            // btnOrder
            // 
            btnOrder.BackColor = SystemColors.ButtonHighlight;
            btnOrder.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnOrder.Location = new Point(9, 331);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(166, 33);
            btnOrder.TabIndex = 1;
            btnOrder.Text = "Go To Order";
            btnOrder.UseVisualStyleBackColor = false;
            btnOrder.Click += btnOrder_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { img, btnAddToCart });
            dataGridView1.Location = new Point(9, 53);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(853, 272);
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
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(359, 9);
            label1.Name = "label1";
            label1.Size = new Size(147, 28);
            label1.TabIndex = 4;
            label1.Text = "Home Bage";
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            BackgroundImage = Properties.Resources.f22;
            ClientSize = new Size(873, 373);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(btnOrder);
            Controls.Add(btnCart);
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