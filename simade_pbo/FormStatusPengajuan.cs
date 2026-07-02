using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormStatusPengajuan : Form
    {
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        public FormStatusPengajuan()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormStatusPengajuan_Load);
            this.btnMenuDashboard.Click += new System.EventHandler(this.btnMenuDashboard_Click);
            this.btnMenuStatus.Click += new System.EventHandler(this.btnMenuStatus_Click);
            this.btnMenuRiwayat.Click += new System.EventHandler(this.btnMenuRiwayat_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.dgvPengajuan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPengajuan_CellClick);
        }

        private void FormStatusPengajuan_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NamaUserLogin))
                lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";

            dgvPengajuan.ReadOnly = true;
            dgvDetailBarang.ReadOnly = true;
            dgvDetailBarang.AllowUserToAddRows = false;

            dgvPengajuan.AutoGenerateColumns = false;
            colNo.DataPropertyName = "no_urut";
            colIdPinjam.DataPropertyName = "kode_peminjaman";
            colTglPinjam.DataPropertyName = "tgl_pinjam";
            colStatusUtama.DataPropertyName = "status";

            dgvDetailBarang.AutoGenerateColumns = false;
            colDetailNama.DataPropertyName = "nama_aset";
            colDetailMerk.DataPropertyName = "merk";
            colDetailIdentitas.DataPropertyName = "no_identitas";
            colDetailJumlah.DataPropertyName = "jumlah";

            LoadPermohonanBerjalan();
        }

        private void LoadPermohonanBerjalan()
        {
            if (string.IsNullOrEmpty(IdUserLogin)) return;

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    // REVISI: Menggunakan IN ('pending', 'dipinjam') agar status yang berubah tetap tampil di sini
                    string query = @"SELECT kode_peminjaman, tgl_pinjam, status_peminjaman AS status
                                     FROM peminjaman
                                     WHERE id_user = @idUser AND status_peminjaman IN ('pending', 'dipinjam')
                                     GROUP BY kode_peminjaman, tgl_pinjam, status_peminjaman
                                     ORDER BY tgl_pinjam DESC";

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

                            dgvPengajuan.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat nota berjalan: " + ex.Message, "Error");
                }
            }
        }

        private void dgvPengajuan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvPengajuan.Rows[e.RowIndex];

                    if (row.Cells["colIdPinjam"].Value != null)
                    {
                        string kodeNotaTerpilih = row.Cells["colIdPinjam"].Value.ToString();

                        if (row.Cells["colStatusUtama"].Value != null)
                        {
                            lblStatusBadge.Text = row.Cells["colStatusUtama"].Value.ToString().ToUpper();
                        }

                        using (MySqlConnection conn = Koneksi.GetConn())
                        {
                            conn.Open();

                            // Menggunakan b.nomor_identitas sesuai fakta fisik tabel barang di phpMyAdmin
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
                    MessageBox.Show("Gagal membongkar isi nota: " + ex.Message, "Rincian Error");
                }
            }
        }

        private void btnMenuDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardWarga frm = new FormDashboardWarga();
            //frm.IdUserLogin = this.IdUserLogin;
            //frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        private void btnMenuStatus_Click(object sender, EventArgs e) { LoadPermohonanBerjalan(); }

        private void btnMenuRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayatWarga frm = new FormRiwayatWarga();
            frm.IdUserLogin = this.IdUserLogin;
            frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormLogin l = new FormLogin();
            l.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void btnMenuDashboard_Click_1(object sender, EventArgs e)
        {

        }
    }
}