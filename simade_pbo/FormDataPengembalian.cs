using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simade_pbo
{
    public partial class FormDataPengembalian : Form
    {
        public FormDataPengembalian()
        {
            InitializeComponent();
        }

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin halamanDashboardAdmin = new FormDashboardAdmin();

            halamanDashboardAdmin.StartPosition = FormStartPosition.CenterScreen;

            halamanDashboardAdmin.ShowDialog();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam halamanDataPinjam = new FormDataPinjam();

            halamanDataPinjam.StartPosition = FormStartPosition.CenterScreen;

            halamanDataPinjam.ShowDialog();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan halamanDataPengambilan = new FormDataPengambilan();

            halamanDataPengambilan.StartPosition = FormStartPosition.CenterScreen;

            halamanDataPengambilan.ShowDialog();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian halamanDataPengembalian = new FormDataPengembalian();

            halamanDataPengembalian.StartPosition = FormStartPosition.CenterScreen;

            halamanDataPengembalian.ShowDialog();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin halamanRegisterAdmin = new FormRegisterAdmin();

            halamanRegisterAdmin.StartPosition = FormStartPosition.CenterScreen;

            halamanRegisterAdmin.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
