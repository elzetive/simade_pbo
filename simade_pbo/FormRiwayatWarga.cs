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

        // Variabel angka internal jika konversi properti string mengalami null
        private int idUserWargaFix = 4;

        public FormRiwayatWarga()
        {
            InitializeComponent();

            // Mengaitkan Event Runtime secara terpadu
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
            this.StartPosition = FormStartPosition.CenterScreen;

            if (!string.IsNullOrEmpty(IdUserLogin))
            {
                int.TryParse(IdUserLogin, out idUserWargaFix);
            }

            if (!string.IsNullOrEmpty(NamaUserLogin))
            {
                lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";
            }

            dgvRiwayat.ReadOnly = true;
            dgvDetailBarang.ReadOnly = true;

            // Pemetaan Data Transaksi Bundel Nota (Tabel Kiri)
            dgvRiwayat.AutoGenerateColumns = false;
            colNo.DataPropertyName = "no_urut";
            colHistId.DataPropertyName = "kode_peminjaman";
            colHistTglPinjam.DataPropertyName = "tgl_pinjam";
            colHistTglKembali.DataPropertyName = "tgl_kembali";

            // Pemetaan Data Rincian Multi-Barang (Tabel Kanan)
            // PERBAIKAN: Menghapus referensi mapping colDetailMerk dan colDetailIdentitas yang sudah tidak ada di file desainer baru
            dgvDetailBarang.AutoGenerateColumns = false;
            colDetailNama.DataPropertyName = "nama_aset";
            colDetailJumlah.DataPropertyName = "jumlah";

            tampilDataSelesai();
        }

        private void tampilDataSelesai()
        {
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    // SINKRONISASI DATABASE BARU: Menarik nota induk warga yang berstatus 'dikembalikan'
                    string query = @"SELECT kode_peminjaman, 
                                            DATE_FORMAT(tgl_pinjam, '%d/%m/%Y') AS tgl_pinjam, 
                                            DATE_FORMAT(updated_at, '%d/%m/%Y') AS tgl_kembali
                                     FROM peminjaman
                                     WHERE id_user = @idUser AND status_peminjaman = 'dikembalikan'
                                     ORDER BY updated_at DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idUser", idUserWargaFix);
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
                    MessageBox.Show("Gagal memuat arsip transaksi selesai: " + ex.Message, "Error Database");
                }
            }
        }

        private void dgvRiwayat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataTable dtHeader = (DataTable)dgvRiwayat.DataSource;
                    string kodeNotaTerpilih = dtHeader.Rows[e.RowIndex]["kode_peminjaman"].ToString();

                    using (MySqlConnection conn = Koneksi.GetConn())
                    {
                        conn.Open();
                        // SINKRONISASI SKEMA HEADER-DETAIL BARU: Membedah item barang via detail_peminjaman tanpa kolom fisik lama yang tidak ada
                        string queryDetail = @"SELECT b.nama_barang AS nama_aset, 
                                                      dp.jumlah_pinjam AS jumlah
                                               FROM peminjaman p
                                               INNER JOIN detail_peminjaman dp ON p.id_peminjaman = dp.id_peminjaman
                                               INNER JOIN barang b ON dp.id_barang = b.id_barang
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
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal membongkar rincian item riwayat: " + ex.Message, "Rincian Error");
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

        private void btnMenuRiwayat_Click(object sender, EventArgs e)
        {
            tampilDataSelesai();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}