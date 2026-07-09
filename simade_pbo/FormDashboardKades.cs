using MySql.Data.MySqlClient;
using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace simade_pbo
{
    // INHERITANCE: Mewarisi visual kontainer dasar dari class induk 'Form'.
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
            this.BeginInvoke(new MethodInvoker(HitungStatistikRealtime));

            // Inisialisasi data filter sebelum grafik dimuat pertama kali
            LoadFilterBarang();
            LoadFilterBulan();

            // Pasang event handler dinamis: grafik otomatis update tiap filter diubah kades
            cbBarang.SelectedIndexChanged += (s, ev) => LoadChartDinamis();
            cbBulan.SelectedIndexChanged += (s, ev) => LoadChartDinamis();

            LoadChartDinamis();
        }

        // ==========================================================
        // DATA POPULATION: Mengisi ComboBox Filter dari Database
        // ==========================================================
        private void LoadFilterBarang()
        {
            cbBarang.Items.Clear();
            cbBarang.Items.Add("== Semua Barang ==");

            string query = "SELECT nama_barang FROM barang ORDER BY nama_barang ASC";
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cbBarang.Items.Add(dr["nama_barang"].ToString());
                        }
                    }
                }
                cbBarang.SelectedIndex = 0; // Default: Semua barang
            }
            catch (Exception ex) { MessageBox.Show("Gagal memuat filter barang: " + ex.Message); }
        }

        private void LoadFilterBulan()
        {
            cbBulan.Items.Clear();
            cbBulan.Items.Add("== Semua Bulan ==");
            cbBulan.Items.AddRange(new string[] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" });
            cbBulan.SelectedIndex = 0; // Default: Semua bulan
        }

        // ==========================================================
        // STATISTIK KANBAN CARD LOGIC (REALTIME)
        // ==========================================================
        private void HitungStatistikRealtime()
        {
            int totalBarangSemua = 0, totalSedangDipinjam = 0, totalSiapTersedia = 0;
            string query = @"
                SELECT b.jumlah_barang, b.kondisi_bagus,
                       IFNULL((SELECT SUM(dp.jumlah_pinjam) FROM detail_peminjaman dp
                               INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                               WHERE dp.id_barang = b.id_barang AND LOWER(p.status_peminjaman) IN ('dipinjam','disetujui')), 0) AS jumlah_dipinjam
                FROM barang b";

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            totalBarangSemua += Convert.ToInt32(dr["jumlah_barang"]);
                            int dipinjam = Convert.ToInt32(dr["jumlah_dipinjam"]);
                            totalSedangDipinjam += dipinjam;
                            int tersedia = Convert.ToInt32(dr["kondisi_bagus"]) - dipinjam;
                            if (tersedia < 0) tersedia = 0;
                            totalSiapTersedia += tersedia;
                        }
                    }
                }
                lblTotal.Text = totalBarangSemua.ToString();
                lblPinjam.Text = totalSedangDipinjam.ToString();
                lblTersedia.Text = totalSiapTersedia.ToString();
            }
            catch (Exception ex) { MessageBox.Show("Gagal memuat statistik: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ==========================================================
        // DYNAMIC CHARTING MECHANISM: Filter Barang & Bulan (Timestamp)
        // ==========================================================
        private void LoadChartDinamis()
        {
            chartPeminjaman.Series.Clear();
            chartPeminjaman.Titles.Clear();
            chartPeminjaman.Titles.Add("Grafik Distribusi Peminjaman Aset Desa");

            ChartArea area = chartPeminjaman.ChartAreas[0];
            area.AxisX.Title = "Bulan / Aset";
            area.AxisY.Title = "Total Unit Terpinjam";
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            Series series = new Series
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(59, 130, 246), // Konsisten memakai warna biru modern tema
                IsValueShownAsLabel = true
            };
            chartPeminjaman.Series.Add(series);

            // LOGIKA QUERY FILTRATION: Mengambil data riil berdasarkan timestamp record peminjaman
            string query = @"
                SELECT 
                    MONTH(p.tgl_pinjam) AS bulan_angka,
                    b.nama_barang,
                    SUM(dp.jumlah_pinjam) AS total_jumlah
                FROM detail_peminjaman dp
                INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                INNER JOIN barang b ON dp.id_barang = b.id_barang
                WHERE p.status_peminjaman IN ('dipinjam', 'disetujui', 'dikembalikan') ";

            // Suntik klausa WHERE tambahan secara dinamis berdasarkan input ComboBox Kades
            if (cbBarang.SelectedIndex > 0)
                query += " AND b.nama_barang = @namaBarang ";
            if (cbBulan.SelectedIndex > 0)
                query += " AND MONTH(p.tgl_pinjam) = @bulanAngka ";

            query += " GROUP BY MONTH(p.tgl_pinjam), b.nama_barang ORDER BY bulan_angka ASC";

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (cbBarang.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@namaBarang", cbBarang.SelectedItem.ToString());
                        if (cbBulan.SelectedIndex > 0)
                            cmd.Parameters.AddWithValue("@bulanAngka", cbBulan.SelectedIndex);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow row in dt.Rows)
                            {
                                int bln = Convert.ToInt32(row["bulan_angka"]);
                                int total = Convert.ToInt32(row["total_jumlah"]);
                                string barangNama = row["nama_barang"].ToString();

                                string[] namaBulanArr = { "", "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agu", "Sep", "Okt", "Nov", "Des" };
                                string labelX = namaBulanArr[bln];

                                // Jika memfilter satu barang, sumbu X fokus menampilkan urutan bulan
                                // Jika memfilter satu bulan, sumbu X fokus membandingkan nama barang
                                if (cbBarang.SelectedIndex > 0) labelX = namaBulanArr[bln];
                                else if (cbBulan.SelectedIndex > 0) labelX = barangNama;
                                else labelX = $"{barangNama} ({namaBulanArr[bln]})";

                                series.Points.AddXY(labelX, total);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Gagal memperbarui grafik: " + ex.Message); }
        }

        // ==========================================================
        // NAVIGATION CONTROL SYSTEM
        // ==========================================================
        private void btnDashboard_Click(object sender, EventArgs e) { HitungStatistikRealtime(); LoadChartDinamis(); }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();
            this.Close();
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Close();
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
        private void lblPinjam_Click(object sender, EventArgs e) { }
    }
}