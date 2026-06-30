using MySql.Data.MySqlClient;
using simade_pbo.Model;
using System;
using System.Collections.Generic;
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
                catch (Exception) { }
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
                        "Gagal mengambil data: " + ex.Message,
                        "ERROR KONEKSI",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            return dt;
        }

        public bool isExist(string nama_barang)
        {
            bool cek = false;
            Query = "SELECT * FROM barang WHERE nama_barang = '" + nama_barang + "'";
            if (jalankanQuery(Query).Rows.Count > 0)
            {
                cek = true;
            }
            return cek;
        }

        public int simpan()
        {
            Query = "INSERT INTO barang (id_sub, nama_barang, status) VALUES ('" + Id_kategori + "', '" + Nama_barang + "', '" + Status_ketersediaan + "')";
            return jalankanNonQuery(Query);
        }

        public DataTable tampilSemua()
        {
            // SINKRONISASI MUTLAK: Menggunakan join tiga arah (barang -> sub_kategori -> kategori) sesuai berkas .sql
            Query = @"SELECT 
                        b.id_barang, 
                        b.id_sub, 
                        sk.nama_sub, 
                        k.nama_kategori,
                        b.nama_barang, 
                        b.merk, 
                        b.nomor_identitas, 
                        b.status AS status_ketersediaan 
                      FROM barang b 
                      INNER JOIN sub_kategori sk ON b.id_sub = sk.id_sub
                      INNER JOIN kategori k ON sk.id_kategori = k.id_kategori";
            return jalankanQuery(Query);
        }

        public int ubah(int idLama)
        {
            Query = "UPDATE barang SET id_sub = '" + Id_kategori + "', nama_barang = '" + Nama_barang + "', status = '" + Status_ketersediaan + "' WHERE id_barang = '" + idLama + "'";
            return jalankanNonQuery(Query);
        }

        public int hapus(int id_barang)
        {
            Query = "DELETE FROM barang WHERE id_barang = '" + id_barang + "'";
            return jalankanNonQuery(Query);
        }

        public DataTable tampilByNama(string nama_barang)
        {
            Query = @"SELECT 
                        b.id_barang, 
                        b.id_sub, 
                        sk.nama_sub, 
                        k.nama_kategori,
                        b.nama_barang, 
                        b.merk, 
                        b.nomor_identitas, 
                        b.status AS status_ketersediaan 
                      FROM barang b 
                      INNER JOIN sub_kategori sk ON b.id_sub = sk.id_sub
                      INNER JOIN kategori k ON sk.id_kategori = k.id_kategori
                      WHERE b.nama_barang LIKE '" + nama_barang + "%'";
            return jalankanQuery(Query);
        }

        public string hitungStatistik(string status)
        {
            string hasil = "0";
            if (status == "TOTAL")
                Query = "SELECT COUNT(*) FROM barang";
            else if (status == "DIPINJAM")
                Query = "SELECT COUNT(*) FROM barang WHERE status = 'dipinjam'";
            else if (status == "TERSEDIA")
                Query = "SELECT COUNT(*) FROM barang WHERE status = 'tersedia'";

            DataTable dt = jalankanQuery(Query);
            if (dt != null && dt.Rows.Count > 0)
            {
                hasil = dt.Rows[0][0].ToString();
            }
            return hasil;
        }

        public DataTable tampilRiwayat()
        {
            Query = @"SELECT 
                        p.id_pinjam AS colID, 
                        b.nama_barang AS colNama, 
                        u.nama_lengkap AS colPeminjam, 
                        p.tgl_pinjam AS colTanggalPinjam, 
                        p.tgl_kembali AS colTanggalKembali, 
                        p.status_peminjaman AS colStatus 
                      FROM peminjaman p 
                      INNER JOIN user u ON p.id_user = u.id_user 
                      INNER JOIN barang b ON p.id_barang = b.id_barang";
            return jalankanQuery(Query);
        }
    }
}