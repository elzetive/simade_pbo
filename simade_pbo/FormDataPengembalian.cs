using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPengembalian : Form
    {
        string kodeNotaAktif = "";

        public FormDataPengembalian()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new System.EventHandler(this.FormDataPengembalian_Load);
            this.dgvTabelList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);

            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            txtNama_Lengkap.ReadOnly = true;
            txtNo_Hp.ReadOnly = true;
            txtTgl_Pinjam.ReadOnly = true;
            txtTgl_Kembali.ReadOnly = true;
        }

        private void FormDataPengembalian_Load(object sender, EventArgs e)
        {
            SegarkanGridUtama();
        }

        private void SegarkanGridUtama()
        {
            dgvTabelList.AutoGenerateColumns = false;
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    string query = @"SELECT p.kode_peminjaman, u.nama_lengkap, u.no_hp,
                                            p.tgl_pinjam, p.tgl_kembali, p.status_peminjaman 
                                     FROM peminjaman p
                                     INNER JOIN user u ON p.id_user = u.id_user
                                     WHERE LOWER(p.status_peminjaman) IN ('disetujui', 'dipinjam', 'dikembalikan')
                                     ORDER BY p.id_peminjaman DESC";

                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        FormatDanTampilkanDataAtas(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data pengembalian: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDanTampilkanDataAtas(DataTable dt)
        {
            dgvTabelList.DataSource = null;
            dgvTabelList.AutoGenerateColumns = false;

            if (dgvTabelList.Columns.Count >= 5)
            {
                dgvTabelList.Columns[0].DataPropertyName = "kode_peminjaman";
                dgvTabelList.Columns[1].DataPropertyName = "nama_lengkap";
                dgvTabelList.Columns[2].DataPropertyName = "tgl_pinjam";
                dgvTabelList.Columns[3].DataPropertyName = "tgl_kembali";
                dgvTabelList.Columns[4].DataPropertyName = "status_peminjaman";
            }
            dgvTabelList.DataSource = dt;
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            string kataKunci = txtCari.Text.Trim();
            if (string.IsNullOrEmpty(kataKunci))
            {
                SegarkanGridUtama();
                return;
            }

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    string queryCari = @"SELECT p.kode_peminjaman, u.nama_lengkap, u.no_hp,
                                                p.tgl_pinjam, p.tgl_kembali, p.status_peminjaman 
                                         FROM peminjaman p
                                         INNER JOIN user u ON p.id_user = u.id_user
                                         WHERE LOWER(p.status_peminjaman) IN ('disetujui', 'dipinjam', 'dikembalikan') 
                                         AND (p.kode_peminjaman LIKE @key OR u.nama_lengkap LIKE @key)";

                    using (MySqlCommand cmd = new MySqlCommand(queryCari, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", "%" + kataKunci + "%");
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            FormatDanTampilkanDataAtas(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTabelList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow baris = this.dgvTabelList.Rows[e.RowIndex];
                    DataRowView rowView = baris.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow dr = rowView.Row;
                        kodeNotaAktif = dr["kode_peminjaman"].ToString();
                        string statusTransaksi = dr["status_peminjaman"].ToString().ToLower();

                        txtNama_Lengkap.Text = dr["nama_lengkap"].ToString();
                        txtTgl_Pinjam.Text = dr["tgl_pinjam"].ToString();
                        txtTgl_Kembali.Text = dr["tgl_kembali"].ToString();
                        txtNo_Hp.Text = dr["no_hp"].ToString();

                        txtDikembalikanOleh.Clear();
                        MuatDetailBarangBawah(kodeNotaAktif);

                        if (statusTransaksi == "dikembalikan")
                        {
                            btnSimpan.Enabled = false;
                            txtDikembalikanOleh.ReadOnly = true;
                        }
                        else
                        {
                            btnSimpan.Enabled = true;
                            txtDikembalikanOleh.ReadOnly = false;
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void MuatDetailBarangBawah(string kodePeminjaman)
        {
            dgvDetail.DataSource = null;
            dgvDetail.AutoGenerateColumns = false;

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    string queryDetail = @"SELECT b.nama_barang, k.nama_kategori, dp.deskripsi_barang, dp.jumlah_pinjam,
                                                  dp.jumlah_kembali, dp.kondisi_bagus, dp.kondisi_rusak, dp.denda, '' AS dikembalikan_oleh
                                           FROM detail_peminjaman dp
                                           INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                                           INNER JOIN barang b ON dp.id_barang = b.id_barang
                                           LEFT JOIN kategori k ON b.id_kategori = k.id_kategori
                                           WHERE p.kode_peminjaman = @kode";

                    using (MySqlCommand cmd = new MySqlCommand(queryDetail, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode", kodePeminjaman);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dtDetail = new DataTable();
                            da.Fill(dtDetail);

                            bool dataSudahKembali = false;

                            if (dgvTabelList.CurrentRow != null)
                            {
                                string statusNotaUtama = dgvTabelList.CurrentRow.Cells[4].Value?.ToString().ToLower() ?? "";
                                if (statusNotaUtama == "dikembalikan")
                                {
                                    dataSudahKembali = true;
                                    txtDikembalikanOleh.Text = "Warga/Admin";
                                }
                            }

                            foreach (DataRow row in dtDetail.Rows)
                            {
                                if (row["jumlah_kembali"] == DBNull.Value || Convert.ToInt32(row["jumlah_kembali"]) == 0)
                                {
                                    int jmlPinjam = Convert.ToInt32(row["jumlah_pinjam"]);
                                    row["jumlah_kembali"] = jmlPinjam;
                                    row["kondisi_bagus"] = jmlPinjam;
                                    row["kondisi_rusak"] = 0;
                                    row["denda"] = 0;
                                }
                            }

                            if (dgvDetail.Columns.Count >= 8)
                            {
                                dgvDetail.Columns[0].DataPropertyName = "nama_barang";
                                dgvDetail.Columns[1].DataPropertyName = "nama_kategori";
                                dgvDetail.Columns[2].DataPropertyName = "deskripsi_barang";
                                dgvDetail.Columns[3].DataPropertyName = "jumlah_pinjam";
                                dgvDetail.Columns[4].DataPropertyName = "jumlah_kembali";
                                dgvDetail.Columns[5].DataPropertyName = "kondisi_bagus";
                                dgvDetail.Columns[6].DataPropertyName = "kondisi_rusak";
                                dgvDetail.Columns[7].DataPropertyName = "denda";
                            }

                            dgvDetail.DataSource = dtDetail;

                            dgvDetail.ReadOnly = false;
                            dgvDetail.Columns[0].ReadOnly = true;
                            dgvDetail.Columns[1].ReadOnly = true;
                            dgvDetail.Columns[2].ReadOnly = true;
                            dgvDetail.Columns[3].ReadOnly = true;

                            dgvDetail.Columns[4].ReadOnly = dataSudahKembali;
                            dgvDetail.Columns[5].ReadOnly = dataSudahKembali;
                            dgvDetail.Columns[6].ReadOnly = dataSudahKembali;
                            dgvDetail.Columns[7].ReadOnly = dataSudahKembali;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat rincian barang: " + ex.Message, "Error Rincian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kodeNotaAktif))
            {
                MessageBox.Show("Silakan pilih berkas transaksi terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDikembalikanOleh.Text.Trim()))
            {
                MessageBox.Show("Kolom 'Dikembalikan Oleh' wajib diisi!", "Peringatan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDikembalikanOleh.Focus();
                return;
            }

            dgvDetail.EndEdit();

            DialogResult res = MessageBox.Show($"Simpan pengembalian untuk Nota {kodeNotaAktif}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                MySqlConnection conn = Koneksi.GetConn();
                MySqlTransaction transaksi = null;

                try
                {
                    conn.Open();
                    transaksi = conn.BeginTransaction();

                    int idPeminjamanAsli = 0;
                    string queryCariID = "SELECT id_peminjaman FROM peminjaman WHERE kode_peminjaman = @kode";
                    using (MySqlCommand cmdCari = new MySqlCommand(queryCariID, conn, transaksi))
                    {
                        cmdCari.Parameters.AddWithValue("@kode", kodeNotaAktif);
                        object objId = cmdCari.ExecuteScalar();
                        if (objId != null) idPeminjamanAsli = Convert.ToInt32(objId);
                    }

                    if (idPeminjamanAsli == 0) throw new Exception("ID Peminjaman tidak ditemukan.");

                    // 1. UPDATE INDUK (Tabel peminjaman) -> diubah ke 'dikembalikan' agar rumus hitungan barang dipinjam berkurang
                    string queryUpdateInduk = "UPDATE peminjaman SET status_peminjaman = 'dikembalikan', tgl_kembali = NOW() WHERE id_peminjaman = @id";
                    using (MySqlCommand cmdInduk = new MySqlCommand(queryUpdateInduk, conn, transaksi))
                    {
                        cmdInduk.Parameters.AddWithValue("@id", idPeminjamanAsli);
                        cmdInduk.ExecuteNonQuery();
                    }

                    // 2. QUERY UPDATE DETAIL (Mencatat log fisik barang kembali)
                    string queryUpdateDetail = @"UPDATE detail_peminjaman 
                                                 SET jumlah_kembali = @jmlKembali, 
                                                     kondisi_bagus = @bagus, 
                                                     kondisi_rusak = @rusak, 
                                                     denda = @denda
                                                 WHERE id_peminjaman = @id 
                                                 AND id_barang = (SELECT id_barang FROM barang WHERE nama_barang = @namaBarang LIMIT 1)";

                    // 3. QUERY UPDATE BARANG (MUTASI DINAMIS KETAT: Mengurangi stok bagus hanya sebesar barang yang rusak)
                    string queryUpdateStokBarang = @"UPDATE barang 
                                                     SET kondisi_bagus = kondisi_bagus - @rusak,
                                                         kondisi_rusak = kondisi_rusak + @rusak
                                                     WHERE nama_barang = @namaBarang";

                    foreach (DataGridViewRow baris in dgvDetail.Rows)
                    {
                        if (baris.Cells[0].Value != null)
                        {
                            string namaBarang = baris.Cells[0].Value.ToString();
                            int jmlKembali = baris.Cells[4].Value != null ? Convert.ToInt32(baris.Cells[4].Value) : 0;
                            int kondBagus = baris.Cells[5].Value != null ? Convert.ToInt32(baris.Cells[5].Value) : 0;
                            int kondRusak = baris.Cells[6].Value != null ? Convert.ToInt32(baris.Cells[6].Value) : 0;
                            int nominalDenda = baris.Cells[7].Value != null ? Convert.ToInt32(baris.Cells[7].Value) : 0;

                            // Eksekusi Update Tabel Detail Peminjaman
                            using (MySqlCommand cmdDetail = new MySqlCommand(queryUpdateDetail, conn, transaksi))
                            {
                                cmdDetail.Parameters.AddWithValue("@jmlKembali", jmlKembali);
                                cmdDetail.Parameters.AddWithValue("@bagus", kondBagus);
                                cmdDetail.Parameters.AddWithValue("@rusak", kondRusak);
                                cmdDetail.Parameters.AddWithValue("@denda", nominalDenda);
                                cmdDetail.Parameters.AddWithValue("@id", idPeminjamanAsli);
                                cmdDetail.Parameters.AddWithValue("@namaBarang", namaBarang);

                                cmdDetail.ExecuteNonQuery();
                            }

                            // Eksekusi Update ke Tabel Barang riil
                            using (MySqlCommand cmdBarang = new MySqlCommand(queryUpdateStokBarang, conn, transaksi))
                            {
                                cmdBarang.Parameters.AddWithValue("@rusak", kondRusak);
                                cmdBarang.Parameters.AddWithValue("@namaBarang", namaBarang);

                                cmdBarang.ExecuteNonQuery();
                            }
                        }
                    }

                    transaksi.Commit();
                    MessageBox.Show("Sukses! Status nota telah diubah menjadi DIKEMBALIKAN. Logika dinamis hitungan stok Data Barang otomatis sinkron.", "Informasi Gudang", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BersihkanSeluruhForm();
                    SegarkanGridUtama();
                }
                catch (Exception ex)
                {
                    if (transaksi != null) transaksi.Rollback();
                    MessageBox.Show("Gagal memproses pengembalian: " + ex.Message, "Error Database Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            BersihkanSeluruhForm();
        }

        private void BersihkanSeluruhForm()
        {
            kodeNotaAktif = "";
            txtNama_Lengkap.Clear();
            txtNo_Hp.Clear();
            txtTgl_Pinjam.Clear();
            txtTgl_Kembali.Clear();
            txtDikembalikanOleh.Clear();
            txtDikembalikanOleh.ReadOnly = false;
            if (txtCari != null) txtCari.Clear();
            dgvDetail.DataSource = null;
            btnSimpan.Enabled = false;
        }

        private void dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

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
            FormDataPengambilan frm = new FormDataPengambilan();
            frm.Show();
            this.Close();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            SegarkanGridUtama();
            BersihkanSeluruhForm();
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

        private void dgvTabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvTabelList_CellClick(sender, e);
        }
    }
}