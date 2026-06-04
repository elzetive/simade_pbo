using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using simade_pbo.Service;

namespace simade_pbo
{
    public partial class FormRiwayat : Form
    {
        Barang_service barang = new Barang_service();

        public FormRiwayat()
        {
            InitializeComponent();
            tampilRiwayat();
        }

        void tampilRiwayat()
        {
            dgvRiwayat.AutoGenerateColumns = false;

            coid.DataPropertyName = "colID";
            colNama.DataPropertyName = "colNama";
            colPeminjam.DataPropertyName = "colPeminjam";
            colTanggalPinjam.DataPropertyName = "colTanggalPinjam";
            colTanggalKembali.DataPropertyName = "colTanggalKembali";
            colStatus.DataPropertyName = "colStatus";

            dgvRiwayat.DataSource = barang.tampilRiwayat();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();
            this.Hide();
        }

        private void btnBarang_Click_1(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();
            this.Hide();
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}