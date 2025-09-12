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
            label1.Location = new Point(381, 23);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 0;
            label1.Text = "Admin Bage";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 56);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 1;
            label2.Text = "Categories";
            // 
            // dataGridViewCat
            // 
            dataGridViewCat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCat.Location = new Point(6, 74);
            dataGridViewCat.Name = "dataGridViewCat";
            dataGridViewCat.Size = new Size(782, 113);
            dataGridViewCat.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 219);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 3;
            label3.Text = "Products";
            // 
            // dataGridViewPro
            // 
            dataGridViewPro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPro.Location = new Point(6, 235);
            dataGridViewPro.Name = "dataGridViewPro";
            dataGridViewPro.Size = new Size(782, 159);
            dataGridViewPro.TabIndex = 4;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(685, 406);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 32);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 406);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(103, 32);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(800, 450);
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