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
    public partial class FormBarangKades : Form
    {
        Barang_service barang = new Barang_service();

        public FormBarangKades()
        {
            InitializeComponent();
            tampilData();
        }

        void tampilData()
        {
            dgvBarang.DataSource = barang.tampilSemua();
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

        private void btnBarang_Click(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();
            this.Hide();
        }

        private void btnRiwayat_Click_1(object sender, EventArgs e)
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

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}