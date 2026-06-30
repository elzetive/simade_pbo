using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormRiwayatWarga : Form
    {
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        public FormRiwayatWarga()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.FormRiwayatWarga_Load);
            this.btnMenuDashboard.Click += new System.EventHandler(this.btnMenuDashboard_Click);
            this.btnMenuStatus.Click += new System.EventHandler(this.btnMenuStatus_Click);
            this.btnMenuRiwayat.Click += new System.EventHandler(this.btnMenuRiwayat_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.dgvRiwayat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRiwayat_CellClick);
        }

        private void FormRiwayatWarga_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NamaUserLogin))
            {
                lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";
            }

            dgvRiwayat.ReadOnly = true;
            dgvDetailBarang.ReadOnly = true;
            dgvDetailBarang.AllowUserToAddRows = false;

            // Pemetaan Data Transaksi Bundel Nota (Kiri)
            dgvRiwayat.AutoGenerateColumns = false;
            colNo.DataPropertyName = "no_urut";
            colHistId.DataPropertyName = "kode_peminjaman";
            colHistTglPinjam.DataPropertyName = "tgl_pinjam";
            colHistTglKembali.DataPropertyName = "tgl_kembali";

            // Pemetaan Data Rincian Multi-Barang (Kanan)
            dgvDetailBarang.AutoGenerateColumns = false;
            colDetailNama.DataPropertyName = "nama_aset";
            colDetailMerk.DataPropertyName = "merk";
            colDetailIdentitas.DataPropertyName = "no_identitas";
            colDetailJumlah.DataPropertyName = "jumlah";

            tampilDataSelesai();
        }

        private void tampilDataSelesai()
        {
            if (string.IsNullOrEmpty(IdUserLogin)) return;

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    // REVISI UTAMA: Mengubah MAX(tgl_kembali) menjadi MAX(updated_at)
                    // Agar mencatat tanggal dan jam riil saat Admin melakukan konfirmasi pengembalian berkas
                    string query = @"SELECT kode_peminjaman, 
                                            DATE_FORMAT(MIN(tgl_pinjam), '%d/%m/%Y') AS tgl_pinjam, 
                                            DATE_FORMAT(MAX(updated_at), '%d/%m/%Y') AS tgl_kembali
                                     FROM peminjaman
                                     WHERE id_user = @idUser AND status_peminjaman = 'dikembalikan'
                                     GROUP BY kode_peminjaman
                                     ORDER BY MAX(updated_at) DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idUser", Convert.ToInt32(IdUserLogin));
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dt.Columns.Add("no_urut", typeof(int));
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["no_urut"] = i + 1;
                            }

                            dgvRiwayat.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat arsip transaksi: " + ex.Message, "Error Database");
                }
            }
        }

        private void dgvRiwayat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvRiwayat.Rows[e.RowIndex];

                    if (row.Cells["colHistId"].Value != null)
                    {
                        string kodeNotaTerpilih = row.Cells["colHistId"].Value.ToString();

                        using (MySqlConnection conn = Koneksi.GetConn())
                        {
                            conn.Open();
                            // Query membongkar rincian aset di dalam nota terpilih beserta kolom nomor_identitas fisik
                            string queryDetail = @"SELECT b.nama_barang AS nama_aset, b.merk, b.nomor_identitas AS no_identitas, 1 AS jumlah
                                                   FROM peminjaman p
                                                   INNER JOIN barang b ON p.id_barang = b.id_barang
                                                   WHERE p.kode_peminjaman = @kodeNota";

                            using (MySqlCommand cmd = new MySqlCommand(queryDetail, conn))
                            {
                                cmd.Parameters.AddWithValue("@kodeNota", kodeNotaTerpilih);
                                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                                {
                                    DataTable dtDetail = new DataTable();
                                    da.Fill(dtDetail);
                                    dgvDetailBarang.DataSource = dtDetail;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal membongkar item riwayat: " + ex.Message, "Rincian Error");
                }
            }
        }

        private void btnMenuDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardWarga frm = new FormDashboardWarga();
            frm.IdUserLogin = this.IdUserLogin;
            frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        private void btnMenuStatus_Click(object sender, EventArgs e)
        {
            FormStatusPengajuan frm = new FormStatusPengajuan();
            frm.IdUserLogin = this.IdUserLogin;
            frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        private void btnMenuRiwayat_Click(object sender, EventArgs e) { tampilDataSelesai(); }
        private void btnLogOut_Click(object sender, EventArgs e) { FormLogin l = new FormLogin(); l.Show(); this.Close(); }
        private void btnExit_Click(object sender, EventArgs e) { Application.Exit(); }
    }
}