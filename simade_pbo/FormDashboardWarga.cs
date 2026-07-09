using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    //inheritence : mewarisi semua bawaan visual dari class induk Form
    public partial class FormDashboardWarga : Form
    {
        //collection data sementara untuk menampung list barang yang akan diajukan peminjaman
        DataTable dtAntrean = new DataTable();

        int idBarangTerpilih = 0;
        int stokTersediaTerpilih = 0;

        //enkapsulasi : property public untuk menampung token session user login agar bisa diteruskan ke form lain
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        int idUserLoginAngka = 4; // Variabel fallback default

        public FormDashboardWarga()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // EVENT-DRIVEN: Mengaitkan aksi komponen visual ke method handler internal.
            this.Load += new System.EventHandler(this.FormDashboardWarga_Load);
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
            this.btnTambahKeList.Click += new System.EventHandler(this.btnTambahKeList_Click);
            this.btnKirimPengajuan.Click += new System.EventHandler(this.btnKirimPengajuan_Click);

            this.btnMenuDashboard.Click += new System.EventHandler(this.btnMenuDashboard_Click);
            this.btnMenuStatus.Click += new System.EventHandler(this.btnMenuStatus_Click);
            this.btnMenuRiwayat.Click += new System.EventHandler(this.btnMenuRiwayat_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
        }

        private void FormDashboardWarga_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IdUserLogin))
            {
                int.TryParse(IdUserLogin, out idUserLoginAngka);
            }

            if (!string.IsNullOrEmpty(NamaUserLogin))
            {
                lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";
            }

            txtNamaBarang.ReadOnly = true;
            dtpTanggalPinjam.Value = DateTime.Now;
            dtpTanggalKembali.Value = DateTime.Now;

            tampilDaftarAset();
            inisialisasiTabelAntrean();
        }

        //read : mengambil data dari database dan menampilkannya ke datagridview
        void tampilDaftarAset()
        {
            dgvBarang.AutoGenerateColumns = false;

            //abstraksi query SQL untuk menampilkan daftar aset desa yang tersedia, termasuk stok siap pakai setelah memperhitungkan peminjaman aktif
            string queryKatalog = @"
            SELECT 
                b.id_barang, 
                b.nama_barang, 
                k.nama_kategori, 
                (b.kondisi_bagus - IFNULL(peminjaman_aktif.total_terpinjam, 0)) AS stok_siap_pakai
            FROM barang b
            INNER JOIN kategori k ON b.id_kategori = k.id_kategori
            LEFT JOIN (
                SELECT dp.id_barang, SUM(dp.jumlah_pinjam) AS total_terpinjam
                FROM detail_peminjaman dp
                INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                WHERE p.status_peminjaman IN ('pending', 'dipinjam', 'disetujui', 'Disetujui (Siap Ambil)')
                GROUP BY dp.id_barang
            ) peminjaman_aktif ON b.id_barang = peminjaman_aktif.id_barang
            WHERE (b.kondisi_bagus - IFNULL(peminjaman_aktif.total_terpinjam, 0)) > 0
            ORDER BY b.nama_barang ASC";

            try
            {
                //abstraksi koneksi database menggunakan class Koneksi untuk menghindari duplikasi string koneksi di banyak tempat
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryKatalog, conn))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            nama_barang.DataPropertyName = "nama_barang";
                            id_kategori.DataPropertyName = "nama_kategori";
                            jumlah_tersedia.DataPropertyName = "stok_siap_pakai";

                            dgvBarang.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat katalog aset desa terbaru: " + ex.Message, "Database Error");
            }
        }

        void inisialisasiTabelAntrean()
        {
            dtAntrean.Reset();
            dtAntrean.Columns.Clear();

            dtAntrean.Columns.Add("id_barang", typeof(int));
            dtAntrean.Columns.Add("nama_barang", typeof(string));
            dtAntrean.Columns.Add("jumlah_pinjam", typeof(int));

            dgvAntrean.AutoGenerateColumns = false;
            colCartNama.DataPropertyName = "nama_barang";
            colCartQty.DataPropertyName = "jumlah_pinjam";

            dgvAntrean.DataSource = dtAntrean;
        }

        void bersihkanInputanBarang()
        {
            txtNamaBarang.Clear();
            txtJumlahPinjam.Clear();
            idBarangTerpilih = 0;
            stokTersediaTerpilih = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin menutup aplikasi?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataTable dt = (DataTable)dgvBarang.DataSource;
                    DataRow dr = dt.Rows[e.RowIndex];

                    if (dr != null)
                    {
                        idBarangTerpilih = Convert.ToInt32(dr["id_barang"]);
                        stokTersediaTerpilih = Convert.ToInt32(dr["stok_siap_pakai"]);

                        txtNamaBarang.Text = dr["nama_barang"].ToString();
                        txtJumlahPinjam.Text = "1";
                    }
                }
                catch (Exception) { }
            }
        }

        //validasi inputan jumlah pinjam agar tidak melebihi stok tersedia dan menambahkan ke list antrean peminjaman
        private void btnTambahKeList_Click(object sender, EventArgs e)
        {
            if (idBarangTerpilih == 0 || string.IsNullOrEmpty(txtNamaBarang.Text))
            {
                MessageBox.Show("Silakan pilih barang dari daftar aset desa terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlahPinjam.Text, out int jumlahInput) || jumlahInput <= 0)
            {
                MessageBox.Show("Masukkan jumlah pinjam yang valid (angka harus di atas 0)!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jumlahInput > stokTersediaTerpilih)
            {
                MessageBox.Show($"Gagal menambahkan! Jumlah pinjam ({jumlahInput} unit) melebihi jumlah tersedia ({stokTersediaTerpilih} unit).",
                                "Stok Tidak Mencukupi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtJumlahPinjam.Focus();
                return;
            }

            foreach (DataRow row in dtAntrean.Rows)
            {
                if (Convert.ToInt32(row["id_barang"]) == idBarangTerpilih)
                {
                    MessageBox.Show("Barang ini sudah ada di dalam list antrean!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            dtAntrean.Rows.Add(idBarangTerpilih, txtNamaBarang.Text, jumlahInput);
            bersihkanInputanBarang();
        }

        //create : mengirim data peminjaman ke database, termasuk master-detail table dengan transaksi aman
        private void btnKirimPengajuan_Click(object sender, EventArgs e)
        {
            if (dtAntrean.Rows.Count == 0)
            {
                MessageBox.Show("List antrean peminjaman masih kosong! Silakan tambahkan barang terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tglPinjam = dtpTanggalPinjam.Value.ToString("yyyy-MM-dd");
            string tglKembali = dtpTanggalKembali.Value.ToString("yyyy-MM-dd");

            if (dtpTanggalKembali.Value.Date < dtpTanggalPinjam.Value.Date)
            {
                MessageBox.Show("Tanggal kembali tidak boleh lebih awal dari tanggal pinjam!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kodePeminjamanOtomatis = "AD_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                conn.Open();

                // Menjamin keamanan data. Jika satu detail gagal diinput, seluruh transaksi otomatis batal (Rollback).
                using (MySqlTransaction tr = conn.BeginTransaction())
                {
                    try
                    {
                        //Menginput data induk nota ke tabel peminjaman.
                        string queryMaster = @"INSERT INTO peminjaman (kode_peminjaman, id_user, tgl_pinjam, tgl_kembali, status_peminjaman)
                                       VALUES (@kode, @uid, @tglP, @tglK, 'pending')";

                        long idPeminjamanTerbuat = 0;
                        using (MySqlCommand cmdMaster = new MySqlCommand(queryMaster, conn, tr))
                        {
                            //Menyaring input text untuk menangkal serangan peretasan SQL Injection.
                            cmdMaster.Parameters.AddWithValue("@kode", kodePeminjamanOtomatis);
                            cmdMaster.Parameters.AddWithValue("@uid", idUserLoginAngka);
                            cmdMaster.Parameters.AddWithValue("@tglP", tglPinjam);
                            cmdMaster.Parameters.AddWithValue("@tglK", tglKembali);
                            cmdMaster.ExecuteNonQuery();

                            idPeminjamanTerbuat = cmdMaster.LastInsertedId;
                        }

                        //Memecah baris data antrean masuk ke tabel detail_peminjaman.
                        int suksesDetailCount = 0;
                        foreach (DataRow barisAntrean in dtAntrean.Rows)
                        {
                            int idBarang = Convert.ToInt32(barisAntrean["id_barang"]);
                            int qtyPinjam = Convert.ToInt32(barisAntrean["jumlah_pinjam"]);

                            string queryDetail = @"INSERT INTO detail_peminjaman (id_peminjaman, id_barang, jumlah_pinjam, status, keterangan) 
                                           VALUES (@idMaster, @idBarang, @qty, 'pending', '-')";

                            using (MySqlCommand cmdDetail = new MySqlCommand(queryDetail, conn, tr))
                            {
                                cmdDetail.Parameters.AddWithValue("@idMaster", idPeminjamanTerbuat);
                                cmdDetail.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdDetail.Parameters.AddWithValue("@qty", qtyPinjam);
                                suksesDetailCount += cmdDetail.ExecuteNonQuery();
                            }
                        }

                        if (suksesDetailCount == dtAntrean.Rows.Count)
                        {
                            tr.Commit(); //Data berhasil dikunci permanen ke harddisk database.
                            MessageBox.Show($"Pengajuan peminjaman berhasil dikirim ke Admin!\nKode Nota Anda: {kodePeminjamanOtomatis}",
                                            "Sukses Pengajuan", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dtAntrean.Rows.Clear();
                            bersihkanInputanBarang();
                            tampilDaftarAset();
                        }
                        else
                        {
                            tr.Rollback();
                            MessageBox.Show("Terjadi masalah penyimpanan data.", "Gagal Simpan");
                        }
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback(); //Menggagalkan input otomatis jika jaringan Wi-Fi/Server terputus di tengah jalan.
                        MessageBox.Show("Terjadi kendala sirkulasi database: " + ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnMenuDashboard_Click(object sender, EventArgs e) { tampilDaftarAset(); }

        //Membuka halaman baru menggunakan .Close() untuk menghancurkan form lama dari RAM laptop.
        private void btnMenuStatus_Click(object sender, EventArgs e)
        {
            FormStatusPengajuan status = new FormStatusPengajuan();
            status.IdUserLogin = this.IdUserLogin;
            status.NamaUserLogin = this.NamaUserLogin;
            status.Show();
            this.Close(); //Menghancurkan form dashboard agar hemat resource RAM saat demo.
        }

        private void btnMenuRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayatWarga riwayat = new FormRiwayatWarga();
            riwayat.IdUserLogin = this.IdUserLogin;
            riwayat.NamaUserLogin = this.NamaUserLogin;
            riwayat.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }
    }
}