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

            // Textbox detail hanya untuk dilihat
            txtNama.ReadOnly = true;
            txtKategori.ReadOnly = true;
            txtKondisiBagus.ReadOnly = true;
            txtStatus.ReadOnly = true;

            this.StartPosition = FormStartPosition.CenterScreen;

            // Supaya kolom tidak double
            dgvBarang.AutoGenerateColumns = false;

            // Mapping kolom DataGrid
            this.colNama.DataPropertyName = "nama_barang";
            this.colKategori.DataPropertyName = "nama_kategori";
            this.colJumlah.DataPropertyName = "jumlah_barang";
            this.colKondisiBagus.DataPropertyName = "kondisi_bagus";
            this.colKondisiRusak.DataPropertyName = "kondisi_rusak";
            this.colStatus.DataPropertyName = "status";

            tampilData();
        }

        void tampilData()
        {
            dgvBarang.DataSource = barang.tampilSemua();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();
            this.Hide();
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Sudah di halaman ini
        }

        private void btnRiwayat_Click_1(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin Log Out?",
                "Konfirmasi Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Hide();
            }
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNama.Text = dgvBarang.Rows[e.RowIndex].Cells["colNama"].Value.ToString();
                txtKategori.Text = dgvBarang.Rows[e.RowIndex].Cells["colKategori"].Value.ToString();
                txtKondisiBagus.Text = dgvBarang.Rows[e.RowIndex].Cells["colKondisiBagus"].Value.ToString();
                txtKondisiRusak.Text = dgvBarang.Rows[e.RowIndex].Cells["colKondisiRusak"].Value.ToString();
                txtStatus.Text = dgvBarang.Rows[e.RowIndex].Cells["colStatus"].Value.ToString();

                // Warna status
                if (txtStatus.Text.ToLower() == "tersedia")
                    txtStatus.BackColor = Color.LightGreen;
                else
                    txtStatus.BackColor = Color.LightCoral;
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}