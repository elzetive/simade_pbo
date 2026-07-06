using System;
using System.Data;
using System.Windows.Forms;
using simade_pbo.Service;
using MySql.Data.MySqlClient;

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

            // Mapping kolom DataGrid ke desainer visual C#
            this.colNama.DataPropertyName = "nama_barang";
            this.colKategori.DataPropertyName = "nama_kategori";
            this.colTotal.DataPropertyName = "jumlah_total";
            this.colDipinjam.DataPropertyName = "jumlah_dipinjam";
            this.colTersedia.DataPropertyName = "jumlah_tersedia";

            // Mendaftarkan event Load secara eksplisit
            this.Load += new System.EventHandler(this.FormBarangKades_Load);
        }

        private void FormBarangKades_Load(object sender, EventArgs e)
        {
            // AMAN: Handle window sudah dibuat oleh OS, thread UI siap mengeksekusi data
            this.BeginInvoke(new MethodInvoker(tampilData));
        }

        // Tampil semua data dengan kalkulasi matematika inventaris yang benar (Anti-Minus)
        void tampilData()
        {
            string queryAkurat = @"
                SELECT 
                    b.nama_barang, 
                    k.nama_kategori, 
                    b.jumlah_barang AS jumlah_total,
                    IFNULL(SUM(CASE WHEN p.status_peminjaman = 'dipinjam' THEN dp.jumlah_pinjam ELSE 0 END), 0) AS jumlah_dipinjam,
                    (b.jumlah_barang - 
                     IFNULL(SUM(CASE WHEN p.status_peminjaman IN ('pending', 'dipinjam') THEN dp.jumlah_pinjam ELSE 0 END), 0)
                    ) AS jumlah_tersedia
                FROM barang b
                INNER JOIN kategori k ON b.id_kategori = k.id_kategori
                LEFT JOIN detail_peminjaman dp ON b.id_barang = dp.id_barang
                LEFT JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                GROUP BY b.id_barang, b.nama_barang, k.nama_kategori, b.jumlah_barang
                ORDER BY b.nama_barang ASC";

            dtBarang = new DataTable();
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryAkurat, conn))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dtBarang);
                        }
                    }
                }

                // Pengaman Logika: Jika ada anomali yang menghasilkan nilai di bawah 0, paksa set ke 0
                foreach (DataRow row in dtBarang.Rows)
                {
                    if (Convert.ToInt32(row["jumlah_tersedia"]) < 0)
                    {
                        row["jumlah_tersedia"] = 0;
                    }
                }

                dgvBarang.DataSource = dtBarang;
                isiKategori();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat log data aset desa: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Isi combobox kategori secara dinamis dari data yang termuat
        void isiKategori()
        {
            if (dtBarang == null || dtBarang.Rows.Count == 0) return;

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
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin menutup aplikasi?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();
            this.Close(); // SINKRONISASI: Menggunakan .Close() menggantikan .Hide() demi efisiensi memori RAM
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Sudah di halaman ini
        }

        private void btnRiwayat_Click_1(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Close(); // SINKRONISASI: Menggunakan .Close() menggantikan .Hide() demi efisiensi memori RAM
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
                this.Close();
            }
        }

        // Klik row data grid view untuk memunculkan modal rincian informasi
        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    string nama = dgvBarang.Rows[e.RowIndex].Cells["colNama"].Value.ToString();
                    string kategori = dgvBarang.Rows[e.RowIndex].Cells["colKategori"].Value.ToString();
                    string total = dgvBarang.Rows[e.RowIndex].Cells["colTotal"].Value.ToString();
                    string dipinjam = dgvBarang.Rows[e.RowIndex].Cells["colDipinjam"].Value.ToString();
                    string tersedia = dgvBarang.Rows[e.RowIndex].Cells["colTersedia"].Value.ToString();

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
                catch (Exception) { }
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e) { }

        // SEARCH REALTIME
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // FILTER KATEGORI
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // Fungsi filter data pencarian lokal
        void filterData()
        {
            if (dtBarang == null || dtBarang.Rows.Count == 0) return;

            DataView dv = dtBarang.DefaultView;
            string cari = txtCari.Text.Trim().Replace("'", "''"); // Pencegahan SQL Injection lokal string
            string kategori = cmbKategori.Text;

            string filter = "";

            // Search nama barang
            if (!string.IsNullOrEmpty(cari))
            {
                filter += $"nama_barang LIKE '%{cari}%'";
            }

            // Filter kategori
            if (kategori != "Semua" && !string.IsNullOrEmpty(kategori))
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