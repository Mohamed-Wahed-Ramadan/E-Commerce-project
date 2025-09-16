namespace WinForms.pressentation
{
    partial class SignInForm
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
            txtName = new TextBox();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(131, 72);
            txtName.Name = "txtName";
            txtName.Size = new Size(309, 23);
            txtName.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(131, 185);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(309, 23);
            txtPassword.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(131, 126);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(309, 23);
            txtEmail.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Font = new Font("Simplified Arabic Fixed", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.Location = new Point(294, 237);
            button1.Name = "button1";
            button1.Size = new Size(146, 33);
            button1.TabIndex = 4;
            button1.Text = "Sign In";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(17, 72);
            label1.Name = "label1";
            label1.Size = new Size(60, 25);
            label1.TabIndex = 5;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(17, 126);
            label2.Name = "label2";
            label2.Size = new Size(72, 25);
            label2.TabIndex = 6;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Simplified Arabic Fixed", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(17, 183);
            label3.Name = "label3";
            label3.Size = new Size(108, 25);
            label3.TabIndex = 7;
            label3.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Simplified Arabic Fixed", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(178, 14);
            label4.Name = "label4";
            label4.Size = new Size(194, 26);
            label4.TabIndex = 8;
            label4.Text = "Register Bage";
            label4.Click += label4_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = SystemColors.ButtonHighlight;
            btnBack.Font = new Font("Simplified Arabic Fixed", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnBack.Location = new Point(10, 12);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 28);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // SignInForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            BackgroundImage = Properties.Resources.f22;
            ClientSize = new Size(488, 295);
            Controls.Add(btnBack);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtName);
            Name = "SignInForm";
            Text = "SignInForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnBack;
    }
}