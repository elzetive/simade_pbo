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
            //DataTable dt = barang.tampilRiwayat();

            // 2. Kunci agar tidak membuat kolom baru otomatis di kanan desainer
            dgvRiwayat.AutoGenerateColumns = false;

            // 3. Petakan langsung data menggunakan urutan kolom DataTable asli database
            //if (dgvRiwayat.Columns.Count >= 6 && dt != null && dt.Columns.Count >= 6)
            //{
            //    this.coid.DataPropertyName = dt.Columns[0].ColumnName;
            //    this.colNama.DataPropertyName = dt.Columns[1].ColumnName;
            //    this.colPeminjam.DataPropertyName = dt.Columns[2].ColumnName;
            //    this.colTanggalPinjam.DataPropertyName = dt.Columns[3].ColumnName;
            //    this.colTanggalKembali.DataPropertyName = dt.Columns[4].ColumnName;
            //    this.colStatus.DataPropertyName = dt.Columns[5].ColumnName;
            //}

            // 4. Ikat datanya ke DataGridView
            //dgvRiwayat.DataSource = dt;
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

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvRiwayat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRiwayat.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString().ToLower();

                    if (status == "pending")
                    {
                        e.CellStyle.BackColor = Color.Khaki;
                    }
                    else if (status == "dipinjam")
                    {
                        e.CellStyle.BackColor = Color.LightSalmon;
                    }
                    else if (status == "dikembalikan")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }
    }
}