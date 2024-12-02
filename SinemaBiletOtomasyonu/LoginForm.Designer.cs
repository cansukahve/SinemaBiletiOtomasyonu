namespace SinemaBiletOtomasyonu
{
    /// <summary>
    /// LoginForm sýnýfý, kullanýcý giriþ ekranýný temsil eder.
    /// </summary>
    partial class LoginForm
    {
        /// <summary>
        /// Form tasarýmýyla iliþkili gerekli bileþen deðiþkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanýlan kaynaklarý temizler ve bellek sýzýntýlarýný önler.
        /// </summary>
        /// <param name="disposing">
        /// Eðer true ise, yönetilen kaynaklar temizlenir; false ise yalnýzca yönetilmeyen kaynaklar temizlenir.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            // Eðer yönetilen kaynaklar temizleniyorsa ve components boþ deðilse, bileþenleri temizle
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // Temizlik iþlemine devam et
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Windows Form Designer tarafýndan oluþturulan ve formun baþlatýlmasý için gerekli kod.
        /// Bu kodu düzenlemeyin.
        /// </summary>
        private void InitializeComponent()
        {
            // Form tasarýmýyla iliþkili bileþenlerin baþlatýlmasý
            this.components = new System.ComponentModel.Container();

            // Formun tasarým sürecinde iþlemlerin geçici olarak durdurulmasý
            this.SuspendLayout();

            // Formun tasarým sürecinin tamamlanmasý
            this.ResumeLayout(false);
        }

        #endregion
    }
}

