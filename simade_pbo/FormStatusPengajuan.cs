using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormStatusPengajuan : Form
    {
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        private int idUserWargaFix = 4;

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
            this.StartPosition = FormStartPosition.CenterScreen;

            if (!string.IsNullOrEmpty(IdUserLogin))
            {
                int.TryParse(IdUserLogin, out idUserWargaFix);
            }

            if (!string.IsNullOrEmpty(NamaUserLogin))
            {
                lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";
            }

            dgvPengajuan.ReadOnly = true;
            dgvDetailBarang.ReadOnly = true;

            dgvPengajuan.AutoGenerateColumns = false;
            colNo.DataPropertyName = "no_urut";
            colIdPinjam.DataPropertyName = "kode_peminjaman";
            colTglPinjam.DataPropertyName = "tgl_pinjam";
            colStatusUtama.DataPropertyName = "status";

            dgvDetailBarang.AutoGenerateColumns = false;
            colDetailNama.DataPropertyName = "nama_aset";
            colDetailJumlah.DataPropertyName = "jumlah";

            // PERBAIKAN: Kosongkan tulisan status bawaan desainer saat halaman pertama kali dibuka
            lblStatusBadge.Text = "";
            lblStatusBadge.BackColor = Color.Transparent;

            LoadPermohonanBerjalan();
        }

        private void LoadPermohonanBerjalan()
        {
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT kode_peminjaman, 
                                            DATE_FORMAT(tgl_pinjam, '%d/%m/%Y') AS tgl_pinjam, 
                                            status_peminjaman AS status
                                     FROM peminjaman
                                     WHERE id_user = @idUser AND status_peminjaman IN ('pending', 'disetujui', 'dipinjam', 'ditolak')
                                     ORDER BY id_peminjaman DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idUser", idUserWargaFix);
                        using (MySqlConnection da = new MySqlConnection()) // fallback check
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

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
                    MessageBox.Show("Gagal memuat antrean permohonan berjalan: " + ex.Message, "Error Database");
                }
            }
        }

        private void dgvPengajuan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataTable dtHeader = (DataTable)dgvPengajuan.DataSource;
                    string kodeNotaTerpilih = dtHeader.Rows[e.RowIndex]["kode_peminjaman"].ToString();
                    string statusTeks = dtHeader.Rows[e.RowIndex]["status"].ToString().ToUpper();

                    // Tulisan status dan warna latar belakang baru akan muncul secara dinamis mengikuti baris data yang diklik warga
                    lblStatusBadge.Text = statusTeks;

                    if (statusTeks == "PENDING") lblStatusBadge.BackColor = Color.Orange;
                    else if (statusTeks == "DISETUJUI" || statusTeks == "DIPINJAM") lblStatusBadge.BackColor = Color.MediumSeaGreen;
                    else if (statusTeks == "DITOLAK") lblStatusBadge.BackColor = Color.Crimson;

                    using (MySqlConnection conn = Koneksi.GetConn())
                    {
                        conn.Open();
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
                    MessageBox.Show("Gagal membongkar rincian item nota berjalan: " + ex.Message, "Rincian Error");
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
            // Reset status badge ketika menu disegarkan kembali
            lblStatusBadge.Text = "";
            lblStatusBadge.BackColor = Color.Transparent;
            dgvDetailBarang.DataSource = null;

            LoadPermohonanBerjalan();
        }

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
    }
}