namespace WinForms.pressentation
{
    partial class AdminForm
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
            label1 = new Label();
            label2 = new Label();
            dataGridViewCat = new DataGridView();
            label3 = new Label();
            dataGridViewPro = new DataGridView();
            btnSave = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPro).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(435, 31);
            label1.Name = "label1";
            label1.Size = new Size(91, 20);
            label1.TabIndex = 0;
            label1.Text = "Admin Bage";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 75);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 1;
            label2.Text = "Categories";
            // 
            // dataGridViewCat
            // 
            dataGridViewCat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCat.Location = new Point(7, 99);
            dataGridViewCat.Margin = new Padding(3, 4, 3, 4);
            dataGridViewCat.Name = "dataGridViewCat";
            dataGridViewCat.RowHeadersWidth = 51;
            dataGridViewCat.Size = new Size(894, 151);
            dataGridViewCat.TabIndex = 2;
            dataGridViewCat.CellContentClick += dataGridViewCat_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 292);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 3;
            label3.Text = "Products";
            // 
            // dataGridViewPro
            // 
            dataGridViewPro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPro.Location = new Point(7, 313);
            dataGridViewPro.Margin = new Padding(3, 4, 3, 4);
            dataGridViewPro.Name = "dataGridViewPro";
            dataGridViewPro.RowHeadersWidth = 51;
            dataGridViewPro.Size = new Size(894, 212);
            dataGridViewPro.TabIndex = 4;
            dataGridViewPro.CellContentClick += dataGridViewPro_CellContentClick;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(783, 541);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(118, 43);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(14, 541);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(118, 43);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(914, 600);
            Controls.Add(btnBack);
            Controls.Add(btnSave);
            Controls.Add(dataGridViewPro);
            Controls.Add(label3);
            Controls.Add(dataGridViewCat);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AdminForm";
            Text = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPro).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DataGridView dataGridViewCat;
        private Label label3;
        private DataGridView dataGridViewPro;
        private Button btnSave;
        private Button btnBack;
    }
}