using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SinemaBiletOtomasyonu
{
    public partial class PaymentForm : Form
    {
        // Film ID'si, koltuk numarası, fiyat ve kullanıcı ID'si
        private readonly int _movieId;
        private readonly string _seatNumber;
        private readonly decimal _price;
        private readonly int _userId;

        // Veritabanı bağlantısı için bağlantı dizesi
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=SinemaDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";

        // Ana panel ve renkler
        private TableLayoutPanel mainPanel;
        private Color primaryColor = Color.FromArgb(88, 86, 214);    // Koyu mor
        private Color secondaryColor = Color.FromArgb(149, 147, 230); // Orta mor
        private Color backgroundColor = Color.FromArgb(240, 242, 255); // Açık mor-mavi

        public PaymentForm(int movieId, string seatNumber, decimal price, int userId)
        {
            InitializeComponent();
            _movieId = movieId; // Film ID'sini saklar
            _seatNumber = seatNumber; // Koltuk numarasını saklar
            _price = price; // Fiyatı saklar
            _userId = userId; // Kullanıcı ID'sini saklar
            InitializePaymentControls(); // Ödeme kontrollerini başlatır
        }

        private void InitializePaymentControls()
        {
            this.Text = "Ödeme"; // Form başlığı
            this.Size = new Size(450, 550); // Form boyutu
            this.StartPosition = FormStartPosition.CenterScreen; // Formun ekranın ortasında açılmasını sağlar
            this.BackColor = backgroundColor; // Formun arka plan rengi

            // Ana panel
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, // Paneli formun tamamına yayar
                ColumnCount = 3, // Sütun sayısı
                RowCount = 6, // Satır sayısı
                Padding = new Padding(20), // İçerikler arasındaki boşluk
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None // Hücre kenarlık stili
            };

            // Sütun stilleri
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Başlık
            Label lblTitle = new Label
            {
                Text = "Ödeme Bilgileri", // Başlık metni
                Font = new Font("Segoe UI", 16, FontStyle.Bold), // Yazı tipi, boyutu ve stili
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                TextAlign = ContentAlignment.MiddleCenter, // Yazının ortalanması
                ForeColor = primaryColor // Yazı rengi
            };
            mainPanel.Controls.Add(lblTitle, 0, 0); // Başlığı panela ekler
            mainPanel.SetColumnSpan(lblTitle, 3); // Başlığın üç sütunu kaplamasını sağlar

            // Tutar
            Label lblPrice = new Label
            {
                Text = "Tutar:", // Etiket metni
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                TextAlign = ContentAlignment.MiddleLeft // Yazının sola hizalanması
            };
            mainPanel.Controls.Add(lblPrice, 1, 1); // Etiketi panela ekler

            Label lblPriceValue = new Label
            {
                Text = $"{_price:C}", // Fiyatı para birimi olarak gösterir
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                TextAlign = ContentAlignment.MiddleLeft // Yazının sola hizalanması
            };
            mainPanel.Controls.Add(lblPriceValue, 2, 1); // Fiyatı panela ekler

            // Kart Numarası
            Label lblCardNumber = new Label
            {
                Text = "Kart Numarası:", // Etiket metni
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                TextAlign = ContentAlignment.MiddleLeft // Yazının sola hizalanması
            };
            mainPanel.Controls.Add(lblCardNumber, 1, 2); // Etiketi panela ekler

            TextBox txtCardNumber = new TextBox
            {
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill // Metin kutusunun paneli doldurmasını sağlar
            };
            mainPanel.Controls.Add(txtCardNumber, 2, 2); // Metin kutusunu panela ekler

            // Son Kullanma Tarihi
            Label lblExpiry = new Label
            {
                Text = "Son Kullanma:", // Etiket metni
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                TextAlign = ContentAlignment.MiddleLeft // Yazının sola hizalanması
            };
            mainPanel.Controls.Add(lblExpiry, 1, 3); // Etiketi panela ekler

            TextBox txtExpiry = new TextBox
            {
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Metin kutusunun paneli doldurmasını sağlar
                Text = "MM/YY", // Varsayılan metin
                ForeColor = Color.Gray // Yazı rengi
            };
            txtExpiry.GotFocus += (s, e) => {
                if (txtExpiry.Text == "MM/YY")
                {
                    txtExpiry.Text = "";
                    txtExpiry.ForeColor = Color.Black;
                }
            };
            txtExpiry.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtExpiry.Text))
                {
                    txtExpiry.Text = "MM/YY";
                    txtExpiry.ForeColor = Color.Gray;
                }
            };
            mainPanel.Controls.Add(txtExpiry, 2, 3); // Metin kutusunu panela ekler

            // CVV
            Label lblCVV = new Label
            {
                Text = "CVV:", // Etiket metni
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                TextAlign = ContentAlignment.MiddleLeft // Yazının sola hizalanması
            };
            mainPanel.Controls.Add(lblCVV, 1, 4); // Etiketi panela ekler

            TextBox txtCVV = new TextBox
            {
                Font = new Font("Segoe UI", 12), // Yazı tipi ve boyutu
                Dock = DockStyle.Fill, // Metin kutusunun paneli doldurmasını sağlar
                MaxLength = 3 // Maksimum karakter sayısı
            };
            mainPanel.Controls.Add(txtCVV, 2, 4); // Metin kutusunu panela ekler

            this.Controls.Add(mainPanel); // Ana paneli forma ekler

            // Alt panel için butonlar
            TableLayoutPanel buttonPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom, // Paneli formun altına yerleştirir
                ColumnCount = 2, // Sütun sayısı
                RowCount = 1, // Satır sayısı
                BackColor = Color.Transparent, // Arka plan rengi
                Height = 50, // Yükseklik
                Margin = new Padding(0, 10, 0, 10) // Kenar boşlukları
            };
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Geri butonu
            Button backButton = new Button
            {
                Text = "Geri", // Buton metni
                Width = 100, // Buton genişliği
                Height = 35, // Buton yüksekliği
                Margin = new Padding(10, 5, 5, 5), // Kenar boşlukları
                BackColor = secondaryColor, // Arka plan rengi
                ForeColor = Color.White, // Yazı rengi
                FlatStyle = FlatStyle.Flat, // Düz stil
                Font = new Font("Segoe UI", 10), // Yazı tipi ve boyutu
                Anchor = AnchorStyles.None // Butonun konumunu sabitler
            };
            backButton.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
            backButton.Click += (s, e) => this.Close(); // Butona tıklama olayı
            buttonPanel.Controls.Add(backButton, 0, 0); // Butonu panela ekler

            // Ödeme Yap butonu
            Button payButton = new Button
            {
                Text = "Ödeme Yap", // Buton metni
                Width = 100, // Buton genişliği
                Height = 35, // Buton yüksekliği
                Margin = new Padding(5, 5, 10, 5), // Kenar boşlukları
                BackColor = primaryColor, // Arka plan rengi
                ForeColor = Color.White, // Yazı rengi
                FlatStyle = FlatStyle.Flat, // Düz stil
                Font = new Font("Segoe UI", 10), // Yazı tipi ve boyutu
                Anchor = AnchorStyles.None // Butonun konumunu sabitler
            };
            payButton.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
            payButton.Click += PayButton_Click; // Butona tıklama olayı
            buttonPanel.Controls.Add(payButton, 1, 0); // Butonu panela ekler

            this.Controls.Add(buttonPanel); // Buton panelini forma ekler
        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Koltuğu rezerve et ve IsOccupied'ı güncelle
                    string updateSeatQuery = @"
                        UPDATE Seats
                        SET IsReserved = 1,
                            IsOccupied = 1
                        WHERE MovieID = @MovieID
                        AND SeatNumber = @SeatNumber";

                    using (SqlCommand updateSeatCmd = new SqlCommand(updateSeatQuery, conn))
                    {
                        updateSeatCmd.Parameters.AddWithValue("@MovieID", _movieId);
                        updateSeatCmd.Parameters.AddWithValue("@SeatNumber", _seatNumber);
                        updateSeatCmd.ExecuteNonQuery();
                    }

                    // Rezervasyonu kaydet
                    string insertBookingQuery = @"
                        INSERT INTO Bookings (UserID, MovieID, SeatID, BookingDate, TotalAmount)
                        SELECT @UserID, @MovieID, s.SeatID, GETDATE(), @TotalAmount
                        FROM Seats s
                        WHERE s.MovieID = @MovieID AND s.SeatNumber = @SeatNumber";

                    using (SqlCommand insertBookingCmd = new SqlCommand(insertBookingQuery, conn))
                    {
                        insertBookingCmd.Parameters.AddWithValue("@UserID", _userId);
                        insertBookingCmd.Parameters.AddWithValue("@MovieID", _movieId);
                        insertBookingCmd.Parameters.AddWithValue("@SeatNumber", _seatNumber);
                        insertBookingCmd.Parameters.AddWithValue("@TotalAmount", _price);
                        insertBookingCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Ödeme başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Bilet detayları formunu aç
                    TicketDetailsForm ticketDetails = new TicketDetailsForm(_userId, _movieId, _seatNumber, _price);
                    this.Hide();
                    ticketDetails.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödeme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessPayment()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Önce seçilen koltuğu işaretle
                    string updateSeatQuery = "UPDATE Seats SET IsOccupied = 1 WHERE MovieID = @MovieID AND SeatNumber = @SeatNumber";
                    using (SqlCommand updateSeatCmd = new SqlCommand(updateSeatQuery, connection))
                    {
                        updateSeatCmd.Parameters.AddWithValue("@MovieID", _movieId);
                        updateSeatCmd.Parameters.AddWithValue("@SeatNumber", _seatNumber);
                        updateSeatCmd.ExecuteNonQuery();
                    }

                    // Koltuk ID'sini al
                    int seatId = 0;
                    string getSeatIdQuery = "SELECT SeatID FROM Seats WHERE MovieID = @MovieID AND SeatNumber = @SeatNumber";
                    using (SqlCommand getSeatIdCmd = new SqlCommand(getSeatIdQuery, connection))
                    {
                        getSeatIdCmd.Parameters.AddWithValue("@MovieID", _movieId);
                        getSeatIdCmd.Parameters.AddWithValue("@SeatNumber", _seatNumber);
                        seatId = (int)getSeatIdCmd.ExecuteScalar();
                    }

                    // Rezervasyonu kaydet
                    string insertBookingQuery = @"
                        INSERT INTO Bookings (UserID, MovieID, SeatID, TotalAmount, BookingDate)
                        VALUES (@UserID, @MovieID, @SeatID, @TotalAmount, GETDATE())";

                    using (SqlCommand insertBookingCmd = new SqlCommand(insertBookingQuery, connection))
                    {
                        insertBookingCmd.Parameters.AddWithValue("@UserID", _userId);
                        insertBookingCmd.Parameters.AddWithValue("@MovieID", _movieId);
                        insertBookingCmd.Parameters.AddWithValue("@SeatID", seatId);
                        insertBookingCmd.Parameters.AddWithValue("@TotalAmount", _price);
                        insertBookingCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödeme işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
