using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPinjam : Form
    {
        public FormDataPinjam()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormDataPinjam_Load);
            this.dgvTabelList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellClick);
        }

        private void FormDataPinjam_Load(object sender, EventArgs e)
        {
            dgvTabelList.ReadOnly = true;
            dgvTabelList.AutoGenerateColumns = false;

            // Pemetaan nama kolom DataGridView (sesuai nama objek di desainer) ke nama kolom kueri SQL
            Peminjam.DataPropertyName = "nama_peminjam";
            Barang.DataPropertyName = "nama_barang";
            jumlah_pinjam.DataPropertyName = "jumlah_pinjam";
            Waktu_Pinjam.DataPropertyName = "tgl_pinjam";
            Waktu_Kembali.DataPropertyName = "tgl_kembali";
            status.DataPropertyName = "status_peminjaman";

            TampilkanListPeminjam();
            HitungRingkasanStatistik();
        }

        public void TampilkanListPeminjam()
        {
            // Kueri join table untuk mengambil data peminjaman beserta detail profil user warga dan sub_kategori barang
            string query = @"SELECT p.kode_peminjaman,
                                    u.nama_lengkap AS nama_peminjam, u.alamat, u.no_hp,
                                    b.nama_barang, sk.nama_sub AS nama_kategori, 1 AS jumlah_pinjam,
                                    DATE_FORMAT(p.tgl_pinjam, '%d/%m/%Y') AS tgl_pinjam, 
                                    IFNULL(DATE_FORMAT(p.tgl_kembali, '%d/%m/%Y'), '-') AS tgl_kembali,
                                    p.status_peminjaman
                             FROM peminjaman p
                             INNER JOIN user u ON p.id_user = u.id_user
                             INNER JOIN barang b ON p.id_barang = b.id_barang
                             INNER JOIN sub_kategori sk ON b.id_sub = sk.id_sub
                             ORDER BY p.id_pinjam DESC";

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvTabelList.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data log peminjaman: " + ex.Message, "Database Error");
            }
        }

        private void HitungRingkasanStatistik()
        {
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    // Menghitung jumlah total pengajuan masuk
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM peminjaman", conn))
                    {
                        lblTotal.Text = cmd.ExecuteScalar().ToString();
                    }
                    // Menghitung jumlah berkas yang sudah disetujui (diterima/dipinjam)
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM peminjaman WHERE status_peminjaman = 'dipinjam'", conn))
                    {
                        lblDipinjam.Text = cmd.ExecuteScalar().ToString();
                    }
                    // Menghitung jumlah berkas yang ditolak / selesai
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM peminjaman WHERE status_peminjaman = 'dikembalikan'", conn))
                    {
                        lblTersedia.Text = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception) { }
            }
        }

        // EVENT CELL CLICK: Saat baris di tabel diklik, data profil lengkap langsung mengisi TextBox Detail di bawah
        private void dgvTabelList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dgvTabelList.DataSource;
                    DataRowView drv = (DataRowView)bs[e.RowIndex];

                    if (drv != null)
                    {
                        txtNama_Lengkap.Text = drv["nama_peminjam"].ToString();
                        txtAlamat.Text = drv["alamat"].ToString();
                        txtNo_Hp.Text = drv["no_hp"].ToString();
                        txtNama_Barang.Text = drv["nama_barang"].ToString();
                        txtKategori_Barang.Text = drv["nama_kategori"].ToString();
                        txtJumlah_Pinjam.Text = drv["jumlah_pinjam"].ToString();
                        txtTgl_Pinjam.Text = drv["tgl_pinjam"].ToString();
                        txtTgl_Kembali.Text = drv["tgl_kembali"].ToString();
                        txtStatus_Pinjam.Text = drv["status_peminjaman"].ToString().ToUpper();
                    }
                }
                catch (Exception) { }
            }
        }

        // PERBAIKAN UTAMA: Menyediakan fungsi Batal/Clear Form yang diminta oleh file Designer
        private void btnBatalKategori_Click(object sender, EventArgs e)
        {
            txtNama_Lengkap.Clear();
            txtAlamat.Clear();
            txtNo_Hp.Clear();
            txtNama_Barang.Clear();
            txtKategori_Barang.Clear();
            txtJumlah_Pinjam.Clear();
            txtTgl_Pinjam.Clear();
            txtTgl_Kembali.Clear();
            txtStatus_Pinjam.Clear();
            txtCari.Clear();
            TampilkanListPeminjam();
        }

        // --- Sistem Navigasi Aliran Sidebar Admin ---
        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            frm.Show();
            this.Close();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            TampilkanListPeminjam();
            HitungRingkasanStatistik();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan frm = new FormDataPengambilan();
            frm.Show();
            this.Close();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian frm = new FormDataPengembalian();
            frm.Show();
            this.Close();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin halamanRegisterAdmin = new FormRegisterAdmin();
            halamanRegisterAdmin.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin halamanLogin = new FormLogin();
                halamanLogin.Show();
                this.Close();
            }
        }
    }
}