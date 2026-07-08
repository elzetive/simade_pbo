using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace simade_pbo.Service
{
    public class Pinjam_service
    {
        public DataTable tampilSemuaPeminjaman()
        {
            string query = @"SELECT p.kode_peminjaman, u.nama_lengkap, p.tgl_pinjam, p.tgl_kembali, p.status_peminjaman 
                    FROM peminjaman p 
                    INNER JOIN user u ON p.id_user = u.id_user";
            return jalankanQuery(query);
        }

        public DataTable tampilDetailBarang(string kodePeminjaman)
        {
            // RUMUS YANG DISINKRONKAN:
            // Jumlah Tersedia = jumlah_barang - kondisi_rusak - SUM(jumlah_pinjam yang status detailnya 'disetujui')
            string query = @"
            SELECT 
                dp.id_detail_peminjaman,
                dp.id_peminjaman,
                dp.id_barang,
                b.nama_barang,
                k.nama_kategori,
                dp.jumlah_pinjam,
                u.alamat,
                u.no_hp AS nomor_telepon,
                dp.status,
                dp.keterangan,
                (b.jumlah_barang - IFNULL(b.kondisi_rusak, 0) - 
                    IFNULL((SELECT SUM(x.jumlah_pinjam) 
                            FROM detail_peminjaman x 
                            WHERE x.id_barang = b.id_barang AND x.status = 'disetujui'), 0)
                ) AS jumlah_tersedia_realtime
            FROM detail_peminjaman dp
            INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
            INNER JOIN barang b ON dp.id_barang = b.id_barang
            LEFT JOIN kategori k ON b.id_kategori = k.id_kategori
            LEFT JOIN user u ON p.id_user = u.id_user
            WHERE p.kode_peminjaman = @kode";

            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode", kodePeminjaman);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil detail barang peminjaman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public int ubahStatusDetailItem(int idDetail, int jumlah, string status, string keterangan)
        {
            string query = "UPDATE detail_peminjaman SET " + "jumlah_pinjam = " + jumlah + ", " + "status = '" + status + "', " + "keterangan = '" + keterangan + "' " + "WHERE id_detail_peminjaman = " + idDetail;
            return jalankanNonQuery(query);
        }

        public int ubahStatusNotaInduk(string kodePeminjaman, string statusBaru)
        {
            string query = "UPDATE peminjaman SET status_peminjaman = '" + statusBaru + "' WHERE kode_peminjaman = '" + kodePeminjaman + "'";
            return jalankanNonQuery(query);
        }

        private DataTable jalankanQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror Query SQL: " + ex.Message);
            }
            return dt;
        }

        private int jalankanNonQuery(string query)
        {
            int result = 0;
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror SQL NonQuery: " + ex.Message);
            }
            return result;
        }

        public DataTable cariPeminjaman(string kataKunci)
        {
            string query = @"SELECT p.kode_peminjaman, u.nama_lengkap, p.tgl_pinjam, p.tgl_kembali, p.status_peminjaman 
                    FROM peminjaman p INNER JOIN user u ON p.id_user = u.id_user 
                    WHERE p.kode_peminjaman 
                    LIKE '%" + kataKunci + "%' OR u.nama_lengkap LIKE '%" + kataKunci + "%'";
            return jalankanQuery(query);
        }

        public int updateStokBarangKembali(int idDetailPeminjaman, int kondisiBagus, int kondisiRusak, string dikembalikanOleh)
        {
            string query = $@"
        UPDATE detail_peminjaman dp
        INNER JOIN barang b ON dp.id_barang = b.id_barang
        SET 
            dp.kondisi_bagus = {kondisiBagus},
            dp.kondisi_rusak = {kondisiRusak},
            dp.dikembalikan_oleh = '{dikembalikanOleh}',
            dp.jumlah_kembali = {kondisiBagus + kondisiRusak},
            
            b.jumlah_dipinjam = b.jumlah_dipinjam - {kondisiBagus + kondisiRusak},
            b.kondisi_bagus = b.kondisi_bagus - {kondisiRusak},
            b.kondisi_rusak = b.kondisi_rusak + {kondisiRusak}
        WHERE dp.id_detail_peminjaman = {idDetailPeminjaman};";

            return jalankanNonQuery(query);
        }
    }
}