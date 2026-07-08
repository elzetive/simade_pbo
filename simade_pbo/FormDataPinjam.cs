using System;
using System.Data;
using System.Windows.Forms;
using simade_pbo.Service;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPinjam : Form
    {
        Pinjam_service pinjamService = new Pinjam_service();
        string kodeNotaAktif = "";

        public FormDataPinjam()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Mendaftarkan event utama secara aman agar siklus UI lancar
            this.Load += new System.EventHandler(this.FormDataPinjam_Load);
            this.dgvUtama.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUtama_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);

            // Mendaftarkan event untuk mendeteksi perubahan ComboBox dan mengunci sel keterangan
            this.dgvDetail.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellValueChanged);
            this.dgvDetail.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDetail_CurrentCellDirtyStateChanged);

            // KUNCI UTAMA: Mencegah pengetikan di kolom keterangan sebelum proses edit dimulai
            this.dgvDetail.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvDetail_CellBeginEdit);

            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            // Kunci Solusi Ringan Navigasi Sidebar Left
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click);
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // Operasi Form Kontrol bawah
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            this.btnBatalDetail.Click += new System.EventHandler(this.btnBatalDetail_Click);

            txtNamaLengkap.ReadOnly = true;
            txtAlamat.ReadOnly = true;
            txtNoHp.ReadOnly = true;
            txtTglPinjam.ReadOnly = true;
            txtTglKembali.ReadOnly = true;
        }

        private void FormDataPinjam_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(SegarkanGridUtama));
        }

        private void SegarkanGridUtama()
        {
            dgvUtama.AutoGenerateColumns = false;
            DataTable dtUtama = pinjamService.tampilSemuaPeminjaman();
            FormatDanTampilkanData(dtUtama);
            HitungKPIPeminjaman(dtUtama);
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            string kataKunci = txtCari.Text.Trim();
            DataTable dtHasil = string.IsNullOrEmpty(kataKunci) ? pinjamService.tampilSemuaPeminjaman() : pinjamService.cariPeminjaman(kataKunci);
            FormatDanTampilkanData(dtHasil);
        }

        private void FormatDanTampilkanData(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["status_peminjaman"] != DBNull.Value)
                {
                    string statusLower = row["status_peminjaman"].ToString().ToLower().Trim();
                    if (statusLower == "pending") row["status_peminjaman"] = "Pending";
                    else if (statusLower == "disetujui") row["status_peminjaman"] = "Disetujui";
                    else if (statusLower == "ditolak") row["status_peminjaman"] = "Ditolak";
                    else if (statusLower == "dipinjam") row["status_peminjaman"] = "Dipinjam";
                    else if (statusLower == "dikembalikan") row["status_peminjaman"] = "Dikembalikan";
                }
            }

            dgvUtama.DataSource = dt;
            dgvUtama.Columns[0].DataPropertyName = "kode_peminjaman";
            dgvUtama.Columns[1].DataPropertyName = "nama_lengkap";
            dgvUtama.Columns[2].DataPropertyName = "tgl_pinjam";
            dgvUtama.Columns[3].DataPropertyName = "tgl_kembali";
            dgvUtama.Columns[4].DataPropertyName = "status_peminjaman";
        }

        private void HitungKPIPeminjaman(DataTable dt)
        {
            int totalPending = 0; int totalDisetujui = 0; int totalDitolak = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["status_peminjaman"] != DBNull.Value)
                {
                    string st = row["status_peminjaman"].ToString().ToLower().Trim();
                    if (st == "pending") totalPending++;
                    else if (st == "disetujui" || st == "dipinjam" || st == "dikembalikan") totalDisetujui++;
                    else if (st == "ditolak") totalDitolak++;
                }
            }

            if (lblTotalDisetujui != null) lblTotalDisetujui.Text = totalDisetujui.ToString();
            if (lblTotalDitolak != null) lblTotalDitolak.Text = totalDitolak.ToString();
            if (lblTotalPengajuan != null) lblTotalPengajuan.Text = dt.Rows.Count.ToString();
        }

        private void dgvUtama_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikSelTabel(e.RowIndex);
        }

        private void dgvUtama_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikSelTabel(e.RowIndex);
        }

        private void EksekusiKlikSelTabel(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                try
                {
                    DataGridViewRow baris = this.dgvUtama.Rows[rowIndex];
                    DataRowView rowView = (baris.DataBoundItem != null) ? (DataRowView)baris.DataBoundItem : null;

                    if (rowView != null)
                    {
                        DataRow dr = rowView.Row;
                        kodeNotaAktif = dr["kode_peminjaman"].ToString();
                        txtNamaLengkap.Text = dr["nama_lengkap"].ToString();

                        if (dr["tgl_pinjam"] != DBNull.Value)
                        {
                            DateTime tglP = Convert.ToDateTime(dr["tgl_pinjam"]);
                            txtTglPinjam.Text = tglP.ToString("dd/MM/yyyy");
                        }
                        else txtTglPinjam.Clear();

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

                        string statusNotaUtama = dr["status_peminjaman"].ToString().ToLower().Trim();
                        DataTable dtDetail = pinjamService.tampilDetailBarang(kodeNotaAktif);

                        if (dtDetail.Rows.Count > 0)
                        {
                            txtAlamat.Text = dtDetail.Rows[0]["alamat"].ToString();
                            if (dtDetail.Columns.Contains("nomor_telepon")) txtNoHp.Text = dtDetail.Rows[0]["nomor_telepon"].ToString();
                            else if (dtDetail.Columns.Contains("no_hp")) txtNoHp.Text = dtDetail.Rows[0]["no_hp"].ToString();

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

                        dgvDetail.DataSource = null;
                        dgvDetail.AutoGenerateColumns = false;

                        dgvDetail.Columns[0].DataPropertyName = "nama_barang";
                        dgvDetail.Columns[1].DataPropertyName = "nama_kategori";
                        dgvDetail.Columns[2].DataPropertyName = "jumlah_pinjam";
                        dgvDetail.Columns[3].DataPropertyName = "jumlah_tersedia_realtime";
                        dgvDetail.Columns[4].DataPropertyName = "status";
                        dgvDetail.Columns[5].DataPropertyName = "keterangan";

                        dgvDetail.DataSource = dtDetail;

                        if (statusNotaUtama == "disetujui" || statusNotaUtama == "ditolak" || statusNotaUtama == "dipinjam" || statusNotaUtama == "dikembalikan")
                        {
                            dgvDetail.ReadOnly = true;
                            btnSimpan.Enabled = false;
                        }
                        else
                        {
                            dgvDetail.ReadOnly = false;

                            // Set dasar ReadOnly kolom bawaan
                            dgvDetail.Columns[0].ReadOnly = true;  // Nama Barang LOCK
                            dgvDetail.Columns[1].ReadOnly = true;  // Kategori LOCK (Sesuai database awal)
                            dgvDetail.Columns[3].ReadOnly = true;  // Stok LOCK

                            dgvDetail.Columns[2].ReadOnly = false; // Jumlah pinjam bisa diedit
                            dgvDetail.Columns[4].ReadOnly = false; // Status ComboBox bisa diganti
                            dgvDetail.Columns[5].ReadOnly = true;  // Keterangan LOCK awal (Nanti dibuka dinamis hanya jika Ditolak)

                            // Jalankan penyesuaian visual dan sistem untuk kolom keterangan
                            UpdateVisualKolomKeterangan();

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

        private void dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void dgvDetail_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDetail.IsCurrentCellDirty)
            {
                dgvDetail.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                UpdateVisualKolomKeterangan();
            }
        }

        // KUNCI UTAMA: Memblokir pengetikan di kolom Keterangan (Indeks 5) jika statusnya bukan "Ditolak"
        private void dgvDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Jika yang ingin diedit adalah Kolom Keterangan (Indeks 5)
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                var row = dgvDetail.Rows[e.RowIndex];
                string statusVal = row.Cells[4].Value?.ToString().ToLower().Trim() ?? "";

                // JIKA STATUS BUKAN DITOLAK, BATALKAN EDIT SECARA TOTAL (DISABLE TOTAL KETIK)
                if (statusVal != "ditolak")
                {
                    e.Cancel = true;
                }
            }
        }

        // Fungsi pembantu untuk merubah status dan warna kolom keterangan (Indeks 5) secara dinamis
        private void UpdateVisualKolomKeterangan()
        {
            foreach (DataGridViewRow row in dgvDetail.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string status = row.Cells[4].Value.ToString().ToLower().Trim();

                    if (status == "ditolak")
                    {
                        row.Cells[5].ReadOnly = false; // Enable secara sistem
                        row.Cells[5].Style.BackColor = System.Drawing.Color.White; // Putih (Bisa diketik alasan penolakannya)
                    }
                    else
                    {
                        row.Cells[5].ReadOnly = true;  // Disable secara sistem
                        row.Cells[5].Style.BackColor = System.Drawing.Color.FromArgb(240, 240, 240); // Abu-abu tanda mati

                        // Opsional: hapus teks keterangan jika status diubah kembali ke pending/disetujui
                        // row.Cells[5].Value = ""; 
                    }
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kodeNotaAktif))
            {
                MessageBox.Show("Silakan pilih transaksi peminjaman terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvDetail.EndEdit();
            bool adaYangDisetujui = false;
            int totalBaris = dgvDetail.Rows.Count;

            for (int i = 0; i < totalBaris; i++)
            {
                var row = dgvDetail.Rows[i];
                if (row.DataBoundItem != null)
                {
                    DataRowView drv = (DataRowView)row.DataBoundItem;
                    int idDetail = Convert.ToInt32(drv["id_detail_peminjaman"]);

                    int jumlah = Convert.ToInt32(row.Cells[2].Value);
                    int jumlahSedia = Convert.ToInt32(row.Cells[3].Value);
                    string statusValue = row.Cells[4].Value?.ToString() ?? "pending";
                    string keteranganValue = row.Cells[5].Value?.ToString() ?? "";

                    string statusCek = statusValue.ToLower().Trim();

                    if (statusCek == "disetujui" && jumlah > jumlahSedia)
                    {
                        string namaBarang = row.Cells[0].Value?.ToString() ?? "Barang";
                        MessageBox.Show($"Gagal menyimpan! Stok untuk '{namaBarang}' tidak mencukupi.\nDiminta: {jumlah}, Tersedia: {jumlahSedia}.", "Stok Kurang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    pinjamService.ubahStatusDetailItem(idDetail, jumlah, statusCek, keteranganValue);

                    if (statusCek == "disetujui") adaYangDisetujui = true;
                }
            }

            string statusNotaFinal = adaYangDisetujui ? "disetujui" : "ditolak";

            if (pinjamService.ubahStatusNotaInduk(kodeNotaAktif, statusNotaFinal) > 0)
            {
                MessageBox.Show("Status verifikasi peminjaman berhasil disimpan!\nStatus Nota: " + statusNotaFinal.ToUpper() + "\n", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SegarkanGridUtama();
                BersihkanPanelDetail();
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