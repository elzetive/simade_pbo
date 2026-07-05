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
            string query = @"SELECT dp.id_detail_peminjaman AS id_detail_peminjaman, 
                            b.nama_barang AS nama_barang, 
                            k.nama_kategori AS nama_kategori, 
                            dp.jumlah_pinjam AS jumlah_pinjam, 
                            dp.status AS status, 
                            dp.keterangan AS keterangan,
                            u.alamat AS alamat,
                            u.no_hp AS nomor_telepon,
                            (b.kondisi_bagus - IFNULL((SELECT SUM(jumlah_pinjam) FROM detail_peminjaman WHERE id_barang = b.id_barang AND status = 'disetujui'), 0)) AS jumlah_tersedia
                     FROM detail_peminjaman dp 
                     INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman 
                     INNER JOIN user u ON p.id_user = u.id_user 
                     INNER JOIN barang b ON dp.id_barang = b.id_barang 
                     INNER JOIN kategori k ON b.id_kategori = k.id_kategori 
                     WHERE p.kode_peminjaman = '" + kodePeminjaman + "'";

            return jalankanQuery(query);
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
            // 1. Ambil id_barang terlebih dahulu berdasarkan id_detail_peminjaman
            // 2. Update status pengembalian di detail_peminjaman (kondisi_bagus, kondisi_rusak, denda, dll)
            // 3. Update data inventaris di tabel barang (kurangi dipinjam, sesuaikan kondisi bagus & rusak)
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