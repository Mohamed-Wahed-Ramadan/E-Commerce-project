namespace WinForms.pressentation
{
    partial class LoginForm
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
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnDone = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(135, 135);
            txtEmail.Margin = new Padding(3, 5, 3, 5);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(354, 27);
            txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(135, 246);
            txtPassword.Margin = new Padding(3, 5, 3, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(354, 27);
            txtPassword.TabIndex = 1;
            // 
            // btnDone
            // 
            btnDone.Location = new Point(215, 384);
            btnDone.Margin = new Padding(3, 5, 3, 5);
            btnDone.Name = "btnDone";
            btnDone.Size = new Size(149, 59);
            btnDone.TabIndex = 3;
            btnDone.Text = "Login";
            btnDone.UseVisualStyleBackColor = true;
            btnDone.Click += btnDone_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(63, 140);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 4;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 251);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 5;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(245, 35);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 6;
            label3.Text = "Login Bage";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(5, 10);
            btnBack.Margin = new Padding(3, 5, 3, 5);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(86, 39);
            btnBack.TabIndex = 7;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(646, 489);
            Controls.Add(btnBack);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDone);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 5, 3, 5);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnDone;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnBack;
    }
}