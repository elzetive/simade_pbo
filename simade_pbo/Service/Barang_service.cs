using MySql.Data.MySqlClient;
using simade_pbo.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace simade_pbo.Service
{
    internal class Barang_service : Barang_cls
    {
        // Variabel untuk menyimpan query SQL yang akan dijalankan
        string Query;

        // Constructor
        public Barang_service()
        {
            Query = "";
        }

        // ==========================================================
        // METHOD MENJALANKAN QUERY INSERT, UPDATE DAN DELETE
        // ==========================================================
        private int jalankanNonQuery(string query)
        {
            int retVal = -1;

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    // Membuka koneksi ke database
                    conn.Open();

                    // Menjalankan query Non Query
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Mengembalikan jumlah baris yang berhasil diproses
                    retVal = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    // Error diabaikan karena akan ditangani oleh method pemanggil
                }
            }

            return retVal;
        }

        // ==========================================================
        // METHOD MENJALANKAN QUERY SELECT
        // ==========================================================
        private DataTable jalankanQuery(string query)
        {
            // Membuat objek DataTable sebagai penampung hasil query
            DataTable dt = new DataTable();

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    // Membuka koneksi database
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            // Mengisi DataTable dengan hasil query
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Menampilkan pesan apabila terjadi kesalahan koneksi
                    MessageBox.Show(
                        "Gagal mengambil data : " + ex.Message,
                        "ERROR DATABASE",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        // ==========================================================
        // METHOD CEK APAKAH BARANG SUDAH ADA
        // ==========================================================
        public bool isExist(string nama_barang)
        {
            bool hasil = false;

            Query = "SELECT COUNT(*) FROM barang WHERE nama_barang = '" +
                    nama_barang.Replace("'", "''") + "'";

            DataTable dt = jalankanQuery(Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                int jumlah = Convert.ToInt32(dt.Rows[0][0]);

                if (jumlah > 0)
                {
                    hasil = true;
                }
            }

            return hasil;
        }

        // ==========================================================
        // METHOD MENYIMPAN DATA BARANG BARU
        // ==========================================================
        public int simpan()
        {
            // Saat barang pertama kali ditambahkan,
            // seluruh barang dianggap masih dalam kondisi bagus
            Query =
                "INSERT INTO barang " +
                "(id_kategori, nama_barang, jumlah_barang, kondisi_bagus, kondisi_rusak) " +
                "VALUES (" +
                Id_kategori + ", '" +
                Nama_barang + "', " +
                Jumlah_barang + ", " +
                Jumlah_barang + ", 0)";

            return jalankanNonQuery(Query);
        }

        // ==========================================================
        // METHOD MENAMPILKAN SELURUH DATA BARANG
        // ==========================================================
        public DataTable tampilSemua()
        {
            // Mengambil seluruh data barang beserta stok realtime
            // Logika disamakan dengan Dashboard Admin
            Query = @"
            SELECT
                b.id_barang,
                b.id_kategori,
                b.nama_barang,
                k.nama_kategori,
                b.jumlah_barang AS jumlah_total,
                b.kondisi_bagus,
                b.kondisi_rusak,

                IFNULL((
                    SELECT SUM(dp.jumlah_pinjam)
                    FROM detail_peminjaman dp
                    INNER JOIN peminjaman p
                        ON dp.id_peminjaman = p.id_peminjaman
                    WHERE dp.id_barang = b.id_barang
                    AND LOWER(p.status_peminjaman)
                        IN ('dipinjam','disetujui')
                ),0) AS jumlah_dipinjam,

                (
                    b.kondisi_bagus -
                    IFNULL((
                        SELECT SUM(dp.jumlah_pinjam)
                        FROM detail_peminjaman dp
                        INNER JOIN peminjaman p
                            ON dp.id_peminjaman = p.id_peminjaman
                        WHERE dp.id_barang = b.id_barang
                        AND LOWER(p.status_peminjaman)
                            IN ('dipinjam','disetujui')
                    ),0)
                ) AS jumlah_tersedia

            FROM barang b
            INNER JOIN kategori k
                ON b.id_kategori = k.id_kategori

            ORDER BY b.nama_barang ASC";

            return jalankanQuery(Query);
        }

        // ==========================================================
        // METHOD MENCARI DATA BARANG BERDASARKAN NAMA
        // ==========================================================
        public DataTable tampilByNama(string nama_barang)
        {
            // Mengambil data barang berdasarkan nama
            // dengan perhitungan stok yang sama seperti Dashboard
            Query = @"
            SELECT
                b.id_barang,
                b.id_kategori,
                b.nama_barang,
                k.nama_kategori,
                b.jumlah_barang AS jumlah_total,
                b.kondisi_bagus,
                b.kondisi_rusak,

                IFNULL((
                    SELECT SUM(dp.jumlah_pinjam)
                    FROM detail_peminjaman dp
                    INNER JOIN peminjaman p
                        ON dp.id_peminjaman = p.id_peminjaman
                    WHERE dp.id_barang = b.id_barang
                    AND LOWER(p.status_peminjaman)
                        IN ('dipinjam','disetujui')
                ),0) AS jumlah_dipinjam,

                (
                    b.kondisi_bagus -
                    IFNULL((
                        SELECT SUM(dp.jumlah_pinjam)
                        FROM detail_peminjaman dp
                        INNER JOIN peminjaman p
                            ON dp.id_peminjaman = p.id_peminjaman
                        WHERE dp.id_barang = b.id_barang
                        AND LOWER(p.status_peminjaman)
                            IN ('dipinjam','disetujui')
                    ),0)
                ) AS jumlah_tersedia

            FROM barang b
            INNER JOIN kategori k
                ON b.id_kategori = k.id_kategori

            WHERE b.nama_barang LIKE '" + nama_barang.Replace("'", "''") + @"%'

            ORDER BY b.nama_barang ASC";

            return jalankanQuery(Query);
        }

        // ==========================================================
        // METHOD MENGUBAH DATA BARANG
        // ==========================================================
        public int ubah(int id)
        {
            // Menyimpan kondisi barang lama
            int kondisiBagusLama = 0;
            int kondisiRusakLama = 0;
            int jumlahTotalLama = 0;

            // Mengambil data barang yang akan diubah
            string queryCek = "SELECT jumlah_barang, kondisi_bagus, kondisi_rusak " +
                              "FROM barang WHERE id_barang = " + id;

            DataTable dtCek = jalankanQuery(queryCek);

            if (dtCek != null && dtCek.Rows.Count > 0)
            {
                jumlahTotalLama = Convert.ToInt32(dtCek.Rows[0]["jumlah_barang"]);
                kondisiBagusLama = Convert.ToInt32(dtCek.Rows[0]["kondisi_bagus"]);
                kondisiRusakLama = Convert.ToInt32(dtCek.Rows[0]["kondisi_rusak"]);
            }

            // Menghitung selisih jumlah barang
            int selisih = this.Jumlah_barang - jumlahTotalLama;

            int kondisiBagusBaru = kondisiBagusLama;
            int kondisiRusakBaru = kondisiRusakLama;

            // Jika stok bertambah maka seluruh tambahan dianggap kondisi bagus
            if (selisih > 0)
            {
                kondisiBagusBaru += selisih;
            }
            // Jika stok berkurang maka dikurangi dari kondisi bagus terlebih dahulu
            else if (selisih < 0)
            {
                kondisiBagusBaru += selisih;

                if (kondisiBagusBaru < 0)
                {
                    kondisiRusakBaru += kondisiBagusBaru;
                    kondisiBagusBaru = 0;

                    // Mencegah kondisi rusak menjadi negatif
                    if (kondisiRusakBaru < 0)
                        kondisiRusakBaru = 0;
                }
            }

            // Menyimpan perubahan ke database
            Query = @"
                UPDATE barang SET
                    nama_barang = '" + this.Nama_barang + @"',
                    id_kategori = " + this.Id_kategori + @",
                    jumlah_barang = " + this.Jumlah_barang + @",
                    kondisi_bagus = " + kondisiBagusBaru + @",
                    kondisi_rusak = " + kondisiRusakBaru + @"
                WHERE id_barang = " + id;

            return jalankanNonQuery(Query);
        }

        // ==========================================================
        // METHOD MENGHAPUS DATA BARANG
        // ==========================================================
        public int hapus(int id_barang)
        {
            Query = "DELETE FROM barang WHERE id_barang = " + id_barang;

            return jalankanNonQuery(Query);
        }

        // ==========================================================
        // METHOD MENGAMBIL DETAIL KONDISI BARANG
        // ==========================================================
        public DataTable DetailKondisi(string namaBarang)
        {
            Query = @"
                SELECT
                    kondisi_bagus,
                    kondisi_rusak
                FROM barang
                WHERE nama_barang = '" + namaBarang.Replace("'", "''") + @"'
                LIMIT 1";

            return jalankanQuery(Query);
        }

        // ==========================================================
        // METHOD MENGUBAH DETAIL KONDISI BARANG
        // ==========================================================
        public int ubahDetailKondisi(int id, int bagus, int rusak)
        {
            // Total stok merupakan penjumlahan kondisi bagus dan rusak
            int totalBaru = bagus + rusak;

            Query = @"
                UPDATE barang SET
                    kondisi_bagus = " + bagus + @",
                    kondisi_rusak = " + rusak + @",
                    jumlah_barang = " + totalBaru + @"
                WHERE id_barang = " + id;

            return jalankanNonQuery(Query);
        }

        // ==========================================================
        // METHOD MENGHITUNG STATISTIK DASHBOARD
        // ==========================================================
        public string hitungStatistik(string tipe)
        {
            string hasil = "0";

            // Menghitung total seluruh barang
            if (tipe == "TOTAL")
            {
                Query = @"
                    SELECT IFNULL(SUM(jumlah_barang),0)
                    FROM barang";
            }

            // Menghitung jumlah barang yang sedang dipinjam
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

            // Menghitung stok tersedia
            // Barang tersedia hanya dihitung dari kondisi bagus
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

            DataTable dt = jalankanQuery(Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                hasil = dt.Rows[0][0].ToString();
            }

            return hasil;
        }

        // ==========================================================
        // METHOD DATA GRAFIK PEMINJAMAN PER BULAN
        // ==========================================================
        public DataTable GrafikPeminjaman()
        {
            Query = @"
                SELECT
                    MONTH(tgl_pinjam) AS bulan,
                    COUNT(*) AS jumlah
                FROM peminjaman
                GROUP BY MONTH(tgl_pinjam)
                ORDER BY MONTH(tgl_pinjam)";

            return jalankanQuery(Query);
        }

        // ==========================================================
        // METHOD DATA BARANG PALING SERING DIPINJAM
        // ==========================================================
        public DataTable GrafikBarangTerpinjam()
        {
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

            return jalankanQuery(Query);
        }

        // ==========================================================
        // METHOD MENAMPILKAN RIWAYAT PEMINJAMAN
        // ==========================================================
        public DataTable tampilRiwayat()
        {
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

            return jalankanQuery(Query);
        }
    }
}