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

        DataTable dtBarang = new DataTable();

        public FormBarangKades()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            // Supaya kolom tidak double
            dgvBarang.AutoGenerateColumns = false;

            // Mapping kolom DataGrid
            this.colNama.DataPropertyName = "nama_barang";
            this.colKategori.DataPropertyName = "nama_kategori";
            this.colTotal.DataPropertyName = "jumlah_total";
            this.colDipinjam.DataPropertyName = "jumlah_dipinjam";
            this.colTersedia.DataPropertyName = "jumlah_tersedia";

            tampilData();
            isiKategori();
        }

        // tampil semua data
        void tampilData()
        {
            dtBarang = barang.tampilSemua();

            dgvBarang.DataSource = dtBarang;
        }

        // isi combobox kategori
        void isiKategori()
        {
            cmbKategori.Items.Clear();

            cmbKategori.Items.Add("Semua");

            foreach (DataRow row in dtBarang.Rows)
            {
                string kategori = row["nama_kategori"].ToString();

                if (!cmbKategori.Items.Contains(kategori))
                {
                    cmbKategori.Items.Add(kategori);
                }
            }

            cmbKategori.SelectedIndex = 0;
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

        // klik row dgv
        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nama =
                    dgvBarang.Rows[e.RowIndex].Cells["colNama"].Value.ToString();

                string kategori =
                    dgvBarang.Rows[e.RowIndex].Cells["colKategori"].Value.ToString();

                string total =
                    dgvBarang.Rows[e.RowIndex].Cells["colTotal"].Value.ToString();

                string dipinjam =
                    dgvBarang.Rows[e.RowIndex].Cells["colDipinjam"].Value.ToString();

                string tersedia =
                    dgvBarang.Rows[e.RowIndex].Cells["colTersedia"].Value.ToString();

                MessageBox.Show(
                    this,
                    "Nama Barang : " + nama +
                    "\nKategori : " + kategori +
                    "\nTotal : " + total +
                    "\nDipinjam : " + dipinjam +
                    "\nTersedia : " + tersedia,
                    "Detail Barang",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        // SEARCH REALTIME
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // FILTER KATEGORI
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // fungsi filter
        void filterData()
        {
            DataView dv = dtBarang.DefaultView;

            string cari = txtCari.Text.Trim();
            string kategori = cmbKategori.Text;

            string filter = "";

            // search nama barang
            if (cari != "")
            {
                filter += $"nama_barang LIKE '%{cari}%'";
            }

            // filter kategori
            if (kategori != "Semua" && kategori != "")
            {
                if (filter != "")
                {
                    filter += " AND ";
                }

                filter += $"nama_kategori = '{kategori}'";
            }

            dv.RowFilter = filter;

            dgvBarang.DataSource = dv;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Silakan klik salah satu barang pada tabel untuk melihat detail.",
                "Informasi"
            );
        }
    }
}