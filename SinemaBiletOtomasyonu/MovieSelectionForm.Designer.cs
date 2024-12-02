namespace SinemaBiletOtomasyonu
{
    /// <summary>
    /// MovieSelectionForm sınıfı, film seçim ekranını temsil eder.
    /// Kullanıcılar bu ekrandan film seçimi yapabilir.
    /// </summary>
    partial class MovieSelectionForm
    {
        /// <summary>
        /// Form tasarımıyla ilişkili gerekli bileşen değişkeni.
        /// Form bileşenlerini yönetmek için kullanılır.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan kaynakları temizler ve bellek sızıntılarını önler.
        /// </summary>
        /// <param name="disposing">
        /// Eğer true ise, yönetilen kaynaklar temizlenir; false ise yalnızca yönetilmeyen kaynaklar temizlenir.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            // Eğer yönetilen kaynaklar temizleniyorsa ve components boş değilse, bileşenleri temizle
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // Temizlik işlemine devam et
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Windows Form Designer tarafından oluşturulan ve formun başlatılması için gerekli kod.
        /// Bu kodu düzenlemeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MovieSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MovieSelectionForm";
            this.Text = "Film Seçimi";
            this.Load += new System.EventHandler(this.MovieSelectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
