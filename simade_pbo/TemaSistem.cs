using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace simade_pbo
{
    //abstraksi class : untuk mengatur tema sistem agar konsisten
    class TemaSistem
    {
        public static Color WarnaBackgroundForm = Color.FromArgb(245, 247, 250);
        public static Color WarnaPanelOverlay = Color.FromArgb(120, 55, 65, 81);
        public static Color WarnaTombolBiru = Color.FromArgb(59, 130, 246);

        public static void TerapkanTema(Form frm, Panel overlayPanel, Button btnUtama, Button btnClose)
        {
            frm.SuspendLayout();

            //oop : menggunakan reflection untuk mengakses properti protected DoubleBuffered dari Form dan Panel
            typeof(Form).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, frm, new object[] { true });

            frm.BackColor = WarnaBackgroundForm;
            frm.FormBorderStyle = FormBorderStyle.None; // Menghilangkan border default OS agar tampilan modern custom
            frm.StartPosition = FormStartPosition.CenterScreen; // Memastikan form selalu muncul di tengah layar

            if (overlayPanel != null)
            {
                overlayPanel.BackColor = WarnaPanelOverlay;
                typeof(Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, overlayPanel, new object[] { true });
            }

            if (btnUtama != null)
            {
                btnUtama.BackColor = WarnaTombolBiru;
                btnUtama.ForeColor = Color.White;
                btnUtama.FlatStyle = FlatStyle.Flat; // Menghilangkan efek 3D jadul tombol bawaan Windows
                btnUtama.Cursor = Cursors.Hand; // Mengubah kursor menjadi ikon tangan interaktif
            }

            if (btnClose != null)
            {
                btnClose.BackColor = Color.Red;
                btnClose.ForeColor = Color.White;
                btnClose.FlatStyle = FlatStyle.Flat;
                btnClose.Cursor = Cursors.Hand;
            }

            frm.ResumeLayout(false);
        }
    }
}