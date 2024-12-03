using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SinemaBiletOtomasyonu
{
    public partial class TicketDetailsForm : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=SinemaDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        private int _userId;
        private int _movieId;
        private string _seatNumber;
        private decimal _price;
        private Color primaryColor = Color.FromArgb(51, 122, 183); // Mavi
        private Color secondaryColor = Color.FromArgb(217, 83, 79); // Kırmızı

        public TicketDetailsForm(int userId, int movieId, string seatNumber, decimal price)
        {
            InitializeComponent();
            _userId = userId; // Kullanıcı ID'sini saklar
            _movieId = movieId; // Film ID'sini saklar
            _seatNumber = seatNumber; // Koltuk numarasını saklar
            _price = price; // Fiyatı saklar
            this.Size = new Size(400, 500); // Form boyutu
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Formun kenarlık stilini belirler
            this.MaximizeBox = false; // Formun büyütülmesini engeller
            this.MinimizeBox = false; // Formun küçültülmesini engeller
            this.StartPosition = FormStartPosition.CenterScreen; // Formun ekranın ortasında açılmasını sağlar
            InitializeTicketDetails(); // Bilet detaylarını başlatır
        }

        private void InitializeTicketDetails()
        {
            try
            {
                // Ana panel
                TableLayoutPanel mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill, // Paneli formun tamamına yayar
                    ColumnCount = 1, // Sütun sayısı
                    RowCount = 3, // Satır sayısı
                    Padding = new Padding(20) // İçerikler arasındaki boşluk
                };
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F)); // Üst satır stili
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // Orta satır stili
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F)); // Alt satır stili

                // Başlık
                Label lblTitle = new Label
                {
                    Text = "Bilet Detayları", // Başlık metni
                    Font = new Font("Segoe UI", 18, FontStyle.Bold), // Yazı tipi, boyutu ve stili
                    ForeColor = primaryColor, // Yazı rengi
                    Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                    TextAlign = ContentAlignment.MiddleCenter // Yazının ortalanması
                };
                mainPanel.Controls.Add(lblTitle, 0, 0); // Başlığı panela ekler

                // Bilet detayları paneli
                Panel detailsPanel = new Panel
                {
                    Dock = DockStyle.Fill, // Paneli içine doldurur
                    AutoScroll = true, // Otomatik kaydırma özelliği
                    BackColor = Color.White, // Arka plan rengi
                    Padding = new Padding(10) // İçerikler arasındaki boşluk
                };

                // Akış paneli
                FlowLayoutPanel flowPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top, // Paneli üste sabitler
                    AutoSize = true, // Otomatik boyutlandırma
                    FlowDirection = FlowDirection.TopDown, // Akış yönü
                    WrapContents = false // İçerikleri satırda sığdığında alt satıra geçer
                };

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Film bilgilerini al
                    string movieQuery = "SELECT MovieName, ShowTime FROM Movies WHERE MovieID = @MovieID";
                    using (SqlCommand cmd = new SqlCommand(movieQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MovieID", _movieId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AddDetailRow(flowPanel, "Film Adı:", reader["MovieName"].ToString());
                                AddDetailRow(flowPanel, "Gösterim:", ((DateTime)reader["ShowTime"]).ToString("dd/MM/yyyy HH:mm"));
                            }
                        }
                    }

                    // Kullanıcı bilgilerini al
                    string userQuery = "SELECT FullName, Email FROM Users WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(userQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", _userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AddDetailRow(flowPanel, "Ad Soyad:", reader["FullName"].ToString());
                                AddDetailRow(flowPanel, "E-posta:", reader["Email"].ToString());
                            }
                        }
                    }

                    AddDetailRow(flowPanel, "Koltuk:", _seatNumber);
                    AddDetailRow(flowPanel, "Ücret:", _price.ToString("C2"));
                }

                detailsPanel.Controls.Add(flowPanel); // Akış panelini detay paneline ekler
                mainPanel.Controls.Add(detailsPanel, 0, 1); // Detay panelini ana panela ekler

                // Butonlar paneli
                TableLayoutPanel buttonPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill, // Paneli içine doldurur
                    ColumnCount = 2, // Sütun sayısı
                    RowCount = 1, // Satır sayısı
                    BackColor = Color.Transparent, // Arka plan rengi
                    Height = 50 // Yükseklik
                };
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Sütun stili
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Sütun stili

                // Film Listesine Dön butonu
                Button btnBack = new Button
                {
                    Text = "Geri", // Buton metni
                    Width = 120, // Buton genişliği
                    Height = 35, // Buton yüksekliği
                    Margin = new Padding(5), // Kenar boşlukları
                    BackColor = secondaryColor, // Arka plan rengi
                    ForeColor = Color.White, // Yazı rengi
                    FlatStyle = FlatStyle.Flat, // Düz stil
                    Font = new Font("Segoe UI", 10), // Yazı tipi ve boyutu
                    Anchor = AnchorStyles.None // Butonun konumunu sabitler
                };
                btnBack.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
                btnBack.Click += (s, e) =>
                {
                    MovieSelectionForm movieForm = new MovieSelectionForm(_userId);
                    movieForm.Show();
                    this.Close();
                };
                buttonPanel.Controls.Add(btnBack, 0, 0); // Butonu panela ekler

                // Kapat butonu
                Button btnClose = new Button
                {
                    Text = "Kapat", // Buton metni
                    Width = 100, // Buton genişliği
                    Height = 35, // Buton yüksekliği
                    Margin = new Padding(5), // Kenar boşlukları
                    BackColor = primaryColor, // Arka plan rengi
                    ForeColor = Color.White, // Yazı rengi
                    FlatStyle = FlatStyle.Flat, // Düz stil
                    Font = new Font("Segoe UI", 10), // Yazı tipi ve boyutu
                    Anchor = AnchorStyles.None // Butonun konumunu sabitler
                };
                btnClose.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
                btnClose.Click += (s, e) => this.Close(); // Butona tıklama olayı
                buttonPanel.Controls.Add(btnClose, 1, 0); // Butonu panela ekler

                mainPanel.Controls.Add(buttonPanel, 0, 2); // Buton panelini ana panela ekler
                this.Controls.Add(mainPanel); // Ana paneli forma ekler
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilet detayları yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDetailRow(FlowLayoutPanel panel, string label, string value)
        {
            Panel rowPanel = new Panel
            {
                AutoSize = true,
                Width = panel.Width - 20,
                Padding = new Padding(5),
                Margin = new Padding(0, 5, 0, 5)
            };

            Label lblTitle = new Label
            {
                Text = label,
                Font = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Arial", 10),
                AutoSize = true,
                Location = new Point(120, 0)
            };

            rowPanel.Controls.Add(lblTitle);
            rowPanel.Controls.Add(lblValue);
            panel.Controls.Add(rowPanel);
        }
    }
}
