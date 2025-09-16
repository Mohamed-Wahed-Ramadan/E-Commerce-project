namespace WinForms.pressentation
{
    partial class OrderForm
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
            dataGridView1 = new DataGridView();
            btnSave = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(169, 19);
            label1.Name = "label1";
            label1.Size = new Size(177, 28);
            label1.TabIndex = 0;
            label1.Text = "Orders Bage";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 62);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(474, 169);
            dataGridView1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.ButtonHighlight;
            btnSave.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(375, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(105, 30);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            btnBack.BackColor = SystemColors.ButtonHighlight;
            btnBack.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnBack.Location = new Point(6, 245);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(105, 30);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // OrderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            BackgroundImage = Properties.Resources.f22;
            ClientSize = new Size(487, 287);
            Controls.Add(btnBack);
            Controls.Add(btnSave);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "OrderForm";
            Text = "OrderForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Button btnSave;
        private Button btnBack;
    }
}