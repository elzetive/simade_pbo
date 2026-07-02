using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPengambilan : Form
    {
        public FormDataPengambilan()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormDataPengambilan_Load);
        }

        private void FormDataPengambilan_Load(object sender, EventArgs e)
        {
            MuatAntreanPending();
        }

        private void MuatAntreanPending()
        {
            //using (MySqlConnection conn = Koneksi.GetConn())
            //{
            //    try
            //    {
            //        conn.Open();
            //        // Mengambil kelompok transaksi warga yang berstatus pending untuk divalidasi
            //        string query = @"SELECT p.kode_peminjaman AS 'Kode Nota', 
            //                                u.nama_lengkap AS 'Nama Peminjam', 
            //                                COUNT(p.id_barang) AS 'Total Item', 
            //                                p.tgl_pinjam AS 'Tanggal Pengajuan'
            //                         FROM peminjaman p
            //                         INNER JOIN user u ON p.id_user = u.id_user
            //                         WHERE p.status_peminjaman = 'pending'
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
            //        MessageBox.Show("Gagal memuat antrean pending: " + ex.Message, "Database Error");
            //    }
            //}
        }

        // Aksi Klik Grid untuk menyetujui Peminjaman Berkas (Pending -> Dipinjam)
        private void dgvTabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    string kodeNota = dgvTabelList.Rows[e.RowIndex].Cells["Kode Nota"].Value.ToString();
            //    DialogResult res = MessageBox.Show("Setujui pengambilan aset untuk Nota: " + kodeNota + "?", "Konfirmasi Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (res == DialogResult.Yes)
            //    {
            //        using (MySqlConnection conn = Koneksi.GetConn())
            //        {
            //            try
            //            {
            //                conn.Open();
            //                string updateQuery = "UPDATE peminjaman SET status_peminjaman = 'dipinjam' WHERE kode_peminjaman = @kode";
            //                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
            //                {
            //                    cmd.Parameters.AddWithValue("@kode", kodeNota);
            //                    cmd.ExecuteNonQuery();
            //                }
            //                MessageBox.Show("Berkas sukses diubah menjadi 'DIPINJAM'!", "Sukses Validasi");
            //                MuatAntreanPending();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show("Gagal menyetujui berkas: " + ex.Message);
            //            }
            //        }
            //    }
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

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

        private void dgvTabelList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}