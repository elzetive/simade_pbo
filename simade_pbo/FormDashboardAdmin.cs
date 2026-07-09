using simade_pbo.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    // Deklarasi kelas FormDashboardAdmin yang mewarisi sifat dari kelas Form (GUI)
    public partial class FormDashboardAdmin : Form
    {
        // Membuat objek instansiasi dari Barang_service untuk mengelola data barang
        Barang_service barangService = new Barang_service();
        // Membuat objek instansiasi dari Kategori_service untuk mengelola data kategori
        Kategori_service kategoriService = new Kategori_service();

        // Variabel global untuk menyimpan ID barang yang sedang dipilih/diklik oleh admin (0 berarti kosong)
        int idBarangAktif = 0;

        // Method Constructor: Dijalankan pertama kali saat form ini diciptakan
        public FormDashboardAdmin()
        {
            // Menginisialisasi seluruh komponen visual (tombol, textboxt, dll) yang didesain di form
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new System.EventHandler(this.FormDashboardAdmin_Load_1);
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
            this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click);
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            this.btnTambahKategori.Click += new System.EventHandler(this.btnTambahKategori_Click);
            this.btnBatalKategori.Click += new System.EventHandler(this.btnBatalKategori_Click);
            this.btnEditDetail.Click += new System.EventHandler(this.btnEditDetail_Click);
            this.btnBatalDetail.Click += new System.EventHandler(this.btnBatalDetail_Click);
        }

        // Method event ketika form pertama kali terbuka di layar
        private void FormDashboardAdmin_Load_1(object sender, EventArgs e)
        {
            // Memanggil method tampilGrid() secara asinkron agar UI tidak membeku saat memuat data database
            this.BeginInvoke(new MethodInvoker(tampilGrid));
        }

        // Method untuk memuat daftar kategori ke dalam komponen dropdown (ComboBox)
        void muatKategori()
        {
            // Mengambil semua data kategori dari database berupa struktur DataTable melalui kelas service
            DataTable dt = kategoriService.tampilSemuaKategori();

            // Validasi data: Jika data kosong atau kolom yang diperlukan tidak ditemukan di database
            if (dt == null || !dt.Columns.Contains("nama_kategori") || !dt.Columns.Contains("id_kategori"))
            {
                // Putus sumber data ComboBox (kosongkan)
                cmbKategoriBarang.DataSource = null;
                // Hapus semua isi item di ComboBox
                cmbKategoriBarang.Items.Clear();
                // Hentikan jalannya method
                return;
            }

            // Mengatur teks yang akan ditampilkan ke pengguna (Nama Kategori)
            cmbKategoriBarang.DisplayMember = "nama_kategori";
            // Mengatur nilai data di latar belakang program (ID Kategori berbasis angka)
            cmbKategoriBarang.ValueMember = "id_kategori";
            // Memasukkan tabel data (DataTable) sebagai sumber data ComboBox
            cmbKategoriBarang.DataSource = dt;
            // Mengatur agar dropdown tidak otomatis memilih item apa pun saat pertama muncul (-1 = kosong)
            cmbKategoriBarang.SelectedIndex = -1;
        }

        // Method untuk membersihkan semua inputan TextBox dan ComboBox di layar Form
        void bersihkan()
        {
            txtNamaBarang.Clear(); // Mengosongkan text nama barang
            cmbKategoriBarang.SelectedIndex = -1; // Mereset pilihan kategori menjadi kosong
            txtJumlahBarang.Clear(); // Mengosongkan text jumlah barang
            txtCari.Clear(); // Mengosongkan kolom pencarian
            idBarangAktif = 0; // Mereset pointer ID barang aktif kembali ke nol
            if (txtKategoriBarang != null) txtKategoriBarang.Clear(); // Mengosongkan kolom input kategori baru jika komponennya ada
            txtNamaDetail.Clear(); // Mengosongkan nama barang pada form rincian kondisi
            txtKondisiBagus.Text = "0"; // Mereset teks jumlah kondisi bagus ke angka string "0"
            txtKondisiRusak.Text = "0"; // Mereset teks jumlah kondisi rusak ke angka string "0"
        }

        // Method utama untuk memuat data dari database, menghitung sirkulasi stok, dan menampilkan statistik
        void tampilGrid()
        {
            // Menyusun kueri SQL kompleks dengan subquery untuk menghitung status peminjaman barang secara real-time
            string queryDinamis = @"
        SELECT 
            b.id_barang,
            b.nama_barang,
            b.id_kategori,
            k.nama_kategori,
            b.jumlah_barang AS jumlah_total,
            b.kondisi_bagus,
            b.kondisi_rusak,
            /* Subquery 1: Menghitung jumlah barang yang sedang dipinjam atau disetujui admin */
            IFNULL((
                SELECT SUM(dp.jumlah_pinjam) 
                FROM detail_peminjaman dp 
                INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                WHERE dp.id_barang = b.id_barang 
                AND LOWER(p.status_peminjaman) IN ('dipinjam', 'disetujui')
            ), 0) AS jumlah_dipinjam,
            /* Subquery 2: Menghitung jumlah barang yang sudah di-booking tapi statusnya masih 'pending' */
            IFNULL((
                SELECT SUM(dp.jumlah_pinjam) 
                FROM detail_peminjaman dp 
                INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                WHERE dp.id_barang = b.id_barang 
                AND LOWER(p.status_peminjaman) = 'pending'
            ), 0) AS jumlah_booking,
            /* Formula Matematika SQL: Menghitung sisa stok siap pakai (Barang Bagus dikurangi Yang Sedang Dipinjam) */
            (b.kondisi_bagus - IFNULL((
                SELECT SUM(dp.jumlah_pinjam) 
                FROM detail_peminjaman dp 
                INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                WHERE dp.id_barang = b.id_barang 
                AND LOWER(p.status_peminjaman) IN ('dipinjam', 'disetujui')
            ), 0)) AS jumlah_tersedia
        FROM barang b
        INNER JOIN kategori k ON b.id_kategori = k.id_kategori";

            // Fitur Pencarian Dinamis: Jika kolom cari diisi oleh admin
            if (txtCari.Text.Length > 0)
            {
                // Menambahkan klausa WHERE untuk memfilter nama barang berdasarkan pola teks tertentu
                queryDinamis += " WHERE b.nama_barang LIKE @cari ";
            }

            // Mengurutkan hasil tampilan tabel berdasarkan nama barang dari A ke Z (Alphabetis)
            queryDinamis += " ORDER BY b.nama_barang ASC";

            // Membuat objek tabel kosong di memori C# untuk menampung baris hasil database
            DataTable dt = new DataTable();

            // Membuka blok koneksi database MySQL dan memastikan koneksi ditutup otomatis setelah blok selesai (using)
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open(); // Membuka jalur komunikasi data ke database server
                    // Menyiapkan perintah eksekusi SQL berbasis kueri dinamis di atas
                    using (MySqlCommand cmd = new MySqlCommand(queryDinamis, conn))
                    {
                        // Jika filter pencarian aktif, pasang parameter anti SQL Injection (@cari)
                        if (txtCari.Text.Length > 0)
                        {
                            cmd.Parameters.AddWithValue("@cari", "%" + txtCari.Text.Trim() + "%");
                        }

                        // Jembatan data untuk menarik hasil kueri dari database server masuk ke memori aplikasi
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt); // Memasukkan semua baris hasil database ke dalam objek DataTable (dt)
                        }
                    }
                }
                catch (Exception ex) // Blok penanganan jika koneksi database atau kueri mengalami error
                {
                    // Menampilkan kotak dialog kesalahan yang berisi rincian pesan error sistem
                    MessageBox.Show("Gagal memuat kalkulasi sirkulasi dinamis: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Mematikan pembuatan kolom otomatis pada komponen DataGridView agar tata letak tidak berantakan
            dgvBarang.AutoGenerateColumns = false;
            // Memetakan kolom ke-1 di UI DataGridView ke kolom "nama_barang" hasil kueri SQL
            dgvBarang.Columns[0].DataPropertyName = "nama_barang";
            // Memetakan kolom ke-2 di UI DataGridView ke kolom "nama_kategori" hasil kueri SQL
            dgvBarang.Columns[1].DataPropertyName = "nama_kategori";
            // Memetakan kolom ke-3 di UI DataGridView ke kolom "jumlah_total" hasil kueri SQL
            dgvBarang.Columns[2].DataPropertyName = "jumlah_total";
            // Memetakan kolom ke-4 di UI DataGridView ke kolom "jumlah_tersedia" hasil kueri SQL
            dgvBarang.Columns[3].DataPropertyName = "jumlah_tersedia";
            // Memetakan kolom ke-5 di UI DataGridView ke kolom "jumlah_dipinjam" hasil kueri SQL
            dgvBarang.Columns[4].DataPropertyName = "jumlah_dipinjam";

            // Menjadikan DataTable (dt) sebagai sumber pasokan data utama tabel visual (DataGridView) di layar
            dgvBarang.DataSource = dt;

            // Variabel lokal untuk menghitung total akumulasi statistik kotak ringkasan dashboard
            int totalBarangSemua = 0;
            int totalSedangDipinjam = 0;
            int totalSiapTersedia = 0;

            // Melakukan perulangan untuk memeriksa data baris demi baris di dalam tabel hasil kueri
            foreach (DataRow row in dt.Rows)
            {
                // Jika kolom total tidak bernilai kosong (null), tambahkan angkanya ke akumulator total aset barang
                if (row["jumlah_total"] != DBNull.Value) totalBarangSemua += Convert.ToInt32(row["jumlah_total"]);
                // Jika kolom dipinjam tidak bernilai kosong, tambahkan angkanya ke akumulator total barang terpinjam
                if (row["jumlah_dipinjam"] != DBNull.Value) totalSedangDipinjam += Convert.ToInt32(row["jumlah_dipinjam"]);
                // Jika kolom tersedia tidak bernilai kosong, tambahkan angkanya ke akumulator total barang siap pakai
                if (row["jumlah_tersedia"] != DBNull.Value) totalSiapTersedia += Convert.ToInt32(row["jumlah_tersedia"]);
            }

            // Mengubah teks Label Statistik Total Barang di layar UI dengan angka hasil hitungan kalkulasi di atas
            lblTotal.Text = totalBarangSemua.ToString();
            // Mengubah teks Label Statistik Barang Dipinjam di layar UI dengan angka hasil hitungan kalkulasi di atas
            lblDipinjam.Text = totalSedangDipinjam.ToString();
            // Mengubah teks Label Statistik Barang Tersedia di layar UI dengan angka hasil hitungan kalkulasi di atas
            lblTersedia.Text = totalSiapTersedia.ToString();

            // Memanggil fungsi untuk me-refresh pilihan ComboBox kategori agar selalu sinkron dengan database terbaru
            muatKategori();
            // Mengunci kolom nama barang di bagian detail kondisi agar nilainya aman dari ketikan iseng (hanya bisa dibaca)
            txtNamaDetail.ReadOnly = true;
        }

        // Method event ketika tombol keluar (X / Exit) diklik oleh pengguna
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Menampilkan kotak konfirmasi pilihan YES atau NO untuk memastikan niat admin ingin keluar program
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Jika admin menekan tombol YES, matikan seluruh proses jalannya sistem aplikasi SIMADE secara total
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        // Method event untuk memproses penambahan data barang baru ke database (Tombol Tambah)
        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi input: Memeriksa apakah ada kolom input teks atau pilihan kategori yang dikosongkan admin
            if (string.IsNullOrWhiteSpace(txtNamaBarang.Text) || cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtJumlahBarang.Text))
            {
                // Memunculkan peringatan bahwa data input tidak lengkap
                MessageBox.Show("Mohon lengkapi semua data barang sebelum menambahkan.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Batalkan proses tambah
            }

            // Validasi tipe data angka: Mencoba mengubah teks input stok barang menjadi tipe data integer (angka murni)
            if (!int.TryParse(txtJumlahBarang.Text, out int jumlah))
            {
                // Muncul peringatan jika admin menginput karakter non-angka (seperti huruf atau simbol)
                MessageBox.Show("Jumlah barang harus berupa angka valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Batalkan proses tambah
            }

            // Mengambil teks nama barang inputan dan membuang spasi liar di ujung teks awal/akhir (.Trim)
            string namaBarangBaru = txtNamaBarang.Text.Trim();

            // LOGIKA PBO SERVICE: Menggunakan kelas service untuk mengecek apakah nama barang tersebut sudah ada di DB
            if (barangService.isExist(namaBarangBaru))
            {
                // Jika terdeteksi duplikasi nama barang, hentikan proses dan tampilkan pesan penolakan
                MessageBox.Show("Barang sudah terdaftar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Menyusun kueri SQL lokal INSERT dengan parameter aman (@nama, @kat, @qty, @bagus) untuk menghindari peretasan
            string queryInsert = "INSERT INTO barang (nama_barang, id_kategori, jumlah_barang, kondisi_bagus, kondisi_rusak) VALUES (@nama, @kat, @qty, @bagus, 0)";

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open(); // Membuka gerbang database
                    using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
                    {
                        // Memetakan dan mengamankan nilai input ke dalam masing-masing variabel parameter SQL kueri
                        cmd.Parameters.AddWithValue("@nama", namaBarangBaru);
                        cmd.Parameters.AddWithValue("@kat", Convert.ToInt32(cmbKategoriBarang.SelectedValue));
                        cmd.Parameters.AddWithValue("@qty", jumlah);
                        // Logika bisnis: Barang baru masuk otomatis semuanya dialokasikan dalam kondisi bagus
                        cmd.Parameters.AddWithValue("@bagus", jumlah);

                        cmd.ExecuteNonQuery(); // Eksekusi penembakan kueri INSERT langsung ke dalam tabel database MySQL
                    }
                    // Menampilkan notifikasi sukses kepada pengguna
                    MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bersihkan(); // Bersihkan formulir input di UI
                    tampilGrid(); // Refresh isi tabel visual agar barang baru langsung memosisikan diri di grid
                }
                catch (Exception ex) // Blok penanganan jika terjadi kendala pada kueri insert database
                {
                    MessageBox.Show("Gagal simpan database: " + ex.Message, "Error");
                }
            }
        }

        // Method event untuk memproses pembaruan data barang induk (Tombol Edit Induk)
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Validasi data pengaman: Jika tidak ada ID barang aktif yang dipilih dari tabel, batalkan proses edit
            if (idBarangAktif == 0) return;

            // Validasi tipe data angka: Memastikan jumlah inputan stok berupa format angka murni
            if (!int.TryParse(txtJumlahBarang.Text, out int totalInput)) return;

            // Kueri SQL lokal untuk mengupdate data barang induk sekaligus merevisi kondisi bagus secara matematis
            // Rumus: Kondisi Bagus Baru = Total Stok Baru dikurangi Nilai Kondisi Rusak Lama di database
            string queryUpdate = "UPDATE barang SET nama_barang = @nama, id_kategori = @kat, jumlah_barang = @total, kondisi_bagus = (@total - kondisi_rusak) WHERE id_barang = @id";

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open(); // Membuka jalur database
                    using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                    {
                        // Mengamankan data masukan dari form input ke parameter kueri UPDATE SQL
                        cmd.Parameters.AddWithValue("@nama", txtNamaBarang.Text.Trim());
                        cmd.Parameters.AddWithValue("@kat", Convert.ToInt32(cmbKategoriBarang.SelectedValue));
                        cmd.Parameters.AddWithValue("@total", totalInput);
                        cmd.Parameters.AddWithValue("@id", idBarangAktif); // Mengunci target data berdasarkan ID barang aktif

                        cmd.ExecuteNonQuery(); // Eksekusi penembakan kueri UPDATE langsung ke database server
                    }
                    MessageBox.Show("Data induk logistik berhasil diperbarui!", "Sukses");
                    bersihkan(); // Bersihkan form masukan teks
                    tampilGrid(); // Perbarui grid data visual dashboard
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengubah data: " + ex.Message, "Error");
                }
            }
        }

        // Method event untuk menghapus data barang tertentu dari sistem (Tombol Hapus)
        private void btnHapus_Click(object sender, EventArgs e)
        {
            // Validasi pengaman: Memastikan ada ID barang yang ditarget untuk dihapus (> 0)
            if (idBarangAktif > 0)
            {
                // Menampilkan dialog box konfirmasi peringatan kritis untuk mencegah salah klik hapus data oleh admin
                if (MessageBox.Show("Yakin data akan dihapus?", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // LOGIKA PBO SERVICE: Mengeksekusi fungsi hapus pada class service dengan melempar ID barang aktif
                    // Jika database memberikan respon baris terpengaruh lebih besar dari nol (> 0), artinya sukses didelete
                    if (barangService.hapus(idBarangAktif) > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus", "HAPUS DATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bersihkan(); // Kosongkan isian form
                        tampilGrid(); // Perbarui isi tabel visual dashboard
                    }
                }
            }
        }

        // Method event ketika tombol batal diklik oleh admin
        private void btnBatal_Click(object sender, EventArgs e) { bersihkan(); } // Langsung memicu pembersihan total kolom inputan

        // Method event ketika admin mengetikkan sesuatu kata kunci di kolom pencarian barang
        private void txtCari_TextChanged(object sender, EventArgs e) { tampilGrid(); } // Membaca ulang isi grid tabel secara dinamis setiap ada ketikan huruf baru

        // Method event untuk memproses penambahan kategori barang baru dari sub-fitur mini-input kategori
        private void btnTambahKategori_Click(object sender, EventArgs e)
        {
            // Validasi: Jika kolom input kategori baru kosong atau hanya berisi spasi, batalkan proses simpan
            if (string.IsNullOrEmpty(txtKategoriBarang.Text.Trim())) return;

            string kategoriBaru = txtKategoriBarang.Text.Trim(); // Mengambil nama kategori masukan baru

            // LOGIKA PBO SERVICE: Memeriksa apakah teks nama kategori tersebut sudah ada di database melalui service kategori
            if (!kategoriService.isExist(kategoriBaru)) // Tanda seru (!) berarti "Jika TIDAK ada duplikasi"
            {
                // LOGIKA PBO SERVICE: Menyimpan kategori baru ke tabel kategori database via service kategori
                if (kategoriService.simpanKategori(kategoriBaru) > 0)
                {
                    MessageBox.Show("Kategori berhasil ditambahkan!", "SUKSES");
                    txtKategoriBarang.Clear(); // Bersihkan kolom isian teks kategori baru
                    tampilGrid(); // Refresh grid data utama agar kategori baru langsung terdaftar di pilihan ComboBox
                }
            }
        }

        // Method event ketika admin membatalkan pengisian kotak teks input kategori baru
        private void btnBatalKategori_Click(object sender, EventArgs e) { txtKategoriBarang.Clear(); } // Menghapus tulisan di kolom kategori baru

        // Method event penangkap interaksi: Terjadi ketika admin mengklik sebuah baris sel di tabel visual (DataGridView)
        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Memastikan baris data yang diklik adalah valid (indeks baris 0 ke atas, bukan baris judul kolom tabel)
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Menangkap dan mengonversi objek penampung data dari DataGridView kembali menjadi struktur DataTable asal
                    DataTable dt = (DataTable)dgvBarang.DataSource;
                    // Pengaman: Memastikan data tabel memuat baris data yang sesuai dengan urutan baris yang diklik
                    if (dt != null && dt.Rows.Count > e.RowIndex)
                    {
                        DataRow dr = dt.Rows[e.RowIndex]; // Mengunci objek baris data database yang bersangkutan

                        // Memindahkan isi nilai dari baris tabel database ke variabel global ID Barang Aktif
                        idBarangAktif = Convert.ToInt32(dr["id_barang"]);
                        // Memindahkan nilai nama barang database ke input TextBox Form Data Barang
                        txtNamaBarang.Text = dr["nama_barang"].ToString();
                        // Memindahkan nilai jumlah total stok database ke input TextBox Form Data Barang
                        txtJumlahBarang.Text = dr["jumlah_total"].ToString();

                        // Sinkronisasi Kategori: Jika kolom ID kategori di database tidak kosong
                        if (dr["id_kategori"] != DBNull.Value)
                        {
                            // Mengatur agar ComboBox kategori otomatis memilih opsi kategori yang cocok berdasarkan ID-nya
                            cmbKategoriBarang.SelectedValue = dr["id_kategori"];
                        }

                        // Sinkronisasi data ke Form Rincian Kondisi Kelayakan di bagian kanan layar form
                        txtNamaDetail.Text = dr["nama_barang"].ToString(); // Mengisi nama barang rincian kondisi
                        txtKondisiBagus.Text = dr["kondisi_bagus"].ToString(); // Mengisi angka jumlah kondisi bagus saat ini
                        txtKondisiRusak.Text = dr["kondisi_rusak"].ToString(); // Mengisi angka jumlah kondisi rusak saat ini
                    }
                }
                catch (Exception) { } // Mengabaikan error kecil jika terjadi kesalahan klik/konversi di UI luar
            }
        }

        // Method event ketika tombol LogOut diklik oleh pengguna admin
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin(); // Menciptakan instansiasi form halaman login baru
            login.Show(); // Menampilkan form login baru tersebut ke layar monitor
            this.Close(); // Menutup dan mematikan form dashboard admin yang sedang aktif saat ini
        }

        // Method event navigasi: Klik menu data barang untuk membersihkan dan me-refresh ulang halaman dashboard barang
        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            bersihkan();
            tampilGrid();
        }

        // Method event navigasi: Menutup dashboard dan mengalihkan admin ke Halaman Transaksi Data Peminjaman
        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam frm = new FormDataPinjam(); // Membuat objek halaman transaksi peminjaman
            frm.Show(); // Tampilkan halaman baru ke layar
            this.Close(); // Tutup halaman dashboard ini
        }

        // Method event navigasi: Menutup dashboard dan mengalihkan admin ke Halaman Transaksi Serah Terima Pengambilan Barang
        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan frm = new FormDataPengambilan(); // Membuat objek halaman serah terima ambil
            frm.Show(); // Tampilkan ke layar
            this.Close(); // Tutup halaman dashboard ini
        }

        // Method event navigasi: Menutup dashboard dan mengalihkan admin ke Halaman Transaksi Pengembalian Logistik
        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian frm = new FormDataPengembalian(); // Membuat objek halaman logistik pengembalian
            frm.Show(); // Tampilkan ke layar
            this.Close(); // Tutup halaman dashboard ini
        }

        // Method event navigasi: Menutup dashboard dan membuka form pendaftaran akun admin baru (Form Register)
        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin frm = new FormRegisterAdmin(); // Membuat objek halaman registrasi admin baru
            frm.Show(); // Tampilkan ke layar
            this.Close(); // Tutup halaman dashboard ini
        }

        // Method event ketika admin membatalkan pengeditan nilai di form rincian kondisi barang bagus/rusak
        private void btnBatalDetail_Click(object sender, EventArgs e) { bersihkan(); } // Menghapus seluruh kolom inputan log di layar

        // Method event VALIDASI KRITIS: Memproses pembaruan rincian jumlah barang kondisi bagus vs rusak (Tombol Simpan Kondisi)
        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            // Validasi pengaman: Harus memilih data barang di tabel grid terlebih dahulu sebelum mengedit detail kondisi
            if (idBarangAktif == 0) return;

            // Validasi format data: Memastikan input kondisi bagus dan rusak adalah angka integer murni yang valid
            if (!int.TryParse(txtKondisiBagus.Text, out int bagus) || !int.TryParse(txtKondisiRusak.Text, out int rusak))
            {
                MessageBox.Show("Input kondisi harus berupa angka valid!", "Peringatan");
                return; // Batalkan proses simpan detail
            }

            // Menghitung total proporsi kondisi baru dari penjumlahan input admin: (Bagus + Rusak)
            int totalKondisiBaru = bagus + rusak;

            // Menyusun kueri SQL lokal untuk memeriksa jumlah stok asli barang yang tercatat saat ini di database server
            string queryCariTotal = "SELECT jumlah_barang FROM barang WHERE id_barang = @id";
            int jumlahBarangAsli = 0; // Variabel penampung angka stok asli database

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                conn.Open(); // Buka gerbang koneksi database
                using (MySqlCommand cmd = new MySqlCommand(queryCariTotal, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idBarangAktif); // Mengunci target berdasarkan ID barang aktif
                    object res = cmd.ExecuteScalar(); // Mengeksekusi kueri dan mengambil satu nilai skalar (angka stok) dari DB
                    if (res != null) jumlahBarangAsli = Convert.ToInt32(res); // Mengonversi hasil database menjadi tipe integer C#
                }
            }

            // FILTER KESELAMATAN ASET DATA: Memeriksa apakah total proporsi kondisi baru melanggar jumlah stok asli di gudang
            if (totalKondisiBaru != jumlahBarangAsli)
            {
                // Jika tidak sama (misal total stok asli 10, tapi admin menginput bagus 8 dan rusak 5 (total 13)), proses ditolak keras!
                MessageBox.Show($"Validasi Gagal! Total kondisi baru tidak sama dengan Total Stok Barang ({jumlahBarangAsli}).", "Inkonsistensi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Gagalkan penyimpanan untuk menghindari kerusakan perhitungan data inventaris sekolah/kantor
            }

            // Kueri SQL lokal UPDATE untuk mengganti proporsi kolom kondisi_bagus dan kondisi_rusak berdasarkan input ter-validasi
            string queryUpdateDetail = "UPDATE barang SET kondisi_bagus = @bagus, kondisi_rusak = @rusak WHERE id_barang = @id";
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open(); // Membuka jalur data database
                    using (MySqlCommand cmd = new MySqlCommand(queryUpdateDetail, conn))
                    {
                        // Mengunci nilai parameter kueri SQL agar bersih dari ancaman peretasan kueri ilegal
                        cmd.Parameters.AddWithValue("@bagus", bagus);
                        cmd.Parameters.AddWithValue("@rusak", rusak);
                        cmd.Parameters.AddWithValue("@id", idBarangAktif);

                        cmd.ExecuteNonQuery(); // Mengeksekusi perubahan data detail kelayakan barang langsung di tabel database
                    }
                    MessageBox.Show("Detail kondisi berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bersihkan(); // Bersihkan kolom isian teks form
                    tampilGrid(); // Muat ulang grid visual untuk melihat update perubahan status sirkulasi logistik barang
                }
                catch (Exception ex) // Penanganan jika kueri database mengalami kegagalan di tengah jalan
                {
                    MessageBox.Show("Gagal memperbarui detail kondisi: " + ex.Message, "Error");
                }
            }
        }
    }
}