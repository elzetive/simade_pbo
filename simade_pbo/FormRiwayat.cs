using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;
using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace simade_pbo
{
    public partial class FormRiwayat : Form
    {
        Barang_service barang = new Barang_service();

        DataTable dtRiwayat = new DataTable();

        public FormRiwayat()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            tampilRiwayat();
        }

        void tampilRiwayat()
        {
            dtRiwayat = barang.tampilRiwayat();

            dgvRiwayat.AutoGenerateColumns = false;

            // Mapping kolom dgv
            colNama.DataPropertyName = "nama_barang";
            colPeminjam.DataPropertyName = "username";
            colTanggalPinjam.DataPropertyName = "tgl_pinjam";
            colTanggalKembali.DataPropertyName = "tgl_kembali";
            colStatus.DataPropertyName = "status_peminjaman";

            dgvRiwayat.DataSource = dtRiwayat;

            isiStatus();
        }

        void isiStatus()
        {
            cmbStatus.Items.Clear();

            cmbStatus.Items.Add("Semua");
            cmbStatus.Items.Add("Disetujui");
            cmbStatus.Items.Add("Ditolak");

            cmbStatus.SelectedIndex = 0;
        }

        // FILTER DATA
        void filterData()
        {
            DataView dv = dtRiwayat.DefaultView;

            string cari = txtCari.Text.Trim();
            string status = cmbStatus.Text.ToLower();

            string filter = "";

            // SEARCH NAMA BARANG
            if (cari != "")
            {
                filter = $"nama_barang LIKE '%{cari}%'";
            }

            // FILTER STATUS
            if (status != "semua" && status != "")
            {
                if (filter != "")
                {
                    filter += " AND ";
                }

                filter += $"status_peminjaman LIKE '%{status}%'";
            }

            dv.RowFilter = filter;

            dgvRiwayat.DataSource = dv;
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
            // Sudah di halaman ini
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

        // SEARCH REALTIME
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // FILTER STATUS
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // WARNA STATUS
        private void dgvRiwayat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRiwayat.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString().ToLower();

                    // RESET default dulu
                    e.CellStyle.ForeColor = Color.Black;

                    if (status == "disetujui")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                    }
                    else if (status == "ditolak")
                    {
                        e.CellStyle.BackColor = Color.LightCoral;
                    }
                    else if (status == "pending")
                    {
                        e.CellStyle.BackColor = Color.Khaki;
                    }
                }
            }
        }
    }
}