using System;
using System.Windows.Forms;

namespace SinemaBiletOtomasyonu
{
    // Program�n giri� noktas�n� i�eren statik s�n�f.
    static class Program
    {
        // Mevcut kullan�c� ad�n� tutan �zellik.
        public static string CurrentUsername { get; set; }

        /// <summary>
        /// Uygulaman�n ana giri� noktas�.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // G�rsel stilleri etkinle�tir.
            Application.EnableVisualStyles();
            // Uyumlu metin olu�turma varsay�lan�n� ayarla.
            Application.SetCompatibleTextRenderingDefault(false);
            // Giri� formunu �al��t�r.
            Application.Run(new LoginForm());
        }
    }
}
