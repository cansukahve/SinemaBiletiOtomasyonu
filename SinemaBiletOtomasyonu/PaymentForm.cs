using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SinemaBiletOtomasyonu
{
    public partial class PaymentForm : Form
    {
        private readonly int _movieId;
        private readonly string _seatNumber;
        private readonly decimal _price;
        private readonly int _userId;
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=SinemaDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        private TableLayoutPanel mainPanel;

        public PaymentForm(int movieId, string seatNumber, decimal price, int userId)
        {
            InitializeComponent();
            _movieId = movieId;
            _seatNumber = seatNumber;
            _price = price;
            _userId = userId;
            InitializePaymentControls();
        }

        private void InitializePaymentControls()
        {
            this.Text = "Ödeme";
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7,
                Padding = new Padding(20),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            // Başlık
            Label lblTitle = new Label
            {
                Text = "Ödeme Bilgileri",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(lblTitle);
            mainPanel.SetColumnSpan(lblTitle, 2);

            // Tutar
            Label lblPrice = new Label
            {
                Text = "Tutar:",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblPrice);

            Label lblPriceValue = new Label
            {
                Text = $"{_price:C}",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblPriceValue);

            // Kart Numarası
            Label lblCardNumber = new Label
            {
                Text = "Kart Numarası:",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblCardNumber);

            TextBox txtCardNumber = new TextBox
            {
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(txtCardNumber);

            // Son Kullanma Tarihi
            Label lblExpiry = new Label
            {
                Text = "Son Kullanma:",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblExpiry);

            TextBox txtExpiry = new TextBox
            {
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                Text = "MM/YY",
                ForeColor = Color.Gray
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
            mainPanel.Controls.Add(txtExpiry);

            // CVV
            Label lblCVV = new Label
            {
                Text = "CVV:",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblCVV);

            TextBox txtCVV = new TextBox
            {
                Font = new Font("Arial", 12),
                Dock = DockStyle.Fill,
                MaxLength = 3
            };
            mainPanel.Controls.Add(txtCVV);

            // Alt panel için butonlar
            TableLayoutPanel buttonPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Height = 40,
                Margin = new Padding(0, 10, 0, 10)
            };
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Geri butonu
            Button backButton = new Button
            {
                Text = "Geri",
                Width = 100,
                Height = 35,
                Margin = new Padding(10, 5, 5, 5),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10),
                Anchor = AnchorStyles.None
            };
            backButton.Click += (s, e) => this.Close();
            buttonPanel.Controls.Add(backButton, 0, 0);

            // Ödeme Yap butonu
            Button payButton = new Button
            {
                Text = "Ödeme Yap",
                Width = 100,
                Height = 35,
                Margin = new Padding(5, 5, 10, 5),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10),
                Anchor = AnchorStyles.None
            };
            payButton.Click += PayButton_Click;
            buttonPanel.Controls.Add(payButton, 1, 0);

            mainPanel.Controls.Add(buttonPanel, 0, 2);
            mainPanel.SetColumnSpan(buttonPanel, 2);

            this.Controls.Add(mainPanel);
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
