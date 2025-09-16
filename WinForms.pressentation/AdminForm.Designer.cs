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
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Simplified Arabic Fixed", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(217, 9);
            label1.Name = "label1";
            label1.Size = new Size(152, 26);
            label1.TabIndex = 0;
            label1.Text = "Admin Bage";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(162, 46);
            label2.Name = "label2";
            label2.Size = new Size(132, 25);
            label2.TabIndex = 1;
            label2.Text = "Categories";
            // 
            // dataGridViewCat
            // 
            dataGridViewCat.BackgroundColor = SystemColors.ActiveCaption;
            dataGridViewCat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCat.Location = new Point(162, 73);
            dataGridViewCat.Name = "dataGridViewCat";
            dataGridViewCat.RowHeadersWidth = 51;
            dataGridViewCat.Size = new Size(367, 88);
            dataGridViewCat.TabIndex = 2;
            dataGridViewCat.CellContentClick += dataGridViewCat_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(6, 169);
            label3.Name = "label3";
            label3.Size = new Size(108, 25);
            label3.TabIndex = 3;
            label3.Text = "Products";
            label3.Click += label3_Click;
            // 
            // dataGridViewPro
            // 
            dataGridViewPro.BackgroundColor = SystemColors.ActiveCaption;
            dataGridViewPro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPro.Location = new Point(6, 197);
            dataGridViewPro.Name = "dataGridViewPro";
            dataGridViewPro.RowHeadersWidth = 51;
            dataGridViewPro.Size = new Size(523, 159);
            dataGridViewPro.TabIndex = 4;
            dataGridViewPro.CellContentClick += dataGridViewPro_CellContentClick;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.White;
            btnSave.Font = new Font("Simplified Arabic Fixed", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(6, 73);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 32);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.Font = new Font("Simplified Arabic Fixed", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnBack.Location = new Point(6, 14);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(103, 32);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            BackgroundImage = Properties.Resources.f22;
            ClientSize = new Size(541, 368);
            Controls.Add(btnBack);
            Controls.Add(btnSave);
            Controls.Add(dataGridViewPro);
            Controls.Add(label3);
            Controls.Add(dataGridViewCat);
            Controls.Add(label2);
            Controls.Add(label1);
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