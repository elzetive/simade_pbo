using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace simade_pbo
{
    class TemaSistem
    {
        public static Color WarnaBackgroundForm = Color.FromArgb(245, 247, 250);
        public static Color WarnaPanelOverlay = Color.FromArgb(120, 55, 65, 81);
        public static Color WarnaTombolBiru = Color.FromArgb(59, 130, 246);

        public static void TerapkanTema(Form frm, Panel overlayPanel, Button btnUtama, Button btnClose)
        {
            frm.SuspendLayout();

            typeof(Form).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, frm, new object[] { true });

            frm.BackColor = WarnaBackgroundForm;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.StartPosition = FormStartPosition.CenterScreen;

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
                btnUtama.FlatStyle = FlatStyle.Flat;
                btnUtama.Cursor = Cursors.Hand;
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