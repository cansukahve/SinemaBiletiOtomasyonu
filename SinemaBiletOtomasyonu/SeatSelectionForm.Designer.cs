namespace SinemaBiletOtomasyonu
{
    // Koltuk seçim formunu temsil eden sınıf.
    partial class SeatSelectionForm
    {
        /// <summary>
        /// Tasarımcı tarafından gerekli olan değişken.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            // Formun düzenini askıya al
            this.SuspendLayout();
            //
            // SeatSelectionForm
            //
            // Otomatik ölçekleme boyutlarını ayarla
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            // Otomatik ölçekleme modunu fonta göre ayarla
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Formun istemci boyutunu ayarla
            this.ClientSize = new System.Drawing.Size(1067, 554);
            // Formun kenar boşluklarını ayarla
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // Formun adını ayarla
            this.Name = "SeatSelectionForm";
            // Formun başlığını ayarla
            this.Text = "Koltuk Seçimi";
            // Formun düzenini geri yükle
            this.ResumeLayout(false);
        }

        #endregion
    }
}
