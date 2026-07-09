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

            // Menampilkan grafik jumlah peminjaman barang per bulan
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
        // // Menampilkan grafik jumlah peminjaman barang per bulan
        // ==========================================================
        private void LoadChart()
        {
            chartPeminjaman.Series.Clear();
            chartPeminjaman.Titles.Clear();

            chartPeminjaman.Titles.Add("Grafik Peminjaman Barang per Bulan");

            ChartArea area = chartPeminjaman.ChartAreas[0];

            area.AxisX.Title = "Bulan";
            area.AxisY.Title = "Jumlah Barang Dipinjam";

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            Series series = new Series();

            series.ChartType = SeriesChartType.Column;
            series.Color = Color.RoyalBlue;
            series.IsValueShownAsLabel = true;

            chartPeminjaman.Series.Add(series);

            DataTable dt = barang.GrafikPeminjaman();

            foreach (DataRow row in dt.Rows)
            {
                int bulan = Convert.ToInt32(row["bulan"]);
                int jumlah = Convert.ToInt32(row["jumlah"]);

                string namaBulan = "";

                switch (bulan)
                {
                    case 1: namaBulan = "Jan"; break;
                    case 2: namaBulan = "Feb"; break;
                    case 3: namaBulan = "Mar"; break;
                    case 4: namaBulan = "Apr"; break;
                    case 5: namaBulan = "Mei"; break;
                    case 6: namaBulan = "Jun"; break;
                    case 7: namaBulan = "Jul"; break;
                    case 8: namaBulan = "Agu"; break;
                    case 9: namaBulan = "Sep"; break;
                    case 10: namaBulan = "Okt"; break;
                    case 11: namaBulan = "Nov"; break;
                    case 12: namaBulan = "Des"; break;
                }

                series.Points.AddXY(namaBulan, jumlah);
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