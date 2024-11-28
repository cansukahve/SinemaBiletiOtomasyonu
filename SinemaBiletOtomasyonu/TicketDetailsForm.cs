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

        public TicketDetailsForm(int userId, int movieId, string seatNumber, decimal price)
        {
            InitializeComponent();
            _userId = userId;
            _movieId = movieId;
            _seatNumber = seatNumber;
            _price = price;
            this.Size = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeTicketDetails();
        }

        private void InitializeTicketDetails()
        {
            try
            {
                TableLayoutPanel mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    Padding = new Padding(20)
                };
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

                // Bilet detayları paneli
                Panel detailsPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.White,
                    Padding = new Padding(10)
                };

                FlowLayoutPanel flowPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false
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

                detailsPanel.Controls.Add(flowPanel);
                mainPanel.Controls.Add(detailsPanel, 0, 0);

                // Butonlar paneli
                TableLayoutPanel buttonPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    BackColor = Color.White,
                    Height = 40
                };
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                // Film Listesine Dön butonu
                Button btnBack = new Button
                {
                    Text = "Film Listesine Dön",
                    Width = 120,
                    Height = 35,
                    Margin = new Padding(5),
                    BackColor = Color.FromArgb(51, 122, 183),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 10),
                    Anchor = AnchorStyles.None
                };
                btnBack.Click += (s, e) =>
                {
                    MovieSelectionForm movieForm = new MovieSelectionForm(_userId);
                    movieForm.Show();
                    this.Close();
                };
                buttonPanel.Controls.Add(btnBack, 0, 0);

                // Kapat butonu
                Button btnClose = new Button
                {
                    Text = "Kapat",
                    Width = 100,
                    Height = 35,
                    Margin = new Padding(5),
                    BackColor = Color.FromArgb(217, 83, 79),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 10),
                    Anchor = AnchorStyles.None
                };
                btnClose.Click += (s, e) => this.Close();
                buttonPanel.Controls.Add(btnClose, 1, 0);

                mainPanel.Controls.Add(buttonPanel, 0, 1);
                this.Controls.Add(mainPanel);
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
