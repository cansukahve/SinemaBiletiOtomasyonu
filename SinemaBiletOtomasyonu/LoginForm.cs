using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace SinemaBiletOtomasyonu
{
    public partial class LoginForm : Form
    {
        // Veritabanı bağlantısı için bağlantı dizesi
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
            // Formun genel özelliklerini ayarlar
            this.Size = new Size(400, 600); // Formun boyutunu belirler
            this.StartPosition = FormStartPosition.CenterScreen; // Formun ekranın ortasında açılmasını sağlar
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Formun kenarlık stilini belirler
            this.MaximizeBox = false; // Formun büyütülmesini engeller
            this.MinimizeBox = false; // Formun küçültülmesini engeller
            this.BackColor = Color.FromArgb(240, 242, 255);  // Formun arka plan rengini belirler (Açık mor-mavi tonu)

            // Logo (Film ikonu)
            Label logoLabel = new Label
            {
                Text = "🎬 🎥 🍿", // Logo için film ikonları
                Font = new Font("Segoe UI", 36), // Yazı tipi ve boyutu
                ForeColor = Color.FromArgb(88, 86, 214), // Yazı rengi (Koyu mor)
                TextAlign = ContentAlignment.MiddleCenter, // Yazının ortalanması
                Size = new Size(400, 80), // Logo boyutu
                Location = new Point(0, 30) // Logo konumu
            };

            // Başlık
            lblTitle = new Label
            {
                Text = "HOŞGELDİNİZ", // Başlık metni
                Font = new Font("Segoe UI", 24, FontStyle.Bold), // Yazı tipi, boyutu ve stili
                ForeColor = Color.FromArgb(88, 86, 214), // Yazı rengi (Koyu mor)
                TextAlign = ContentAlignment.MiddleCenter, // Yazının ortalanması
                Size = new Size(400, 50), // Başlık boyutu
                Location = new Point(0, 110) // Başlık konumu
            };

            // Alt başlık
            lblSubtitle = new Label
            {
                Text = "Giriş yapmak için bilgilerinizi giriniz", // Alt başlık metni
                Font = new Font("Segoe UI", 10), // Yazı tipi ve boyutu
                ForeColor = Color.FromArgb(149, 147, 230), // Yazı rengi (Orta mor)
                TextAlign = ContentAlignment.MiddleCenter, // Yazının ortalanması
                Size = new Size(400, 30), // Alt başlık boyutu
                Location = new Point(0, 160) // Alt başlık konumu
            };

            // Kullanıcı adı alanı
            txtUsername = new TextBox
            {
                Size = new Size(280, 40), // Kullanıcı adı alanı boyutu
                Location = new Point(10, 0), // Kullanıcı adı alanı konumu
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                BorderStyle = BorderStyle.None, // Kenarlık stili
                BackColor = Color.White, // Arka plan rengi
                Text = "Kullanıcı Adı", // Varsayılan metin
                ForeColor = Color.Gray // Yazı rengi
            };

            // Kullanıcı adı ikonu
            Label userIcon = new Label
            {
                Text = "👤", // Kullanıcı ikonu
                Font = new Font("Segoe UI", 15), // Yazı tipi ve boyutu
                Size = new Size(40, 40), // İkon boyutu
                TextAlign = ContentAlignment.MiddleCenter, // İkonun ortalanması
                Location = new Point(260, 0) // İkon konumu
            };

            // Kullanıcı adı paneli
            Panel usernamePanel = new Panel
            {
                Size = new Size(300, 40), // Panel boyutu
                Location = new Point(50, 220), // Panel konumu
                BackColor = Color.White, // Arka plan rengi
                BorderStyle = BorderStyle.FixedSingle // Kenarlık stili
            };
            usernamePanel.Controls.AddRange(new Control[] { txtUsername, userIcon }); // Paneldeki kontroller

            // Şifre alanı
            txtPassword = new TextBox
            {
                Size = new Size(280, 40), // Şifre alanı boyutu
                Location = new Point(10, 0), // Şifre alanı konumu
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                BorderStyle = BorderStyle.None, // Kenarlık stili
                BackColor = Color.White, // Arka plan rengi
                UseSystemPasswordChar = true, // Şifre karakterlerini gizler
                Text = "Şifre", // Varsayılan metin
                ForeColor = Color.Gray // Yazı rengi
            };

            // Şifre ikonu
            Label passwordIcon = new Label
            {
                Text = "🔒", // Şifre ikonu
                Font = new Font("Segoe UI", 15), // Yazı tipi ve boyutu
                Size = new Size(40, 40), // İkon boyutu
                TextAlign = ContentAlignment.MiddleCenter, // İkonun ortalanması
                Location = new Point(260, 0) // İkon konumu
            };

            // Şifre paneli
            Panel passwordPanel = new Panel
            {
                Size = new Size(300, 40), // Panel boyutu
                Location = new Point(50, 280), // Panel konumu
                BackColor = Color.White, // Arka plan rengi
                BorderStyle = BorderStyle.FixedSingle // Kenarlık stili
            };
            passwordPanel.Controls.AddRange(new Control[] { txtPassword, passwordIcon }); // Paneldeki kontroller

            // Giriş butonu
            btnLogin = new Button
            {
                Text = "GİRİŞ YAP", // Buton metni
                Size = new Size(300, 45), // Buton boyutu
                Location = new Point(50, 360), // Buton konumu
                FlatStyle = FlatStyle.Flat, // Düz stil
                BackColor = Color.FromArgb(88, 86, 214), // Arka plan rengi (Koyu mor)
                ForeColor = Color.White, // Yazı rengi
                Font = new Font("Segoe UI", 12, FontStyle.Bold), // Yazı tipi, boyutu ve stili
                Cursor = Cursors.Hand // İmleç stili
            };
            btnLogin.FlatAppearance.BorderSize = 0; // Kenarlık boyutu

            // Kayıt ol butonu
            btnRegister = new Button
            {
                Text = "KAYIT OL", // Buton metni
                Size = new Size(300, 45), // Buton boyutu
                Location = new Point(50, 420), // Buton konumu
                FlatStyle = FlatStyle.Flat, // Düz stil
                BackColor = Color.White, // Arka plan rengi
                ForeColor = Color.FromArgb(88, 86, 214), // Yazı rengi (Koyu mor)
                Font = new Font("Segoe UI", 12, FontStyle.Bold), // Yazı tipi, boyutu ve stili
                Cursor = Cursors.Hand // İmleç stili
            };
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(88, 86, 214); // Kenarlık rengi

            // Alt bilgi
            Label footerLabel = new Label
            {
                Text = "🎬 Sinema Bilet Otomasyonu 🍿", // Alt bilgi metni
                Font = new Font("Segoe UI", 9), // Yazı tipi ve boyutu
                ForeColor = Color.FromArgb(149, 147, 230), // Yazı rengi (Orta mor)
                TextAlign = ContentAlignment.MiddleCenter, // Yazının ortalanması
                Size = new Size(400, 30), // Alt bilgi boyutu
                Location = new Point(0, 500) // Alt bilgi konumu
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
            // Kullanıcı adı ve şifre kontrolü
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
