using MySql.Data.MySqlClient;
using simade_pbo.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace simade_pbo.Service
{
    internal class Barang_service : Barang_cls
    {
        string Query;

        public Barang_service()
        {
            Query = "";
        }

        private int jalankanNonQuery(string query)
        {
            int retVal = -1;

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    retVal = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }

            return retVal;
        }

        private DataTable jalankanQuery(string query)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Gagal mengambil data : " + ex.Message,
                        "ERROR DATABASE",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            return dt;
        }

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

        public int simpan()
        {
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

        public DataTable tampilSemua()
        {
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

        public DataTable tampilByNama(string nama_barang)
        {
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

        public int ubah(int id)
        {
            int kondisiBagusLama = 0;
            int kondisiRusakLama = 0;
            int jumlahTotalLama = 0;

            string queryCek = "SELECT jumlah_barang, kondisi_bagus, kondisi_rusak " +
                              "FROM barang WHERE id_barang = " + id;

            DataTable dtCek = jalankanQuery(queryCek);

            if (dtCek != null && dtCek.Rows.Count > 0)
            {
                jumlahTotalLama = Convert.ToInt32(dtCek.Rows[0]["jumlah_barang"]);
                kondisiBagusLama = Convert.ToInt32(dtCek.Rows[0]["kondisi_bagus"]);
                kondisiRusakLama = Convert.ToInt32(dtCek.Rows[0]["kondisi_rusak"]);
            }

            int selisih = this.Jumlah_barang - jumlahTotalLama;
            int kondisiBagusBaru = kondisiBagusLama;
            int kondisiRusakBaru = kondisiRusakLama;

            if (selisih > 0)
            {
                kondisiBagusBaru += selisih;
            }
            else if (selisih < 0)
            {
                kondisiBagusBaru += selisih;

                if (kondisiBagusBaru < 0)
                {
                    kondisiRusakBaru += kondisiBagusBaru;
                    kondisiBagusBaru = 0;

                    if (kondisiRusakBaru < 0)
                        kondisiRusakBaru = 0;
                }
            }

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

        public int hapus(int id_barang)
        {
            Query = "DELETE FROM barang WHERE id_barang = " + id_barang;

            return jalankanNonQuery(Query);
        }

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

        public int ubahDetailKondisi(int id, int bagus, int rusak)
        {
            int totalBaru = bagus + rusak;

            Query = @"
                UPDATE barang SET
                    kondisi_bagus = " + bagus + @",
                    kondisi_rusak = " + rusak + @",
                    jumlah_barang = " + totalBaru + @"
                WHERE id_barang = " + id;

            return jalankanNonQuery(Query);
        }

        public string hitungStatistik(string tipe)
        {
            string hasil = "0";

            if (tipe == "TOTAL")
            {
                Query = @"
                    SELECT IFNULL(SUM(jumlah_barang),0)
                    FROM barang";
            }

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