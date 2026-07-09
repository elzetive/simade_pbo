using MySql.Data.MySqlClient;
using simade_pbo.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace simade_pbo.Service
{
    // Class untuk mengelola seluruh proses pengambilan, penyimpanan, perubahan, dan penghapusan data barang
    internal class Barang_service : Barang_cls
    {
        string Query; // Menyimpan perintah SQL

        // Constructor
        public Barang_service()
        {
            Query = ""; // Memberi pengaturan awal pada variabel query
        }

        // Method untuk menjalankan query INSERT, UPDATE, dan DELETE
        private int jalankanNonQuery(string query)
        {
            int retVal = -1; // Variabel untuk menyimpan hasil eksekusi query

            using (MySqlConnection conn = Koneksi.GetConn()) // Membuka koneksi ke database
            {
                try
                {
                    conn.Open(); // Membuka koneksi MySQL

                    MySqlCommand cmd = new MySqlCommand(query, conn); // Membuat objek command menggunakan query yang diterima

                    retVal = cmd.ExecuteNonQuery(); // Menjalankan query dan mengembalikan jumlah baris yang terpengaruh
                }
                catch (Exception) { } // Error diabaikan karena nilai gagal sudah diwakili oleh retVal = -1
            }
             
            return retVal; // Mengembalikan hasil eksekusi query
        }

        // Method untuk menjalankan query SELECT dan mengembalikan hasil dalam bentuk DataTable
        private DataTable jalankanQuery(string query)
        {
            DataTable dt = new DataTable(); // Wadah kosong untuk menyimpan hasil query

            using (MySqlConnection conn = Koneksi.GetConn()) // Membuka koneksi database
            {
                try
                {
                    conn.Open(); // Membuka koneksi MySQL

                    using (MySqlCommand cmd = new MySqlCommand(query, conn)) // Membuat command SQL
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) // Adapter digunakan untuk mengambil hasil query
                        {
                            adapter.Fill(dt); // Mengisi DataTable dengan hasil query
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Pesan apabila terjadi kesalahan
                    MessageBox.Show(
                        "Gagal mengambil data : " + ex.Message,
                        "ERROR DATABASE",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            return dt; // Mengembalikan hasil query
        }

        // Method untuk mengecek apakah nama barang sudah ada di database atau belum
        public bool isExist(string nama_barang)
        {
            bool hasil = false; // Nilai awal dianggap belum ada

            Query = "SELECT COUNT(*) FROM barang WHERE nama_barang = '" + nama_barang.Replace("'", "''") + "'"; // Query untuk menghitung jumlah barang berdasarkan nama

            DataTable dt = jalankanQuery(Query); // Menjalankan query

            if (dt != null && dt.Rows.Count > 0) // Memastikan data berhasil diperoleh
            {
                int jumlah = Convert.ToInt32(dt.Rows[0][0]); // Mengambil jumlah data yang ditemukan

                if (jumlah > 0) // Jika jumlah lebih dari nol berarti barang sudah ada
                {
                    hasil = true;
                }
            }
            return hasil; // Mengembalikan hasil pengecekan
        }

        // Method untuk menghapus data barang berdasarkan id
        public int hapus(int id_barang)
        {
            // Query hapus data
            Query = "DELETE FROM barang WHERE id_barang = " + id_barang;

            // Menjalankan query delete
            return jalankanNonQuery(Query);
        }


        // Method untuk mengambil data grafik jumlah peminjaman setiap bulan
        public DataTable GrafikPeminjaman()
        {
            // Query menghitung jumlah transaksi peminjaman berdasarkan bulan
            Query = @"
                SELECT
                    MONTH(tgl_pinjam) AS bulan,
                    COUNT(*) AS jumlah
                FROM peminjaman
                GROUP BY MONTH(tgl_pinjam)
                ORDER BY MONTH(tgl_pinjam)";

            // Mengembalikan hasil query dalam bentuk DataTable
            return jalankanQuery(Query);
        }


        // Method untuk mengambil data 5 barang yang paling sering dipinjam
        public DataTable GrafikBarangTerpinjam()
        {
            // Query menghitung total peminjaman setiap barang
            Query = @"
                SELECT
                    b.nama_barang,
                    SUM(dp.jumlah_pinjam) AS jumlah
                FROM detail_peminjaman dp
                INNER JOIN barang b
                    ON dp.id_barang = b.id_barang
                INNER JOIN peminjaman p
                    ON dp.id_peminjaman = p.id_peminjaman
                WHERE LOWER(p.status_peminjaman)
                    IN ('dipinjam','disetujui')
                GROUP BY b.nama_barang
                ORDER BY jumlah DESC
                LIMIT 5";

            // Mengembalikan hasil query untuk ditampilkan pada grafik dashboard
            return jalankanQuery(Query);
        }

        // Method untuk mengambil seluruh data riwayat peminjaman
        public DataTable tampilRiwayat()
        {
            // Query mengambil data riwayat peminjaman beserta nama barang dan peminjam
            Query = @"
                SELECT
                    p.id_peminjaman,
                    b.nama_barang,
                    u.username,
                    p.tgl_pinjam,
                    p.tgl_kembali,
                    p.status_peminjaman
                FROM peminjaman p
                INNER JOIN user u
                    ON p.id_user = u.id_user
                INNER JOIN detail_peminjaman dp
                    ON p.id_peminjaman = dp.id_peminjaman
                INNER JOIN barang b
                    ON dp.id_barang = b.id_barang
                ORDER BY p.id_peminjaman DESC";

            // Mengembalikan data riwayat peminjaman
            return jalankanQuery(Query);
        }
    }
}

//FormDashboardAdmin hanya menggunakan jalankanNonQuery sampai hapus 