namespace SinemaBiletOtomasyonu
{
    /// <summary>
    /// LoginForm s�n�f�, kullan�c� giri� ekran�n� temsil eder.
    /// </summary>
    partial class LoginForm
    {
        /// <summary>
        /// Form tasar�m�yla ili�kili gerekli bile�en de�i�keni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullan�lan kaynaklar� temizler ve bellek s�z�nt�lar�n� �nler.
        /// </summary>
        /// <param name="disposing">
        /// E�er true ise, y�netilen kaynaklar temizlenir; false ise yaln�zca y�netilmeyen kaynaklar temizlenir.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            // E�er y�netilen kaynaklar temizleniyorsa ve components bo� de�ilse, bile�enleri temizle
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // Temizlik i�lemine devam et
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Windows Form Designer taraf�ndan olu�turulan ve formun ba�lat�lmas� i�in gerekli kod.
        /// Bu kodu d�zenlemeyin.
        /// </summary>
        private void InitializeComponent()
        {
            // Form tasar�m�yla ili�kili bile�enlerin ba�lat�lmas�
            this.components = new System.ComponentModel.Container();

            // Formun tasar�m s�recinde i�lemlerin ge�ici olarak durdurulmas�
            this.SuspendLayout();

            // Formun tasar�m s�recinin tamamlanmas�
            this.ResumeLayout(false);
        }

        #endregion
    }
}

