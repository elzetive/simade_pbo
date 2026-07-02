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
            //MuatBarangSedangDipinjam();
        }

        private void MuatBarangSedangDipinjam()
        {
            //using (MySqlConnection conn = Koneksi.GetConn())
            //{
            //    try
            //    {
            //        conn.Open();
            //        // Menampilkan berkas transaksi warga yang status fisiknya sedang di tangan warga (dipinjam)
            //        string query = @"SELECT p.kode_peminjaman AS 'Kode Nota', 
            //                                u.nama_lengkap AS 'Nama Peminjam', 
            //                                COUNT(p.id_barang) AS 'Total Barang', 
            //                                p.tgl_pinjam AS 'Tanggal Pinjam'
            //                         FROM peminjaman p
            //                         INNER JOIN user u ON p.id_user = u.id_user
            //                         WHERE p.status_peminjaman = 'dipinjam'
            //                         GROUP BY p.kode_peminjaman, u.nama_lengkap, p.tgl_pinjam";

            //        using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
            //        {
            //            DataTable dt = new DataTable();
            //            da.Fill(dt);
            //            dgvTabelList.DataSource = dt;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Gagal memuat data sirkulasi pinjam: " + ex.Message, "Error");
            //    }
            //}
        }
        //private void dgvTabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        string kodeNota = dgvTabelList.Rows[e.RowIndex].Cells["Kode Nota"].Value.ToString();
        //        DialogResult res = MessageBox.Show("Konfirmasi bahwa Nota: " + kodeNota + " sudah dikembalikan ke gudang desa?", "Siklus Inventaris", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (res == DialogResult.Yes)
        //        {
        //            using (MySqlConnection conn = Koneksi.GetConn())
        //            {
        //                MySqlTransaction tr = null;
        //                try
        //                {
        //                    conn.Open();
        //                    tr = conn.BeginTransaction();

        //                    string updPeminjaman = "UPDATE peminjaman SET status_peminjaman = 'dikembalikan', tgl_kembali = NOW() WHERE kode_peminjaman = @kode";
        //                    using (MySqlCommand cmd1 = new MySqlCommand(updPeminjaman, conn, tr))
        //                    {
        //                        cmd1.Parameters.AddWithValue("@kode", kodeNota);
        //                        cmd1.ExecuteNonQuery();
        //                    }

        //                    string updBarang = @"UPDATE barang 
        //                                         SET status = 'tersedia' 
        //                                         WHERE id_barang IN (SELECT id_barang FROM peminjaman WHERE kode_peminjaman = @kode)";
        //                    using (MySqlCommand cmd2 = new MySqlCommand(updBarang, conn, tr))
        //                    {
        //                        cmd2.Parameters.AddWithValue("@kode", kodeNota);
        //                        cmd2.ExecuteNonQuery();
        //                    }

        //                    tr.Commit();
        //                    MessageBox.Show("Sukses! Unit kembali tersedia di sistem peminjaman warga.", "Siklus Selesai");
        //                    MuatBarangSedangDipinjam();
        //                }
        //                catch (Exception ex)
        //                {
        //                    if (tr != null) tr.Rollback();
        //                    MessageBox.Show("Gagal memproses pengembalian barang: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //}

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            frm.Show();
            this.Hide();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam frm = new FormDataPinjam();
            frm.Show();
            this.Hide();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan frm = new FormDataPengambilan();
            frm.Show();
            this.Hide();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e) 
        {
            FormDataPengembalian frm = new FormDataPengembalian();
            frm.Show();
            this.Hide();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin frm = new FormRegisterAdmin();
            frm.Show();
            this.Hide();
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