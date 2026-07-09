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
            string query = @"SELECT 
                        p.kode_peminjaman, 
                        u.nama_lengkap, 
                        p.tgl_pinjam, 
                        p.tgl_kembali, 
                        p.status_peminjaman 
                    FROM peminjaman p 
                    INNER JOIN user u ON p.id_user = u.id_user 
                    WHERE LOWER(p.status_peminjaman) IN ('pending', 'disetujui', 'ditolak')
                    ORDER BY p.tgl_pinjam DESC";

            return jalankanQuery(query);
        }

        public DataTable tampilDetailBarang(string kodePeminjaman)
        {
            string query = @"
        SELECT 
            b.nama_barang, 
            k.nama_kategori, 
            dp.jumlah_pinjam, 
            b.kondisi_bagus AS jumlah_tersedia_realtime, 
            dp.status, 
            dp.keterangan,
            dp.id_detail_peminjaman,
            u.alamat,          
            u.no_hp AS nomor_telepon 
        FROM detail_peminjaman dp
        JOIN barang b ON dp.id_barang = b.id_barang
        JOIN kategori k ON b.id_kategori = k.id_kategori
        JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
        JOIN user u ON p.id_user = u.id_user 
        WHERE p.kode_peminjaman = @kodePeminjaman";

            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kodePeminjaman", kodePeminjaman);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) { adapter.Fill(dt); }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Gagal: " + ex.Message); }
            }
            return dt;
        }

        public DataTable tampilDetailBarangDisetujuiSah(string kodePeminjaman)
        {
            string query = @"
        SELECT 
            b.nama_barang, 
            k.nama_kategori, 
            dp.jumlah_pinjam, 
            b.kondisi_bagus AS jumlah_tersedia_realtime, 
            dp.status, 
            dp.keterangan,
            dp.id_detail_peminjaman,
            u.alamat,          
            u.no_hp AS nomor_telepon 
        FROM detail_peminjaman dp
        JOIN barang b ON dp.id_barang = b.id_barang
        JOIN kategori k ON b.id_kategori = k.id_kategori
        JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
        JOIN user u ON p.id_user = u.id_user 
        WHERE p.kode_peminjaman = @kodePeminjaman 
          AND dp.status = 'disetujui'";

            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kodePeminjaman", kodePeminjaman);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) { adapter.Fill(dt); }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Gagal: " + ex.Message); }
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
            string query = @"SELECT 
                        p.kode_peminjaman, 
                        u.nama_lengkap, 
                        p.tgl_pinjam, 
                        p.tgl_kembali, 
                        p.status_peminjaman 
                    FROM peminjaman p 
                    INNER JOIN user u ON p.id_user = u.id_user 
                    WHERE LOWER(p.status_peminjaman) IN ('pending', 'disetujui', 'ditolak')
                      AND (p.kode_peminjaman LIKE @kataKunci 
                           OR u.nama_lengkap LIKE @kataKunci 
                           OR p.tgl_pinjam LIKE @kataKunci 
                           OR p.tgl_kembali LIKE @kataKunci 
                           OR p.status_peminjaman LIKE @kataKunci)";

            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kataKunci", "%" + kataKunci + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Pencarian SQL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
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