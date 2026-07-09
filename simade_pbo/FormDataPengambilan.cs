using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using simade_pbo.Service;

namespace simade_pbo
{
    public partial class FormDataPengambilan : Form
    {
        // Deklarasi service dan variabel global yang dipakai di form ini
        Pinjam_service pinjamService = new Pinjam_service();
        string kodeNotaAktif = "";
        DateTime hariIni = DateTime.Now;

        // Isinya buat nyiapin posisi form, daftarin event klik tombol, dan ngunci textbox biar gak bisa diedit sembarangan
        public FormDataPengambilan()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // Biar form muncul pas di tengah layar

            // Daftarin event untuk load form, klik tabel, dan error grid
            this.Load += new System.EventHandler(this.FormDataPengambilan_Load);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);

            // Gak usah error kalau kolom cari kosong, langsung daftarin event ketik
            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            // Daftarin event klik buat semua tombol navigasi menu sidebar
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click);
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // Daftarin event klik tombol aksi (Simpan & Batal)
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            if (this.btnBatal != null)
            {
                this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            }

            // Ngunci data biodata biar read-only (biar admin gak asal ubah teks biodata orang)
            txtNamaPeminjam.ReadOnly = true;
            txtNoTelepon.ReadOnly = true;
            txtTglPinjam.ReadOnly = true;
            txtTglKembali.ReadOnly = true;
        }

        // Fungsi saat form pertama kali kebuka di layar
        private void FormDataPengambilan_Load(object sender, EventArgs e)
        {
            // Pake BeginInvoke biar loading data gak bikin aplikasi nge-lag/freeze pas baru buka
            this.BeginInvoke(new MethodInvoker(InitDataDanKPI));
        }

        // Fungsi jembatan buat manggil angka ringkasan (KPI) sekaligus nampilin isi tabel utama
        private void InitDataDanKPI()
        {
            HitungKPIGlobalMurni(); // Update angka-angka di atas dashboard
            SegarkanGridUtama();     // Isi data ke tabel peminjaman
        }

        // Fungsi buat ngitung statistik/angka ringkasan yang ada di card dashboard atas
        // Dia bakal ngetotal berapa yang ngantre, pinjaman hari ini, yang lagi dibawa, atau yang hangus
        private void HitungKPIGlobalMurni()
        {
            DataTable dtSemua = pinjamService.tampilSemuaPeminjaman();

            int hitungAntrean = 0;
            int hitungHariIni = 0;
            int hitungDiambil = 0;
            int hitungHangus = 0;

            if (dtSemua != null)
            {
                foreach (DataRow row in dtSemua.Rows)
                {
                    string statusReal = "";

                    // Cek nama kolom status di DB (biar gak error kalau namanya beda di database)
                    if (dtSemua.Columns.Contains("status"))
                    {
                        statusReal = row["status"].ToString().ToLower().Trim();
                    }
                    else if (dtSemua.Columns.Contains("status_peminjaman"))
                    {
                        statusReal = row["status_peminjaman"].ToString().ToLower().Trim();
                    }

                    // Logika pemilahan status untuk angka card dashboard
                    if (statusReal == "disetujui" || statusReal == "pending")
                    {
                        hitungAntrean++;

                        if (dtSemua.Columns.Contains("tgl_pinjam") && row["tgl_pinjam"] != DBNull.Value)
                        {
                            DateTime tglPinjam = Convert.ToDateTime(row["tgl_pinjam"]);
                            if (tglPinjam.Date == DateTime.Today.Date)
                            {
                                hitungHariIni++;
                            }
                        }
                    }
                    else if (statusReal == "dipinjam" || statusReal == "diambil")
                    {
                        hitungDiambil++;
                    }
                    else if (statusReal == "hangus")
                    {
                        hitungHangus++;
                    }
                }
            }

            // Tampilkan hasil hitungan ke label teks masing-masing card
            if (lblAntrean != null) lblAntrean.Text = hitungAntrean.ToString();
            if (lblHariIni != null) lblHariIni.Text = hitungHariIni.ToString();
            if (lblDiambil != null) lblDiambil.Text = hitungDiambil.ToString();
            if (lblHangus != null) lblHangus.Text = hitungHangus.ToString();
        }

        // Fungsi buat narik data dari DB terus di-filter dan dimasukin ke tabel besar (dataGridView1)
        private void SegarkanGridUtama()
        {
            dataGridView1.AutoGenerateColumns = false;

            DataTable dtMentah;
            string kataKunci = txtCari != null ? txtCari.Text.Trim() : "";

            // Kalau kolom cari diisi, cari pake keyword. Kalau kosong, tampilin semua data.
            if (!string.IsNullOrEmpty(kataKunci))
            {
                dtMentah = pinjamService.cariPeminjaman(kataKunci);
            }
            else
            {
                dtMentah = pinjamService.tampilSemuaPeminjaman();
            }

            if (dtMentah == null)
            {
                dataGridView1.DataSource = null;
                return;
            }

            // Bikin struktur tabel filter tiruan baru buat nampung status tampilan ramah user
            DataTable dtFiltered = dtMentah.Clone();

            if (!dtFiltered.Columns.Contains("status_tampilan"))
            {
                dtFiltered.Columns.Add("status_tampilan", typeof(string));
            }

            foreach (DataRow row in dtMentah.Rows)
            {
                string statusReal = "";
                if (dtMentah.Columns.Contains("status"))
                {
                    statusReal = row["status"].ToString().ToLower().Trim();
                }
                else if (dtMentah.Columns.Contains("status_peminjaman"))
                {
                    statusReal = row["status_peminjaman"].ToString().ToLower().Trim();
                }

                // Filter: Yang masuk ke form pengambilan cuma barang yang status logistiknya cocok
                if (statusReal == "disetujui" || statusReal == "pending" || statusReal == "diambil" || statusReal == "dipinjam" || statusReal == "hangus")
                {
                    DataRow newRow = dtFiltered.NewRow();
                    newRow.ItemArray = row.ItemArray;

                    // Bikin huruf pertama status jadi kapital (misal: "dipinjam" jadi "Dipinjam")
                    if (!string.IsNullOrEmpty(statusReal))
                    {
                        newRow["status_tampilan"] = char.ToUpper(statusReal[0]) + statusReal.Substring(1);
                    }
                    else
                    {
                        newRow["status_tampilan"] = "-";
                    }

                    dtFiltered.Rows.Add(newRow);
                }
            }

            // Lempar hasil filter ke fungsi formatter kolom
            FormatDanTampilkanData(dtFiltered);
        }

        // Fungsi otomatis jalan pas kita ngetik di kolom pencarian (langsung nge-refresh isi tabel)
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            SegarkanGridUtama();
        }

        // Fungsi buat ngiket kolom database ke kolom DataGridView biar datanya gak ketuker-tuker posisi
        private void FormatDanTampilkanData(DataTable dt)
        {
            dataGridView1.AutoGenerateColumns = false;

            if (dataGridView1.Columns.Count >= 5)
            {
                dataGridView1.Columns[0].DataPropertyName = "kode_peminjaman";
                dataGridView1.Columns[1].DataPropertyName = "nama_lengkap";
                dataGridView1.Columns[2].DataPropertyName = "tgl_pinjam";
                dataGridView1.Columns[3].DataPropertyName = "tgl_kembali";
                dataGridView1.Columns[4].DataPropertyName = "status_tampilan";
            }

            dataGridView1.DataSource = dt;
        }

        // Fungsi pas baris tabel utama diklik: Buat ngambil data baris itu terus ditaruh ke form detail di sebelah kanan/bawah
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Memastikan yang diklik bukan baris judul/header tabel
            {
                try
                {
                    DataGridViewRow baris = this.dataGridView1.Rows[e.RowIndex];
                    DataRowView rowView = baris.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow dr = rowView.Row;

                        // Pindahin data teks biasa dari tabel ke textbox
                        kodeNotaAktif = dr["kode_peminjaman"].ToString();
                        txtNamaPeminjam.Text = dr["nama_lengkap"].ToString();

                        // Format tanggal pinjam biar enak dibaca (dd/MM/yyyy)
                        if (dr["tgl_pinjam"] != DBNull.Value)
                        {
                            DateTime tPinjam = Convert.ToDateTime(dr["tgl_pinjam"]);
                            txtTglPinjam.Text = tPinjam.ToString("dd/MM/yyyy");
                        }
                        else txtTglPinjam.Clear();

                        // Format tanggal kembali
                        if (dr["tgl_kembali"] != DBNull.Value)
                        {
                            DateTime tKembali = Convert.ToDateTime(dr["tgl_kembali"]);
                            txtTglKembali.Text = tKembali.ToString("dd/MM/yyyy");
                        }
                        else txtTglKembali.Text = "-";

                        // Ambil detail list barang bawaan dari nota yang diklik dari service database
                        DataTable dtDetail = pinjamService.tampilDetailBarangDisetujuiSah(kodeNotaAktif);

                        // Cari nomor telepon peminjam di baris detail pertama
                        if (dtDetail != null && dtDetail.Rows.Count > 0)
                        {
                            if (dtDetail.Columns.Contains("nomor_telepon"))
                                txtNoTelepon.Text = dtDetail.Rows[0]["nomor_telepon"].ToString();
                            else if (dtDetail.Columns.Contains("no_hp"))
                                txtNoTelepon.Text = dtDetail.Rows[0]["no_hp"].ToString();
                            else
                                txtNoTelepon.Text = "-";
                        }
                        else
                        {
                            txtNoTelepon.Text = "-";
                        }

                        // Kosongin grid detail bawah dulu baru diisi data barang yang baru
                        dgvDetail.DataSource = null;
                        dgvDetail.AutoGenerateColumns = false;

                        if (dgvDetail.Columns.Count >= 4)
                        {
                            dgvDetail.Columns[0].DataPropertyName = "nama_barang";
                            dgvDetail.Columns[1].DataPropertyName = "nama_kategori";
                            dgvDetail.Columns[2].DataPropertyName = "jumlah_pinjam";
                            dgvDetail.Columns[3].DataPropertyName = "deskripsi_barang";
                        }

                        dgvDetail.DataSource = dtDetail;

                        // Pengaman: Kunci kolom nama, kategori, jumlah. Hanya kolom deskripsi/kondisi barang (indeks 3) yang boleh diketik admin.
                        btnSimpan.Enabled = true;
                        dgvDetail.ReadOnly = false;
                        dgvDetail.Columns[0].ReadOnly = true;
                        dgvDetail.Columns[1].ReadOnly = true;
                        dgvDetail.Columns[2].ReadOnly = true;
                        dgvDetail.Columns[3].ReadOnly = false;

                        // Otomatis isi nama pengambil sama dengan nama peminjam biar cepet
                        if (txtDiambilOleh != null)
                        {
                            txtDiambilOleh.ReadOnly = false;
                            txtDiambilOleh.Text = txtNamaPeminjam.Text;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat rincian item: " + ex.Message, "Error Rincian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Fungsi peredam error grid biar aplikasi gak nge-crash/keluar paksa kalau user salah ketik tipe data di tabel
        private void dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; // Cuekin aja error internal render sel-nya
        }

        // Fungsi Tombol Simpan: Buat nyimpen berita acara kalau fisik barang beneran udah dikasihin ke orangnya
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // Validasi cek apakah user udah milih data nota atau belum
            if (string.IsNullOrEmpty(kodeNotaAktif))
            {
                MessageBox.Show("Silakan pilih antrean transaksi terlebih dahulu pada tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi nama orang yang ngambil barang wajib ditulis buat pertanggungjawaban gudang
            if (txtDiambilOleh != null && string.IsNullOrWhiteSpace(txtDiambilOleh.Text))
            {
                MessageBox.Show("Nama Pengambil barang wajib diisi sebagai penanggung jawab fisik!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiambilOleh.Focus();
                return;
            }

            dgvDetail.EndEdit(); // Paksa grid buat udahan ngedit biar tulisan terakhir di sel kesimpen sempurna

            string namaPenerima = txtDiambilOleh != null ? txtDiambilOleh.Text.Trim() : txtNamaPeminjam.Text;

            DialogResult konfirmasi = MessageBox.Show($"Konfirmasi penyerahan fisik barang untuk Nota {kodeNotaAktif} resmi diambil oleh {namaPenerima}?", "Siklus Logistik SIMADE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                int totalBaris = dgvDetail.Rows.Count;
                bool suksesUpdateDeskripsi = true;

                try
                {
                    // Koneksi SQL manual buat nge-loop data kondisi/deskripsi barang per baris item bawaannya
                    using (MySqlConnection conn = Koneksi.GetConn())
                    {
                        conn.Open();
                        for (int i = 0; i < totalBaris; i++)
                        {
                            var row = dgvDetail.Rows[i];
                            if (row.DataBoundItem != null)
                            {
                                DataRowView drv = (DataRowView)row.DataBoundItem;
                                int idDetail = Convert.ToInt32(drv["id_detail_peminjaman"]);
                                string deskripsiInput = row.Cells[3].Value?.ToString() ?? "";

                                // Update catatan keadaan barang keluar dan nama pengambilnya ke database detail
                                string queryUpdate = "UPDATE detail_peminjaman SET deskripsi_barang = @deskripsi, dipinjam_oleh = @oleh WHERE id_detail_peminjaman = @id";
                                using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                                {
                                    cmd.Parameters.AddWithValue("@deskripsi", deskripsiInput);
                                    cmd.Parameters.AddWithValue("@oleh", namaPenerima);
                                    cmd.Parameters.AddWithValue("@id", idDetail);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    suksesUpdateDeskripsi = false;
                    MessageBox.Show("Gagal menyimpan rincian berita acara pengambilan barang: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Kalau simpan detail kondisi barang aman, ganti status nota induknya jadi 'dipinjam'
                if (suksesUpdateDeskripsi)
                {
                    if (pinjamService.ubahStatusNotaInduk(kodeNotaAktif, "dipinjam") > 0)
                    {
                        MessageBox.Show("Sukses! Fisik barang resmi diserahkan.\nStatus Nota diperbarui menjadi 'DIPINJAM'.", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Segarkan dashboard dan bersihkan bekas ketikan di form
                        HitungKPIGlobalMurni();
                        SegarkanGridUtama();
                        BersihkanPanelDetail();
                    }
                    else
                    {
                        MessageBox.Show("Gagal memperbarui status nota peminjaman induk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Tombol Batal: Buat ngebersihin form kalau gak jadi memproses transaksi yang dipilih
        private void btnBatal_Click(object sender, EventArgs e)
        {
            BersihkanPanelDetail();
        }

        // Fungsi pembantu buat ngosongin semua isi textbox dan tabel detail biar form kembali bersih semula
        private void BersihkanPanelDetail()
        {
            kodeNotaAktif = "";
            txtNamaPeminjam.Clear();
            txtNoTelepon.Clear();
            txtTglPinjam.Clear();
            txtTglKembali.Clear();
            if (txtDiambilOleh != null) txtDiambilOleh.Clear();
            dgvDetail.DataSource = null;
        }

        // --- Blok di bawah ini adalah fungsi Navigasi Menu/Tombol Pindah Form ---

        // Pindah ke Halaman Dashboard Pengelolaan Data Barang
        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            frm.Show();
            this.Close();
        }

        // Pindah ke Halaman Pengajuan/Persetujuan Data Pinjam
        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam frm = new FormDataPinjam();
            frm.Show();
            this.Close();
        }

        // Tetap di Halaman Ambil: Cuma nge-refresh data biar dapet update-an paling baru
        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            InitDataDanKPI();
            BersihkanPanelDetail();
        }

        // Pindah ke Halaman Manajemen Pengembalian Barang
        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian frm = new FormDataPengembalian();
            frm.Show();
            this.Close();
        }

        // Pindah ke Halaman Registrasi Tambah Akun Admin Baru
        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin frm = new FormRegisterAdmin();
            frm.Show();
            this.Close();
        }

        // Fungsi Tombol Log Out: Tanya user, kalau iya balik ke form login awal
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormLogin halamanLogin = new FormLogin();
                halamanLogin.Show();
                this.Close();
            }
        }

        // Fungsi Tombol Exit: Keluar dan matiin total semua proses jalannya aplikasi
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}