using MySql.Data.MySqlClient;
using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace simade_pbo
{
    public partial class FormDashboardKades : Form
    {
        // Object Barang_service digunakan untuk mengambil
        // data barang dan statistik dari database
        Barang_service barang = new Barang_service();

        // ==========================================================
        // CONSTRUCTOR
        // Dijalankan ketika Form Dashboard Kepala Desa dibuka
        // ==========================================================
        public FormDashboardKades()
        {
            // Menginisialisasi seluruh komponen Form
            InitializeComponent();

            // Mengatur posisi Form agar muncul di tengah layar
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ==========================================================
        // Event Load
        // Dijalankan saat Dashboard pertama kali dibuka
        // ==========================================================
        private void FormDashboardKades_Load(object sender, EventArgs e)
        {
            // Menjalankan proses perhitungan statistik
            // setelah seluruh komponen Form selesai dimuat
            this.BeginInvoke(new MethodInvoker(HitungStatistikRealtime));

            // Menampilkan grafik barang yang paling sering dipinjam
            LoadChart();
        }

        // ==========================================================
        // Menghitung statistik Dashboard secara realtime
        // ==========================================================
        private void HitungStatistikRealtime()
        {
            // Menyimpan total seluruh barang
            int totalBarangSemua = 0;

            // Menyimpan jumlah barang yang sedang dipinjam
            int totalSedangDipinjam = 0;

            // Menyimpan jumlah barang yang masih tersedia
            // (hanya barang dengan kondisi bagus)
            int totalSiapTersedia = 0;

            // Query untuk mengambil data barang beserta
            // jumlah barang yang sedang dipinjam
            string query = @"
                SELECT
                    b.id_barang,
                    b.jumlah_barang,
                    b.kondisi_bagus,

                    IFNULL((
                        SELECT SUM(dp.jumlah_pinjam)
                        FROM detail_peminjaman dp
                        INNER JOIN peminjaman p
                            ON dp.id_peminjaman = p.id_peminjaman
                        WHERE dp.id_barang = b.id_barang
                        AND LOWER(p.status_peminjaman) IN ('dipinjam','disetujui')
                    ),0) AS jumlah_dipinjam

                FROM barang b";

            try
            {
                // Membuka koneksi ke database
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();

                    // Menjalankan query
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))

                    // Membaca seluruh hasil query
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Mengambil total stok barang
                            int jumlahBarang = Convert.ToInt32(dr["jumlah_barang"]);

                            // Mengambil jumlah barang dengan kondisi bagus
                            int kondisiBagus = Convert.ToInt32(dr["kondisi_bagus"]);

                            // Mengambil jumlah barang yang sedang dipinjam
                            int dipinjam = Convert.ToInt32(dr["jumlah_dipinjam"]);

                            // Menghitung total seluruh barang
                            totalBarangSemua += jumlahBarang;

                            // Menghitung total barang yang sedang dipinjam
                            totalSedangDipinjam += dipinjam;

                            // Menghitung stok yang masih tersedia
                            int tersedia = kondisiBagus - dipinjam;

                            // Mencegah hasil stok bernilai negatif
                            if (tersedia < 0)
                                tersedia = 0;

                            // Menjumlahkan stok yang tersedia
                            totalSiapTersedia += tersedia;
                        }
                    }
                }

                // Menampilkan hasil perhitungan pada Dashboard

                // Total seluruh barang
                lblTotal.Text = totalBarangSemua.ToString();

                // Total barang yang sedang dipinjam
                lblPinjam.Text = totalSedangDipinjam.ToString();

                // Total barang yang masih tersedia
                lblTersedia.Text = totalSiapTersedia.ToString();
            }
            catch (Exception ex)
            {
                // Menampilkan pesan jika koneksi database gagal
                MessageBox.Show(
                    "Gagal memuat statistik Dashboard Kepala Desa : " + ex.Message,
                    "Error Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // Menampilkan grafik barang yang paling sering dipinjam
        // ==========================================================
        private void LoadChart()
        {
            // Menghapus data grafik sebelumnya
            chartPeminjaman.Series.Clear();
            chartPeminjaman.Titles.Clear();

            // Memberikan judul grafik
            chartPeminjaman.Titles.Add("Barang yang Paling Sering Dipinjam");

            // Mengambil area Chart
            ChartArea area = chartPeminjaman.ChartAreas[0];

            // Mengatur tampilan garis bantu sumbu X
            area.AxisX.MajorGrid.LineColor = Color.LightGray;

            // Menonaktifkan garis bantu sumbu Y
            area.AxisY.MajorGrid.Enabled = false;

            // Mengambil data grafik dari Barang_service
            DataTable dt = barang.GrafikBarangTerpinjam();

            // Membuat objek Series baru
            Series series = new Series();

            // Mengatur jenis grafik menjadi Bar Horizontal
            series.ChartType = SeriesChartType.Bar;

            // Memberikan warna batang grafik
            series.Color = Color.RoyalBlue;

            // Menampilkan nilai pada setiap batang grafik
            series.IsValueShownAsLabel = true;

            // Mengatur jenis dan ukuran font
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Menambahkan Series ke Chart
            chartPeminjaman.Series.Add(series);

            // Menghilangkan Legend karena hanya menggunakan satu Series
            chartPeminjaman.Legends.Clear();

            // Memastikan data tidak kosong
            if (dt != null)
            {
                // Menampilkan seluruh data ke dalam grafik
                foreach (DataRow row in dt.Rows)
                {
                    // Mengambil nama barang
                    string namaBarang = row["nama_barang"].ToString();

                    // Mengambil jumlah peminjaman
                    int jumlah = Convert.ToInt32(row["jumlah"]);

                    // Menambahkan data ke grafik
                    series.Points.AddXY(namaBarang, jumlah);
                }
            }
        }

        // ==========================================================
        // Tombol Dashboard
        // Tidak menjalankan proses karena pengguna
        // sudah berada di halaman Dashboard
        // ==========================================================
        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        // ==========================================================
        // Tombol Data Barang
        // Berpindah ke halaman Data Barang
        // ==========================================================
        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Membuka Form Barang
            FormBarangKades frm = new FormBarangKades();
            frm.Show();

            // Menutup Dashboard
            this.Close();
        }

        // ==========================================================
        // Tombol Riwayat
        // Berpindah ke halaman Riwayat Peminjaman
        // ==========================================================
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            // Membuka Form Riwayat
            FormRiwayat frm = new FormRiwayat();
            frm.Show();

            // Menutup Dashboard
            this.Close();
        }

        // ==========================================================
        // Tombol Logout
        // Keluar dari akun dan kembali ke halaman Login
        // ==========================================================
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Membuka Form Login
            FormLogin login = new FormLogin();
            login.Show();

            // Menutup Dashboard
            this.Close();
        }

        // ==========================================================
        // Tombol Keluar
        // Menutup seluruh aplikasi
        // ==========================================================
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi sebelum keluar
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin keluar?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Jika memilih Ya maka aplikasi ditutup
            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ==========================================================
        // Event bawaan Visual Studio
        // Saat ini belum digunakan
        // ==========================================================
        private void label1_Click(object sender, EventArgs e) { }

        private void lblTotal_Click(object sender, EventArgs e) { }

        private void lblTersedia_Click(object sender, EventArgs e) { }

        private void chart1_Click(object sender, EventArgs e) { }

        private void lblPinjam_Click(object sender, EventArgs e) { }
    }
}