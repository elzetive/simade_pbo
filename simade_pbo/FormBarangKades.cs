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
            this.StartPosition = FormStartPosition.CenterScreen;

            // Kunci auto generate agar kolom tidak duplikat ke kanan
            dgvBarang.AutoGenerateColumns = false;

            // Petakan langsung menggunakan nama variabel komponen desainer kalian
            this.coid.DataPropertyName = "id_barang";
            this.colNama.DataPropertyName = "nama_barang";
            this.colKondisi.DataPropertyName = "kondisi";

            tampilData();
        }

        void tampilData()
        {
            dgvBarang.DataSource = barang.tampilSemua();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); // Menggunakan Close agar siklus aplikasi tetap terjaga aman
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();
            this.Hide();
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Diabaikan karena user sudah berada di halaman ini
        }

        private void btnRiwayat_Click_1(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Hide();
            }
        }

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}