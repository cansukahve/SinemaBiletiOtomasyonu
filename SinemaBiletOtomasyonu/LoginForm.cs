using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace SinemaBiletOtomasyonu
{
    public partial class LoginForm : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=SinemaDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label lblTitle;
        private Label lblSubtitle;

        public LoginForm()
        {
            InitializeComponent();
            InitializeCustomDesign();
        }

        private void InitializeCustomDesign()
        {
            // Form ayarları
            this.Size = new Size(400, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 255);  // Açık mor-mavi tonu

            // Logo (Film ikonu)
            Label logoLabel = new Label
            {
                Text = "🎬 🎥 🍿",
                Font = new Font("Segoe UI", 36),
                ForeColor = Color.FromArgb(88, 86, 214), // Koyu mor
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 80),
                Location = new Point(0, 30)
            };

            // Başlık
            lblTitle = new Label
            {
                Text = "HOŞGELDİNİZ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 86, 214), // Koyu mor
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 50),
                Location = new Point(0, 110)
            };

            // Alt başlık
            lblSubtitle = new Label
            {
                Text = "Giriş yapmak için bilgilerinizi giriniz",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(149, 147, 230), // Orta mor
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(0, 160)
            };

            // Kullanıcı adı alanı
            txtUsername = new TextBox
            {
                Size = new Size(280, 40),
                Location = new Point(10, 0),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Text = "Kullanıcı Adı",
                ForeColor = Color.Gray
            };

            // Kullanıcı adı ikonu
            Label userIcon = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 15),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(260, 0)
            };

            Panel usernamePanel = new Panel
            {
                Size = new Size(300, 40),
                Location = new Point(50, 220),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            usernamePanel.Controls.AddRange(new Control[] { txtUsername, userIcon });

            // Şifre alanı
            txtPassword = new TextBox
            {
                Size = new Size(280, 40),
                Location = new Point(10, 0),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                UseSystemPasswordChar = true,
                Text = "Şifre",
                ForeColor = Color.Gray
            };

            // Şifre ikonu
            Label passwordIcon = new Label
            {
                Text = "🔒",
                Font = new Font("Segoe UI", 15),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(260, 0)
            };

            Panel passwordPanel = new Panel
            {
                Size = new Size(300, 40),
                Location = new Point(50, 280),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            passwordPanel.Controls.AddRange(new Control[] { txtPassword, passwordIcon });

            // Giriş butonu
            btnLogin = new Button
            {
                Text = "GİRİŞ YAP",
                Size = new Size(300, 45),
                Location = new Point(50, 360),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(88, 86, 214), // Koyu mor
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;

            // Kayıt ol butonu
            btnRegister = new Button
            {
                Text = "KAYIT OL",
                Size = new Size(300, 45),
                Location = new Point(50, 420),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(88, 86, 214), // Koyu mor
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(88, 86, 214);

            // Alt bilgi
            Label footerLabel = new Label
            {
                Text = "🎬 Sinema Bilet Otomasyonu 🍿",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(149, 147, 230), // Orta mor
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(0, 500)
            };

            // Hover efektleri
            btnLogin.MouseEnter += (s, e) => {
                btnLogin.BackColor = Color.FromArgb(149, 147, 230); // Orta mor
            };
            btnLogin.MouseLeave += (s, e) => {
                btnLogin.BackColor = Color.FromArgb(88, 86, 214); // Koyu mor
            };

            btnRegister.MouseEnter += (s, e) => {
                btnRegister.ForeColor = Color.White;
                btnRegister.BackColor = Color.FromArgb(88, 86, 214);
            };
            btnRegister.MouseLeave += (s, e) => {
                btnRegister.ForeColor = Color.FromArgb(88, 86, 214);
                btnRegister.BackColor = Color.White;
            };

            // TextBox olayları
            txtUsername.Enter += (s, e) => {
                if (txtUsername.Text == "Kullanıcı Adı")
                {
                    txtUsername.Text = "";
                    txtUsername.ForeColor = Color.FromArgb(88, 86, 214);
                }
                usernamePanel.BorderStyle = BorderStyle.FixedSingle;
            };
            txtUsername.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    txtUsername.Text = "Kullanıcı Adı";
                    txtUsername.ForeColor = Color.Gray;
                }
            };

            txtPassword.Enter += (s, e) => {
                if (txtPassword.Text == "Şifre")
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.FromArgb(88, 86, 214);
                    txtPassword.UseSystemPasswordChar = true;
                }
                passwordPanel.BorderStyle = BorderStyle.FixedSingle;
            };
            txtPassword.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.Text = "Şifre";
                    txtPassword.ForeColor = Color.Gray;
                    txtPassword.UseSystemPasswordChar = false;
                }
            };

            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;

            // Kontrolleri forma ekle
            this.Controls.AddRange(new Control[] {
                logoLabel,
                lblTitle,
                lblSubtitle,
                usernamePanel,
                passwordPanel,
                btnLogin,
                btnRegister,
                footerLabel
            });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text == "Kullanıcı Adı" ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == "Şifre")
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UserID FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);
                            MovieSelectionForm movieForm = new MovieSelectionForm(userId);
                            this.Hide();
                            movieForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giriş yapılırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            this.Hide();
            registerForm.ShowDialog();
            this.Show();
        }
    }
}
