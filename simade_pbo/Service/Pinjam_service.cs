using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace simade_pbo.Service
{
    public class Pinjam_service //OOP
    {
        // Method untuk mengambil daftar seluruh nota induk peminjaman dari database
        public DataTable tampilSemuaPeminjaman()
        {
            // Menyusun kueri SQL untuk mengambil data peminjaman digabung dengan nama lengkap user (peminjam)
            string query = @"SELECT 
                        p.kode_peminjaman, 
                        u.nama_lengkap, 
                        p.tgl_pinjam, 
                        p.tgl_kembali, 
                        p.status_peminjaman 
                    FROM peminjaman p 
                    INNER JOIN user u ON p.id_user = u.id_user 
                    /* Memfilter data agar hanya memunculkan status pending, disetujui, atau ditolak */
                    WHERE LOWER(p.status_peminjaman) IN ('pending', 'disetujui', 'ditolak')
                    /* Mengurutkan hasil dari tanggal peminjaman paling baru (terkini) */
                    ORDER BY p.tgl_pinjam DESC";

            // Mengeksekusi kueri di atas lewat fungsi pembantu internal dan mengembalikan hasilnya berupa DataTable
            return jalankanQuery(query);
        }

        // Method untuk mengambil rincian barang-barang apa saja yang ada di dalam satu nomor nota peminjaman tertentu
        public DataTable tampilDetailBarang(string kodePeminjaman)
        {
            // Menyusun kueri SQL relasi multi-tabel (detail_peminjaman, barang, kategori, peminjaman, user)
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
        /* Menyaring data secara spesifik berdasarkan parameter kode peminjaman */
        WHERE p.kode_peminjaman = @kodePeminjaman";

            // Membuat wadah tabel kosong di memori
            DataTable dt = new DataTable();
            // Membuka blok koneksi aman ke database MySQL
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open(); // Membuka jalur komunikasi database
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Memasukkan nilai kodePeminjaman secara aman menggunakan parameter anti SQL Injection
                        cmd.Parameters.AddWithValue("@kodePeminjaman", kodePeminjaman);
                        // Mengisi wadah DataTable (dt) dengan hasil kueri dari database
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) { adapter.Fill(dt); }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Gagal: " + ex.Message); }
            }
            return dt; // Mengembalikan tabel rincian barang
        }

        // Method khusus untuk mengambil detail barang yang HANYA berstatus 'disetujui' (untuk keperluan serah terima/pengambilan)
        public DataTable tampilDetailBarangDisetujuiSah(string kodePeminjaman)
        {
            // Menggunakan struktur kueri yang mirip dengan tampilDetailBarang
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
          /* Filter tambahan: Hanya menarik barang yang disetujui oleh admin, mengabaikan yang ditolak */
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

        // Method untuk memperbarui status persetujuan individual per item barang (Contoh: Menyetujui Laptop, tapi Menolak Proyektor)
        public int ubahStatusDetailItem(int idDetail, int jumlah, string status, string keterangan)
        {
            // Menyusun kueri string UPDATE lokal secara manual (Penggabungan string)
            string query = "UPDATE detail_peminjaman SET " + "jumlah_pinjam = " + jumlah + ", " + "status = '" + status + "', " + "keterangan = '" + keterangan + "' " + "WHERE id_detail_peminjaman = " + idDetail;
            // Mengeksekusi kueri UPDATE ke database lewat fungsi non-query dan mengembalikan jumlah baris yang berhasil diubah
            return jalankanNonQuery(query);
        }

        // Method untuk memperbarui status keseluruhan Nota Induk (Contoh: Mengubah dari 'pending' menjadi 'disetujui' atau 'ditolak')
        public int ubahStatusNotaInduk(string kodePeminjaman, string statusBaru)
        {
            // Menyusun kueri string UPDATE manual untuk memperbarui tabel induk peminjaman
            string query = "UPDATE peminjaman SET status_peminjaman = '" + statusBaru + "' WHERE kode_peminjaman = '" + kodePeminjaman + "'";
            return jalankanNonQuery(query);
        }

        // Method privat pembantu untuk mengeksekusi kueri SELECT (Mengambil data masukan)
        private DataTable jalankanQuery(string query) //enkapsulasi
        {
            DataTable dt = new DataTable(); // Menyiapkan objek penampung tabel data
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn()) // Menghubungkan ke database
                {
                    // Menarik data langsung berdasarkan string kueri tanpa membuka koneksi secara manual (.Fill mengurusnya otomatis)
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        da.Fill(dt); // Memasukkan baris data ke DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror Query SQL: " + ex.Message); // Tampilkan pesan jika terjadi kesalahan sintaks SQL
            }
            return dt; // Mengembalikan hasil data
        }

        // Method privat pembantu untuk mengeksekusi kueri manipulasi data (INSERT, UPDATE, DELETE)
        private int jalankanNonQuery(string query) //enkapsulasi
        {
            int result = 0; // Menyiapkan variabel penanda sukses (menyimpan jumlah baris yang berubah)
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open(); // Wajib membuka koneksi secara manual sebelum melakukan manipulasi data
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        result = cmd.ExecuteNonQuery(); // Mengeksekusi perintah SQL dan menangkap status baris terpengaruh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror SQL NonQuery: " + ex.Message);
            }
            return result; // Mengembalikan angka indikator (0 = gagal, 1 atau lebih = sukses)
        }

        // Method untuk menyaring daftar transaksi peminjaman berdasarkan inputan kata kunci dari admin (Fitur Cari)
        public DataTable cariPeminjaman(string kataKunci)
        {
            // Menyusun kueri SQL pencarian global lintas kolom menggunakan klausa LIKE
            string query = @"SELECT 
                        p.kode_peminjaman, 
                        u.nama_lengkap, 
                        p.tgl_pinjam, 
                        p.tgl_kembali, 
                        p.status_peminjaman 
                    FROM peminjaman p 
                    INNER JOIN user u ON p.id_user = u.id_user 
                    WHERE LOWER(p.status_peminjaman) IN ('pending', 'disetujui', 'ditolak')
                      /* Memeriksa kecocokan kata kunci pada kode nota, nama peminjaman, tanggal, atau status */
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
                        // Memasang parameter pencarian dengan pola '%kata_kunci%' (pencarian parsial) untuk menghindari SQL Injection
                        cmd.Parameters.AddWithValue("@kataKunci", "%" + kataKunci + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt); // Mengisi tabel data berdasarkan kecocokan pencarian
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Pencarian SQL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt; // Mengembalikan hasil daftar pencarian
        }
    }
}