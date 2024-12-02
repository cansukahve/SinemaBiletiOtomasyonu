namespace SinemaBiletOtomasyonu
{
    // Bilet detaylarını gösteren formu temsil eden sınıf.
    partial class TicketDetailsForm
    {
        /// <summary>
        /// Tasarımcı tarafından gerekli olan değişken.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Bilet detaylarını göstermek için kullanılan etiketler (Labels).
        private System.Windows.Forms.Label lblMovieName;
        private System.Windows.Forms.Label lblShowTime;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSeatNumber;
        private System.Windows.Forms.Label lblPrice;

        /// <summary>
        /// Kullanılan kaynakları temizler.
        /// </summary>
        /// <param name="disposing">Yönetilen kaynaklar serbest bırakılacaksa doğru; yanlışsa yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            // Eğer yönetilen kaynaklar serbest bırakılacaksa ve bileşenler boş değilse
            if (disposing && (components != null))
            {
                // Bileşenleri serbest bırak
                components.Dispose();
            }
            // Temel sınıfın Dispose metodunu çağır
            base.Dispose(disposing);
        }

        #region Windows Form Designer tarafından oluşturulan kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun içeriğini kod editörüyle değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            // Bileşenleri başlat
            this.components = new System.ComponentModel.Container();
            this.lblMovieName = new System.Windows.Forms.Label();
            this.lblShowTime = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSeatNumber = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();

            // Formun düzenini askıya al
            this.SuspendLayout();
            //
            // TicketDetailsForm
            //
            // Otomatik ölçekleme boyutlarını ayarla
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            // Otomatik ölçekleme modunu fonta göre ayarla
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Formun istemci boyutunu ayarla
            this.ClientSize = new System.Drawing.Size(500, 600);
            // Formun kenar çerçevesini sabit diyalog olarak ayarla
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            // Formun maksimumleştirilmesini engelle
            this.MaximizeBox = false;
            // Formun küçültülmesini engelle
            this.MinimizeBox = false;
            // Formun adını ayarla
            this.Name = "TicketDetailsForm";
            // Formun başlangıç konumunu ekranın ortasına ayarla
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // Formun başlığını ayarla
            this.Text = "Bilet Detayları";
            // Formun düzenini geri yükle
            this.ResumeLayout(false);
        }

        #endregion
    }
}
