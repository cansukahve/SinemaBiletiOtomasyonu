namespace SinemaBiletOtomasyonu // Projenin ad alanı
{
    partial class PaymentForm // PaymentForm adlı formun kısmi tanımı
    {
        /// <summary>
        /// Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        /// <param name="disposing">Yönetilen kaynakların temizlenip temizlenmeyeceğini belirten bir parametre.</param>
        protected override void Dispose(bool disposing) // Dispose metodu, kaynakların temizlenmesini sağlar.
        {
            if (disposing && (components != null)) // Eğer bileşenler boş değilse
            {
                components.Dispose(); // Bileşenleri temizle
            }
            base.Dispose(disposing); // Base sınıfın Dispose metodunu çağır
        }

        #region Windows Form Designer tarafından oluşturulan kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot
        /// Bu metodun içeriğini kod düzenleyicisi ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent() // Formun kullanıcı arayüzü bileşenlerini başlatan metot
        {
            this.SuspendLayout(); // Formun yeniden çizimini durdurur

            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Formun otomatik boyutlandırma oranını belirler
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Otomatik ölçekleme modunu belirler
            this.ClientSize = new System.Drawing.Size(400, 350); // Formun boyutlarını (genişlik ve yükseklik) ayarlar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; // Formun kenarlık stilini sabit bir diyalog kutusu olarak ayarlar
            this.MaximizeBox = false; // Formun büyütme düğmesini devre dışı bırakır
            this.MinimizeBox = false; // Formun küçültme düğmesini devre dışı bırakır
            this.Name = "PaymentForm"; // Formun adı
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Formun ekranda başlangıç pozisyonunu merkez olarak ayarlar
            this.Text = "Ödeme Bilgileri"; // Formun başlık çubuğunda görünen metin
            this.ResumeLayout(false); // Formun yeniden çizimini etkinleştirir
        }

        #endregion
    }
}
