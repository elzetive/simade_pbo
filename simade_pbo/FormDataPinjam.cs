using System;
using System.Data;
using System.Windows.Forms;
using simade_pbo.Service;

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

            this.Load += new System.EventHandler(this.FormDataPinjam_Load);
            this.dgvUtama.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUtama_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);

            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            txtNamaLengkap.ReadOnly = true;
            txtAlamat.ReadOnly = true;
            txtNoHp.ReadOnly = true;
            txtTglPinjam.ReadOnly = true;
            txtTglKembali.ReadOnly = true;
        }

        private void FormDataPinjam_Load(object sender, EventArgs e)
        {
            SegarkanGridUtama();
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
            DataTable dtHasil;

            if (string.IsNullOrEmpty(kataKunci))
            {
                dtHasil = pinjamService.tampilSemuaPeminjaman();
            }
            else
            {
                dtHasil = pinjamService.cariPeminjaman(kataKunci);
            }

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
            int totalTransaksi = dt.Rows.Count;
            int totalPending = 0;
            int totalDisetujui = 0;
            int totalDitolak = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["status_peminjaman"] != DBNull.Value)
                {
                    string st = row["status_peminjaman"].ToString().ToLower().Trim();
                    if (st == "pending") totalPending++;
                    else if (st == "disetujui") totalDisetujui++;
                    else if (st == "ditolak") totalDitolak++;
                }
            }

            if (lblTotalPengajuan != null) lblTotalPengajuan.Text = totalTransaksi.ToString();
            if (lblTotalDisetujui != null) lblTotalDisetujui.Text = totalDisetujui.ToString();
            if (lblTotalDitolak != null) lblTotalDitolak.Text = totalDitolak.ToString();
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
                DataGridViewRow baris = this.dgvUtama.Rows[rowIndex];
                DataRowView rowView = (baris.DataBoundItem != null) ? (DataRowView)baris.DataBoundItem : null;

                if (rowView != null)
                {
                    DataRow dr = rowView.Row;

                    kodeNotaAktif = dr["kode_peminjaman"].ToString();
                    txtNamaLengkap.Text = dr["nama_lengkap"].ToString();
                    txtTglPinjam.Text = dr["tgl_pinjam"].ToString();

                    if (dr["tgl_kembali"] != DBNull.Value)
                    {
                        txtTglKembali.Text = dr["tgl_kembali"].ToString();
                    }
                    else
                    {
                        txtTglKembali.Text = "-";
                    }

                    string statusNotaUtama = dr["status_peminjaman"].ToString().ToLower().Trim();

                    DataTable dtDetail = pinjamService.tampilDetailBarang(kodeNotaAktif);

                    if (dtDetail.Rows.Count > 0)
                    {
                        txtAlamat.Text = dtDetail.Rows[0]["alamat"].ToString();
                        txtNoHp.Text = dtDetail.Rows[0]["nomor_telepon"].ToString();

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
                        txtAlamat.Clear();
                        txtNoHp.Clear();
                    }

                    dgvDetail.DataSource = null;
                    dgvDetail.AutoGenerateColumns = false;

                    dgvDetail.Columns[0].DataPropertyName = "nama_barang";
                    dgvDetail.Columns[1].DataPropertyName = "nama_kategori";
                    dgvDetail.Columns[2].DataPropertyName = "jumlah_pinjam";
                    dgvDetail.Columns[3].DataPropertyName = "jumlah_tersedia";
                    dgvDetail.Columns[4].DataPropertyName = "status";
                    dgvDetail.Columns[5].DataPropertyName = "keterangan";

                    dgvDetail.DataSource = dtDetail;

                    if (statusNotaUtama == "disetujui" || statusNotaUtama == "ditolak")
                    {
                        dgvDetail.ReadOnly = true;
                        btnSimpan.Enabled = false;
                    }
                    else
                    {
                        dgvDetail.ReadOnly = false;
                        dgvDetail.Columns[0].ReadOnly = true;
                        dgvDetail.Columns[1].ReadOnly = true;
                        dgvDetail.Columns[3].ReadOnly = true;

                        dgvDetail.Columns[2].ReadOnly = false;
                        dgvDetail.Columns[4].ReadOnly = false;
                        dgvDetail.Columns[5].ReadOnly = false;

                        btnSimpan.Enabled = true;
                    }
                }
            }
        }

        private void dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
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
                    string status = row.Cells[4].Value?.ToString() ?? "pending";
                    string keterangan = row.Cells[5].Value?.ToString() ?? "";

                    string statusCek = status.ToLower().Trim();

                    if (statusCek == "disetujui" && jumlah > jumlahSedia)
                    {
                        string namaBarang = row.Cells[0].Value?.ToString() ?? "Barang";
                        MessageBox.Show($"Gagal menyimpan! Stok untuk '{namaBarang}' tidak mencukupi.\nDiminta: {jumlah}, Tersedia: {jumlahSedia}.", "Stok Kurang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    pinjamService.ubahStatusDetailItem(idDetail, jumlah, statusCek, keterangan);

                    if (statusCek == "disetujui")
                    {
                        adaYangDisetujui = true;
                    }
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
            this.Hide();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam frm = new FormDataPinjam();
            frm.Show();
            this.Hide();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan frm = new FormDataPengambilan();
            frm.Show();
            this.Hide();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian frm = new FormDataPengembalian();
            frm.Show();
            this.Hide();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin frm = new FormRegisterAdmin();
            frm.Show();
            this.Hide();
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