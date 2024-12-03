using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SinemaBiletOtomasyonu
{
    public partial class SeatSelectionForm : Form
    {
        private int _movieId;
        private string _movieName;
        private DateTime _showTime;
        private decimal _price;
        private int _userId;
        private Button selectedSeat = null;
        private string connectionString;

        // Renk paleti
        private Color primaryColor = Color.FromArgb(88, 86, 214);    // Koyu mor
        private Color secondaryColor = Color.FromArgb(149, 147, 230); // Orta mor
        private Color backgroundColor = Color.FromArgb(240, 242, 255); // Açık mor-mavi

        public SeatSelectionForm(int movieId, string movieName, DateTime showTime, decimal price, int userId)
        {
            InitializeComponent();
            _movieId = movieId;
            _movieName = movieName;
            _showTime = showTime;
            _price = price;
            _userId = userId;
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SinemaDatabase;Integrated Security=True";

            this.Text = $"{movieName} - Koltuk Seçimi"; // Form başlığı
            InitializeSeats(); // Koltukları başlat
        }

        private void InitializeSeats()
        {
            // Ana panel oluşturma
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, // Paneli formun tamamına yayar
                ColumnCount = 1, // Sütun sayısı
                RowCount = 3, // Satır sayısı
                BackColor = backgroundColor // Arka plan rengi
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Sütun stili
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));  // Üst bilgi için
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));  // Koltuklar için
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));  // Alt butonlar için

            // Perde yazısı
            Label screenLabel = new Label
            {
                Text = "PERDE", // Etiket metni
                Font = new Font("Segoe UI", 14, FontStyle.Bold), // Yazı tipi ve boyutu
                TextAlign = ContentAlignment.MiddleCenter, // Yazının ortalanması
                Dock = DockStyle.Fill, // Yazının paneli doldurmasını sağlar
                ForeColor = Color.White, // Yazı rengi
                BackColor = primaryColor, // Arka plan rengi
                Margin = new Padding(100, 10, 100, 20) // Kenar boşlukları
            };
            mainPanel.Controls.Add(screenLabel, 0, 0); // Etiketi panela ekler

            // Koltuk paneli
            TableLayoutPanel seatPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, // Paneli içine doldurur
                ColumnCount = 5, // Sütun sayısı
                RowCount = 5, // Satır sayısı
                BackColor = Color.Transparent // Arka plan rengi
            };

            // Koltuk paneli için stil ayarları
            for (int i = 0; i < 5; i++)
            {
                seatPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Sütun stili
                seatPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F)); // Satır stili
            }

            // Koltukları oluştur
            char[] rows = { 'A', 'B', 'C', 'D', 'E' }; // Satır harfleri
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    string seatNumber = $"{rows[row]}{col + 1}"; // Koltuk numarası
                    Button seatButton = new Button
                    {
                        Text = seatNumber, // Buton metni
                        Dock = DockStyle.Fill, // Butonu paneli doldurur
                        Margin = new Padding(5), // Kenar boşlukları
                        BackColor = secondaryColor, // Arka plan rengi
                        ForeColor = Color.White, // Yazı rengi
                        FlatStyle = FlatStyle.Flat, // Buton stilini düz yapar
                        Font = new Font("Segoe UI", 9, FontStyle.Bold) // Yazı tipi ve boyutu
                    };
                    seatButton.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
                    seatButton.Click += SeatButton_Click; // Tıklama olayını bağla
                    seatPanel.Controls.Add(seatButton, col, row); // Butonu panela ekler
                }
            }
            mainPanel.Controls.Add(seatPanel, 0, 1); // Koltuk panelini ana panela ekler

            // Alt panel için butonlar ve gösterge
            TableLayoutPanel bottomPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, // Paneli içine doldurur
                ColumnCount = 4, // Sütun sayısı
                RowCount = 1, // Satır sayısı
                BackColor = Color.Transparent // Arka plan rengi
            };
            bottomPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // Sütun stili
            bottomPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // Sütun stili
            bottomPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // Sütun stili
            bottomPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // Sütun stili

            // Koltuk durumu göstergesi
            FlowLayoutPanel legendPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, // Paneli içine doldurur
                FlowDirection = FlowDirection.LeftToRight, // Akış yönü
                BackColor = Color.Transparent // Arka plan rengi
            };

            // Boş koltuk göstergesi
            Panel emptyLegend = CreateLegendItem("Boş Koltuk", secondaryColor); // Gösterge oluştur
            legendPanel.Controls.Add(emptyLegend); // Göstergeyi panela ekler

            // Dolu koltuk göstergesi
            Panel occupiedLegend = CreateLegendItem("Dolu Koltuk", Color.FromArgb(255, 99, 99)); // Gösterge oluştur
            legendPanel.Controls.Add(occupiedLegend); // Göstergeyi panela ekler

            // Seçili koltuk göstergesi
            Panel selectedLegend = CreateLegendItem("Seçili Koltuk", Color.FromArgb(255, 193, 7)); // Gösterge oluştur
            legendPanel.Controls.Add(selectedLegend); // Göstergeyi panela ekler

            bottomPanel.Controls.Add(legendPanel, 1, 0); // Gösterge panelini alt panela ekler
            bottomPanel.SetColumnSpan(legendPanel, 2); // Gösterge panelinin sütunlarını birleştirir

            // Geri butonu
            Button backButton = new Button
            {
                Text = "← Geri", // Buton metni
                Dock = DockStyle.Fill, // Butonu paneli doldurur
                Margin = new Padding(10), // Kenar boşlukları
                BackColor = secondaryColor, // Arka plan rengi
                ForeColor = Color.White, // Yazı rengi
                FlatStyle = FlatStyle.Flat, // Buton stilini düz yapar
                Font = new Font("Segoe UI", 10) // Yazı tipi ve boyutu
            };
            backButton.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
            backButton.Click += (s, e) =>
            {
                MovieSelectionForm movieForm = new MovieSelectionForm(_userId);
                movieForm.Show();
                this.Close();
            };
            bottomPanel.Controls.Add(backButton, 0, 0); // Butonu alt panela ekler

            // Ödemeye Geç butonu
            Button paymentButton = new Button
            {
                Text = "Ödemeye Geç →", // Buton metni
                Dock = DockStyle.Fill, // Butonu paneli doldurur
                Margin = new Padding(10), // Kenar boşlukları
                BackColor = primaryColor, // Arka plan rengi
                ForeColor = Color.White, // Yazı rengi
                FlatStyle = FlatStyle.Flat, // Buton stilini düz yapar
                Font = new Font("Segoe UI", 10) // Yazı tipi ve boyutu
            };
            paymentButton.FlatAppearance.BorderSize = 0; // Kenarlık boyutu
            paymentButton.Click += PaymentButton_Click; // Tıklama olayını bağla
            bottomPanel.Controls.Add(paymentButton, 3, 0); // Butonu alt panela ekler

            mainPanel.Controls.Add(bottomPanel, 0, 2); // Alt paneli ana panela ekler

            this.Controls.Add(mainPanel); // Ana paneli forma ekler

            LoadSeatStatus(); // Koltuk durumlarını yükle
        }

        private Panel CreateLegendItem(string text, Color color)
        {
            Panel panel = new Panel
            {
                Width = 150,
                Height = 30,
                Margin = new Padding(5)
            };

            Panel colorBox = new Panel
            {
                BackColor = color,
                Size = new Size(20, 20),
                Location = new Point(5, 5),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label label = new Label
            {
                Text = text,
                AutoSize = true,
                Location = new Point(30, 5)
            };

            panel.Controls.Add(colorBox);
            panel.Controls.Add(label);

            return panel;
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            Button clickedSeat = (Button)sender;

            if (clickedSeat.BackColor == Color.FromArgb(255, 99, 99))
            {
                MessageBox.Show("Bu koltuk dolu!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedSeat != null)
            {
                selectedSeat.BackColor = secondaryColor;
            }

            selectedSeat = clickedSeat;
            selectedSeat.BackColor = Color.FromArgb(255, 193, 7);

            var result = MessageBox.Show(
                $"{selectedSeat.Text} numaralı koltuğu seçtiniz. Ödemeye geçmek ister misiniz?",
                "Koltuk Seçimi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                OpenPaymentForm(selectedSeat.Text);
            }
            else
            {
                selectedSeat.BackColor = secondaryColor;
                selectedSeat = null;
            }
        }

        private void LoadSeatStatus()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SeatNumber, IsOccupied FROM Seats WHERE MovieID = @MovieID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MovieID", _movieId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string seatNumber = reader.GetString(0);
                                bool isOccupied = reader.GetBoolean(1);

                                foreach (Control control in this.Controls)
                                {
                                    if (control is TableLayoutPanel mainPanel)
                                    {
                                        foreach (Control panelControl in mainPanel.Controls)
                                        {
                                            if (panelControl is TableLayoutPanel seatPanel)
                                            {
                                                foreach (Control seatControl in seatPanel.Controls)
                                                {
                                                    if (seatControl is Button button && button.Text == seatNumber)
                                                    {
                                                        button.BackColor = isOccupied ? Color.FromArgb(255, 99, 99) : secondaryColor;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Koltuk durumları yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenPaymentForm(string seatNumber)
        {
            this.Hide();
            PaymentForm paymentForm = new PaymentForm(_movieId, seatNumber, _price, _userId);
            paymentForm.ShowDialog();
            this.Close();
        }

        private void PaymentButton_Click(object sender, EventArgs e)
        {
            if (selectedSeat != null)
            {
                OpenPaymentForm(selectedSeat.Text);
            }
            else
            {
                MessageBox.Show("Lütfen bir koltuk seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void flowLayoutPanelSeats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
