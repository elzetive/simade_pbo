using System;
using System.Data;
using System.Windows.Forms;
using simade_pbo.Service;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormBarangKades : Form
    {       // Membuat objek Barang_service untuk mengambil data barang dari database
        Barang_service barang = new Barang_service();

        // DataTable sebagai tempat menyimpan data barang yang diambil dari database
        DataTable dtBarang = new DataTable();

        // Constructor FormBarangKades
        public FormBarangKades()
        {
            // Menginisialisasi seluruh komponen pada form
            InitializeComponent();

            // Mengatur posisi form agar muncul di tengah layar
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menonaktifkan pembuatan kolom otomatis
            // karena kolom sudah dibuat melalui Designer Visual Studio
            dgvBarang.AutoGenerateColumns = false;

            // Menghubungkan setiap kolom DataGridView dengan field pada database
            this.colNama.DataPropertyName = "nama_barang";
            this.colKategori.DataPropertyName = "nama_kategori";
            this.colTotal.DataPropertyName = "jumlah_total";
            this.colDipinjam.DataPropertyName = "jumlah_dipinjam";
            this.colTersedia.DataPropertyName = "jumlah_tersedia";

            // Mendaftarkan event Load secara manual
            // agar method FormBarangKades_Load dijalankan saat form dibuka
            this.Load += new System.EventHandler(this.FormBarangKades_Load);
        }

        // Event yang dijalankan ketika Form pertama kali dibuka
        private void FormBarangKades_Load(object sender, EventArgs e)
        {
            // BeginInvoke digunakan agar proses pengambilan data dilakukan
            // setelah form selesai dimuat sehingga tampilan tidak terasa lambat
            this.BeginInvoke(new MethodInvoker(tampilData));
        }

        // Method untuk menampilkan seluruh data barang beserta stoknya
        void tampilData()
        {
            // Query SQL untuk menghitung stok barang secara realtime
            // Barang tersedia dihitung dari kondisi_bagus sehingga barang rusak
            // tidak ikut dianggap sebagai stok yang masih bisa dipinjam
            string queryAkurat = @"
            SELECT
                b.id_barang,
                b.nama_barang,
                k.nama_kategori,

                b.jumlah_barang AS jumlah_total,

                IFNULL((
                    SELECT SUM(dp.jumlah_pinjam)
                    FROM detail_peminjaman dp
                    INNER JOIN peminjaman p
                        ON dp.id_peminjaman = p.id_peminjaman
                    WHERE dp.id_barang = b.id_barang
                    AND LOWER(p.status_peminjaman) IN ('dipinjam','disetujui')
                ),0) AS jumlah_dipinjam,

                (
                    b.kondisi_bagus -
                    IFNULL((
                        SELECT SUM(dp.jumlah_pinjam)
                        FROM detail_peminjaman dp
                        INNER JOIN peminjaman p
                            ON dp.id_peminjaman = p.id_peminjaman
                        WHERE dp.id_barang = b.id_barang
                        AND LOWER(p.status_peminjaman) IN ('dipinjam','disetujui')
                    ),0)
                ) AS jumlah_tersedia

            FROM barang b
            INNER JOIN kategori k
                ON b.id_kategori = k.id_kategori

            ORDER BY b.nama_barang ASC";

            // Membuat DataTable baru
            dtBarang = new DataTable();

            try
            {
                // Membuka koneksi ke database
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();

                    // Menjalankan query SQL
                    using (MySqlCommand cmd = new MySqlCommand(queryAkurat, conn))
                    {
                        // Mengambil hasil query ke dalam DataTable
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dtBarang);
                        }
                    }
                }

                // Memastikan stok tersedia tidak bernilai negatif
                foreach (DataRow row in dtBarang.Rows)
                {
                    int tersedia = Convert.ToInt32(row["jumlah_tersedia"]);

                    if (tersedia < 0)
                    {
                        row["jumlah_tersedia"] = 0;
                    }
                }

                // Menampilkan data ke DataGridView
                dgvBarang.DataSource = dtBarang;

                // Mengisi ComboBox kategori
                isiKategori();
            }
            catch (Exception ex)
            {
                // Menampilkan pesan apabila terjadi kesalahan saat mengambil data
                MessageBox.Show(
                    "Gagal memuat log data aset desa: " + ex.Message,
                    "Error Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Method untuk mengisi daftar kategori pada ComboBox
        void isiKategori()
        {
            // Jika data kosong maka proses dihentikan
            if (dtBarang == null || dtBarang.Rows.Count == 0)
                return;

            // Menghapus isi ComboBox terlebih dahulu
            cmbKategori.Items.Clear();

            // Menambahkan pilihan "Semua"
            cmbKategori.Items.Add("Semua");

            // Mengambil seluruh kategori dari DataTable
            foreach (DataRow row in dtBarang.Rows)
            {
                string kategori = row["nama_kategori"].ToString();

                // Menambahkan kategori hanya jika belum ada
                if (!cmbKategori.Items.Contains(kategori))
                {
                    cmbKategori.Items.Add(kategori);
                }
            }

            // Menjadikan "Semua" sebagai pilihan awal
            cmbKategori.SelectedIndex = 0;
        }

        // Tombol keluar aplikasi
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi sebelum aplikasi ditutup
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin menutup aplikasi?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Jika memilih Yes maka aplikasi ditutup
            if (konfirmasi == DialogResult.Yes)
                Application.Exit();
        }

        // Tombol menuju Dashboard
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            // Membuka Form Dashboard
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();

            // Menutup Form Barang agar penggunaan memori lebih efisien
            this.Close();
        }

        // Tombol Barang
        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Tidak melakukan apa-apa karena pengguna sudah berada di halaman Barang
        }

        // Tombol menuju Riwayat
        private void btnRiwayat_Click_1(object sender, EventArgs e)
        {
            // Membuka Form Riwayat
            FormRiwayat frm = new FormRiwayat();
            frm.Show();

            // Menutup form saat ini
            this.Close();
        }

        // Tombol Logout
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi logout
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin Log Out?",
                "Konfirmasi Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Jika memilih Yes maka kembali ke halaman Login
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();

                this.Close();
            }
        }

        // Event ketika salah satu baris DataGridView diklik
        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Memastikan yang dipilih adalah baris data
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Mengambil data dari baris yang dipilih
                    string nama = dgvBarang.Rows[e.RowIndex].Cells["colNama"].Value.ToString();
                    string kategori = dgvBarang.Rows[e.RowIndex].Cells["colKategori"].Value.ToString();
                    string total = dgvBarang.Rows[e.RowIndex].Cells["colTotal"].Value.ToString();
                    string dipinjam = dgvBarang.Rows[e.RowIndex].Cells["colDipinjam"].Value.ToString();
                    string tersedia = dgvBarang.Rows[e.RowIndex].Cells["colTersedia"].Value.ToString();

                    // Menampilkan informasi detail barang
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
                catch (Exception)
                {
                    // Mengabaikan error apabila data kosong atau tidak dapat dibaca
                }
            }
        }

        // Event bawaan Visual Studio (belum digunakan)
        private void txtId_TextChanged(object sender, EventArgs e) { }

        // Event pencarian secara realtime ketika isi TextBox berubah
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // Event bawaan Visual Studio (belum digunakan)
        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Event ketika kategori dipilih
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // Method untuk melakukan pencarian dan filter data
        void filterData()
        {
            // Jika DataTable kosong maka proses dihentikan
            if (dtBarang == null || dtBarang.Rows.Count == 0)
                return;

            // Mengambil DataView dari DataTable
            DataView dv = dtBarang.DefaultView;

            // Mengambil teks pencarian
            // Replace digunakan untuk menghindari karakter petik tunggal yang dapat menyebabkan error filter
            string cari = txtCari.Text.Trim().Replace("'", "''");

            // Mengambil kategori yang dipilih
            string kategori = cmbKategori.Text;

            // Variabel untuk menyimpan filter
            string filter = "";

            // Filter berdasarkan nama barang
            if (!string.IsNullOrEmpty(cari))
            {
                filter += $"nama_barang LIKE '%{cari}%'";
            }

            // Filter berdasarkan kategori
            if (kategori != "Semua" && !string.IsNullOrEmpty(kategori))
            {
                if (filter != "")
                {
                    filter += " AND ";
                }

                filter += $"nama_kategori = '{kategori}'";
            }

            // Menampilkan hasil filter
            dv.RowFilter = filter;

            // Menampilkan hasil ke DataGridView
            dgvBarang.DataSource = dv;
        }

        // Tombol Detail
        private void btnDetail_Click(object sender, EventArgs e)
        {
            // Memberikan informasi kepada pengguna
            MessageBox.Show(
                "Silakan klik salah satu barang pada tabel untuk melihat detail.",
                "Informasi"
            );
        }
    }
}