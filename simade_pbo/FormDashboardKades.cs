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
        Barang_service barang = new Barang_service();

        public FormDashboardKades()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormDashboardKades_Load(object sender, EventArgs e)
        {
            // Menggunakan BeginInvoke agar form terbuka sangat ringan tanpa lag UI
            this.BeginInvoke(new MethodInvoker(HitungStatistikRealtime));
            LoadChart();
        }

        private void HitungStatistikRealtime()
        {
            int totalBarangSemua = 0;
            int totalSedangDipinjam = 0;
            int totalSiapTersedia = 0;

            // Kueri Cerdas: Mengalkulasi sirkulasi riil di RAM agar sinkron di semua Role (Admin & Kades)
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
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryDinamis, conn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Ambil data master total dari database schema
                                string queryTotalMurni = "SELECT IFNULL(SUM(jumlah_barang), 0) FROM barang";
                                dr.Close(); // Tutup reader pertama untuk eksekusi skalar master

                                using (MySqlCommand cmdTotal = new MySqlCommand(queryTotalMurni, conn))
                                {
                                    totalBarangSemua = Convert.ToInt32(cmdTotal.ExecuteScalar());
                                }

                                // Jalankan ulang untuk hitung sisa sirkulasi warga
                                using (MySqlDataReader dr2 = cmd.ExecuteReader())
                                {
                                    if (dr2.Read())
                                    {
                                        totalSedangDipinjam = Convert.ToInt32(dr2["dipinjam"]);
                                        int totalBooking = Convert.ToInt32(dr2["booking"]);

                                        // RUMUS EMAS: Total barang dikurangi yang dipinjam dan yang di-booking warga
                                        totalSiapTersedia = totalBarangSemua - (totalSedangDipinjam + totalBooking);
                                    }
                                }
                            }
                        }
                    }
                }

                // Menghindari nilai negatif palsu jika ada anomali input data
                if (totalSiapTersedia < 0) totalSiapTersedia = 0;

                // Tampilkan ke Label Dashboard Pak Kades
                lblTotal.Text = totalBarangSemua.ToString();
                lblPinjam.Text = totalSedangDipinjam.ToString();
                lblTersedia.Text = totalSiapTersedia.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengalkulasi KPI statistik Kepala Desa: " + ex.Message, "Error Database Dashboard");
            }
        }

        private void LoadChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            chart1.Titles.Add("Barang yang Paling Sering Dipinjam");

            ChartArea area = chart1.ChartAreas[0];

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.Enabled = false;

            DataTable dt = barang.GrafikBarangTerpinjam();

            Series series = new Series();
            series.ChartType = SeriesChartType.Bar;
            series.Color = Color.RoyalBlue;
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            chart1.Series.Add(series);

            // HILANGKAN tulisan "Jumlah Dipinjam" agar chart bersih
            chart1.Legends.Clear();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string namaBarang = row["nama_barang"].ToString();
                    int jumlah = Convert.ToInt32(row["jumlah"]);

                    series.Points.AddXY(namaBarang, jumlah);
                }
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Sudah berada di Dashboard
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();
            this.Close(); // Menggunakan .Close() agar memori laptop enteng
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Close(); // Menggunakan .Close() agar memori laptop enteng
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void lblTotal_Click(object sender, EventArgs e) { }
        private void lblTersedia_Click(object sender, EventArgs e) { }
        private void chart1_Click(object sender, EventArgs e) { }
    }
}