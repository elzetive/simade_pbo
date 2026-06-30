using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDashboardWarga : Form
    {
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        private int idSubTerpilih = 0;
        private string namaBarangTerpilih = "";
        private int stokTersediaMaksimal = 0;
        private DataTable dtKeranjangPeminjaman;

        public FormDashboardWarga()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.FormDashboardWarga_Load);
            this.btnMenuDashboard.Click += new System.EventHandler(this.btnMenuDashboard_Click);
            this.btnMenuStatus.Click += new System.EventHandler(this.btnMenuStatus_Click);
            this.btnMenuRiwayat.Click += new System.EventHandler(this.btnMenuRiwayat_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnPinjam.Click += new System.EventHandler(this.btnPinjam_Click);
            this.btnAjukanFinal.Click += new System.EventHandler(this.btnAjukanFinal_Click);

            this.dgvBarang.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvBarang_RowPostPaint);
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
        }

        private void FormDashboardWarga_Load(object sender, EventArgs e)
        {
            tampilDataWarga();
        }

        public void tampilDataWarga()
        {
            try
            {
                txtNamaBarang.ReadOnly = true;
                txtJumlah.ReadOnly = true;
                dtpPinjam.Enabled = false;
                dtpPinjam.Value = DateTime.Now;

                if (!string.IsNullOrEmpty(NamaUserLogin))
                {
                    lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";
                }

                dgvBarang.AutoGenerateColumns = false;
                colId.DataPropertyName = "";
                colNamaAset.DataPropertyName = "nama_barang";
                colKategori.DataPropertyName = "nama_sub";
                colStokTersedia.DataPropertyName = "jumlah_prediksi_tersedia";

                dtKeranjangPeminjaman = new DataTable();
                dtKeranjangPeminjaman.Columns.Add("id_sub", typeof(int));
                dtKeranjangPeminjaman.Columns.Add("nama_barang", typeof(string));
                dtKeranjangPeminjaman.Columns.Add("jumlah_pinjam", typeof(int));

                dgvKeranjang.AutoGenerateColumns = false;
                colCartNama.DataPropertyName = "nama_barang";
                colCartQty.DataPropertyName = "jumlah_pinjam";
                dgvKeranjang.DataSource = dtKeranjangPeminjaman;

                LoadDataBarang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal inisialisasi: " + ex.Message, "Error");
            }
        }

        private void LoadDataBarang()
        {
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                                        sk.id_sub,
                                        sk.nama_sub,
                                        b.nama_barang,
                                        COUNT(CASE WHEN b.status = 'tersedia' THEN 1 END) AS jumlah_prediksi_tersedia
                                    FROM barang b
                                    INNER JOIN sub_kategori sk ON b.id_sub = sk.id_sub
                                    GROUP BY sk.id_sub, b.nama_barang, sk.nama_sub";

                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBarang.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data master: " + ex.Message, "Database Error");
                }
            }
        }

        private void dgvBarang_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try { dgvBarang.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString(); } catch { }
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvBarang.Rows[e.RowIndex];
                    DataRowView drv = row.DataBoundItem as DataRowView;

                    if (drv != null)
                    {
                        idSubTerpilih = Convert.ToInt32(drv["id_sub"]);
                        namaBarangTerpilih = drv["nama_barang"].ToString();
                        stokTersediaMaksimal = Convert.ToInt32(drv["jumlah_prediksi_tersedia"]);

                        txtNamaBarang.Text = namaBarangTerpilih;
                        txtJumlah.Text = stokTersediaMaksimal > 0 ? "1" : "0";
                    }
                }
                catch (Exception) { }
            }
        }

        private void btnPinjam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamaBarang.Text) || idSubTerpilih == 0)
            {
                MessageBox.Show("Silakan pilih barang terlebih dahulu!", "Peringatan");
                return;
            }

            int qty = Convert.ToInt32(txtJumlah.Text);
            if (qty <= 0 || qty > stokTersediaMaksimal)
            {
                MessageBox.Show("Stok unit tidak mencukupi!", "Informasi Stok");
                return;
            }

            foreach (DataRow row in dtKeranjangPeminjaman.Rows)
            {
                if (Convert.ToInt32(row["id_sub"]) == idSubTerpilih && row["nama_barang"].ToString() == namaBarangTerpilih)
                {
                    MessageBox.Show("Item ini sudah masuk ke list antrean!", "Peringatan");
                    return;
                }
            }

            dtKeranjangPeminjaman.Rows.Add(idSubTerpilih, namaBarangTerpilih, qty);
            txtNamaBarang.Clear();
            txtJumlah.Clear();
            idSubTerpilih = 0;
            namaBarangTerpilih = "";
            stokTersediaMaksimal = 0;
        }

        private void btnAjukanFinal_Click(object sender, EventArgs e)
        {
            if (dtKeranjangPeminjaman.Rows.Count == 0)
            {
                MessageBox.Show("List antrean masih kosong!", "Peringatan");
                return;
            }

            DialogResult konfirmasi = MessageBox.Show("Kirim seluruh pengajuan berkas?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi != DialogResult.Yes) return;

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                MySqlTransaction tr = null;
                try
                {
                    conn.Open();
                    tr = conn.BeginTransaction();

                    // LOGIKA OPSI B: Membuat satu Nomor Resi Unik berbasis kombinasi waktu untuk seluruh isi keranjang
                    string generatorNota = "TRX-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    foreach (DataRow cartRow in dtKeranjangPeminjaman.Rows)
                    {
                        int idSub = Convert.ToInt32(cartRow["id_sub"]);
                        string namaBarang = cartRow["nama_barang"].ToString();
                        int qty = Convert.ToInt32(cartRow["jumlah_pinjam"]);

                        string searchBase = "SELECT id_barang FROM barang WHERE id_sub = @idSub AND nama_barang = @namaBarang AND status = 'tersedia' LIMIT @qty";
                        DataTable dtFisik = new DataTable();

                        using (MySqlCommand cmdCari = new MySqlCommand(searchBase, conn, tr))
                        {
                            cmdCari.Parameters.AddWithValue("@idSub", idSub);
                            cmdCari.Parameters.AddWithValue("@namaBarang", namaBarang);
                            cmdCari.Parameters.AddWithValue("@qty", qty);
                            using (MySqlDataAdapter da = new MySqlDataAdapter(cmdCari)) { da.Fill(dtFisik); }
                        }

                        foreach (DataRow fisikRow in dtFisik.Rows)
                        {
                            int idBarangFisik = Convert.ToInt32(fisikRow["id_barang"]);

                            // Simpan kode_peminjaman ke dalam database
                            string insQuery = "INSERT INTO peminjaman (kode_peminjaman, id_user, id_barang, tgl_pinjam, status_peminjaman) VALUES (@kode, @uid, @bid, CURDATE(), 'pending')";
                            using (MySqlCommand cmdIns = new MySqlCommand(insQuery, conn, tr))
                            {
                                int fixUser = string.IsNullOrEmpty(IdUserLogin) ? 3 : Convert.ToInt32(IdUserLogin);
                                cmdIns.Parameters.AddWithValue("@kode", generatorNota);
                                cmdIns.Parameters.AddWithValue("@uid", fixUser);
                                cmdIns.Parameters.AddWithValue("@bid", idBarangFisik);
                                cmdIns.ExecuteNonQuery();
                            }

                            string updQuery = "UPDATE barang SET status = 'dipinjam' WHERE id_barang = @bid";
                            using (MySqlCommand cmdUpd = new MySqlCommand(updQuery, conn, tr))
                            {
                                cmdUpd.Parameters.AddWithValue("@bid", idBarangFisik);
                                cmdUpd.ExecuteNonQuery();
                            }
                        }
                    }

                    tr.Commit();
                    MessageBox.Show("Pengajuan sukses dibundel dinamis dengan Nomor Nota: " + generatorNota, "Sukses");
                    dtKeranjangPeminjaman.Clear();
                    LoadDataBarang();
                }
                catch (Exception ex)
                {
                    if (tr != null) tr.Rollback();
                    MessageBox.Show("Gagal memproses transaksi: " + ex.Message, "Error");
                }
            }
        }

        private void btnMenuDashboard_Click(object sender, EventArgs e) { LoadDataBarang(); }

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
            FormRiwayatWarga frm = new FormRiwayatWarga();
            frm.IdUserLogin = this.IdUserLogin;
            frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e) { FormLogin login = new FormLogin(); login.Show(); this.Close(); }
        private void btnExit_Click(object sender, EventArgs e) { Application.Exit(); }
    }
}