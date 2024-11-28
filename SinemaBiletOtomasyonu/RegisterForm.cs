using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SinemaBiletOtomasyonu
{
    public partial class RegisterForm : Form
    {
        private string connectionString;

        public RegisterForm()
        {
            InitializeComponent();
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SinemaDatabase;Integrated Security=True";
            EnsureDatabase();
        }

        private void EnsureDatabase()
        {
            try
            {
                // Master veritabanına bağlan
                using (SqlConnection masterConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True"))
                {
                    masterConnection.Open();

                    // Veritabanını oluştur (eğer yoksa)
                    string createDbQuery = @"
                    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SinemaDatabase')
                    BEGIN
                        CREATE DATABASE SinemaDatabase;
                    END";

                    using (SqlCommand command = new SqlCommand(createDbQuery, masterConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // SinemaDatabase'e bağlan ve Users tablosunu oluştur
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                    BEGIN
                        CREATE TABLE Users (
                            UserID INT IDENTITY(1,1) PRIMARY KEY,
                            Username NVARCHAR(50) NOT NULL UNIQUE,
                            Password NVARCHAR(50) NOT NULL,
                            Email NVARCHAR(100) NOT NULL,
                            FullName NVARCHAR(100) NOT NULL
                        )
                    END";

                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı oluşturulurken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string fullName = txtFullName.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || 
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Password, Email, FullName) VALUES (@Username, @Password, @Email, @FullName)";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@FullName", fullName);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Kayıt Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
