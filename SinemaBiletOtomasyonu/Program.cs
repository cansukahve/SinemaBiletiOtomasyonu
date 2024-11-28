using System;
using System.Windows.Forms;

namespace SinemaBiletOtomasyonu
{
    static class Program
    {
        public static string CurrentUsername { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
