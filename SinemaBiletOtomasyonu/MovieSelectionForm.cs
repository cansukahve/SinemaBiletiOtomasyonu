using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            // Ana panel
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.FromArgb(240, 240, 240);
            this.Controls.Add(mainPanel);

            // Başlık
            Label titleLabel = new Label();
            titleLabel.Text = "Vizyondaki Filmler";
            titleLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(50, 30);
            mainPanel.Controls.Add(titleLabel);

            // Film container
            movieContainer = new FlowLayoutPanel();
            movieContainer.AutoScroll = true;
            movieContainer.WrapContents = true;
            movieContainer.Padding = new Padding(20);
            movieContainer.Location = new Point(50, 100);
            movieContainer.Size = new Size(
                mainPanel.Width - 100,
                mainPanel.Height - 150
            );
            movieContainer.BackColor = Color.White;
            mainPanel.Controls.Add(movieContainer);

            // Form yeniden boyutlandırıldığında container'ı güncelle
            this.Resize += (s, e) =>
            {
                movieContainer.Size = new Size(
                    mainPanel.Width - 100,
                    mainPanel.Height - 150
                );
            };
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
                Height = 400,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            // Film afişi için PictureBox
            PictureBox posterBox = new PictureBox
            {
                Width = 280,
                Height = 250,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, 10)
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
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = false,
                Width = 280,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 270)
            };

            // Gösterim zamanı için Label
            Label timeLabel = new Label
            {
                Text = "Gösterim: " + Convert.ToDateTime(movie["ShowTime"]).ToString("dd.MM.yyyy HH:mm"),
                Font = new Font("Segoe UI", 10),
                AutoSize = false,
                Width = 280,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 300)
            };

            // Fiyat için Label
            Label priceLabel = new Label
            {
                Text = "Fiyat: ₺" + movie["Price"].ToString(),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Green,
                AutoSize = false,
                Width = 280,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 325)
            };

            // Bilet Al butonu
            Button buyButton = new Button
            {
                Text = "Bilet Al",
                Width = 280,
                Height = 35,
                BackColor = Color.FromArgb(92, 184, 92),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, 350)
            };
            buyButton.FlatAppearance.BorderSize = 0;

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
            cardPanel.Controls.Add(posterBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(timeLabel);
            cardPanel.Controls.Add(priceLabel);
            cardPanel.Controls.Add(buyButton);

            // Panel'i FlowLayoutPanel'e ekle
            movieContainer.Controls.Add(cardPanel);
        }
    }
}
