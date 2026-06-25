using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Menggunakan library MySQL

namespace simade_pbo
{
    public partial class FormDataPinjam : Form
    {
        // Alamat koneksi ke database MySQL XAMPP kamu
        private string connectionString = "server=localhost;database=simade_pbo;uid=root;pwd=;";

        public FormDataPinjam()
        {
            InitializeComponent();
        }

        // 1. EVENT UTAMA: Otomatis berjalan saat Form dibuka (HANYA ADA SATU SEKARANG)
        private void FormDataPinjam_Load(object sender, EventArgs e)
        {
            // Coba ambil data dari database MySQL terlebih dahulu
            bool suksesLoadDatabase = TampilkanListPeminjam();

            // JIKA database gagal terhubung (XAMPP mati), otomatis pakai Data Simulasi/Mock Data agar tidak kosongan
            if (!suksesLoadDatabase)
            {
                LoadDataSimulasi();
            }
        }

        // 2. Fungsi Ambil Data Asli dari MySQL Database
        public bool TampilkanListPeminjam()
        {
            string query = @"SELECT p.id_peminjaman AS 'No',
                                    u.nama_lengkap AS 'Nama Peminjam', 
                                    b.nama_barang AS 'Nama Barang', 
                                    p.tgl_pinjam AS 'Tanggal Pinjam', 
                                    p.tgl_kembali AS 'Tanggal Kembali',
                                    'Kembalikan' AS 'Aksi'
                             FROM peminjaman p
                             INNER JOIN user u ON p.id_user = u.id_user
                             INNER JOIN barang b ON p.id_barang = b.id_barang";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open(); // Buka jalur koneksi

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt); // Mengisi data hasil query ke dalam DataTable

                        // Bersihkan kolom desain manual agar tidak double
                        dgvTabelList.Columns.Clear();

                        // Masukkan data MySQL ke tabel
                        this.dgvTabelList.DataSource = dt;

                        // Rapikan ukuran kolom
                        dgvTabelList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        return true; // Berhasil terhubung ke database
                    }
                }
            }
            catch (Exception)
            {
                // Jika database mati/error, kembalikan nilai false tanpa memunculkan pop-up error yang mengganggu
                return false;
            }
        }

        // 3. Fungsi Ambil Data Cadangan/Simulasi (Jika MySQL XAMPP belum dinyalakan)
        private void LoadDataSimulasi()
        {
            // 1. Membersihkan kolom bawaan jika ada agar tidak bentrok
            dgvTabelList.DataSource = null;
            dgvTabelList.Columns.Clear();

            // 2. Membuat Header Kolom Tabel (Sesuai dengan gambar desainmu)
            dgvTabelList.Columns.Add("No", "No");
            dgvTabelList.Columns.Add("NamaPeminjam", "Nama Peminjam");
            dgvTabelList.Columns.Add("NamaBarang", "Nama Barang");
            dgvTabelList.Columns.Add("TanggalPinjam", "Tanggal Pinjam");
            dgvTabelList.Columns.Add("TanggalKembali", "Tanggal Kembali");
            dgvTabelList.Columns.Add("Aksi", "Aksi");

            // 3. Mengatur Ukuran Lebar Kolom secara Otomatis agar Rapi
            dgvTabelList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 4. Memasukkan Isian Data Pinjam (Mock Data / Simulasi)
            dgvTabelList.Rows.Add("1", "Devinta Yolanda", "Proyektor Epson", "22 Juni 2026", "25 Juni 2026", "Kembalikan");
            dgvTabelList.Rows.Add("2", "Andi Pratama", "Kabel HDMI 5m", "23 Juni 2026", "24 Juni 2026", "Kembalikan");
            dgvTabelList.Rows.Add("3", "Siti Aminah", "Pointer Logi", "25 Juni 2026", "26 Juni 2026", "Kembalikan");
            dgvTabelList.Rows.Add("4", "Budi Santoso", "Speaker Portable", "25 Juni 2026", "27 Juni 2026", "Kembalikan");

            // 5. Membuat tabel menjadi Read-Only (Hanya Baca)
            dgvTabelList.AllowUserToAddRows = false;
            dgvTabelList.ReadOnly = true;
        }

        // Tombol Kembali menuju Dashboard Admin
        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin dashboard = new FormDashboardAdmin();
            dashboard.Show();
            this.Hide();
        }

        // Tombol Log Out untuk menutup aplikasi keseluruhan
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // == JANGAN DIHAPUS: Kode penyelamat biar desainer tidak crash ==
        private void lblPeminjam_Click(object sender, EventArgs e) { }
        private void dgvNamaPeminjam_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblHeaderDataPinjam_Click(object sender, EventArgs e) { }
    }
}