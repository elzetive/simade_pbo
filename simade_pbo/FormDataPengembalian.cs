using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPengembalian : Form
    {
        public FormDataPengembalian()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormDataPengembalian_Load);
        }

        private void FormDataPengembalian_Load(object sender, EventArgs e)
        {
            MuatBarangSedangDipinjam();
        }

        private void MuatBarangSedangDipinjam()
        {
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    // Menampilkan berkas transaksi warga yang status fisiknya sedang di tangan warga (dipinjam)
                    string query = @"SELECT p.kode_peminjaman AS 'Kode Nota', 
                                            u.nama_lengkap AS 'Nama Peminjam', 
                                            COUNT(p.id_barang) AS 'Total Barang', 
                                            p.tgl_pinjam AS 'Tanggal Pinjam'
                                     FROM peminjaman p
                                     INNER JOIN user u ON p.id_user = u.id_user
                                     WHERE p.status_peminjaman = 'dipinjam'
                                     GROUP BY p.kode_peminjaman, u.nama_lengkap, p.tgl_pinjam";

                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvTabelList.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data sirkulasi pinjam: " + ex.Message, "Error");
                }
            }
        }

        // AKSI CELL CLICK KEMBALIKAN: Memutar balik status barang menjadi 'tersedia' kembali ke warga
        private void dgvTabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string kodeNota = dgvTabelList.Rows[e.RowIndex].Cells["Kode Nota"].Value.ToString();
                DialogResult res = MessageBox.Show("Konfirmasi bahwa Nota: " + kodeNota + " sudah dikembalikan ke gudang desa?", "Siklus Inventaris", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    using (MySqlConnection conn = Koneksi.GetConn())
                    {
                        MySqlTransaction tr = null;
                        try
                        {
                            conn.Open();
                            tr = conn.BeginTransaction();

                            // 1. Update status peminjaman di berkas menjadi dikembalikan beserta log waktu jam sekarang (NOW())
                            string updPeminjaman = "UPDATE peminjaman SET status_peminjaman = 'dikembalikan', tgl_kembali = NOW() WHERE kode_peminjaman = @kode";
                            using (MySqlCommand cmd1 = new MySqlCommand(updPeminjaman, conn, tr))
                            {
                                cmd1.Parameters.AddWithValue("@kode", kodeNota);
                                cmd1.ExecuteNonQuery();
                            }

                            // 2. SIKLUS BERPUTAR: Pulihkan status fisik unit di tabel barang menjadi tersedia untuk dipinjam lagi
                            string updBarang = @"UPDATE barang 
                                                 SET status = 'tersedia' 
                                                 WHERE id_barang IN (SELECT id_barang FROM peminjaman WHERE kode_peminjaman = @kode)";
                            using (MySqlCommand cmd2 = new MySqlCommand(updBarang, conn, tr))
                            {
                                cmd2.Parameters.AddWithValue("@kode", kodeNota);
                                cmd2.ExecuteNonQuery();
                            }

                            tr.Commit();
                            MessageBox.Show("Sukses! Unit kembali tersedia di sistem peminjaman warga.", "Siklus Selesai");
                            MuatBarangSedangDipinjam();
                        }
                        catch (Exception ex)
                        {
                            if (tr != null) tr.Rollback();
                            MessageBox.Show("Gagal memproses pengembalian barang: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            frm.Show();
            this.Close();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam frm = new FormDataPinjam();
            frm.Show();
            this.Close();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan frm = new FormDataPengambilan();
            frm.Show();
            this.Close();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e) { MuatBarangSedangDipinjam(); }

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
    }
}