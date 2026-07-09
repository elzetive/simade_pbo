using MySql.Data.MySqlClient;
using simade_pbo.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace simade_pbo.Service
{
    // Class service yang digunakan untuk mengelola seluruh proses
    // pengambilan, penyimpanan, perubahan, dan penghapusan data barang
    internal class Barang_service : Barang_cls
    {
        // Menyimpan query SQL yang akan dijalankan
        string Query;

        // Constructor Barang_service
        public Barang_service()
        {
            // Menginisialisasi variabel query
            Query = "";
        }

        // ==========================================================
        // Method untuk menjalankan query INSERT, UPDATE, dan DELETE
        // ==========================================================
        private int jalankanNonQuery(string query)
        {
            // Variabel untuk menyimpan hasil eksekusi query
            int retVal = -1;

            // Membuka koneksi ke database
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    // Membuka koneksi MySQL
                    conn.Open();

                    // Membuat objek command menggunakan query yang diterima
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Menjalankan query dan mengembalikan jumlah baris yang terpengaruh
                    retVal = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    // Error diabaikan karena nilai gagal sudah diwakili oleh retVal = -1
                }
            }

            // Mengembalikan hasil eksekusi query
            return retVal;
        }

        // ==========================================================
        // Method untuk menjalankan query SELECT
        // dan mengembalikan hasil dalam bentuk DataTable
        // ==========================================================
        private DataTable jalankanQuery(string query)
        {
            // Menyimpan hasil query
            DataTable dt = new DataTable();

            // Membuka koneksi database
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    // Membuka koneksi MySQL
                    conn.Open();

                    // Membuat command SQL
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Adapter digunakan untuk mengambil hasil query
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            // Mengisi DataTable dengan hasil query
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Menampilkan pesan apabila terjadi kesalahan
                    MessageBox.Show(
                        "Gagal mengambil data : " + ex.Message,
                        "ERROR DATABASE",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            // Mengembalikan hasil query
            return dt;
        }

        // ==========================================================
        // Mengecek apakah nama barang sudah ada di database
        // ==========================================================
        public bool isExist(string nama_barang)
        {
            // Nilai awal dianggap belum ada
            bool hasil = false;

            // Query untuk menghitung jumlah barang berdasarkan nama
            Query = "SELECT COUNT(*) FROM barang WHERE nama_barang = '" +
                    nama_barang.Replace("'", "''") + "'";

            // Menjalankan query
            DataTable dt = jalankanQuery(Query);

            // Memastikan data berhasil diperoleh
            if (dt != null && dt.Rows.Count > 0)
            {
                // Mengambil jumlah data yang ditemukan
                int jumlah = Convert.ToInt32(dt.Rows[0][0]);

                // Jika jumlah lebih dari nol berarti barang sudah ada
                if (jumlah > 0)
                {
                    hasil = true;
                }
            }

            // Mengembalikan hasil pengecekan
            return hasil;
        }

        // ==========================================================
        // Menyimpan data barang baru ke database
        // ==========================================================
        public int simpan()
        {
            // Barang baru otomatis memiliki kondisi bagus
            // sesuai jumlah barang yang dimasukkan
            Query =
                "INSERT INTO barang " +
                "(id_kategori, nama_barang, jumlah_barang, kondisi_bagus, kondisi_rusak) " +
                "VALUES (" +
                Id_kategori + ", '" +
                Nama_barang + "', " +
                Jumlah_barang + ", " +
                Jumlah_barang + ", 0)";

            // Menjalankan proses penyimpanan
            return jalankanNonQuery(Query);
        }

        // Method untuk mengubah data barang
        public int ubah(int id)
        {
            // Variabel untuk menyimpan kondisi barang sebelum diubah
            int kondisiBagusLama = 0;
            int kondisiRusakLama = 0;
            int jumlahTotalLama = 0;

            // Query untuk mengambil data barang berdasarkan id
            string queryCek = "SELECT jumlah_barang, kondisi_bagus, kondisi_rusak " +
                              "FROM barang WHERE id_barang = " + id;

            // Menjalankan query
            DataTable dtCek = jalankanQuery(queryCek);

            // Jika data ditemukan
            if (dtCek != null && dtCek.Rows.Count > 0)
            {
                // Menyimpan data lama ke variabel
                jumlahTotalLama = Convert.ToInt32(dtCek.Rows[0]["jumlah_barang"]);
                kondisiBagusLama = Convert.ToInt32(dtCek.Rows[0]["kondisi_bagus"]);
                kondisiRusakLama = Convert.ToInt32(dtCek.Rows[0]["kondisi_rusak"]);
            }

            // Menghitung selisih jumlah barang lama dan baru
            int selisih = this.Jumlah_barang - jumlahTotalLama;

            // Menyimpan kondisi baru
            int kondisiBagusBaru = kondisiBagusLama;
            int kondisiRusakBaru = kondisiRusakLama;

            // Jika jumlah barang bertambah
            if (selisih > 0)
            {
                // Tambahkan ke kondisi bagus
                kondisiBagusBaru += selisih;
            }
            // Jika jumlah barang berkurang
            else if (selisih < 0)
            {
                // Kurangi dari kondisi bagus
                kondisiBagusBaru += selisih;

                // Jika kondisi bagus menjadi negatif
                if (kondisiBagusBaru < 0)
                {
                    // Kekurangannya diambil dari kondisi rusak
                    kondisiRusakBaru += kondisiBagusBaru;

                    // Kondisi bagus tidak boleh kurang dari nol
                    kondisiBagusBaru = 0;

                    // Kondisi rusak juga tidak boleh negatif
                    if (kondisiRusakBaru < 0)
                        kondisiRusakBaru = 0;
                }
            }

            // Query untuk mengubah data barang
            Query = @"
                UPDATE barang SET
                    nama_barang = '" + this.Nama_barang + @"',
                    id_kategori = " + this.Id_kategori + @",
                    jumlah_barang = " + this.Jumlah_barang + @",
                    kondisi_bagus = " + kondisiBagusBaru + @",
                    kondisi_rusak = " + kondisiRusakBaru + @"
                WHERE id_barang = " + id;

            // Menjalankan query update
            return jalankanNonQuery(Query);
        }

        // Method untuk menghapus data barang berdasarkan id
        public int hapus(int id_barang)
        {
            // Query hapus data
            Query = "DELETE FROM barang WHERE id_barang = " + id_barang;

            // Menjalankan query delete
            return jalankanNonQuery(Query);
        }

        // Method untuk mengambil detail kondisi barang
        public DataTable DetailKondisi(string namaBarang)
        {
            // Query mengambil jumlah barang bagus dan rusak
            Query = @"
                SELECT
                    kondisi_bagus,
                    kondisi_rusak
                FROM barang
                WHERE nama_barang = '" + namaBarang.Replace("'", "''") + @"'
                LIMIT 1";

            // Mengembalikan hasil query
            return jalankanQuery(Query);
        }

        // Method untuk mengubah jumlah barang bagus dan rusak
        public int ubahDetailKondisi(int id, int bagus, int rusak)
        {
            // Menghitung total barang dari kondisi bagus dan rusak
            int totalBaru = bagus + rusak;

            // Query update kondisi barang
            Query = @"
                UPDATE barang SET
                    kondisi_bagus = " + bagus + @",
                    kondisi_rusak = " + rusak + @",
                    jumlah_barang = " + totalBaru + @"
                WHERE id_barang = " + id;

            // Menjalankan query update
            return jalankanNonQuery(Query);
        }

        // Method untuk menghitung statistik barang pada dashboard
        public string hitungStatistik(string tipe)
        {
            // Nilai awal jika belum ada data
            string hasil = "0";

            // Jika yang diminta adalah total seluruh barang
            if (tipe == "TOTAL")
            {
                Query = @"
                    SELECT IFNULL(SUM(jumlah_barang),0)
                    FROM barang";
            }

            // Jika yang diminta adalah total barang yang sedang dipinjam
            else if (tipe == "DIPINJAM")
            {
                Query = @"
                    SELECT IFNULL(SUM(dp.jumlah_pinjam),0)
                    FROM detail_peminjaman dp
                    INNER JOIN peminjaman p
                        ON dp.id_peminjaman = p.id_peminjaman
                    WHERE LOWER(p.status_peminjaman)
                        IN ('dipinjam','disetujui')";
            }

            // Jika yang diminta adalah jumlah barang yang masih tersedia
            else if (tipe == "TERSEDIA")
            {
                Query = @"
                    SELECT
                    (
                        IFNULL(SUM(kondisi_bagus),0)
                        -
                        IFNULL(
                        (
                            SELECT SUM(dp.jumlah_pinjam)
                            FROM detail_peminjaman dp
                            INNER JOIN peminjaman p
                                ON dp.id_peminjaman = p.id_peminjaman
                            WHERE LOWER(p.status_peminjaman)
                                IN ('dipinjam','disetujui')
                        ),0)
                    )
                    FROM barang";
            }

            // Menjalankan query sesuai jenis statistik
            DataTable dt = jalankanQuery(Query);

            // Jika data berhasil diperoleh
            if (dt != null && dt.Rows.Count > 0)
            {
                // Mengambil hasil perhitungan
                hasil = dt.Rows[0][0].ToString();
            }

            // Mengembalikan hasil statistik
            return hasil;
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