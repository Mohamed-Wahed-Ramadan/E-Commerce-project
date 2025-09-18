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
using Autofac;
using E_Commerce.application.Interfaces;
using E_Commerce.DTOs.User;

namespace WinForms.pressentation
{
    public partial class Form1 : Form
    {
        // Services injected via Autofac
        private readonly IUserServices _userService;
        private readonly IProductServices _productService;
        private readonly ICartServices _cartService;
        private readonly IOrderServices _orderService;
        private readonly ICategoryServices _categoryService;
        private readonly IContainer _container;

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

        // Search and Filter controls
        private TextBox searchTextBox;
        private ComboBox categoryComboBox;

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
        private Color successColor = Color.FromArgb(0, 150, 0);        // Darker green for success
        private Color errorColor = Color.FromArgb(200, 0, 0);          // Darker red for error
        private Color warningColor = Color.FromArgb(255, 165, 0);      // Orange for warnings

        // Logo path - Updated to use project structure
        private string logoPath;
        private Image logoImage;

        public Form1()
        {
            InitializeComponent();

            // Initialize Autofac container and resolve services
            _container = Autofac.Inject();
            _userService = _container.Resolve<IUserServices>();
            _productService = _container.Resolve<IProductServices>();
            _cartService = _container.Resolve<ICartServices>();
            _orderService = _container.Resolve<IOrderServices>();
            _categoryService = _container.Resolve<ICategoryServices>();

            // Set logo path - E-Commerce-project\images\logo.png
            string projectRoot = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)));
            logoPath = Path.Combine(projectRoot, "images", "logo.png");

            this.Text = "E-Commerce App";
            this.Width = 1400;  // Increased width
            this.Height = 900;  // Increased height
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = backgroundColor;
            this.FormBorderStyle = FormBorderStyle.Sizable;  // Allow resizing
            this.MinimumSize = new Size(1200, 800);  // Set minimum size

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
                Padding = new Padding(15)
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

            // top-right quick buttons with modern style - Better positioning for larger form
            var buttons = new List<Button>();

            var btnHome = CreateModernButton("Home", this.Width - 480);
            btnHome.Click += (s, e) => ShowHomeView();
            buttons.Add(btnHome);

            var btnCart = CreateModernButton("Cart", this.Width - 400);
            btnCart.Click += (s, e) => ShowCartView();
            buttons.Add(btnCart);

            var btnOrders = CreateModernButton("Orders", this.Width - 320);
            btnOrders.Click += (s, e) => ShowOrdersView();
            buttons.Add(btnOrders);

            var btnUser = CreateModernButton("User: Guest", this.Width - 640);
            btnUser.Click += (s, e) =>
            {
                if (currentUser == null)
                {
                    ShowChooseView();
                }
                else
                {
                    ShowUserMenu(btnUser);
                }
            };
            buttons.Add(btnUser);

            // Handle form resize to reposition buttons
            this.Resize += (s, e) => {
                if (buttons.Count >= 4)
                {
                    buttons[0].Location = new Point(this.Width - 480, 22);  // Home
                    buttons[1].Location = new Point(this.Width - 400, 22);  // Cart
                    buttons[2].Location = new Point(this.Width - 320, 22);  // Orders
                    buttons[3].Location = new Point(this.Width - 640, 22);  // User
                }
            };

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
                Padding = new Padding(8, 5, 8, 5),  // Better padding
                MinimumSize = new Size(70, 36)
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
            // Choose view (Login / Register / Browse) - Content will be built dynamically
            viewChoose = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(30),
                AutoScroll = true
            };

            // Home view (product listing) - Improved layout
            viewHome = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(15),
                AutoScroll = true
            };

            // Search and Filter panel with borders
            var searchFilterPanel = CreateBorderedPanel(secondaryColor, 2);
            searchFilterPanel.Dock = DockStyle.Top;
            searchFilterPanel.Height = 80;
            searchFilterPanel.BackColor = lightColor;
            searchFilterPanel.Padding = new Padding(15);

            var searchLabel = new Label
            {
                Text = "Search Products:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = primaryColor,
                Location = new Point(15, 15),
                Size = new Size(120, 23),
                TextAlign = ContentAlignment.MiddleLeft
            };
            searchFilterPanel.Controls.Add(searchLabel);

            searchTextBox = new TextBox
            {
                Name = "txtSearch",
                Location = new Point(140, 12),
                Width = 250,
                Height = 30,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = textColor
            };
            searchFilterPanel.Controls.Add(searchTextBox);

            var categoryLabel = new Label
            {
                Text = "Category:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = primaryColor,
                Location = new Point(410, 15),
                Size = new Size(70, 23),
                TextAlign = ContentAlignment.MiddleLeft
            };
            searchFilterPanel.Controls.Add(categoryLabel);

            categoryComboBox = new ComboBox
            {
                Location = new Point(485, 12),
                Width = 180,
                Height = 30,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = textColor
            };
            LoadCategories();
            searchFilterPanel.Controls.Add(categoryComboBox);

            var searchButton = CreateStyledButton("Search", new Point(680, 10), 80, 35);
            searchButton.Click += (s, e) => {
                string searchTerm = searchTextBox.Text.Trim();
                int? categoryId = null;
                if (categoryComboBox.SelectedItem is Category selectedCategory && selectedCategory.Id != -1)
                {
                    categoryId = selectedCategory.Id;
                }
                LoadProducts(searchTerm, categoryId);
            };
            searchFilterPanel.Controls.Add(searchButton);

            var clearButton = CreateStyledButton("Clear", new Point(770, 10), 80, 35);
            clearButton.Click += (s, e) => {
                searchTextBox.Text = "";
                categoryComboBox.SelectedIndex = 0; // Select "All Categories"
                LoadProducts();
            };
            searchFilterPanel.Controls.Add(clearButton);

            // Add real-time search on text change
            searchTextBox.TextChanged += (s, e) => {
                var timer = searchTextBox.Tag as System.Windows.Forms.Timer;
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                }

                timer = new System.Windows.Forms.Timer();
                timer.Interval = 500; // 500ms delay
                timer.Tick += (sender, args) => {
                    timer.Stop();
                    timer.Dispose();
                    string searchTerm = searchTextBox.Text.Trim();
                    int? categoryId = null;
                    if (categoryComboBox.SelectedItem is Category selectedCategory && selectedCategory.Id != -1)
                    {
                        categoryId = selectedCategory.Id;
                    }
                    LoadProducts(searchTerm, categoryId);
                };
                timer.Start();
                searchTextBox.Tag = timer;
            };

            // Category filter change event
            categoryComboBox.SelectedIndexChanged += (s, e) => {
                string searchTerm = searchTextBox.Text.Trim();
                int? categoryId = null;
                if (categoryComboBox.SelectedItem is Category selectedCategory && selectedCategory.Id != -1)
                {
                    categoryId = selectedCategory.Id;
                }
                LoadProducts(searchTerm, categoryId);
            };

            viewHome.Controls.Add(searchFilterPanel);

            productsFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(15),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = backgroundColor
            };
            viewHome.Controls.Add(productsFlow);

            // Cart view - Improved sizing
            viewCart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Orders view - Improved sizing
            viewOrders = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Admin view - Improved sizing
            viewAdmin = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // add views to body (we'll show/hide)
            pnlBody.Controls.AddRange(new Control[] { viewChoose, viewHome, viewCart, viewOrders, viewAdmin });
        }

        private void LoadCategories()
        {
            try
            {
                categoryComboBox.Items.Clear();

                // Add "All Categories" option
                categoryComboBox.Items.Add(new Category { Id = -1, Name = "All Categories" });
                categoryComboBox.DisplayMember = "Name";
                categoryComboBox.ValueMember = "Id";

                // Add actual categories
                var categories = _categoryService.GetAllCategory();
                foreach (var category in categories)
                {
                    categoryComboBox.Items.Add(category);
                }

                categoryComboBox.SelectedIndex = 0; // Select "All Categories" by default
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading categories: {ex.Message}", false);
            }
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
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
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
            // Clear existing controls and rebuild based on current user state
            viewChoose.Controls.Clear();
            BuildChooseViewContent();
            ShowViewWithAnimation(viewChoose);
        }

        private void BuildChooseViewContent()
        {
            var lbl = new Label
            {
                Text = currentUser == null ? "Welcome to E-Commerce App" : $"Welcome back, {currentUser.UserName}!",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(30, 30),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewChoose.Controls.Add(lbl);

            var subLbl = new Label
            {
                Text = currentUser == null ? "Please choose an option to continue" : "Choose an option below or continue shopping",
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 75),
                AutoSize = true,
                ForeColor = textColor
            };
            viewChoose.Controls.Add(subLbl);

            if (currentUser == null)
            {
                // Show login/register options for guests
                var btnLogin = CreateStyledButton("Login", new Point(30, 130), 180, 50);
                var btnRegister = CreateStyledButton("Register", new Point(230, 130), 180, 50);
                var btnGuest = CreateStyledButton("Browse as Guest", new Point(430, 130), 200, 50);

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
            }
            else
            {
                // Show options for logged-in users
                var btnContinueShopping = CreateStyledButton("Continue Shopping", new Point(30, 130), 200, 50);
                var btnViewCart = CreateStyledButton("View My Cart", new Point(250, 130), 180, 50);
                var btnViewOrders = CreateStyledButton("View My Orders", new Point(450, 130), 180, 50);
                var btnSignOut = CreateStyledButton("Sign Out", new Point(650, 130), 150, 50);

                // Style sign out button differently
                btnSignOut.BackColor = Color.FromArgb(255, 240, 240);
                btnSignOut.ForeColor = errorColor;

                viewChoose.Controls.AddRange(new Control[] { btnContinueShopping, btnViewCart, btnViewOrders, btnSignOut });

                btnContinueShopping.Click += (s, e) => ShowHomeView();
                btnViewCart.Click += (s, e) => ShowCartView();
                btnViewOrders.Click += (s, e) => ShowOrdersView();
                btnSignOut.Click += (s, e) => SignOut();

                // Show admin option if user is admin
                if (currentUser.Role == UserRole.Admin)
                {
                    var btnAdmin = CreateStyledButton("Admin Panel", new Point(30, 200), 180, 50);
                    btnAdmin.BackColor = Color.FromArgb(240, 245, 255);
                    btnAdmin.ForeColor = secondaryColor;
                    btnAdmin.Click += (s, e) => ShowAdminView();
                    viewChoose.Controls.Add(btnAdmin);
                }
            }
        }

        private void ShowHomeView()
        {
            ShowViewWithAnimation(viewHome);
            LoadProducts(); // Load all products initially
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
            if (currentUser == null || currentUser.Role != UserRole.Admin)
            {
                ShowNotification("Admin page only for admin users.", false);
                return;
            }
            ShowViewWithAnimation(viewAdmin);
            BuildAdminView();
        }

        #endregion

        #region Auth (Login/Register) - Using UserService

        private void BuildAuthForm(bool isLogin)
        {
            // Improved popup-like panel inside viewChoose
            var pnl = new Panel
            {
                Size = new Size(500, 350),
                Location = new Point(50, 150),
                BorderStyle = BorderStyle.None,
                BackColor = lightColor,
                Padding = new Padding(20)
            };

            // Add border to panel
            pnl.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, pnl.ClientRectangle,
                    secondaryColor, 2, ButtonBorderStyle.Solid,
                    secondaryColor, 2, ButtonBorderStyle.Solid,
                    secondaryColor, 2, ButtonBorderStyle.Solid,
                    secondaryColor, 2, ButtonBorderStyle.Solid);
            };

            viewChoose.Controls.Add(pnl);
            pnl.BringToFront();

            pnl.Controls.Add(new Label
            {
                Text = isLogin ? "Login" : "Register",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            });

            int yPos = 70;
            pnl.Controls.Add(new Label
            {
                Text = "Email:",
                Location = new Point(20, yPos),
                Size = new Size(80, 23),
                Font = new Font("Segoe UI", 11),
                ForeColor = textColor
            });
            var txtEmail = new TextBox
            {
                Location = new Point(110, yPos - 2),
                Width = 350,
                Height = 30,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = textColor,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnl.Controls.Add(txtEmail);

            yPos += 50;
            pnl.Controls.Add(new Label
            {
                Text = "Username:",
                Location = new Point(20, yPos),
                Size = new Size(80, 23),
                Font = new Font("Segoe UI", 11),
                ForeColor = textColor
            });
            var txtUser = new TextBox
            {
                Location = new Point(110, yPos - 2),
                Width = 350,
                Height = 30,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = textColor,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnl.Controls.Add(txtUser);

            yPos += 50;
            pnl.Controls.Add(new Label
            {
                Text = "Password:",
                Location = new Point(20, yPos),
                Size = new Size(80, 23),
                Font = new Font("Segoe UI", 11),
                ForeColor = textColor
            });
            var txtPass = new TextBox
            {
                Location = new Point(110, yPos - 2),
                Width = 350,
                Height = 30,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = textColor,
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            pnl.Controls.Add(txtPass);

            var btnSubmit = CreateStyledButton(isLogin ? "Login" : "Register", new Point(110, yPos + 60), 140, 40);
            pnl.Controls.Add(btnSubmit);

            var btnCancel = CreateStyledButton("Cancel", new Point(270, yPos + 60), 120, 40);
            pnl.Controls.Add(btnCancel);

            btnCancel.Click += (s, e) => { viewChoose.Controls.Remove(pnl); pnl.Dispose(); };

            btnSubmit.Click += async (s, e) =>
            {
                var email = txtEmail.Text.Trim();
                var username = txtUser.Text.Trim();
                var pass = txtPass.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || (!isLogin && string.IsNullOrEmpty(username)))
                {
                    ShowNotification("Please fill all required fields.", false);
                    return;
                }

                if (isLogin)
                {
                    try
                    {
                        var (userResponse, message) = await _userService.LoginAsync(email, pass);
                        if (userResponse != null)
                        {
                            // Convert UserResponse back to User for compatibility
                            currentUser = new User
                            {
                                Id = userResponse.Id,
                                Email = userResponse.Email,
                                UserName = userResponse.Name,
                                Role = userResponse.Role
                            };

                            LoadCurrentUserCart();
                            ShowNotification("Logged in successfully.", true);
                            viewChoose.Controls.Remove(pnl);
                            pnl.Dispose();
                            UpdateHeaderUser();
                            ShowHomeView();

                            // if admin email -> open admin view
                            if (currentUser.Role == UserRole.Admin) ShowAdminView();
                        }
                        else
                        {
                            ShowNotification(message, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification($"Login error: {ex.Message}", false);
                    }
                }
                else
                {
                    // register
                    try
                    {
                        var createUserDto = new CreateUserDTO
                        {
                            Email = email,
                            UserName = username,
                            Password = pass,
                        };

                        var (success, message) = await _userService.RegisterAsync(createUserDto);
                        if (success)
                        {
                            ShowNotification("Registered successfully. Please login.", true);
                            viewChoose.Controls.Remove(pnl);
                            pnl.Dispose();
                        }
                        else
                        {
                            ShowNotification(message, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification($"Registration error: {ex.Message}", false);
                    }
                }
            };
        }

        private void LoadCurrentUserCart()
        {
            if (currentUser != null)
            {
                currentCart = _cartService.GetCartByUserId(currentUser.Id);
            }
        }

        private void UpdateHeaderUser()
        {
            if (pnlHeader.Tag is Button btnUser)
            {
                btnUser.Text = currentUser == null ? "User: Guest" : $"User: {currentUser.UserName}";

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

        private void ShowUserMenu(Button userButton)
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.BackColor = lightColor;
            contextMenu.ForeColor = textColor;
            contextMenu.Font = new Font("Segoe UI", 10);

            // User info item
            var userInfoItem = new ToolStripLabel($"Logged in as: {currentUser.UserName}")
            {
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = primaryColor
            };
            contextMenu.Items.Add(userInfoItem);

            // Separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Email info
            var emailItem = new ToolStripLabel($"Email: {currentUser.Email}")
            {
                Font = new Font("Segoe UI", 9),
                ForeColor = textColor
            };
            contextMenu.Items.Add(emailItem);

            // Role info
            var roleItem = new ToolStripLabel($"Role: {currentUser.Role}")
            {
                Font = new Font("Segoe UI", 9),
                ForeColor = textColor
            };
            contextMenu.Items.Add(roleItem);

            // Separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Admin panel option (only for admins)
            if (currentUser.Role == UserRole.Admin)
            {
                var adminItem = new ToolStripMenuItem("Admin Panel")
                {
                    Font = new Font("Segoe UI", 9),
                    ForeColor = secondaryColor
                };
                adminItem.Click += (s, e) => ShowAdminView();
                contextMenu.Items.Add(adminItem);
            }

            // Sign out option
            var signOutItem = new ToolStripMenuItem("Sign Out")
            {
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = errorColor
            };
            signOutItem.Click += (s, e) => SignOut();
            contextMenu.Items.Add(signOutItem);

            // Show the menu below the user button
            var buttonLocation = userButton.PointToScreen(Point.Empty);
            contextMenu.Show(buttonLocation.X, buttonLocation.Y + userButton.Height);
        }

        private void SignOut()
        {
            // Show confirmation dialog
            var result = MessageBox.Show(
                "Are you sure you want to sign out?",
                "Confirm Sign Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Clear user session
                currentUser = null;
                currentCart = null;

                // Clear guest cart as well for security
                guestCart.Clear();

                // Update header to show guest user
                UpdateHeaderUser();

                // Show notification
                ShowNotification("You have been signed out successfully.", true);

                // Navigate to choose view
                ShowChooseView();
            }
        }

        #endregion

        #region Products / Home - Using ProductService

        private void LoadProducts(string searchTerm = "", int? categoryId = null)
        {
            productsFlow.Controls.Clear();

            try
            {
                IEnumerable<Product> products;

                if (string.IsNullOrEmpty(searchTerm) && categoryId == null)
                {
                    products = _productService.GetAllProduct();
                }
                else if (!string.IsNullOrEmpty(searchTerm) && categoryId == null)
                {
                    products = _productService.SearchProducts(searchTerm);
                }
                else if (string.IsNullOrEmpty(searchTerm) && categoryId != null)
                {
                    products = _productService.GetAllProduct().Where(p => p.CategoryId == categoryId);
                }
                else
                {
                    products = _productService.SearchProducts(searchTerm).Where(p => p.CategoryId == categoryId);
                }

                foreach (var p in products)
                {
                    var card = BuildProductCard(p);
                    productsFlow.Controls.Add(card);
                }

                // Show message if no products found
                if (!products.Any())
                {
                    var noResultsLabel = new Label
                    {
                        Text = GetNoResultsMessage(searchTerm, categoryId),
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        ForeColor = textColor,
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };
                    productsFlow.Controls.Add(noResultsLabel);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading products: {ex.Message}", false);
            }
        }

        private string GetNoResultsMessage(string searchTerm, int? categoryId)
        {
            if (string.IsNullOrEmpty(searchTerm) && categoryId == null)
            {
                return "No products available.";
            }
            else if (!string.IsNullOrEmpty(searchTerm) && categoryId == null)
            {
                return $"No products found for '{searchTerm}'";
            }
            else if (string.IsNullOrEmpty(searchTerm) && categoryId != null)
            {
                var categoryName = categoryComboBox.SelectedItem is Category cat ? cat.Name : "selected category";
                return $"No products found in {categoryName}";
            }
            else
            {
                var categoryName = categoryComboBox.SelectedItem is Category cat ? cat.Name : "selected category";
                return $"No products found for '{searchTerm}' in {categoryName}";
            }
        }

        private Control BuildProductCard(Product p)
        {
            var card = new Panel
            {
                Width = 280,
                Height = 400,
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
                Size = new Size(260, 180),
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
                Location = new Point(10, 200),
                AutoSize = false,
                Width = 260,
                Height = 24,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                ForeColor = primaryColor,
                TextAlign = ContentAlignment.TopLeft
            };
            lblName.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(lblName);

            var lblDesc = new Label
            {
                Text = p.Description ?? "",
                Location = new Point(10, 230),
                Size = new Size(260, 48),
                AutoEllipsis = true,
                Cursor = Cursors.Hand,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 9)
            };
            lblDesc.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(lblDesc);

            var lblPrice = new Label
            {
                Text = $"Price: {p.Price:N2} EGP",
                Location = new Point(10, 285),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = secondaryColor,
                Cursor = Cursors.Hand
            };
            lblPrice.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(lblPrice);

            // Stock status label
            var stockStatusLabel = new Label
            {
                Location = new Point(10, 310),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            if (p.StockQuantity > 0)
            {
                stockStatusLabel.Text = $"In Stock ({p.StockQuantity})";
                stockStatusLabel.ForeColor = successColor;
            }
            else
            {
                stockStatusLabel.Text = "Out of Stock";
                stockStatusLabel.ForeColor = errorColor;
            }

            stockStatusLabel.Click += (s, e) => ShowProductDetails(p);
            card.Controls.Add(stockStatusLabel);

            var btnAdd = new Button
            {
                Text = p.StockQuantity > 0 ? "Add to Cart" : "Out of Stock",
                Location = new Point(140, 305),
                Width = 130,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = p.StockQuantity > 0 ? lightColor : Color.LightGray,
                ForeColor = p.StockQuantity > 0 ? textColor : Color.Gray,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = p.StockQuantity > 0 ? Cursors.Hand : Cursors.Default,
                Enabled = p.StockQuantity > 0
            };

            // Add hover effects to button only if enabled
            if (p.StockQuantity > 0)
            {
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
            }
            else
            {
                btnAdd.FlatAppearance.BorderColor = Color.Gray;
                btnAdd.FlatAppearance.BorderSize = 1;
            }

            card.Controls.Add(btnAdd);

            return card;
        }

        private void ShowProductDetails(Product product)
        {
            // Create a new form to show product details
            var detailForm = new Form
            {
                Text = product.Name,
                Width = 700,
                Height = 600,
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
                Padding = new Padding(25),
                BackColor = backgroundColor
            };
            detailForm.Controls.Add(mainPanel);

            // Product image
            var pictureBox = new PictureBox
            {
                Size = new Size(300, 300),
                Location = new Point(25, 25),
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
            int rightColumnX = 350;
            int yPos = 25;

            var nameLabel = new Label
            {
                Text = product.Name,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(rightColumnX, yPos),
                Size = new Size(300, 30),
                ForeColor = primaryColor
            };
            mainPanel.Controls.Add(nameLabel);

            yPos += 45;

            var priceLabel = new Label
            {
                Text = $"Price: {product.Price:N2} EGP",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(rightColumnX, yPos),
                AutoSize = true,
                ForeColor = secondaryColor
            };
            mainPanel.Controls.Add(priceLabel);

            yPos += 45;

            // Stock status
            var stockLabel = new Label
            {
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(rightColumnX, yPos),
                AutoSize = true
            };

            if (product.StockQuantity > 0)
            {
                stockLabel.Text = $"✓ In Stock ({product.StockQuantity} available)";
                stockLabel.ForeColor = successColor;
            }
            else
            {
                stockLabel.Text = "✗ Out of Stock";
                stockLabel.ForeColor = errorColor;
            }

            mainPanel.Controls.Add(stockLabel);

            yPos += 45;

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
                Size = new Size(300, 120),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10),
                ScrollBars = ScrollBars.Vertical
            };
            mainPanel.Controls.Add(descText);

            yPos += 140;

            var addButton = CreateStyledButton(
                product.StockQuantity > 0 ? "Add to Cart" : "Out of Stock",
                new Point(rightColumnX, yPos),
                140, 40
            );

            if (product.StockQuantity > 0)
            {
                addButton.Click += (s, e) => {
                    AddToCart(product);
                    detailForm.Close();
                };
            }
            else
            {
                addButton.Enabled = false;
                addButton.BackColor = Color.LightGray;
                addButton.ForeColor = Color.Gray;
            }

            mainPanel.Controls.Add(addButton);

            var closeButton = CreateStyledButton("Close", new Point(rightColumnX + 150, yPos), 140, 40);
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
                using var f = new Font("Segoe UI", Math.Min(width, height) / 10, FontStyle.Bold);
                g.DrawString(text, f, new SolidBrush(primaryColor), rc, sf);
            }
            return bmp;
        }

        #endregion

        #region Cart Logic & UI - Using CartService

        private void AddToCart(Product product)
        {
            if (product.StockQuantity <= 0)
            {
                ShowNotification("This product is out of stock.", false);
                return;
            }

            if (currentUser == null)
            {
                // guest -> add to guestCart
                if (!guestCart.ContainsKey(product.Id)) guestCart[product.Id] = 0;

                // Check if adding one more would exceed stock
                if (guestCart[product.Id] >= product.StockQuantity)
                {
                    ShowNotification($"Cannot add more. Only {product.StockQuantity} items available in stock.", false);
                    return;
                }

                guestCart[product.Id]++;

                // Show notification animation
                ShowNotification("Added to guest cart.", true);
                return;
            }

            try
            {
                // Check current cart quantity first
                var currentCart = _cartService.GetCartByUserId(currentUser.Id);
                var existingCartProduct = currentCart?.CartProducts?.FirstOrDefault(cp => cp.ProductId == product.Id);
                int currentQuantity = existingCartProduct?.Quantity ?? 0;

                if (currentQuantity >= product.StockQuantity)
                {
                    ShowNotification($"Cannot add more. Only {product.StockQuantity} items available in stock.", false);
                    return;
                }

                // Use CartService to add product to cart
                this.currentCart = _cartService.AddProductToCart(currentUser.Id, product.Id, 1);
                _cartService.Save();

                // Show notification animation
                ShowNotification("Added to cart.", true);
            }
            catch (Exception ex)
            {
                ShowNotification($"Error adding to cart: {ex.Message}", false);
            }
        }

        private void ShowNotification(string message, bool isSuccess)
        {
            var notification = new Label
            {
                Text = message,
                BackColor = Color.Black,
                ForeColor = isSuccess ? successColor : errorColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 50,
                Width = 350,
                Location = new Point((this.Width - 350) / 2, -50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(notification);
            notification.BringToFront();

            // Animate notification sliding down and up
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 15;
            int counter = 0;
            timer.Tick += (s, e) => {
                counter++;
                if (counter <= 25)
                {
                    notification.Top += 3;
                }
                else if (counter > 80 && counter <= 105)
                {
                    notification.Top -= 3;
                }
                else if (counter > 105)
                {
                    timer.Stop();
                    this.Controls.Remove(notification);
                    notification.Dispose();
                }
            };
            timer.Start();
        }

        private void BuildCartView()
        {
            viewCart.Controls.Clear();

            var title = new Label
            {
                Text = "Your Shopping Cart",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewCart.Controls.Add(title);

            var pnlItems = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(Math.Max(1000, this.Width - 80), Math.Max(500, this.Height - 220)),
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
                        Text = "Your cart is empty.",
                        Location = new Point(10, 10),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12),
                        ForeColor = textColor
                    });
                }
                else
                {
                    int y = 10;
                    var products = _productService.GetAllProduct();
                    decimal totalPrice = 0;

                    foreach (var kv in guestCart)
                    {
                        var prod = products.FirstOrDefault(p => p.Id == kv.Key);
                        if (prod == null) continue;

                        var itemPanel = CreateBorderedPanel(secondaryColor);
                        itemPanel.Location = new Point(10, y);
                        itemPanel.Size = new Size(Math.Min(950, pnlItems.Width - 40), 80);
                        itemPanel.BackColor = lightColor;
                        pnlItems.Controls.Add(itemPanel);

                        var lbl = new Label
                        {
                            Text = $"{prod.Name}",
                            Location = new Point(15, 15),
                            Size = new Size(200, 25),
                            Font = new Font("Segoe UI", 11, FontStyle.Bold),
                            ForeColor = primaryColor
                        };
                        itemPanel.Controls.Add(lbl);

                        var lblQty = new Label
                        {
                            Text = $"Quantity: {kv.Value}",
                            Location = new Point(15, 45),
                            Size = new Size(100, 20),
                            Font = new Font("Segoe UI", 10),
                            ForeColor = textColor
                        };
                        itemPanel.Controls.Add(lblQty);

                        var lblPrice = new Label
                        {
                            Text = $"Unit Price: {prod.Price:N2} EGP",
                            Location = new Point(230, 15),
                            Size = new Size(150, 25),
                            Font = new Font("Segoe UI", 10),
                            ForeColor = textColor
                        };
                        itemPanel.Controls.Add(lblPrice);

                        decimal itemTotal = prod.Price * kv.Value;
                        totalPrice += itemTotal;

                        var lblTotal1 = new Label
                        {
                            Text = $"Total: {itemTotal:N2} EGP",
                            Location = new Point(230, 45),
                            Size = new Size(150, 20),
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = secondaryColor
                        };
                        itemPanel.Controls.Add(lblTotal1);

                        var btnPlus = CreateCartButton("+", 450, 20, successColor);
                        var btnMinus = CreateCartButton("-", 490, 20, warningColor);
                        var btnDel = CreateCartButton("Remove", 530, 20, errorColor);

                        itemPanel.Controls.AddRange(new Control[] { btnPlus, btnMinus, btnDel });

                        int pid = kv.Key;
                        btnPlus.Click += (s, e) =>
                        {
                            // Check stock availability for guest cart
                            if (guestCart[pid] >= prod.StockQuantity)
                            {
                                ShowNotification($"Cannot add more. Only {prod.StockQuantity} items available in stock.", false);
                                return;
                            }
                            guestCart[pid]++;
                            BuildCartView();
                        };
                        btnMinus.Click += (s, e) =>
                        {
                            if (guestCart[pid] <= 1)
                            {
                                var result = MessageBox.Show(
                                    "This will remove the item from your cart. Continue?",
                                    "Remove Item",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
                                );

                                if (result == DialogResult.Yes)
                                {
                                    guestCart.Remove(pid);
                                    BuildCartView();
                                }
                            }
                            else
                            {
                                guestCart[pid]--;
                                BuildCartView();
                            }
                        };
                        btnDel.Click += (s, e) =>
                        {
                            var result = MessageBox.Show(
                                $"Remove {prod.Name} from your cart?",
                                "Remove Item",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );

                            if (result == DialogResult.Yes)
                            {
                                guestCart.Remove(pid);
                                BuildCartView();
                                ShowNotification("Item removed from cart.", true);
                            }
                        };

                        y += 90;
                    }

                    // Show total for guest cart
                    var guestTotalLabel = new Label
                    {
                        Text = $"Cart Total: {totalPrice:N2} EGP",
                        Location = new Point(20, y + 10),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = secondaryColor
                    };
                    pnlItems.Controls.Add(guestTotalLabel);
                }

                var btnCheckout = CreateStyledButton("Checkout (Login Required)", new Point(20, pnlItems.Bottom + 20), 250, 45);
                btnCheckout.Click += (s, e) => {
                    ShowNotification("Please register or login to complete checkout.", false);
                    ShowChooseView();
                };
                viewCart.Controls.Add(btnCheckout);
                return;
            }

            // persisted user cart
            try
            {
                currentCart = _cartService.GetCartByUserId(currentUser.Id);

                if (currentCart == null || currentCart.CartProducts == null || currentCart.CartProducts.Count == 0)
                {
                    pnlItems.Controls.Add(new Label
                    {
                        Text = "Your cart is empty.",
                        Location = new Point(10, 10),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12),
                        ForeColor = textColor
                    });
                }
                else
                {
                    int y = 10;
                    foreach (var cp in currentCart.CartProducts)
                    {
                        var prod = cp.Product;

                        var itemPanel = CreateBorderedPanel(secondaryColor);
                        itemPanel.Location = new Point(10, y);
                        itemPanel.Size = new Size(Math.Min(950, pnlItems.Width - 40), 80);
                        itemPanel.BackColor = lightColor;
                        pnlItems.Controls.Add(itemPanel);

                        var lbl = new Label
                        {
                            Text = $"{prod.Name}",
                            Location = new Point(15, 15),
                            Size = new Size(200, 25),
                            Font = new Font("Segoe UI", 11, FontStyle.Bold),
                            ForeColor = primaryColor
                        };
                        itemPanel.Controls.Add(lbl);

                        var lblQty = new Label
                        {
                            Text = $"Quantity: {cp.Quantity}",
                            Location = new Point(15, 45),
                            Size = new Size(100, 20),
                            Font = new Font("Segoe UI", 10),
                            ForeColor = textColor
                        };
                        itemPanel.Controls.Add(lblQty);

                        var lblPrice = new Label
                        {
                            Text = $"Unit Price: {prod.Price:N2} EGP",
                            Location = new Point(230, 15),
                            Size = new Size(150, 25),
                            Font = new Font("Segoe UI", 10),
                            ForeColor = textColor
                        };
                        itemPanel.Controls.Add(lblPrice);

                        var lblTotal2 = new Label
                        {
                            Text = $"Total: {(prod.Price * cp.Quantity):N2} EGP",
                            Location = new Point(230, 45),
                            Size = new Size(150, 20),
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = secondaryColor
                        };
                        itemPanel.Controls.Add(lblTotal2);

                        var btnPlus = CreateCartButton("+", 450, 20, successColor);
                        var btnMinus = CreateCartButton("-", 490, 20, warningColor);
                        var btnDel = CreateCartButton("Remove", 530, 20, errorColor);

                        itemPanel.Controls.AddRange(new Control[] { btnPlus, btnMinus, btnDel });

                        int productId = cp.ProductId;
                        btnPlus.Click += (s, e) =>
                        {
                            try
                            {
                                // Get current product to check stock
                                var products = _productService.GetAllProduct();
                                var currentProduct = products.FirstOrDefault(p => p.Id == productId);

                                if (currentProduct == null)
                                {
                                    ShowNotification("Product not found.", false);
                                    return;
                                }

                                // Check if current quantity + 1 would exceed stock
                                if (cp.Quantity >= currentProduct.StockQuantity)
                                {
                                    ShowNotification($"Cannot add more. Only {currentProduct.StockQuantity} items available in stock.", false);
                                    return;
                                }

                                _cartService.AddProductToCart(currentUser.Id, productId, 1);
                                _cartService.Save();
                                BuildCartView();
                            }
                            catch (Exception ex)
                            {
                                ShowNotification($"Error updating cart: {ex.Message}", false);
                            }
                        };
                        btnMinus.Click += (s, e) =>
                        {
                            try
                            {
                                var cart = _cartService.GetCartByUserId(currentUser.Id);
                                var cartProduct = cart.CartProducts.FirstOrDefault(c => c.ProductId == productId);

                                if (cartProduct != null)
                                {
                                    // Check if quantity would go below 1
                                    if (cartProduct.Quantity <= 1)
                                    {
                                        // Ask user if they want to remove the item
                                        var result = MessageBox.Show(
                                            "This will remove the item from your cart. Continue?",
                                            "Remove Item",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question
                                        );

                                        if (result == DialogResult.Yes)
                                        {
                                            cart.CartProducts.Remove(cartProduct);
                                            _cartService.Save();
                                            BuildCartView();
                                        }
                                    }
                                    else
                                    {
                                        cartProduct.Quantity--;
                                        _cartService.Save();
                                        BuildCartView();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ShowNotification($"Error updating cart: {ex.Message}", false);
                            }
                        };
                        btnDel.Click += (s, e) =>
                        {
                            try
                            {
                                var result = MessageBox.Show(
                                    $"Remove {prod.Name} from your cart?",
                                    "Remove Item",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
                                );

                                if (result == DialogResult.Yes)
                                {
                                    var cart = _cartService.GetCartByUserId(currentUser.Id);
                                    var cartProduct = cart.CartProducts.FirstOrDefault(c => c.ProductId == productId);
                                    if (cartProduct != null)
                                    {
                                        cart.CartProducts.Remove(cartProduct);
                                        _cartService.Save();
                                        BuildCartView();
                                        ShowNotification("Item removed from cart.", true);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ShowNotification($"Error removing from cart: {ex.Message}", false);
                            }
                        };

                        y += 90;
                    }
                }

                // show total + checkout
                var lblTotal = new Label
                {
                    Text = $"Cart Total: {currentCart?.OrderTotalPrice ?? 0:N2} EGP",
                    Location = new Point(20, pnlItems.Bottom + 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = secondaryColor
                };
                viewCart.Controls.Add(lblTotal);

                var btnCheckoutUser = CreateStyledButton("Place Order", new Point(20, lblTotal.Bottom + 15), 180, 45);
                btnCheckoutUser.Click += (s, e) =>
                {
                    try
                    {
                        if (currentCart == null || currentCart.CartProducts == null || currentCart.CartProducts.Count == 0)
                        {
                            ShowNotification("Your cart is empty.", false);
                            return;
                        }

                        // Use OrderService to create order
                        var order = _orderService.CreateOrder(currentCart);
                        if (order != null)
                        {
                            // Clear the cart after successful order
                            _cartService.DeleteCart(currentCart);
                            currentCart = _cartService.GetCartByUserId(currentUser.Id);

                            ShowNotification("Order placed successfully!", true);
                            BuildCartView();
                            ShowOrdersView();
                        }
                        else
                        {
                            ShowNotification("Error creating order.", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowNotification($"Error placing order: {ex.Message}", false);
                    }
                };
                viewCart.Controls.Add(btnCheckoutUser);
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading cart: {ex.Message}", false);
            }
        }

        private Button CreateCartButton(string text, int x, int y, Color backColor)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Width = text == "Remove" ? 90 : 35,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
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

        #region Orders & Admin - Using OrderService

        private void BuildOrdersView()
        {
            viewOrders.Controls.Clear();
            var title = new Label
            {
                Text = "Your Order History",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewOrders.Controls.Add(title);

            var pnl = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(Math.Max(1000, this.Width - 80), Math.Max(600, this.Height - 150)),
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
                    Location = new Point(15, 15),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12),
                    ForeColor = textColor
                });
                return;
            }

            try
            {
                var orders = _orderService.GetAllOrders()
                    .Where(o => o.UserId == currentUser.Id)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

                if (orders.Count == 0)
                {
                    pnl.Controls.Add(new Label
                    {
                        Text = "You haven't placed any orders yet.",
                        Location = new Point(15, 15),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12),
                        ForeColor = textColor
                    });
                    return;
                }

                int y = 15;
                foreach (var o in orders)
                {
                    var orderPanel = CreateBorderedPanel(secondaryColor, 2);
                    orderPanel.Location = new Point(15, y);
                    orderPanel.Size = new Size(Math.Min(950, pnl.Width - 50), Math.Max(120, 80 + (o.ProductOrder.Count * 25)));
                    orderPanel.BackColor = lightColor;
                    orderPanel.Padding = new Padding(15);
                    pnl.Controls.Add(orderPanel);

                    var lbl = new Label
                    {
                        Text = $"Order #{o.Id} - {o.OrderDate:dddd, MMMM dd, yyyy 'at' h:mm tt}",
                        Location = new Point(15, 15),
                        Size = new Size(orderPanel.Width - 30, 25),
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = primaryColor
                    };
                    orderPanel.Controls.Add(lbl);

                    var lblTotal = new Label
                    {
                        Text = $"Total Amount: {o.OrderTotalPrice:N2} EGP",
                        Location = new Point(15, 45),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = secondaryColor
                    };
                    orderPanel.Controls.Add(lblTotal);

                    // List items header
                    var itemsHeader = new Label
                    {
                        Text = "Items Ordered:",
                        Location = new Point(15, 75),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        ForeColor = primaryColor
                    };
                    orderPanel.Controls.Add(itemsHeader);

                    // List items
                    int itemY = 100;
                    foreach (var po in o.ProductOrder)
                    {
                        var lblItem = new Label
                        {
                            Text = $"• {po.Product?.Name ?? "Unknown Product"} × {po.Quantity} = {((po.Product?.Price ?? 0) * po.Quantity):N2} EGP",
                            Location = new Point(30, itemY),
                            Size = new Size(orderPanel.Width - 60, 22),
                            Font = new Font("Segoe UI", 10),
                            ForeColor = textColor
                        };
                        orderPanel.Controls.Add(lblItem);
                        itemY += 25;
                    }

                    y += orderPanel.Height + 15;
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading orders: {ex.Message}", false);
            }
        }

        private void BuildAdminView()
        {
            viewAdmin.Controls.Clear();
            var lbl = new Label
            {
                Text = "Admin Panel",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(lbl);

            var subtitle = new Label
            {
                Text = "Manage your e-commerce platform",
                Font = new Font("Segoe UI", 12),
                Location = new Point(20, 55),
                AutoSize = true,
                ForeColor = textColor
            };
            viewAdmin.Controls.Add(subtitle);

            // Management buttons with better spacing
            var btnManageProducts = CreateStyledButton("Manage Products", new Point(20, 100), 200, 50);
            var btnManageCategories = CreateStyledButton("Manage Categories", new Point(240, 100), 200, 50);
            var btnViewAllOrders = CreateStyledButton("View All Orders", new Point(460, 100), 200, 50);

            viewAdmin.Controls.AddRange(new Control[] { btnManageProducts, btnManageCategories, btnViewAllOrders });

            btnManageProducts.Click += (s, e) => BuildAdminProductsEditor();
            btnManageCategories.Click += (s, e) => BuildAdminCategoriesEditor();
            btnViewAllOrders.Click += (s, e) => BuildAdminOrdersView();
        }

        private void BuildAdminProductsEditor()
        {
            viewAdmin.Controls.Clear();
            var title = new Label
            {
                Text = "Products Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(title);

            var dgv = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(Math.Max(900, this.Width - 300), Math.Max(500, this.Height - 200)),
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

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", ReadOnly = true, Width = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Description", DataPropertyName = "Description", Width = 300 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "StockQuantity", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Image Path", DataPropertyName = "ImagePath", Width = 150 });

            try
            {
                var products = _productService.GetAllProduct();
                dgv.DataSource = new BindingSource { DataSource = products };
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading products: {ex.Message}", false);
            }

            viewAdmin.Controls.Add(dgv);

            int buttonX = dgv.Right + 20;
            var btnSave = CreateAdminButton("Save Changes", buttonX, 60);
            var btnAdd = CreateAdminButton("Add New Product", buttonX, 110);
            var btnDelete = CreateAdminButton("Delete Selected", buttonX, 160);
            var btnRefresh = CreateAdminButton("Refresh", buttonX, 210);
            var btnBack = CreateAdminButton("Back to Admin", buttonX, 260);

            viewAdmin.Controls.AddRange(new Control[] { btnSave, btnAdd, btnDelete, btnRefresh, btnBack });

            btnBack.Click += (s, e) => BuildAdminView();

            btnRefresh.Click += (s, e) =>
            {
                try
                {
                    var products = _productService.GetAllProduct();
                    dgv.DataSource = new BindingSource { DataSource = products };
                    ShowNotification("Products refreshed.", true);
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error refreshing products: {ex.Message}", false);
                }
            };

            btnSave.Click += (s, e) =>
            {
                try
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.DataBoundItem is Product p)
                        {
                            _productService.UpdateProduct(p);
                        }
                    }
                    _productService.saveProduct();
                    ShowNotification("Product changes saved successfully.", true);
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error saving products: {ex.Message}", false);
                }
            };

            btnAdd.Click += (s, e) =>
            {
                try
                {
                    var newP = new Product
                    {
                        Name = "New Product",
                        Description = "Enter description here",
                        Price = 0m,
                        StockQuantity = 0,
                        CreatedAt = DateTime.Now,
                        ImagePath = ""
                    };
                    _productService.AddProduct(newP);
                    _productService.saveProduct();
                    dgv.DataSource = new BindingSource { DataSource = _productService.GetAllProduct() };
                    ShowNotification("New product added successfully.", true);
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error adding product: {ex.Message}", false);
                }
            };

            btnDelete.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is Product p)
                {
                    if (MessageBox.Show($"Are you sure you want to delete '{p.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            _productService.DeleteProduct(p);
                            _productService.saveProduct();
                            dgv.DataSource = new BindingSource { DataSource = _productService.GetAllProduct() };
                            ShowNotification("Product deleted successfully.", true);
                        }
                        catch (Exception ex)
                        {
                            ShowNotification($"Error deleting product: {ex.Message}", false);
                        }
                    }
                }
                else
                {
                    ShowNotification("Please select a product to delete.", false);
                }
            };
        }

        private Button CreateAdminButton(string text, int x, int y)
        {
            return CreateStyledButton(text, new Point(x, y), 180, 40);
        }

        private void BuildAdminCategoriesEditor()
        {
            viewAdmin.Controls.Clear();
            var title = new Label
            {
                Text = "Categories Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(title);

            var dgv = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(Math.Max(800, this.Width - 300), Math.Max(500, this.Height - 200)),
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

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", ReadOnly = true, Width = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Description", DataPropertyName = "Description", Width = 450 });

            try
            {
                dgv.DataSource = new BindingSource { DataSource = _categoryService.GetAllCategory() };
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading categories: {ex.Message}", false);
            }

            viewAdmin.Controls.Add(dgv);

            int buttonX = dgv.Right + 20;
            var btnSave = CreateAdminButton("Save Changes", buttonX, 60);
            var btnAdd = CreateAdminButton("Add New Category", buttonX, 110);
            var btnDelete = CreateAdminButton("Delete Selected", buttonX, 160);
            var btnRefresh = CreateAdminButton("Refresh", buttonX, 210);
            var btnBack = CreateAdminButton("Back to Admin", buttonX, 260);

            viewAdmin.Controls.AddRange(new Control[] { btnSave, btnAdd, btnDelete, btnRefresh, btnBack });

            btnBack.Click += (s, e) => BuildAdminView();

            btnRefresh.Click += (s, e) =>
            {
                try
                {
                    dgv.DataSource = new BindingSource { DataSource = _categoryService.GetAllCategory() };
                    LoadCategories(); // Refresh the category filter dropdown too
                    ShowNotification("Categories refreshed.", true);
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error refreshing categories: {ex.Message}", false);
                }
            };

            btnSave.Click += (s, e) =>
            {
                try
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.DataBoundItem is Category c)
                        {
                            _categoryService.UpdateCategory(c);
                        }
                    }
                    _categoryService.SaveCategory();
                    LoadCategories(); // Refresh the category filter dropdown
                    ShowNotification("Category changes saved successfully.", true);
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error saving categories: {ex.Message}", false);
                }
            };

            btnAdd.Click += (s, e) =>
            {
                try
                {
                    var c = new Category
                    {
                        Name = "New Category",
                        Description = "Enter description here",
                        CreatedAt = DateTime.Now
                    };
                    _categoryService.AddCategory(c);
                    _categoryService.SaveCategory();
                    dgv.DataSource = new BindingSource { DataSource = _categoryService.GetAllCategory() };
                    LoadCategories(); // Refresh the category filter dropdown
                    ShowNotification("New category added successfully.", true);
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error adding category: {ex.Message}", false);
                }
            };

            btnDelete.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is Category c)
                {
                    if (MessageBox.Show($"Are you sure you want to delete '{c.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            _categoryService.DeleteCategory(c);
                            _categoryService.SaveCategory();
                            dgv.DataSource = new BindingSource { DataSource = _categoryService.GetAllCategory() };
                            LoadCategories(); // Refresh the category filter dropdown
                            ShowNotification("Category deleted successfully.", true);
                        }
                        catch (Exception ex)
                        {
                            ShowNotification($"Error deleting category: {ex.Message}", false);
                        }
                    }
                }
                else
                {
                    ShowNotification("Please select a category to delete.", false);
                }
            };
        }

        private void BuildAdminOrdersView()
        {
            viewAdmin.Controls.Clear();
            var title = new Label
            {
                Text = "All Orders Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = primaryColor
            };
            viewAdmin.Controls.Add(title);

            var pnl = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(Math.Max(1000, this.Width - 80), Math.Max(600, this.Height - 150)),
                AutoScroll = true,
                BorderStyle = BorderStyle.Fixed3D,
                BackColor = Color.White
            };
            viewAdmin.Controls.Add(pnl);

            var btnBack = CreateAdminButton("Back to Admin", 20, pnl.Bottom + 10);
            btnBack.Click += (s, e) => BuildAdminView();
            viewAdmin.Controls.Add(btnBack);

            try
            {
                var orders = _orderService.GetAllOrders()
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

                if (orders.Count == 0)
                {
                    pnl.Controls.Add(new Label
                    {
                        Text = "No orders found in the system.",
                        Location = new Point(15, 15),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12),
                        ForeColor = textColor
                    });
                    return;
                }

                int y = 15;
                foreach (var o in orders)
                {
                    var orderPanel = CreateBorderedPanel(secondaryColor, 2);
                    orderPanel.Location = new Point(15, y);
                    orderPanel.Size = new Size(Math.Min(950, pnl.Width - 50), Math.Max(140, 100 + (o.ProductOrder.Count * 25)));
                    orderPanel.BackColor = lightColor;
                    orderPanel.Padding = new Padding(15);
                    pnl.Controls.Add(orderPanel);

                    var headerLbl = new Label
                    {
                        Text = $"Order #{o.Id} - Customer: {o.User?.Email ?? "Unknown"}",
                        Location = new Point(15, 15),
                        Size = new Size(orderPanel.Width - 30, 25),
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = primaryColor
                    };
                    orderPanel.Controls.Add(headerLbl);

                    var dateLbl = new Label
                    {
                        Text = $"Date: {o.OrderDate:dddd, MMMM dd, yyyy 'at' h:mm tt}",
                        Location = new Point(15, 45),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10),
                        ForeColor = textColor
                    };
                    orderPanel.Controls.Add(dateLbl);

                    var totalLbl = new Label
                    {
                        Text = $"Total: {o.OrderTotalPrice:N2} EGP",
                        Location = new Point(15, 70),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        ForeColor = secondaryColor
                    };
                    orderPanel.Controls.Add(totalLbl);

                    // Items header
                    var itemsHeader = new Label
                    {
                        Text = "Order Items:",
                        Location = new Point(15, 100),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = primaryColor
                    };
                    orderPanel.Controls.Add(itemsHeader);

                    // List items
                    int itemY = 125;
                    foreach (var po in o.ProductOrder)
                    {
                        var lblItem = new Label
                        {
                            Text = $"• {po.Product?.Name ?? "Unknown Product"} × {po.Quantity} = {((po.Product?.Price ?? 0) * po.Quantity):N2} EGP",
                            Location = new Point(30, itemY),
                            Size = new Size(orderPanel.Width - 60, 22),
                            Font = new Font("Segoe UI", 9),
                            ForeColor = textColor
                        };
                        orderPanel.Controls.Add(lblItem);
                        itemY += 25;
                    }

                    y += orderPanel.Height + 15;
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Error loading orders: {ex.Message}", false);
            }
        }

        #endregion

        #region Dispose Resources on close

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _container?.Dispose();
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}