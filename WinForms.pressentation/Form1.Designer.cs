namespace WinForms.pressentation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSignIn = new Button();
            btnLogin = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnSignIn
            // 
            btnSignIn.BackColor = Color.White;
            btnSignIn.Font = new Font("Simplified Arabic Fixed", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSignIn.ForeColor = Color.Black;
            btnSignIn.Location = new Point(23, 115);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(158, 46);
            btnSignIn.TabIndex = 0;
            btnSignIn.Text = "Sign In";
            btnSignIn.UseVisualStyleBackColor = false;
            btnSignIn.Click += btnSignIn_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.White;
            btnLogin.BackgroundImageLayout = ImageLayout.None;
            btnLogin.Font = new Font("Simplified Arabic Fixed", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.Black;
            btnLogin.Location = new Point(286, 115);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(158, 46);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(71, 18);
            label1.Name = "label1";
            label1.Size = new Size(348, 25);
            label1.TabIndex = 2;
            label1.Text = "Welcome at our E-Commerce WinApp";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            BackgroundImage = Properties.Resources.f22;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(482, 218);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Controls.Add(btnSignIn);
            DoubleBuffered = true;
            ForeColor = Color.MidnightBlue;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSignIn;
        private Button btnLogin;
        private Label label1;
    }
}
