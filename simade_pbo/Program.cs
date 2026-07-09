using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simade_pbo
{
    //kelas statis : sebagai wadah utilitas yang tidak bisa dibuat objek baru
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //atribut dari .net framework yang menandai metode Main sebagai titik awal eksekusi aplikasi Windows Forms.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //halaman pertama yang muncul saat aplikasi dijalankan adalah FormLogin
            Application.Run(new FormLogin());
        }
    }
}