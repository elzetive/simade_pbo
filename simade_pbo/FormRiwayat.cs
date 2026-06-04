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
            this.StartPosition = FormStartPosition.CenterScreen;
            tampilRiwayat();
        }

        void tampilRiwayat()
        {
            // 1. Ambil data riwayat dari database berupa DataTable
            DataTable dt = barang.tampilRiwayat();

            // 2. Kunci agar tidak membuat kolom baru otomatis di kanan desainer
            dgvRiwayat.AutoGenerateColumns = false;

            // 3. Petakan langsung data menggunakan urutan kolom DataTable asli database.
            // Cara ini 100% aman dari masalah huruf besar-kecil atau typo nama field database.
            if (dgvRiwayat.Columns.Count >= 6 && dt != null && dt.Columns.Count >= 6)
            {
                this.coid.DataPropertyName = dt.Columns[0].ColumnName; // Kolom ke-1: ID Peminjaman
                this.colNama.DataPropertyName = dt.Columns[1].ColumnName; // Kolom ke-2: Nama Barang
                this.colPeminjam.DataPropertyName = dt.Columns[2].ColumnName; // Kolom ke-3: Peminjam
                this.colTanggalPinjam.DataPropertyName = dt.Columns[3].ColumnName; // Kolom ke-4: Tanggal Pinjam
                this.colTanggalKembali.DataPropertyName = dt.Columns[4].ColumnName; // Kolom ke-5: Tanggal Kembali
                this.colStatus.DataPropertyName = dt.Columns[5].ColumnName; // Kolom ke-6: Status Peminjaman
            }

            // 4. Ikat datanya ke komponen DataGridView desainer
            dgvRiwayat.DataSource = dt;
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

        private void btnBarang_Click_1(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();
            this.Hide();
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            // Diabaikan karena user sudah berada di halaman ini
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

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}