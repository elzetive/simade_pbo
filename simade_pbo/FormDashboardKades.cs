using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDashboardKades : Form
    {
        // Membuat objek Barang_service agar dapat memanggil fungsi-fungsi yang berhubungan dengan data barang
        Barang_service barang = new Barang_service();

        // Constructor FormDashboardKades
        public FormDashboardKades()
        {
            // Menginisialisasi seluruh komponen yang ada pada Form
            InitializeComponent();

            // Mengatur agar form muncul di tengah layar
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Event yang dijalankan ketika Form pertama kali dibuka
        private void FormDashboardKades_Load(object sender, EventArgs e)
        {
            // BeginInvoke digunakan agar proses perhitungan dijalankan setelah form selesai dimuat
            // sehingga tampilan form tidak terasa lag atau freeze
            this.BeginInvoke(new MethodInvoker(HitungStatistikRealtime));

            // Memanggil method untuk menampilkan grafik peminjaman barang
            LoadChart();
        }

        // Method untuk menghitung statistik barang secara realtime
        private void HitungStatistikRealtime()
        {
            // Variabel untuk menyimpan total seluruh barang
            int totalBarangSemua = 0;

            // Variabel untuk menyimpan jumlah barang yang sedang dipinjam
            int totalSedangDipinjam = 0;

            // Variabel untuk menyimpan jumlah barang yang masih tersedia
            int totalSiapTersedia = 0;

            // Query SQL untuk menghitung:
            // 1. Total seluruh barang
            // 2. Total barang yang sedang dipinjam
            // 3. Total barang yang masih berstatus pending (booking)
            string queryDinamis = @"
                SELECT 
                    IFNULL(SUM(b.jumlah_barang), 0) AS total,
                    IFNULL(SUM(CASE WHEN p.status_peminjaman = 'dipinjam' THEN dp.jumlah_pinjam ELSE 0 END), 0) AS dipinjam,
                    IFNULL(SUM(CASE WHEN p.status_peminjaman = 'pending' THEN dp.jumlah_pinjam ELSE 0 END), 0) AS booking
                FROM barang b
                LEFT JOIN detail_peminjaman dp ON b.id_barang = dp.id_barang
                LEFT JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman";

            try
            {
                // Membuka koneksi ke database
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();

                    // Menjalankan query yang telah dibuat
                    using (MySqlCommand cmd = new MySqlCommand(queryDinamis, conn))
                    {
                        // Membaca hasil query
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Query khusus untuk mengambil total stok asli dari tabel barang
                                string queryTotalMurni = "SELECT IFNULL(SUM(jumlah_barang), 0) FROM barang";

                                // Reader harus ditutup terlebih dahulu sebelum menjalankan query lain
                                dr.Close();

                                // Menjalankan query total stok
                                using (MySqlCommand cmdTotal = new MySqlCommand(queryTotalMurni, conn))
                                {
                                    // Menyimpan total stok barang
                                    totalBarangSemua = Convert.ToInt32(cmdTotal.ExecuteScalar());
                                }

                                // Menjalankan kembali query pertama
                                using (MySqlDataReader dr2 = cmd.ExecuteReader())
                                {
                                    if (dr2.Read())
                                    {
                                        // Mengambil jumlah barang yang sedang dipinjam
                                        totalSedangDipinjam = Convert.ToInt32(dr2["dipinjam"]);

                                        // Mengambil jumlah barang yang masih pending
                                        int totalBooking = Convert.ToInt32(dr2["booking"]);

                                        // Menghitung stok yang masih tersedia
                                        // Rumus:
                                        // Total Barang - (Barang Dipinjam + Barang Pending)
                                        totalSiapTersedia = totalBarangSemua - (totalSedangDipinjam + totalBooking);
                                    }
                                }
                            }
                        }
                    }
                }

                // Jika hasil perhitungan negatif, ubah menjadi 0
                // Hal ini untuk menghindari kesalahan data pada database
                if (totalSiapTersedia < 0)
                    totalSiapTersedia = 0;

                // Menampilkan hasil perhitungan ke Label Dashboard
                lblTotal.Text = totalBarangSemua.ToString();
                lblPinjam.Text = totalSedangDipinjam.ToString();
                lblTersedia.Text = totalSiapTersedia.ToString();
            }
            catch (Exception ex)
            {
                // Menampilkan pesan error apabila terjadi kesalahan koneksi/database
                MessageBox.Show(
                    "Gagal mengalkulasi KPI statistik Kepala Desa: " + ex.Message,
                    "Error Database Dashboard");
            }
        }

        // Method untuk menampilkan grafik barang yang paling sering dipinjam
        private void LoadChart()
        {
            // Menghapus data grafik sebelumnya agar tidak terjadi data ganda
            chartPeminjaman.Series.Clear();
            chartPeminjaman.Titles.Clear();

            // Memberikan judul pada grafik
            chartPeminjaman.Titles.Add("Barang yang Paling Sering Dipinjam");

            // Mengambil area chart yang pertama
            ChartArea area = chartPeminjaman.ChartAreas[0];

            // Mengatur tampilan garis pada sumbu X
            area.AxisX.MajorGrid.LineColor = Color.LightGray;

            // Menonaktifkan garis pada sumbu Y agar grafik lebih rapi
            area.AxisY.MajorGrid.Enabled = false;

            // Mengambil data grafik dari Barang_service
            DataTable dt = barang.GrafikBarangTerpinjam();

            // Membuat objek series baru
            Series series = new Series();

            // Mengatur jenis grafik menjadi Bar (batang horizontal)
            series.ChartType = SeriesChartType.Bar;

            // Mengatur warna batang grafik
            series.Color = Color.RoyalBlue;

            // Menampilkan nilai pada setiap batang
            series.IsValueShownAsLabel = true;

            // Mengatur font angka pada batang grafik
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Menambahkan series ke dalam chart
            chartPeminjaman.Series.Add(series);

            // Menghilangkan legend karena hanya ada satu data
            chartPeminjaman.Legends.Clear();

            // Memastikan data tidak kosong
            if (dt != null)
            {
                // Melakukan perulangan setiap baris data
                foreach (DataRow row in dt.Rows)
                {
                    // Mengambil nama barang
                    string namaBarang = row["nama_barang"].ToString();

                    // Mengambil jumlah peminjaman barang
                    int jumlah = Convert.ToInt32(row["jumlah"]);

                    // Menambahkan data ke grafik
                    series.Points.AddXY(namaBarang, jumlah);
                }
            }
        }

        // Tombol Dashboard
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Tidak melakukan apa-apa karena pengguna sudah berada di halaman Dashboard
        }

        // Tombol menuju halaman Data Barang
        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Membuka FormBarangKades
            FormBarangKades frm = new FormBarangKades();
            frm.Show();

            // Menutup form dashboard agar memori lebih ringan
            this.Close();
        }

        // Tombol menuju halaman Riwayat Peminjaman
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            // Membuka FormRiwayat
            FormRiwayat frm = new FormRiwayat();
            frm.Show();

            // Menutup form dashboard
            this.Close();
        }

        // Tombol Logout
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Kembali ke halaman Login
            FormLogin login = new FormLogin();
            login.Show();

            // Menutup dashboard
            this.Close();
        }

        // Tombol Keluar Aplikasi
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi sebelum aplikasi ditutup
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin keluar?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Jika pengguna memilih Yes maka aplikasi ditutup
            if (konfirmasi == DialogResult.Yes)
                Application.Exit();
        }

        // Event-event berikut dibuat otomatis oleh Visual Studio
        // dan saat ini belum digunakan
        private void label1_Click(object sender, EventArgs e) { }
        private void lblTotal_Click(object sender, EventArgs e) { }
        private void lblTersedia_Click(object sender, EventArgs e) { }
        private void chart1_Click(object sender, EventArgs e) { }
        private void lblPinjam_Click(object sender, EventArgs e) { }
    }
}