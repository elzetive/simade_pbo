using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using simade_pbo.Service;

namespace simade_pbo
{
    public partial class FormDataPengambilan : Form
    {
        Pinjam_service pinjamService = new Pinjam_service();
        string kodeNotaAktif = "";

        // Menetapkan variabel tanggal hari ini sesuai dengan waktu sistem (5 Juli 2026)
        DateTime hariIni = new DateTime(2026, 7, 5);

        public FormDataPengambilan()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new System.EventHandler(this.FormDataPengambilan_Load);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);

            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            txtNamaPeminjam.ReadOnly = true;
            txtNoTelepon.ReadOnly = true;
            txtTglPinjam.ReadOnly = true;
            txtTglKembali.ReadOnly = true;
        }

        private void FormDataPengambilan_Load(object sender, EventArgs e)
        {
            SegarkanGridUtama();
        }

        private void SegarkanGridUtama()
        {
            dataGridView1.AutoGenerateColumns = false;
            DataTable dtMentah = pinjamService.tampilSemuaPeminjaman();

            // Membuat clone struktur DataTable untuk menampung data filter akhir
            DataTable dtFiltered = dtMentah.Clone();

            if (!dtFiltered.Columns.Contains("status_tampilan"))
            {
                dtFiltered.Columns.Add("status_tampilan", typeof(string));
            }

            int hitungAntrean = 0;
            int hitungHangus = 0;
            int hitungHariIni = 0;
            int hitungDiambil = 0;

            foreach (DataRow row in dtMentah.Rows)
            {
                string statusReal = row["status_peminjaman"].ToString().ToLower();
                DateTime tglPinjam = Convert.ToDateTime(row["tgl_pinjam"]);

                if (statusReal == "disetujui")
                {
                    // 1. Logika HANGUS: Jika sudah lewat dari hari ini (5 Juli 2026)
                    if (tglPinjam.Date < hariIni.Date)
                    {
                        hitungHangus++;

                        DataRow newRow = dtFiltered.NewRow();
                        newRow.ItemArray = row.ItemArray;
                        newRow["status_tampilan"] = "Hangus"; // Tampilan status berubah jadi Hangus
                        dtFiltered.Rows.Add(newRow);
                    }
                    // 2. Logika ANTREAN ASLI (Belum Hangus)
                    else
                    {
                        hitungAntrean++; // Angka Antrean bertambah hanya jika belum hangus

                        // Logika HARI INI: Jika pas hari ini
                        if (tglPinjam.Date == hariIni.Date)
                        {
                            hitungHariIni++;
                        }

                        DataRow newRow = dtFiltered.NewRow();
                        newRow.ItemArray = row.ItemArray;
                        newRow["status_tampilan"] = row["status_peminjaman"]; // Tetap "disetujui"
                        dtFiltered.Rows.Add(newRow);
                    }
                }
                // 3. Logika DIAMBIL (status database 'dipinjam')
                else if (statusReal == "dipinjam")
                {
                    hitungDiambil++;
                }
            }

            // Update Angka KPI ke masing-masing Label di interface C#
            if (lblAntrean != null) lblAntrean.Text = hitungAntrean.ToString();
            if (lblHariIni != null) lblHariIni.Text = hitungHariIni.ToString();
            if (lblDiambil != null) lblDiambil.Text = hitungDiambil.ToString();
            if (lblHangus != null) lblHangus.Text = hitungHangus.ToString();

            FormatDanTampilkanData(dtFiltered);
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            // Fitur pencarian otomatis disesuaikan dengan refresh grid utama
            SegarkanGridUtama();
        }

        private void FormatDanTampilkanData(DataTable dt)
        {
            dataGridView1.AutoGenerateColumns = false;

            if (dataGridView1.Columns.Count >= 5)
            {
                dataGridView1.Columns[0].DataPropertyName = "kode_peminjaman";
                dataGridView1.Columns[1].DataPropertyName = "nama_lengkap";
                dataGridView1.Columns[2].DataPropertyName = "tgl_pinjam";
                dataGridView1.Columns[3].DataPropertyName = "tgl_kembali";

                // Gunakan status_tampilan agar transaksi hangus terlihat jelas oleh Admin
                dataGridView1.Columns[4].DataPropertyName = dt.Columns.Contains("status_tampilan") ? "status_tampilan" : "status_peminjaman";
            }

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow baris = this.dataGridView1.Rows[e.RowIndex];
                    DataRowView rowView = baris.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow dr = rowView.Row;

                        // VALIDASI HANGUS: Cegah pemrosesan jika barang sudah hangus
                        string statusCek = baris.Cells[4].Value?.ToString() ?? "";
                        if (statusCek.ToLower() == "hangus")
                        {
                            MessageBox.Show("Transaksi ini sudah HANGUS. Barang tidak dapat diambil kembali!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            btnSimpan.Enabled = false;
                            return;
                        }

                        kodeNotaAktif = dr["kode_peminjaman"].ToString();
                        txtNamaPeminjam.Text = dr["nama_lengkap"].ToString();
                        txtTglPinjam.Text = dr["tgl_pinjam"].ToString();
                        txtTglKembali.Text = dr["tgl_kembali"] != DBNull.Value ? dr["tgl_kembali"].ToString() : "-";

                        DataTable dtDetail = pinjamService.tampilDetailBarang(kodeNotaAktif);

                        if (dtDetail.Rows.Count > 0)
                        {
                            txtNoTelepon.Text = dtDetail.Rows[0]["nomor_telepon"].ToString();
                        }
                        else
                        {
                            txtNoTelepon.Text = "-";
                        }

                        dgvDetail.DataSource = null;
                        dgvDetail.AutoGenerateColumns = false;

                        if (dgvDetail.Columns.Count >= 4)
                        {
                            dgvDetail.Columns[0].DataPropertyName = "nama_barang";
                            dgvDetail.Columns[1].DataPropertyName = "nama_kategori";
                            dgvDetail.Columns[2].DataPropertyName = "jumlah_pinjam";
                            dgvDetail.Columns[3].DataPropertyName = "";
                        }

                        dgvDetail.DataSource = dtDetail;

                        dgvDetail.ReadOnly = false;
                        dgvDetail.Columns[0].ReadOnly = true;
                        dgvDetail.Columns[1].ReadOnly = true;
                        dgvDetail.Columns[2].ReadOnly = true;
                        dgvDetail.Columns[3].ReadOnly = false;

                        btnSimpan.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat rincian item: " + ex.Message, "Error Rincian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Silakan pilih antrean transaksi terlebih dahulu pada tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvDetail.EndEdit();

            DialogResult konfirmasi = MessageBox.Show($"Konfirmasi barang untuk Nota {kodeNotaAktif} resmi diambil?", "Siklus Inventaris", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                int totalBaris = dgvDetail.Rows.Count;
                bool suksesUpdateDeskripsi = true;

                try
                {
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

                                string queryUpdate = "UPDATE detail_peminjaman SET deskripsi_barang = @deskripsi WHERE id_detail_peminjaman = @id";
                                using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                                {
                                    cmd.Parameters.AddWithValue("@deskripsi", deskripsiInput);
                                    cmd.Parameters.AddWithValue("@id", idDetail);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    suksesUpdateDeskripsi = false; // SINKRONISASI: Mengganti nama variabel typo sebelumnya
                    MessageBox.Show("Gagal menyimpan deskripsi barang: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (suksesUpdateDeskripsi)
                {
                    if (pinjamService.ubahStatusNotaInduk(kodeNotaAktif, "dipinjam") > 0)
                    {
                        MessageBox.Show("Sukses! Status diperbarui menjadi 'DIPINJAM' dan deskripsi pengambilan berhasil disimpan ke database.", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SegarkanGridUtama();
                        BersihkanPanelDetail();
                    }
                    else
                    {
                        MessageBox.Show("Gagal memperbarui status nota peminjaman.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            BersihkanPanelDetail();
        }

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

        // --- REVISI NAVIGASI SIDEBAR: Mengubah .Hide() ke .Close() agar Form Dashboard reload segar ---
        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            frm.Show();
            this.Close();
        }

        private void btnData_Pinjam_Click(object sender, EventArgs e)
        {
            FormDataPinjam frm = new FormDataPinjam();
            frm.Show();
            this.Close();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            SegarkanGridUtama();
            BersihkanPanelDetail();
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
            if (MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormLogin halamanLogin = new FormLogin();
                halamanLogin.Show();
                this.Close();
            }
        }
    }
}