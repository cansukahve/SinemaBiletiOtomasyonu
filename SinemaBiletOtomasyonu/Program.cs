using System;
using System.Windows.Forms;

namespace SinemaBiletOtomasyonu
{
    // Programýn giriþ noktasýný içeren statik sýnýf.
    static class Program
    {
        // Mevcut kullanýcý adýný tutan özellik.
        public static string CurrentUsername { get; set; }

        /// <summary>
        /// Uygulamanýn ana giriþ noktasý.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Görsel stilleri etkinleþtir.
            Application.EnableVisualStyles();
            // Uyumlu metin oluþturma varsayýlanýný ayarla.
            Application.SetCompatibleTextRenderingDefault(false);
            // Giriþ formunu çalýþtýr.
            Application.Run(new LoginForm());
        }
    }
}
