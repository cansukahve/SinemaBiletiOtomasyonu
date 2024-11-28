using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

namespace SinemaBiletOtomasyonu
{
    public partial class MovieSelectionForm : Form
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SinemaDatabase;Integrated Security=True";
        private readonly int _userId;
        private FlowLayoutPanel movieContainer;

        public MovieSelectionForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeMovieContainer();
            LoadMovies();
        }

        private void InitializeMovieContainer()
        {
            // Ana panel - Gradient arka plan
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Paint += (sender, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    mainPanel.ClientRectangle,
                    Color.FromArgb(240, 242, 255),  // Açık mor-mavi
                    Color.FromArgb(220, 222, 245),  // Biraz daha koyu mor-mavi
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, mainPanel.ClientRectangle);
                }
            };
            this.Controls.Add(mainPanel);

            // Başlık Panel
            Panel titlePanel = new Panel
            {
                Height = 80, // Başlık panel yüksekliğini azalttım
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(88, 86, 214) // Koyu mor
            };
            mainPanel.Controls.Add(titlePanel);

            // Başlık
            Label titleLabel = new Label
            {
                Text = "🎬 Vizyondaki Filmler 🎥",
                Font = new Font("Segoe UI", 22, FontStyle.Bold), // Font boyutunu biraz küçülttüm
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 25) // Konumu yukarı çektim
            };
            titlePanel.Controls.Add(titleLabel);
            titleLabel.Location = new Point((titlePanel.Width - titleLabel.Width) / 2, (titlePanel.Height - titleLabel.Height) / 2);
            titlePanel.Resize += (s, e) => titleLabel.Location = new Point((titlePanel.Width - titleLabel.Width) / 2, (titlePanel.Height - titleLabel.Height) / 2);

            // Container için panel
            Panel containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 20, 0, 0), // Üstten boşluk ekledik
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(containerPanel);

            // Film container
            movieContainer = new FlowLayoutPanel
            {
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(20),
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScrollMargin = new Size(0, 20)
            };
            containerPanel.Controls.Add(movieContainer);

            // Alt bilgi
            Label footerLabel = new Label
            {
                Text = "🍿 Sinema Bilet Otomasyonu 🎬",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(88, 86, 214),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 40
            };
            mainPanel.Controls.Add(footerLabel);
        }

        private void LoadMovies()
        {
            movieContainer.Controls.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MovieID, MovieName, ShowTime, Price, PosterPath FROM Movies";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable moviesTable = new DataTable();
                            adapter.Fill(moviesTable);

                            foreach (DataRow movie in moviesTable.Rows)
                            {
                                CreateMovieCard(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Film bilgileri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateMovieCard(DataRow movie)
        {
            Panel cardPanel = new Panel
            {
                Width = 300,
                Height = 450,
                Margin = new Padding(15),
                BackColor = Color.White
            };

            // Yuvarlak köşeli panel çizimi
            cardPanel.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    int radius = 10;
                    Rectangle rect = cardPanel.ClientRectangle;
                    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                    path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                    path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                    path.CloseFigure();
                    
                    using (var brush = new SolidBrush(cardPanel.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            // Film afişi için PictureBox
            PictureBox posterBox = new PictureBox
            {
                Width = 280,
                Height = 280,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, 10),
                BackColor = Color.Transparent
            };

            try
            {
                string posterPath = movie["PosterPath"].ToString();
                if (!string.IsNullOrEmpty(posterPath))
                {
                    // Resources/Movies klasöründen resmi yükle
                    string resourcePath = Path.Combine(Application.StartupPath, "Resources", "Movies", posterPath);
                    if (File.Exists(resourcePath))
                    {
                        using (var stream = new FileStream(resourcePath, FileMode.Open, FileAccess.Read))
                        {
                            posterBox.Image = Image.FromStream(stream);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Resim bulunamadı: {resourcePath}");
                        posterBox.BackColor = Color.LightGray;
                        posterBox.Image = null;
                    }
                }
                else
                {
                    posterBox.BackColor = Color.LightGray;
                    posterBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                posterBox.BackColor = Color.LightGray;
                posterBox.Image = null;
                Console.WriteLine($"Resim yükleme hatası: {ex.Message}");
            }

            // Film başlığı için Label
            Label titleLabel = new Label
            {
                Text = movie["MovieName"].ToString(),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = false,
                Width = 280,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 300),
                ForeColor = Color.FromArgb(88, 86, 214)
            };

            // Gösterim zamanı için Label
            Label timeLabel = new Label
            {
                Text = "Gösterim: " + Convert.ToDateTime(movie["ShowTime"]).ToString("dd.MM.yyyy HH:mm"),
                Font = new Font("Segoe UI", 11),
                AutoSize = false,
                Width = 280,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 335),
                ForeColor = Color.FromArgb(100, 100, 100)
            };

            // Fiyat için Label
            Label priceLabel = new Label
            {
                Text = "₺" + movie["Price"].ToString(),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(92, 184, 92),
                AutoSize = false,
                Width = 280,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 365)
            };

            // Bilet Al butonu
            Button buyButton = new Button
            {
                Text = "🎟️ Bilet Al",
                Width = 260,
                Height = 40,
                BackColor = Color.FromArgb(88, 86, 214),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12),
                Location = new Point(20, 395)
            };
            buyButton.FlatAppearance.BorderSize = 0;

            // Hover efekti
            buyButton.MouseEnter += (s, e) => buyButton.BackColor = Color.FromArgb(149, 147, 230);
            buyButton.MouseLeave += (s, e) => buyButton.BackColor = Color.FromArgb(88, 86, 214);

            // Butona tıklama olayı
            buyButton.Click += (sender, e) =>
            {
                int movieId = Convert.ToInt32(movie["MovieID"]);
                string movieName = movie["MovieName"].ToString();
                DateTime showTime = Convert.ToDateTime(movie["ShowTime"]);
                decimal price = Convert.ToDecimal(movie["Price"]);

                SeatSelectionForm seatSelection = new SeatSelectionForm(movieId, movieName, showTime, price, _userId);
                this.Hide();
                seatSelection.ShowDialog();
                this.Show();
            };

            // Kontrolleri panel'e ekle
            cardPanel.Controls.AddRange(new Control[] { posterBox, titleLabel, timeLabel, priceLabel, buyButton });
            movieContainer.Controls.Add(cardPanel);
        }
    }
}
