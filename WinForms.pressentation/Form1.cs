using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using E_Commerce.Context;
using E_Commerce_project.models;
using E_Commerce_project.models.User;
using System.Threading.Tasks;
using System.IO;

namespace WinForms.pressentation
{
    public partial class Form1 : Form
    {
        // DB
        private readonly AppDbContext _db;

        // UI panels (single form with view switching)
        private Panel pnlHeader;
        private Panel pnlBody;
        private Panel pnlFooter;

        // Views
        private Panel viewChoose;
        private Panel viewHome;
        private Panel viewCart;
        private Panel viewOrders;
        private Panel viewAdmin;

        // state
        private User? currentUser = null;
        private Cart? currentCart = null;
        private Dictionary<int, int> guestCart = new();
        private FlowLayoutPanel productsFlow;

        // Animation variables
        private System.Windows.Forms.Timer animationTimer;
        private int animationCounter = 0;
        private const int ANIMATION_DURATION = 20;

        // Updated Colors - New Color Palette
        private Color primaryColor = Color.FromArgb(33, 53, 85);       // #213555 - Dark blue
        private Color secondaryColor = Color.FromArgb(62, 88, 121);    // #3E5879 - Medium blue
        private Color backgroundColor = Color.FromArgb(216, 196, 182); // #D8C4B6 - Light beige
        private Color lightColor = Color.FromArgb(245, 239, 231);      // #F5EFE7 - Very light cream
        private Color textColor = Color.FromArgb(33, 53, 85);          // Dark text on light background
        private Color successColor = Color.FromArgb(0, 255, 0);        // Keep original green for success
        private Color errorColor = Color.FromArgb(255, 0, 0);          // Keep original red for error

        // Logo path - Updated to use project structure
        private string logoPath;
        private Image logoImage;

        public Form1()
        {
            InitializeComponent();

            // Create/resolve AppDbContext directly
            _db = new AppDbContext();

            // Set logo path - E-Commerce-project\images\logo.png
            string projectRoot = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)));
            logoPath = Path.Combine(projectRoot, "images", "logo.png");

            this.Text = "E-Commerce App";
            this.Width = 1200;
            this.Height = 800;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = backgroundColor;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Load logo
            try
            {
                if (File.Exists(logoPath))
                {
                    logoImage = Image.FromFile(logoPath);
                }
                else
                {
                    logoImage = GeneratePlaceholderImage("LOGO", 40, 40);
                }
            }
            catch
            {
                logoImage = GeneratePlaceholderImage("LOGO", 40, 40);
            }

            // Initialize animation timer
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 10;
            animationTimer.Tick += AnimationTimer_Tick;

            BuildLayout();
            BuildViews();
            ShowChooseView();
        }

        #region Layout & Views

        private Panel CreateBorderedPanel(Color borderColor, int borderSize = 1)
        {
            var panel = new Panel();
            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    borderColor, borderSize, ButtonBorderStyle.Solid,
                    borderColor, borderSize, ButtonBorderStyle.Solid,
                    borderColor, borderSize, ButtonBorderStyle.Solid,
                    borderColor, borderSize, ButtonBorderStyle.Solid);
            };
            return panel;
        }

        private void BuildLayout()
        {
            // Header with #213555 background
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = primaryColor
            };

            pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(10)
            };

            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = primaryColor
            };

            this.Controls.Add(pnlBody);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(pnlHeader);

            // Add logo to header
            var logo = new PictureBox
            {
                Image = logoImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(40, 40),
                Location = new Point(20, 20)
            };
            pnlHeader.Controls.Add(logo);

            // header content (title + buttons)
            var lblTitle = new Label
            {
                Text = "E-Commerce App",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = lightColor,
                Location = new Point(70, 22),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            // top-right quick buttons with modern style
            var buttons = new List<Button>();

            var btnHome = CreateModernButton("Home", 800);
            btnHome.Click += (s, e) => ShowHomeView();
            buttons.Add(btnHome);

            var btnCart = CreateModernButton("Cart", 880);
            btnCart.Click += (s, e) => ShowCartView();
            buttons.Add(btnCart);

            var btnOrders = CreateModernButton("Orders", 960);
            btnOrders.Click += (s, e) => ShowOrdersView();
            buttons.Add(btnOrders);

            var btnUser = CreateModernButton("User: Guest", 640);
            btnUser.Click += (s, e) =>
            {
                if (currentUser == null) ShowChooseView();
                else MessageBox.Show($"Logged in as: {currentUser.Email} ({currentUser.UserName})");
            };
            buttons.Add(btnUser);

            foreach (var btn in buttons)
            {
                pnlHeader.Controls.Add(btn);
            }

            // footer status
            var lblFooter = new Label
            {
                Text = "© 2023 E-Commerce App. All rights reserved.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = lightColor,
                Font = new Font("Segoe UI", 9)
            };
            pnlFooter.Controls.Add(lblFooter);

            // keep reference to update user button text
            pnlHeader.Tag = btnUser;
        }

        private Button CreateModernButton(string text, int xPos)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
                Location = new Point(xPos, 22),
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = lightColor,  // #F5EFE7
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Padding = new Padding(5)
            };

            // Add hover effects
            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(240, 235, 225);
                btn.FlatAppearance.BorderColor = secondaryColor;
                btn.FlatAppearance.BorderSize = 2;
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = lightColor;
                btn.FlatAppearance.BorderColor = secondaryColor;
                btn.FlatAppearance.BorderSize = 1;
            };

            // Set initial border - #3E5879
            btn.FlatAppearance.BorderColor = secondaryColor;
            btn.FlatAppearance.BorderSize = 1;

            return btn;
        }

        private void BuildViews()
        {
            // Choose view (Login / Register / Browse)
            viewChoose = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(20)
            };

            var lbl = new Label
            {
                Text = "Welcome to E-Commerce App",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewChoose.Controls.Add(lbl);

            var subLbl = new Label
            {
                Text = "Please choose an option to continue",
                Font = new Font("Segoe UI", 12),
                Location = new Point(20, 60),
                AutoSize = true,
                ForeColor = textColor
            };
            viewChoose.Controls.Add(subLbl);

            var btnLogin = CreateStyledButton("Login", new Point(20, 120), 160, 45);
            var btnRegister = CreateStyledButton("Register", new Point(200, 120), 160, 45);
            var btnGuest = CreateStyledButton("Browse as Guest", new Point(380, 120), 180, 45);

            viewChoose.Controls.AddRange(new Control[] { btnLogin, btnRegister, btnGuest });

            btnLogin.Click += (s, e) => BuildAuthForm(isLogin: true);
            btnRegister.Click += (s, e) => BuildAuthForm(isLogin: false);
            btnGuest.Click += (s, e) =>
            {
                currentUser = null;
                guestCart.Clear();
                ShowHomeView();
                UpdateHeaderUser();
            };

            // Home view (product listing)
            viewHome = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(10)
            };

            productsFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(12),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = backgroundColor
            };
            viewHome.Controls.Add(productsFlow);

            // Cart view
            viewCart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(10)
            };

            // Orders view
            viewOrders = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(10)
            };

            // Admin view
            viewAdmin = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(10)
            };

            // add views to body (we'll show/hide)
            pnlBody.Controls.AddRange(new Control[] { viewChoose, viewHome, viewCart, viewOrders, viewAdmin });
        }

        private Button CreateStyledButton(string text, Point location, int width, int height)
        {
            var btn = new Button
            {
                Text = text,
                Location = location,
                Width = width,
                Height = height,
                FlatStyle = FlatStyle.Flat,
                BackColor = lightColor,  // #F5EFE7
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            // Add hover effects
            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(240, 235, 225);
                btn.FlatAppearance.BorderColor = primaryColor;
                btn.FlatAppearance.BorderSize = 2;
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = lightColor;
                btn.FlatAppearance.BorderColor = secondaryColor;
                btn.FlatAppearance.BorderSize = 1;
            };

            // Set initial border - #3E5879
            btn.FlatAppearance.BorderColor = secondaryColor;
            btn.FlatAppearance.BorderSize = 1;

            return btn;
        }

        #endregion

        #region Show Views with Animation

        private void ClearAllViews()
        {
            viewChoose.Visible = false;
            viewHome.Visible = false;
            viewCart.Visible = false;
            viewOrders.Visible = false;
            viewAdmin.Visible = false;
        }

        private async void ShowViewWithAnimation(Panel view)
        {
            ClearAllViews();

            // Set initial state for animation
            view.Location = new Point(pnlBody.Width, 0);
            view.Visible = true;
            view.BringToFront();

            // Start animation
            animationCounter = 0;
            animationTimer.Start();

            // Animate the view sliding in from right
            while (animationCounter < ANIMATION_DURATION)
            {
                int newX = pnlBody.Width - (pnlBody.Width * animationCounter / ANIMATION_DURATION);
                view.Location = new Point(newX, 0);
                await Task.Delay(10);
                animationCounter++;
            }

            view.Location = new Point(0, 0);
            animationTimer.Stop();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationCounter++;
            if (animationCounter >= ANIMATION_DURATION)
            {
                animationTimer.Stop();
            }
        }

        private void ShowChooseView()
        {
            ShowViewWithAnimation(viewChoose);
        }

        private void ShowHomeView()
        {
            ShowViewWithAnimation(viewHome);
            LoadProducts();
        }

        private void ShowCartView()
        {
            ShowViewWithAnimation(viewCart);
            BuildCartView();
        }

        private void ShowOrdersView()
        {
            ShowViewWithAnimation(viewOrders);
            BuildOrdersView();
        }

        private void ShowAdminView()
        {
            if (currentUser == null || currentUser.Email != "admin@iti.eg")
            {
                ShowNotification("Admin page only for admin user (admin@iti.eg).", false);
                return;
            }
            ShowViewWithAnimation(viewAdmin);
            BuildAdminView();
        }

        #endregion

        #region Auth (Login/Register)

        private void BuildAuthForm(bool isLogin)
        {
            // small popup-like panel inside viewChoose
            var pnl = new Panel
            {
                Size = new Size(450, 300),
                Location = new Point(20, 120),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = lightColor,
                Padding = new Padding(15)
            };
            viewChoose.Controls.Add(pnl);
            pnl.BringToFront();

            pnl.Controls.Add(new Label
            {
                Text = isLogin ? "Login" : "Register",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = primaryColor
            });

            int yPos = 50;
            pnl.Controls.Add(new Label { Text = "Email:", Location = new Point(10, yPos), AutoSize = true, ForeColor = textColor });
            var txtEmail = new TextBox { Location = new Point(120, yPos - 4), Width = 290, Height = 30, BackColor = Color.White, ForeColor = textColor, BorderStyle = BorderStyle.FixedSingle };
            pnl.Controls.Add(txtEmail);

            yPos += 50;
            pnl.Controls.Add(new Label { Text = "Username:", Location = new Point(10, yPos), AutoSize = true, ForeColor = textColor });
            var txtUser = new TextBox { Location = new Point(120, yPos - 4), Width = 290, Height = 30, BackColor = Color.White, ForeColor = textColor, BorderStyle = BorderStyle.FixedSingle };
            pnl.Controls.Add(txtUser);

            yPos += 50;
            pnl.Controls.Add(new Label { Text = "Password:", Location = new Point(10, yPos), AutoSize = true, ForeColor = textColor });
            var txtPass = new TextBox { Location = new Point(120, yPos - 4), Width = 290, Height = 30, BackColor = Color.White, ForeColor = textColor, BorderStyle = BorderStyle.FixedSingle, UseSystemPasswordChar = true };
            pnl.Controls.Add(txtPass);

            var btnSubmit = CreateStyledButton(isLogin ? "Login" : "Register", new Point(120, yPos + 50), 120, 35);
            pnl.Controls.Add(btnSubmit);

            var btnCancel = CreateStyledButton("Cancel", new Point(260, yPos + 50), 100, 35);
            pnl.Controls.Add(btnCancel);

            btnCancel.Click += (s, e) => { viewChoose.Controls.Remove(pnl); pnl.Dispose(); };

            btnSubmit.Click += (s, e) =>
            {
                var email = txtEmail.Text.Trim();
                var username = txtUser.Text.Trim();
                var pass = txtPass.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(username))
                {
                    ShowNotification("Please fill all fields.", false);
                    return;
                }

                if (isLogin)
                {
                    var user = _db.Users.SingleOrDefault(u => u.Email == email);
                    if (user == null)
                    {
                        ShowNotification("Email not found.", false);
                        return;
                    }
                    // verify password (assuming BCrypt)
                    if (!BCrypt.Net.BCrypt.Verify(pass, user.PasswordHash))
                    {
                        ShowNotification("Incorrect password.", false);
                        return;
                    }
                    currentUser = user;
                    LoadOrCreateCartForCurrentUser();
                    ShowNotification("Logged in successfully.", true);
                    viewChoose.Controls.Remove(pnl); pnl.Dispose();
                    UpdateHeaderUser();
                    ShowHomeView();

                    // if admin email -> open admin view
                    if (currentUser.Email == "admin@iti.eg") ShowAdminView();
                }
                else
                {
                    // register
                    var existsEmail = _db.Users.Any(u => u.Email == email);
                    if (existsEmail)
                    {
                        ShowNotification("Email already exists.", false);
                        return;
                    }
                    var existsUser = _db.Users.Any(u => u.UserName == username);
                    if (existsUser)
                    {
                        ShowNotification("Username already taken.", false);
                        return;
                    }
                    if (pass.Length < 4)
                    {
                        ShowNotification("Password must be at least 4 chars.", false);
                        return;
                    }

                    var hashed = BCrypt.Net.BCrypt.HashPassword(pass);
                    var newUser = new User
                    {
                        UserName = username,
                        Email = email,
                        PasswordHash = hashed,
                        FullName = username,
                        Role = email == "admin@iti.eg" ? UserRole.Admin : UserRole.Customer,
                        CreatedAt = DateTime.Now
                    };
                    _db.Users.Add(newUser);
                    _db.SaveChanges();

                    currentUser = newUser;
                    LoadOrCreateCartForCurrentUser();

                    ShowNotification("Registered and logged in.", true);
                    viewChoose.Controls.Remove(pnl); pnl.Dispose();
                    UpdateHeaderUser();
                    ShowHomeView();

                    if (currentUser.Email == "admin@iti.eg") ShowAdminView();
                }
            };
        }

        private void UpdateHeaderUser()
        {
            if (pnlHeader.Tag is Button btnUser)
            {
                btnUser.Text = currentUser == null ? "User: Guest" : $"User: {currentUser.UserName}";

                // تغيير لون نص اسم المستخدم إذا كان مسجل دخول
                if (currentUser != null)
                {
                    btnUser.BackColor = Color.FromArgb(240, 235, 225);
                    btnUser.Font = new Font(btnUser.Font, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    btnUser.BackColor = lightColor;
                    btnUser.Font = new Font(btnUser.Font, FontStyle.Bold);
                }
            }
        }

        #endregion

        #region Products / Home

        private void LoadProducts()
        {
            productsFlow.Controls.Clear();

            var products = _db.Products
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var p in products)
            {
                var card = BuildProductCard(p);
                productsFlow.Controls.Add(card);
            }
        }

        private Control BuildProductCard(Product p)
        {
            var card = new Panel
            {
                Width = 260,
                Height = 350,
                Margin = new Padding(15),
                BackColor = lightColor,
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.Hand
            };

            // Add shadow effect
            card.Paint += (s, e) => {
                // Draw border with secondary color
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    secondaryColor, 1, ButtonBorderStyle.Solid,
                    secondaryColor, 1, ButtonBorderStyle.Solid,
                    secondaryColor, 1, ButtonBorderStyle.Solid,
                    secondaryColor, 1, ButtonBorderStyle.Solid);
            };

            // Add hover effect
            card.MouseEnter += (s, e) => {
                card.BackColor = Color.FromArgb(240, 235, 225);
            };
            card.MouseLeave += (s, e) => {
                card.BackColor = lightColor;
            };

            // Click event to show product details
            card.Click += (s, e) => ShowProductDetails(p);

            // picture
            var pb = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(240, 160),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };

            if (!string.IsNullOrEmpty(p.ImagePath))
            {
                try
                {
                    // Get project root and build image path
                    string projectRoot = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)));
                    string imagePath = Path.Combine(projectRoot, "images", p.ImagePath);
                    
                    if (File.Exists(imagePath))
                    {
                        pb.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        pb.Image = GeneratePlaceholderImage(p.Name, pb.Width, pb.Height);
                    }
                }
                catch { pb.Image = GeneratePlaceholderImage(p.Name, pb.Width, pb.Height); }
            }
            else
            {
                pb.Image = GeneratePlaceholderImage(p.Name, pb.Width, pb.Height);
            }

            // Add click event to picture as well
            pb.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(pb);

            var lblName = new Label
            {
                Text = p.Name,
                Location = new Point(10, 180),
                AutoSize = false,
                Width = 240,
                Height = 24,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                ForeColor = primaryColor
            };
            lblName.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(lblName);

            var lblDesc = new Label
            {
                Text = p.Description ?? "",
                Location = new Point(10, 210),
                Size = new Size(240, 48),
                AutoEllipsis = true,
                Cursor = Cursors.Hand,
                ForeColor = textColor
            };
            lblDesc.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(lblDesc);

            var lblPrice = new Label
            {
                Text = $"Price: {p.Price:N2} EGP",
                Location = new Point(10, 265),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = secondaryColor,
                Cursor = Cursors.Hand
            };
            lblPrice.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(lblPrice);

            var btnAdd = new Button
            {
                Text = "Add to Cart",
                Location = new Point(130, 260),
                Width = 120,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = lightColor,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            // Add hover effects to button
            btnAdd.MouseEnter += (s, e) => {
                btnAdd.BackColor = Color.FromArgb(240, 235, 225);
                btnAdd.FlatAppearance.BorderColor = primaryColor;
                btnAdd.FlatAppearance.BorderSize = 2;
            };

            btnAdd.MouseLeave += (s, e) => {
                btnAdd.BackColor = lightColor;
                btnAdd.FlatAppearance.BorderColor = secondaryColor;
                btnAdd.FlatAppearance.BorderSize = 1;
            };

            btnAdd.FlatAppearance.BorderColor = secondaryColor;
            btnAdd.FlatAppearance.BorderSize = 1;

            btnAdd.Click += (s, e) => AddToCart(p);
            card.Controls.Add(btnAdd);

            return card;
        }

        private void ShowProductDetails(Product product)
        {
            // Create a new form to show product details
            var detailForm = new Form
            {
                Text = product.Name,
                Width = 600,
                Height = 500,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = backgroundColor
            };

            // Main panel
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = backgroundColor
            };
            detailForm.Controls.Add(mainPanel);

            // Product image
            var pictureBox = new PictureBox
            {
                Size = new Size(250, 250),
                Location = new Point(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                try
                {
                    // Get project root and build image path
                    string projectRoot = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)));
                    string imagePath = Path.Combine(projectRoot, "images", product.ImagePath);
                    
                    if (File.Exists(imagePath))
                    {
                        pictureBox.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        pictureBox.Image = GeneratePlaceholderImage(product.Name, pictureBox.Width, pictureBox.Height);
                    }
                }
                catch
                {
                    pictureBox.Image = GeneratePlaceholderImage(product.Name, pictureBox.Width, pictureBox.Height);
                }
            }
            else
            {
                pictureBox.Image = GeneratePlaceholderImage(product.Name, pictureBox.Width, pictureBox.Height);
            }
            mainPanel.Controls.Add(pictureBox);

            // Product details
            int rightColumnX = 300;
            int yPos = 20;

            var nameLabel = new Label
            {
                Text = product.Name,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(rightColumnX, yPos),
                AutoSize = true,
                ForeColor = primaryColor
            };
            mainPanel.Controls.Add(nameLabel);

            yPos += 40;

            var priceLabel = new Label
            {
                Text = $"Price: {product.Price:N2} EGP",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(rightColumnX, yPos),
                AutoSize = true,
                ForeColor = secondaryColor
            };
            mainPanel.Controls.Add(priceLabel);

            yPos += 40;

            var descLabel = new Label
            {
                Text = "Description:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(rightColumnX, yPos),
                AutoSize = true,
                ForeColor = primaryColor
            };
            mainPanel.Controls.Add(descLabel);

            yPos += 30;

            var descText = new TextBox
            {
                Text = product.Description ?? "No description available",
                Multiline = true,
                ReadOnly = true,
                Location = new Point(rightColumnX, yPos),
                Size = new Size(250, 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10)
            };
            mainPanel.Controls.Add(descText);

            yPos += 120;

            var stockLabel = new Label
            {
                Text = $"In Stock: {product.StockQuantity}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(rightColumnX, yPos),
                AutoSize = true,
                ForeColor = textColor
            };
            mainPanel.Controls.Add(stockLabel);

            yPos += 40;

            var addButton = CreateStyledButton("Add to Cart", new Point(rightColumnX, yPos), 120, 35);
            addButton.Click += (s, e) => {
                AddToCart(product);
                detailForm.Close();
            };
            mainPanel.Controls.Add(addButton);

            var closeButton = CreateStyledButton("Close", new Point(rightColumnX + 130, yPos), 120, 35);
            closeButton.Click += (s, e) => detailForm.Close();
            mainPanel.Controls.Add(closeButton);

            detailForm.ShowDialog();
        }

        private Image GeneratePlaceholderImage(string text, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                var rc = new Rectangle(0, 0, width, height);
                using var f = new Font("Segoe UI", 12, FontStyle.Bold);
                g.DrawString(text, f, new SolidBrush(primaryColor), rc, sf);
            }
            return bmp;
        }

        #endregion

        #region Cart Logic & UI

        private void LoadOrCreateCartForCurrentUser()
        {
            if (currentUser == null) { currentCart = null; return; }

            currentCart = _db.Carts.SingleOrDefault(c => c.UserId == currentUser.Id);
            if (currentCart == null)
            {
                currentCart = new Cart
                {
                    UserId = currentUser.Id,
                    orderNumber = new Random().Next(1000, 100000),
                    CartProducts = new List<CartProduct>(),
                    OrderTotalPrice = 0m,
                    CreatedAt = DateTime.Now
                };
                _db.Carts.Add(currentCart);
                _db.SaveChanges();
            }
        }

        private void AddToCart(Product product)
        {
            if (currentUser == null)
            {
                // guest -> add to guestCart
                if (!guestCart.ContainsKey(product.Id)) guestCart[product.Id] = 0;
                guestCart[product.Id]++;

                // Show notification animation
                ShowNotification("Added to guest cart.", true);
                return;
            }

            // persisted cart
            if (currentCart == null) LoadOrCreateCartForCurrentUser();

            var existing = _db.CartProducts.SingleOrDefault(cp => cp.CartId == currentCart.Id && cp.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += 1;
                _db.Update(existing);
            }
            else
            {
                var cp = new CartProduct
                {
                    CartId = currentCart.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    CreatedAt = DateTime.Now
                };
                _db.CartProducts.Add(cp);
            }
            _db.SaveChanges();

            // recalc total
            RecalculateCartTotal(currentCart);

            // Show notification animation
            ShowNotification("Added to cart.", true);
        }

        private void ShowNotification(string message, bool isSuccess)
        {
            var notification = new Label
            {
                Text = message,
                BackColor = Color.Black,  // Keep black background as requested
                ForeColor = isSuccess ? successColor : errorColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40,
                Width = 300,
                Location = new Point((this.Width - 300) / 2, -40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(notification);
            notification.BringToFront();

            // Animate notification sliding down and up
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            int counter = 0;
            timer.Tick += (s, e) => {
                counter++;
                if (counter <= 20)
                {
                    notification.Top += 2;
                }
                else if (counter > 50 && counter <= 70)
                {
                    notification.Top -= 2;
                }
                else if (counter > 70)
                {
                    timer.Stop();
                    this.Controls.Remove(notification);
                    notification.Dispose();
                }
            };
            timer.Start();
        }

        private void RecalculateCartTotal(Cart cart)
        {
            _db.Entry(cart).Collection(c => c.CartProducts).Load();
            decimal total = 0m;
            foreach (var cp in cart.CartProducts)
            {
                _db.Entry(cp).Reference(c => c.Product).Load();
                total += cp.Quantity * cp.Product.Price;
            }
            cart.OrderTotalPrice = total;
            _db.SaveChanges();
        }

        private void BuildCartView()
        {
            viewCart.Controls.Clear();

            var title = new Label
            {
                Text = "Your Cart",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewCart.Controls.Add(title);

            var pnlItems = new Panel
            {
                Location = new Point(12, 48),
                Size = new Size(940, 520),
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                BackColor = backgroundColor
            };
            viewCart.Controls.Add(pnlItems);

            if (currentUser == null)
            {
                // show guest cart items
                if (guestCart.Count == 0)
                {
                    pnlItems.Controls.Add(new Label
                    {
                        Text = "Your (guest) cart is empty.",
                        Location = new Point(10, 10),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11),
                        ForeColor = textColor
                    });
                }
                else
                {
                    int y = 10;
                    foreach (var kv in guestCart)
                    {
                        var prod = _db.Products.Find(kv.Key);
                        if (prod == null) continue;

                        var itemPanel = CreateBorderedPanel(secondaryColor);
                        itemPanel.Location = new Point(10, y);
                        itemPanel.Size = new Size(900, 60);
                        itemPanel.BackColor = lightColor;
                        pnlItems.Controls.Add(itemPanel);

                        var lbl = new Label
                        {
                            Text = $"{prod.Name}",
                            Location = new Point(10, 10),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = primaryColor
                        };
                        itemPanel.Controls.Add(lbl);

                        var lblQty = new Label
                        {
                            Text = $"Qty: {kv.Value}",
                            Location = new Point(10, 35),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 9),
                            ForeColor = textColor
                        };
                        itemPanel.Controls.Add(lblQty);

                        var lblPrice = new Label
                        {
                            Text = $"Price: {prod.Price:N2} EGP",
                            Location = new Point(200, 10),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 10),
                            ForeColor = textColor
                        };
                        itemPanel.Controls.Add(lblPrice);

                        var lblTotal1 = new Label
                        {
                            Text = $"Total: {(prod.Price * kv.Value):N2} EGP",
                            Location = new Point(200, 35),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = secondaryColor
                        };
                        itemPanel.Controls.Add(lblTotal1);

                        var btnPlus = CreateCartButton("+", 400, 15, Color.LightGreen);
                        var btnMinus = CreateCartButton("-", 440, 15, Color.LightCoral);
                        var btnDel = CreateCartButton("Delete", 480, 15, Color.FromArgb(255, 100, 100));

                        itemPanel.Controls.AddRange(new Control[] { btnPlus, btnMinus, btnDel });

                        int pid = kv.Key;
                        btnPlus.Click += (s, e) => { guestCart[pid]++; BuildCartView(); };
                        btnMinus.Click += (s, e) => { guestCart[pid] = Math.Max(0, guestCart[pid] - 1); if (guestCart[pid] == 0) guestCart.Remove(pid); BuildCartView(); };
                        btnDel.Click += (s, e) => { guestCart.Remove(pid); BuildCartView(); };

                        y += 70;
                    }
                }

                var btnCheckout = CreateStyledButton("Checkout (register/login required)", new Point(12, 580), 300, 40);
                btnCheckout.Click += (s, e) => {
                    ShowNotification("Please register or login to complete checkout.", false);
                    ShowChooseView();
                };
                viewCart.Controls.Add(btnCheckout);
                return;
            }

            // persisted user cart
            if (currentCart == null) LoadOrCreateCartForCurrentUser();

            _db.Entry(currentCart).Collection(c => c.CartProducts).Load();
            if (currentCart.CartProducts == null || currentCart.CartProducts.Count == 0)
            {
                pnlItems.Controls.Add(new Label
                {
                    Text = "Your cart is empty.",
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = textColor
                });
            }
            else
            {
                int y = 10;
                foreach (var cp in currentCart.CartProducts)
                {
                    _db.Entry(cp).Reference(x => x.Product).Load();
                    var prod = cp.Product;

                    var itemPanel = CreateBorderedPanel(secondaryColor);
                    itemPanel.Location = new Point(10, y);
                    itemPanel.Size = new Size(900, 60);
                    itemPanel.BackColor = lightColor;
                    pnlItems.Controls.Add(itemPanel);

                    var lbl = new Label
                    {
                        Text = $"{prod.Name}",
                        Location = new Point(10, 10),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = primaryColor
                    };
                    itemPanel.Controls.Add(lbl);

                    var lblQty = new Label
                    {
                        Text = $"Qty: {cp.Quantity}",
                        Location = new Point(10, 35),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9),
                        ForeColor = textColor
                    };
                    itemPanel.Controls.Add(lblQty);

                    var lblPrice = new Label
                    {
                        Text = $"Price: {prod.Price:N2} EGP",
                        Location = new Point(200, 10),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10),
                        ForeColor = textColor
                    };
                    itemPanel.Controls.Add(lblPrice);

                    var lblTotal2 = new Label
                    {
                        Text = $"Total: {(prod.Price * cp.Quantity):N2} EGP",
                        Location = new Point(200, 35),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = secondaryColor
                    };
                    itemPanel.Controls.Add(lblTotal2);

                    var btnPlus = CreateCartButton("+", 400, 15, Color.LightGreen);
                    var btnMinus = CreateCartButton("-", 440, 15, Color.LightCoral);
                    var btnDel = CreateCartButton("Delete", 480, 15, Color.FromArgb(255, 100, 100));

                    itemPanel.Controls.AddRange(new Control[] { btnPlus, btnMinus, btnDel });

                    int cpId = cp.Id;
                    btnPlus.Click += (s, e) =>
                    {
                        var item = _db.CartProducts.Find(cpId);
                        item.Quantity++;
                        _db.SaveChanges();
                        RecalculateCartTotal(currentCart);
                        BuildCartView();
                    };
                    btnMinus.Click += (s, e) =>
                    {
                        var item = _db.CartProducts.Find(cpId);
                        item.Quantity = Math.Max(0, item.Quantity - 1);
                        if (item.Quantity == 0) _db.CartProducts.Remove(item);
                        _db.SaveChanges();
                        RecalculateCartTotal(currentCart);
                        BuildCartView();
                    };
                    btnDel.Click += (s, e) =>
                    {
                        var item = _db.CartProducts.Find(cpId);
                        _db.CartProducts.Remove(item);
                        _db.SaveChanges();
                        RecalculateCartTotal(currentCart);
                        BuildCartView();
                    };

                    y += 70;
                }
            }

            // show total + checkout
            var lblTotal = new Label
            {
                Text = $"Total: {currentCart.OrderTotalPrice:N2} EGP",
                Location = new Point(12, 540),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = secondaryColor
            };
            viewCart.Controls.Add(lblTotal);

            var btnCheckoutUser = CreateStyledButton("Place Order", new Point(12, 570), 140, 40);
            btnCheckoutUser.Click += (s, e) =>
            {
                // create Order and ProductOrders
                var newOrder = new Order
                {
                    UserId = currentUser.Id,
                    OrderDate = DateTime.Now,
                    OrderTotalPrice = currentCart.OrderTotalPrice
                };
                _db.Orders.Add(newOrder);
                _db.SaveChanges(); // ensure Order.Id set

                // Add ProductOrders
                _db.Entry(currentCart).Collection(c => c.CartProducts).Load();
                foreach (var cp in currentCart.CartProducts)
                {
                    var po = new ProductOrder
                    {
                        OrderId = newOrder.Id,
                        ProductId = cp.ProductId,
                        Quantity = cp.Quantity,
                        CreatedAt = DateTime.Now
                    };
                    _db.ProductOrders.Add(po);
                }

                // clear cart
                _db.CartProducts.RemoveRange(currentCart.CartProducts);
                currentCart.OrderTotalPrice = 0;
                _db.SaveChanges();

                ShowNotification("Order placed successfully!", true);
                BuildCartView();
                ShowOrdersView();
            };
            viewCart.Controls.Add(btnCheckoutUser);
        }

        private Button CreateCartButton(string text, int x, int y, Color backColor)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Width = text == "Delete" ? 80 : 30,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            // Add hover effect
            btn.MouseEnter += (s, e) => {
                btn.BackColor = ControlPaint.Light(backColor);
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = backColor;
            };

            return btn;
        }

        #endregion

        #region Orders & Admin

        private void BuildOrdersView()
        {
            viewOrders.Controls.Clear();
            var title = new Label
            {
                Text = "Your Orders",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewOrders.Controls.Add(title);

            var pnl = new Panel
            {
                Location = new Point(12, 48),
                Size = new Size(940, 560),
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                BackColor = backgroundColor
            };
            viewOrders.Controls.Add(pnl);

            if (currentUser == null)
            {
                pnl.Controls.Add(new Label
                {
                    Text = "Please login to view your orders.",
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = textColor
                });
                return;
            }

            var orders = _db.Orders.Where(o => o.UserId == currentUser.Id).OrderByDescending(o => o.OrderDate).ToList();
            if (orders.Count == 0)
            {
                pnl.Controls.Add(new Label
                {
                    Text = "You haven't placed any orders yet.",
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = textColor
                });
                return;
            }

            int y = 10;
            foreach (var o in orders)
            {
                var orderPanel = CreateBorderedPanel(secondaryColor);
                orderPanel.Location = new Point(10, y);
                orderPanel.Size = new Size(900, 100);
                orderPanel.BackColor = lightColor;
                pnl.Controls.Add(orderPanel);

                var lbl = new Label
                {
                    Text = $"Order #{o.Id} - Date: {o.OrderDate:g}",
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = primaryColor
                };
                orderPanel.Controls.Add(lbl);

                var lblTotal = new Label
                {
                    Text = $"Total: {o.OrderTotalPrice:N2} EGP",
                    Location = new Point(10, 40),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = secondaryColor
                };
                orderPanel.Controls.Add(lblTotal);

                // list items
                var pos = _db.ProductOrders.Where(po => po.OrderId == o.Id).ToList();
                int itemY = 70;
                foreach (var po in pos)
                {
                    _db.Entry(po).Reference(x => x.Product).Load();
                    var lblItem = new Label
                    {
                        Text = $"   {po.Product.Name} x{po.Quantity}",
                        Location = new Point(14, itemY),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10),
                        ForeColor = textColor
                    };
                    orderPanel.Controls.Add(lblItem);
                    itemY += 22;
                }

                orderPanel.Height = itemY + 10;
                y += orderPanel.Height + 10;
            }
        }

        private void BuildAdminView()
        {
            viewAdmin.Controls.Clear();
            var lbl = new Label
            {
                Text = "Admin Panel",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(lbl);

            // categories management simple
            var btnManageProducts = CreateStyledButton("Manage Products", new Point(12, 60), 180, 45);
            var btnManageCategories = CreateStyledButton("Manage Categories", new Point(200, 60), 180, 45);
            viewAdmin.Controls.AddRange(new Control[] { btnManageProducts, btnManageCategories });

            btnManageProducts.Click += (s, e) => BuildAdminProductsEditor();
            btnManageCategories.Click += (s, e) => BuildAdminCategoriesEditor();
        }

        private void BuildAdminProductsEditor()
        {
            viewAdmin.Controls.Clear();
            var title = new Label
            {
                Text = "Products Editor",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(title);

            var dgv = new DataGridView
            {
                Location = new Point(12, 48),
                Size = new Size(760, 500),
                ReadOnly = false,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                ForeColor = textColor,
                BorderStyle = BorderStyle.Fixed3D,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = secondaryColor,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = textColor,
                    Font = new Font("Segoe UI", 10)
                }
            };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Id", DataPropertyName = "Id", ReadOnly = true, Width = 40 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Description", DataPropertyName = "Description", Width = 240 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "StockQuantity", Width = 80 });

            var products = _db.Products.ToList();
            dgv.DataSource = new BindingSource { DataSource = products };
            viewAdmin.Controls.Add(dgv);

            var btnSave = CreateAdminButton("Save Changes", 780, 48);
            var btnAdd = CreateAdminButton("Add New", 780, 90);
            var btnDelete = CreateAdminButton("Delete Selected", 780, 132);
            var btnBack = CreateAdminButton("Back", 780, 174);

            viewAdmin.Controls.AddRange(new Control[] { btnSave, btnAdd, btnDelete, btnBack });

            btnBack.Click += (s, e) => BuildAdminView();

            btnSave.Click += (s, e) =>
            {
                // Save grid edits to DB
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.DataBoundItem is Product p)
                    {
                        _db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
                _db.SaveChanges();
                ShowNotification("Saved products.", true);
            };

            btnAdd.Click += (s, e) =>
            {
                var newP = new Product { Name = "New Product", Description = "", Price = 0m, StockQuantity = 0, CreatedAt = DateTime.Now };
                _db.Products.Add(newP);
                _db.SaveChanges();
                dgv.DataSource = _db.Products.ToList();
                ShowNotification("Product added.", true);
            };

            btnDelete.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is Product p)
                {
                    if (MessageBox.Show("Delete product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _db.Products.Remove(p);
                        _db.SaveChanges();
                        dgv.DataSource = _db.Products.ToList();
                        ShowNotification("Product deleted.", true);
                    }
                }
            };
        }

        private Button CreateAdminButton(string text, int x, int y)
        {
            return CreateStyledButton(text, new Point(x, y), 160, 35);
        }

        private void BuildAdminCategoriesEditor()
        {
            viewAdmin.Controls.Clear();
            var title = new Label
            {
                Text = "Categories Editor",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(title);

            var dgv = new DataGridView
            {
                Location = new Point(12, 48),
                Size = new Size(760, 500),
                ReadOnly = false,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                ForeColor = textColor,
                BorderStyle = BorderStyle.Fixed3D,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = secondaryColor,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = textColor,
                    Font = new Font("Segoe UI", 10)
                }
            };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Id", DataPropertyName = "Id", ReadOnly = true, Width = 40 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Description", DataPropertyName = "Description", Width = 420 });

            dgv.DataSource = new BindingSource { DataSource = _db.Categories.ToList() };
            viewAdmin.Controls.Add(dgv);

            var btnSave = CreateAdminButton("Save Changes", 780, 48);
            var btnAdd = CreateAdminButton("Add New", 780, 90);
            var btnDelete = CreateAdminButton("Delete Selected", 780, 132);
            var btnBack = CreateAdminButton("Back", 780, 174);

            viewAdmin.Controls.AddRange(new Control[] { btnSave, btnAdd, btnDelete, btnBack });

            btnBack.Click += (s, e) => BuildAdminView();

            btnSave.Click += (s, e) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.DataBoundItem is Category c) _db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                _db.SaveChanges();
                ShowNotification("Saved categories.", true);
            };

            btnAdd.Click += (s, e) =>
            {
                var c = new Category { Name = "New Category", Description = "", CreatedAt = DateTime.Now };
                _db.Categories.Add(c);
                _db.SaveChanges();
                dgv.DataSource = _db.Categories.ToList();
                ShowNotification("Category added.", true);
            };

            btnDelete.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is Category c)
                {
                    if (MessageBox.Show("Delete category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _db.Categories.Remove(c);
                        _db.SaveChanges();
                        dgv.DataSource = _db.Categories.ToList();
                        ShowNotification("Category deleted.", true);
                    }
                }
            };
        }

        #endregion

        #region Dispose DB on close

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _db.Dispose();
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}