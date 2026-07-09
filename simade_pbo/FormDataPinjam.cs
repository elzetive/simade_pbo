using System;
using System.Data;
using System.Windows.Forms;
using simade_pbo.Service;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPinjam : Form
    {
        // Instansiasi objek layanan pinjam untuk menghubungkan form dengan database
        Pinjam_service pinjamService = new Pinjam_service();
        // Variabel global untuk merekam nomor kode nota peminjaman yang sedang dipilih/diklik
        string kodeNotaAktif = "";

        // Method Constructor: Inisialisasi awal saat form dimuat ke memori berkas
        public FormDataPinjam()
        {
            // Membuka komponen-komponen visual form (tombol, grid, textfield)
            InitializeComponent();
            // Mengatur posisi form agar muncul tepat di tengah layar komputer
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menghubungkan event-event UI (Load, Klik Sel, Error Data, Perubahan Nilai) ke method handler-nya masing-masing
            this.Load += new System.EventHandler(this.FormDataPinjam_Load);
            this.dgvUtama.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUtama_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);
            this.dgvDetail.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellValueChanged);
            this.dgvDetail.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDetail_CurrentCellDirtyStateChanged);
            this.dgvDetail.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvDetail_CellBeginEdit);

            // Jika komponen kotak pencarian tersedia, pasang event pendeteksi ketikan teks
            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            // Menghubungkan seluruh tombol navigasi menu samping (sidebar) dan tombol aksi ke method pembantunya
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click);
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnBatalDetail.Click += new System.EventHandler(this.btnBatalDetail_Click);

            // Mengunci kolom biodata user agar bersifat ReadOnly (hanya bisa dibaca, tidak bisa diedit manual lewat ketikan)
            txtNamaLengkap.ReadOnly = true;
            txtAlamat.ReadOnly = true;
            txtNoHp.ReadOnly = true;
            txtTglPinjam.ReadOnly = true;
            txtTglKembali.ReadOnly = true;
        }

        // Event saat form pertama kali terbuka, langsung memicu fungsi pemuatan data
        private void FormDataPinjam_Load(object sender, EventArgs e)
        {
            // Memanggil method pembaruan tabel secara asinkron agar aplikasi tidak lag/membeku
            this.BeginInvoke(new MethodInvoker(SegarkanGridUtama));
        }

        // Method untuk menyegarkan dan mengambil ulang daftar nota peminjaman dari database
        private void SegarkanGridUtama()
        {
            // Mematikan pembuatan kolom otomatis DataGridView agar struktur grid yang didesain tidak rusak
            dgvUtama.AutoGenerateColumns = false;
            // Memanggil service untuk mengambil semua list peminjaman ('pending', 'disetujui', 'ditolak')
            DataTable dtUtama = pinjamService.tampilSemuaPeminjaman();
            // Memformat teks status sebelum dilempar ke grid
            FormatDanTampilkanData(dtUtama);
            // Menghitung statistik ringkasan data pengajuan (KPI) untuk ditampilkan pada label dashboard
            HitungKPIPeminjaman(dtUtama);
        }

        // Method pembantu untuk merapikan format teks status peminjaman agar seragam menggunakan huruf kapital di awal kata
        private void FormatDanTampilkanData(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["status_peminjaman"] != DBNull.Value)
                {
                    // Mengambil teks status, mengubahnya ke huruf kecil, dan menghapus spasi kosong di ujung teks
                    string statusLower = row["status_peminjaman"].ToString().ToLower().Trim();
                    // Mengubah string mentah database menjadi format teks rapi untuk kebutuhan visual user
                    if (statusLower == "pending") row["status_peminjaman"] = "Pending";
                    else if (statusLower == "disetujui") row["status_peminjaman"] = "Disetujui";
                    else if (statusLower == "ditolak") row["status_peminjaman"] = "Ditolak";
                    else if (statusLower == "dipinjam") row["status_peminjaman"] = "Dipinjam";
                    else if (statusLower == "dikembalikan") row["status_peminjaman"] = "Dikembalikan";
                }
            }

            // Memasukkan DataTable yang sudah diformat ke komponen tabel visual (dgvUtama)
            dgvUtama.DataSource = dt;
            // Menghubungkan urutan indeks kolom grid ke kolom field database yang bersesuaian
            dgvUtama.Columns[0].DataPropertyName = "kode_peminjaman";
            dgvUtama.Columns[1].DataPropertyName = "nama_lengkap";
            dgvUtama.Columns[2].DataPropertyName = "tgl_pinjam";
            dgvUtama.Columns[3].DataPropertyName = "tgl_kembali";
            dgvUtama.Columns[4].DataPropertyName = "status_peminjaman";
        }

        // Method untuk menghitung total akumulasi pengajuan berdasarkan status untuk kebutuhan widget/card statistik
        private void HitungKPIPeminjaman(DataTable dt)
        {
            int totalPending = 0; int totalDisetujui = 0; int totalDitolak = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["status_peminjaman"] != DBNull.Value)
                {
                    string st = row["status_peminjaman"].ToString().ToLower().Trim();
                    if (st == "pending") totalPending++;
                    // Status disetujui, dipinjam, atau dikembalikan dihitung masuk ke dalam kategori pengajuan sukses/disetujui
                    else if (st == "disetujui" || st == "dipinjam" || st == "dikembalikan") totalDisetujui++;
                    else if (st == "ditolak") totalDitolak++;
                }
            }

            // Menampilkan hasil kalkulasi total ke komponen label di UI form jika komponen tersebut terpasang
            if (lblTotalDisetujui != null) lblTotalDisetujui.Text = totalDisetujui.ToString();
            if (lblTotalDitolak != null) lblTotalDitolak.Text = totalDitolak.ToString();
            if (lblTotalPengajuan != null) lblTotalPengajuan.Text = dt.Rows.Count.ToString(); // Total baris mewakili semua pengajuan
        }

        // Event handler jika user mengklik teks di dalam sel tabel utama (memicu method seleksi)
        private void dgvUtama_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikSelTabel(e.RowIndex);
        }

        // Event handler jika user mengklik area sel mana saja di tabel utama
        private void dgvUtama_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikSelTabel(e.RowIndex);
        }

        // Method inti untuk membaca data baris peminjaman yang dipilih dan memuat rincian barangnya ke tabel detail
        private void EksekusiKlikSelTabel(int rowIndex)
        {
            // Memastikan baris data yang diklik valid (bukan baris header/judul kolom)
            if (rowIndex >= 0)
            {
                try
                {
                    // Mengambil objek baris grid yang diklik
                    DataGridViewRow baris = this.dgvUtama.Rows[rowIndex];
                    // Mengambil data source data baris terkait yang terikat ke database
                    DataRowView rowView = (baris.DataBoundItem != null) ? (DataRowView)baris.DataBoundItem : null;

                    if (rowView != null)
                    {
                        DataRow dr = rowView.Row;
                        // Memindahkan data kode nota dan nama peminjam ke variabel global dan TextBox UI
                        kodeNotaAktif = dr["kode_peminjaman"].ToString();
                        txtNamaLengkap.Text = dr["nama_lengkap"].ToString();

                        // Memformat visual tampilan tanggal pinjam ke format standar Indonesia (Hari/Bulan/Tahun)
                        if (dr["tgl_pinjam"] != DBNull.Value)
                        {
                            DateTime tglP = Convert.ToDateTime(dr["tgl_pinjam"]);
                            txtTglPinjam.Text = tglP.ToString("dd/MM/yyyy");
                        }
                        else txtTglPinjam.Clear();

                        // Memformat visual tampilan tanggal kembali (mencegah error jika data kosong atau berupa strip penanda)
                        if (dr["tgl_kembali"] != DBNull.Value)
                        {
                            string tglKembaliStr = dr["tgl_kembali"].ToString().Trim();
                            if (tglKembaliStr.Contains("-") && tglKembaliStr.Length <= 5) txtTglKembali.Text = "-";
                            else
                            {
                                DateTime tglK = Convert.ToDateTime(dr["tgl_kembali"]);
                                txtTglKembali.Text = tglK.ToString("dd/MM/yyyy");
                            }
                        }
                        else txtTglKembali.Text = "-";

                        // Merekam status nota utama untuk menentukan apakah data detail masih boleh diedit atau dikunci
                        string statusNotaUtama = dr["status_peminjaman"].ToString().ToLower().Trim();
                        // Memanggil database via service untuk mengambil rincian list barang belanjaan/pinjaman dalam nota ini
                        DataTable dtDetail = pinjamService.tampilDetailBarang(kodeNotaAktif);

                        if (dtDetail.Rows.Count > 0)
                        {
                            // Menampilkan alamat dan kontak nomor HP peminjam ke panel informasi
                            txtAlamat.Text = dtDetail.Rows[0]["alamat"].ToString();
                            if (dtDetail.Columns.Contains("nomor_telepon")) txtNoHp.Text = dtDetail.Rows[0]["nomor_telepon"].ToString();
                            else if (dtDetail.Columns.Contains("no_hp")) txtNoHp.Text = dtDetail.Rows[0]["no_hp"].ToString();

                            // Memformat teks status per item barang di dalam tabel detail
                            foreach (DataRow rowDetail in dtDetail.Rows)
                            {
                                if (rowDetail["status"] != DBNull.Value)
                                {
                                    string st = rowDetail["status"].ToString().ToLower().Trim();
                                    if (st == "pending") rowDetail["status"] = "Pending";
                                    else if (st == "disetujui") rowDetail["status"] = "Disetujui";
                                    else if (st == "ditolak") rowDetail["status"] = "Ditolak";
                                }
                            }
                        }
                        else
                        {
                            txtAlamat.Clear(); txtNoHp.Clear();
                        }

                        // Memutuskan sementara data source grid detail untuk penyegaran data total
                        dgvDetail.DataSource = null;
                        dgvDetail.AutoGenerateColumns = false;

                        // Memetakan index kolom tabel detail (dgvDetail) ke field data hasil query database
                        dgvDetail.Columns[0].DataPropertyName = "nama_barang";
                        dgvDetail.Columns[1].DataPropertyName = "nama_kategori";
                        dgvDetail.Columns[2].DataPropertyName = "jumlah_pinjam";
                        dgvDetail.Columns[3].DataPropertyName = "jumlah_tersedia_realtime";
                        dgvDetail.Columns[4].DataPropertyName = ""; // Dikosongkan karena berupa jenis kolom pilihan (ComboBox/Drop list status)
                        dgvDetail.Columns[5].DataPropertyName = "keterangan";

                        // Mengikat data rincian barang baru ke grid detail
                        dgvDetail.DataSource = dtDetail;

                        // Perulangan untuk memasukkan nilai pilihan dropdown status (Kolom ke-4) secara manual sesuai isi database
                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            if (dtDetail.Rows[i]["status"] != DBNull.Value)
                            {
                                string statusMentah = dtDetail.Rows[i]["status"].ToString().ToLower().Trim();

                                if (statusMentah == "pending") dgvDetail.Rows[i].Cells[4].Value = "Pending";
                                else if (statusMentah == "disetujui") dgvDetail.Rows[i].Cells[4].Value = "Disetujui";
                                else if (statusMentah == "ditolak") dgvDetail.Rows[i].Cells[4].Value = "Ditolak";
                            }
                        }

                        // SISTEM PENGAMAN OTOMATIS: Jika transaksi peminjaman sudah di-proses (Disetujui/Ditolak/Dipinjam/Selesai)
                        if (statusNotaUtama == "disetujui" || statusNotaUtama == "ditolak" || statusNotaUtama == "dipinjam" || statusNotaUtama == "dikembalikan")
                        {
                            // Kunci mati data grid detail dan matikan tombol simpan agar admin tidak bisa memanipulasi data riwayat lama
                            dgvDetail.ReadOnly = true;
                            btnSimpan.Enabled = false;
                        }
                        else
                        {
                            // Jika status nota masih 'Pending', buka akses edit agar admin bisa menyetujui atau menolak per item barang
                            dgvDetail.ReadOnly = false;
                            dgvDetail.Columns[0].ReadOnly = true; // Nama barang dilarang diubah
                            dgvDetail.Columns[1].ReadOnly = true; // Kategori dilarang diubah
                            dgvDetail.Columns[3].ReadOnly = true; // Stok gudang dilarang diubah
                            dgvDetail.Columns[2].ReadOnly = false; // Jumlah pinjam boleh disesuaikan admin jika stok menipis
                            dgvDetail.Columns[4].ReadOnly = false; // Status (Setuju/Tolak) wajib bisa diubah oleh admin
                            dgvDetail.Columns[5].ReadOnly = true;  // Kolom alasan penolakan dikunci default (akan terbuka otomatis jika status diganti 'Ditolak')

                            // Jalankan penataan visual kolom keterangan alasan
                            UpdateVisualKolomKeterangan();

                            // Aktifkan tombol simpan verifikasi
                            btnSimpan.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat rincian tabel detail: " + ex.Message, "Rincian Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method pengaman untuk meredam crash internal sistem Windows Forms jika terjadi kesalahan format parsing data pada grid
        private void dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; // Mengabaikan error pelemparan crash sistem luar
        }

        // Event handler yang mendeteksi jika admin merubah isi status (Disetujui / Ditolak) pada kolom grid detail
        private void dgvDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Jika baris valid dan kolom yang dirubah adalah kolom status kelayakan (Kolom Indeks 4)
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                // Segarkan aturan visual untuk membuka/menutup kolom ketikan alasan penolakan barang
                UpdateVisualKolomKeterangan();
            }
        }

        // Event handler yang mendeteksi saat admin baru mulai mengklik sel untuk mengedit isi kolom rincian barang
        private void dgvDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Memeriksa jika admin mencoba mengisi kolom keterangan (Kolom Indeks 5)
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                var row = dgvDetail.Rows[e.RowIndex];
                string statusVal = row.Cells[4].Value?.ToString().ToLower().Trim() ?? "";

                // Aturan Ketat: Jika status barang tersebut BUKAN ditolak (alias disetujui/pending), batalkan mode edit secara paksa!
                if (statusVal != "ditolak")
                {
                    e.Cancel = true; // Tolak hak ketik admin pada kolom keterangan alasan
                }
            }
        }

        // Method dinamis untuk mengatur warna latar belakang sel alasan penolakan
        private void UpdateVisualKolomKeterangan()
        {
            foreach (DataGridViewRow row in dgvDetail.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string status = row.Cells[4].Value.ToString().ToLower().Trim();

                    // Jika status barang dipilih 'Ditolak' oleh admin
                    if (status == "ditolak")
                    {
                        row.Cells[5].ReadOnly = false; // Buka kunci ketik agar admin bisa mengetik alasan penolakan
                        row.Cells[5].Style.BackColor = System.Drawing.Color.White; // Ubah background menjadi putih terang (siap diisi)
                    }
                    else
                    {
                        // Jika status berupa disetujui atau pending, kunci kolom alasan agar bersih dari ketikan sampah
                        row.Cells[5].ReadOnly = true;
                        row.Cells[5].Style.BackColor = System.Drawing.Color.FromArgb(240, 240, 240); // Warnai abu-abu mati (tanda terkunci)
                        row.Cells[5].Value = ""; // Bersihkan teks di dalamnya menjadi kosong kembali
                    }
                }
            }
        }

        // Fitur pencarian real-time: Terpancing setiap kali ada perubahan karakter huruf di kotak cari peminjaman
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            string kataKunci = txtCari.Text.Trim();
            DataTable dtHasil;

            // Jika kotak pencarian kosong, muat kembali seluruh daftar transaksi dari database
            if (string.IsNullOrEmpty(kataKunci))
            {
                dtHasil = pinjamService.tampilSemuaPeminjaman();
            }
            else
            {
                // Jika berisi teks, panggil fungsi pencarian global multi-kolom dari class service
                dtHasil = pinjamService.cariPeminjaman(kataKunci);
            }

            // Tampilkan hasil penyaringan data ke dalam grid utama form
            FormatDanTampilkanData(dtHasil);
        }

        // Method UTAMA UTK PROSES VALIDASI VERIFIKASI: Mengecek kevalidan input admin dan menyimpan keputusan ke database
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // Validasi awal: Memastikan admin sudah memilih satu nota transaksi di grid utama sebelum menekan simpan
            if (string.IsNullOrEmpty(kodeNotaAktif))
            {
                MessageBox.Show("Silakan pilih transaksi peminjaman terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Memaksa grid detail untuk mengakhiri mode edit aktif agar seluruh ketikan terakhir admin terekam sempurna ke memori
            dgvDetail.EndEdit();
            int totalBaris = dgvDetail.Rows.Count;

            // TAHAP 1: VALIDASI KELAYAKAN DATA INPUTAN (Pencegahan Error)
            for (int i = 0; i < totalBaris; i++)
            {
                var row = dgvDetail.Rows[i];
                if (row.DataBoundItem != null)
                {
                    string statusCek = row.Cells[4].Value?.ToString().ToLower().Trim() ?? "pending";
                    string keteranganValue = row.Cells[5].Value?.ToString() ?? "";
                    string namaBarang = row.Cells[0].Value?.ToString() ?? "Barang";

                    // Validasi: Larang admin membiarkan status barang tetap menggantung di status 'Pending'
                    if (statusCek == "pending")
                    {
                        MessageBox.Show($"Anda belum memilih status untuk barang '{namaBarang}'! Silakan pilih Disetujui atau Ditolak.",
                                        "Status Belum Dipilih", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        dgvDetail.CurrentCell = row.Cells[4]; // Arahkan fokus kursor langsung ke kolom pilihan status yang melanggar
                        return; // Gagalkan penyimpanan total
                    }

                    // Validasi: Jika status ditolak, tapi admin malas menuliskan alasan/keterangan penolakan barang tersebut
                    if (statusCek == "ditolak" && string.IsNullOrWhiteSpace(keteranganValue))
                    {
                        MessageBox.Show($"Gagal menyimpan! Alasan/Keterangan penolakan untuk barang '{namaBarang}' wajib diisi.",
                                        "Keterangan Kosong", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        dgvDetail.CurrentCell = row.Cells[5]; // Arahkan fokus sel ke kolom teks keterangan yang kosong
                        dgvDetail.BeginEdit(true); // Buka mode edit otomatis agar admin langsung bisa mengetik alasan
                        return; // Gagalkan penyimpanan total
                    }
                }
            }

            // Variabel bendera untuk memantau apakah ada salah satu atau beberapa barang di dalam nota ini yang disetujui pinjam
            bool adaYangDisetujui = false;

            // TAHAP 2: EKSEKUSI PENYIMPANAN DATA PER ITEM BARANG KE DATABASE
            for (int i = 0; i < totalBaris; i++)
            {
                var row = dgvDetail.Rows[i];
                if (row.DataBoundItem != null)
                {
                    DataRowView drv = (DataRowView)row.DataBoundItem;
                    int idDetail = Convert.ToInt32(drv["id_detail_peminjaman"]);
                    int jumlah = Convert.ToInt32(row.Cells[2].Value); // Jumlah kuantitas yang diminta pinjam
                    int jumlahSedia = Convert.ToInt32(row.Cells[3].Value); // Sisa kuota stok riil barang bagus di gudang
                    string statusCek = row.Cells[4].Value?.ToString().ToLower().Trim() ?? "pending";
                    string keteranganValue = row.Cells[5].Value?.ToString() ?? "";
                    string namaBarang = row.Cells[0].Value?.ToString() ?? "Barang";

                    // FILTER KEAMANAN GUDANG: Jika disetujui, tapi jumlah barang yang diminta melebihi sisa stok yang ada di gudang
                    if (statusCek == "disetujui" && jumlah > jumlahSedia)
                    {
                        MessageBox.Show($"Gagal menyimpan! Stok untuk '{namaBarang}' tidak mencukupi.\nDiminta: {jumlah}, Tersedia: {jumlahSedia}.", "Stok Kurang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Batalkan penyimpanan total demi menjaga kestabilan neraca stok gudang barang
                    }

                    // Menembak update status persetujuan item barang individu ke tabel database detail_peminjaman
                    pinjamService.ubahStatusDetailItem(idDetail, jumlah, statusCek, keteranganValue);

                    // Jika ada item yang disetujui, tandai variabel bendera menjadi true
                    if (statusCek == "disetujui")
                    {
                        adaYangDisetujui = true;
                    }
                }
            }

            // TAHAP 3: REVISI STATUS NOTA INDUK PEMINJAMAN
            // Logika Bisnis: Jika ada minimal 1 jenis barang saja yang lolos disetujui, maka status nota induk dinilai "Disetujui"
            // Namun jika semua barang di dalam nota tersebut ditolak total oleh admin, maka status akhir nota menjadi "Ditolak"
            string statusNotaFinal = adaYangDisetujui ? "disetujui" : "ditolak";

            // Update status nota di tabel master peminjaman database
            if (pinjamService.ubahStatusNotaInduk(kodeNotaAktif, statusNotaFinal) > 0)
            {
                MessageBox.Show("Status verifikasi peminjaman berhasil disimpan!\nStatus Nota: " + statusNotaFinal.ToUpper(), "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SegarkanGridUtama();
                BersihkanPanelDetail();
            }
        }

        // Event handler bawaan sistem DataGridView untuk merapikan sinkronisasi memori saat transisi input dropdown dilakukan
        private void dgvDetail_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDetail.IsCurrentCellDirty)
            {
                // Melakukan komit perubahan nilai sesaat setelah pilihan dropdown diklik (meminimalkan delay data)
                dgvDetail.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void BersihkanPanelDetail()
        {
            kodeNotaAktif = "";
            txtNamaLengkap.Clear();
            txtTglPinjam.Clear();
            txtTglKembali.Clear();
            txtAlamat.Clear();
            txtNoHp.Clear();
            dgvDetail.DataSource = null;
            btnSimpan.Enabled = true;
        }

        private void btnBatalDetail_Click(object sender, EventArgs e)
        {
            BersihkanPanelDetail();
        }

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            frm.Show();
            this.Close();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            SegarkanGridUtama();
            BersihkanPanelDetail();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan frm = new FormDataPengambilan();
            frm.Show();
            this.Close();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian frm = new FormDataPengembalian();
            frm.Show();
            this.Close();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin frm = new FormRegisterAdmin();
            frm.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin halamanLogin = new FormLogin();
                halamanLogin.Show();
                this.Close();
            }
        }
    }
}