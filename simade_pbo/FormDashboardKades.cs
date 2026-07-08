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
        // Membuat objek Barang_service agar dapat mengakses data barang dari database
        Barang_service barang = new Barang_service();

        // Constructor Form Dashboard Kepala Desa
        public FormDashboardKades()
        {
            // Menginisialisasi seluruh komponen Form
            InitializeComponent();

            // Mengatur posisi form agar muncul di tengah layar
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Event yang dijalankan saat Form pertama kali dibuka
        private void FormDashboardKades_Load(object sender, EventArgs e)
        {
            // BeginInvoke digunakan agar proses perhitungan statistik dijalankan
            // setelah seluruh komponen form selesai dibuat sehingga tampilan lebih responsif
            this.BeginInvoke(new MethodInvoker(HitungStatistikRealtime));

            // Memanggil method untuk menampilkan grafik barang yang paling sering dipinjam
            LoadChart();
        }

        // Method untuk menghitung seluruh statistik Dashboard Kepala Desa secara realtime
        private void HitungStatistikRealtime()
        {
            // Variabel untuk menyimpan total seluruh barang
            int totalBarangSemua = 0;

            // Variabel untuk menyimpan total barang yang sedang dipinjam
            int totalSedangDipinjam = 0;

            // Variabel untuk menyimpan total barang yang masih tersedia
            // Khusus Dashboard Kepala Desa hanya menghitung barang dengan kondisi bagus
            int totalSiapTersedia = 0;

            // Query mengambil seluruh data barang beserta jumlah yang sedang dipinjam
            // Barang yang dihitung sebagai dipinjam hanya yang berstatus
            // "dipinjam" atau "disetujui"
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

                    // Membaca seluruh data hasil query
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Mengambil total stok barang
                            int jumlahBarang = Convert.ToInt32(dr["jumlah_barang"]);

                            // Mengambil jumlah barang yang masih dalam kondisi bagus
                            int kondisiBagus = Convert.ToInt32(dr["kondisi_bagus"]);

                            // Mengambil jumlah barang yang sedang dipinjam
                            int dipinjam = Convert.ToInt32(dr["jumlah_dipinjam"]);

                            // Menjumlahkan seluruh stok barang
                            totalBarangSemua += jumlahBarang;

                            // Menjumlahkan seluruh barang yang sedang dipinjam
                            totalSedangDipinjam += dipinjam;

                            // Barang tersedia dihitung dari kondisi bagus
                            // kemudian dikurangi barang yang sedang dipinjam
                            int tersedia = kondisiBagus - dipinjam;

                            // Apabila hasil negatif maka diubah menjadi nol
                            // agar tidak terjadi tampilan stok minus
                            if (tersedia < 0)
                                tersedia = 0;

                            // Menjumlahkan seluruh barang yang masih tersedia
                            totalSiapTersedia += tersedia;
                        }
                    }
                }

                // Menampilkan hasil perhitungan pada Card Dashboard

                // Total seluruh barang
                lblTotal.Text = totalBarangSemua.ToString();

                // Total barang yang sedang dipinjam
                lblPinjam.Text = totalSedangDipinjam.ToString();

                // Total barang yang masih tersedia (hanya kondisi bagus)
                lblTersedia.Text = totalSiapTersedia.ToString();
            }
            catch (Exception ex)
            {
                // Menampilkan pesan apabila terjadi kesalahan koneksi database
                MessageBox.Show(
                    "Gagal memuat statistik Dashboard Kepala Desa : " + ex.Message,
                    "Error Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Method untuk menampilkan grafik barang yang paling sering dipinjam
        private void LoadChart()
        {
            // Menghapus data grafik sebelumnya agar tidak terjadi data ganda
            chartPeminjaman.Series.Clear();
            chartPeminjaman.Titles.Clear();

            // Memberikan judul grafik
            chartPeminjaman.Titles.Add("Barang yang Paling Sering Dipinjam");

            // Mengambil area chart pertama
            ChartArea area = chartPeminjaman.ChartAreas[0];

            // Mengatur tampilan garis bantu pada sumbu X
            area.AxisX.MajorGrid.LineColor = Color.LightGray;

            // Menonaktifkan garis bantu pada sumbu Y agar tampilan lebih rapi
            area.AxisY.MajorGrid.Enabled = false;

            // Mengambil data grafik dari Barang_service
            DataTable dt = barang.GrafikBarangTerpinjam();

            // Membuat objek Series baru
            Series series = new Series();

            // Mengatur tipe grafik menjadi Bar Horizontal
            series.ChartType = SeriesChartType.Bar;

            // Memberikan warna batang grafik
            series.Color = Color.RoyalBlue;

            // Menampilkan nilai pada setiap batang grafik
            series.IsValueShownAsLabel = true;

            // Mengatur ukuran dan jenis font label grafik
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Menambahkan Series ke Chart
            chartPeminjaman.Series.Add(series);

            // Menghilangkan Legend karena hanya menggunakan satu Series
            chartPeminjaman.Legends.Clear();

            // Memastikan data hasil query tidak kosong
            if (dt != null)
            {
                // Menampilkan seluruh data ke grafik
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

        // ==========================================================
        // TOMBOL DASHBOARD
        // ==========================================================
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Tidak melakukan proses apa pun
            // karena pengguna sudah berada di halaman Dashboard
        }

        // ==========================================================
        // TOMBOL DATA BARANG
        // ==========================================================
        private void btnBarang_Click(object sender, EventArgs e)
        {
            // Membuka Form Data Barang
            FormBarangKades frm = new FormBarangKades();
            frm.Show();

            // Menutup Dashboard agar tidak terjadi penumpukan Form
            this.Close();
        }

        // ==========================================================
        // TOMBOL RIWAYAT PEMINJAMAN
        // ==========================================================
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            // Membuka Form Riwayat Peminjaman
            FormRiwayat frm = new FormRiwayat();
            frm.Show();

            // Menutup Dashboard
            this.Close();
        }

        // ==========================================================
        // TOMBOL LOGOUT
        // ==========================================================
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Membuka Form Login
            FormLogin login = new FormLogin();
            login.Show();

            // Menutup Dashboard Kepala Desa
            this.Close();
        }

        // ==========================================================
        // TOMBOL KELUAR APLIKASI
        // ==========================================================
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi sebelum aplikasi ditutup
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin keluar?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Jika pengguna memilih Ya maka aplikasi ditutup
            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ==========================================================
        // EVENT YANG DIBUAT OTOMATIS OLEH VISUAL STUDIO
        // Saat ini belum digunakan
        // ==========================================================
        private void label1_Click(object sender, EventArgs e) { }

        private void lblTotal_Click(object sender, EventArgs e) { }

        private void lblTersedia_Click(object sender, EventArgs e) { }

        private void chart1_Click(object sender, EventArgs e) { }

        private void lblPinjam_Click(object sender, EventArgs e) { }
    }
}