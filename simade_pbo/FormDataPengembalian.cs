using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDataPengembalian : Form
    {
        private string kodeNotaAktif = "";

        public FormDataPengembalian()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Mengikat event handler utama secara aman
            this.Load += new System.EventHandler(this.FormDataPengembalian_Load);
            this.dgvTabelList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellClick);
            this.dgvDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetail_DataError);

            if (this.txtCari != null)
            {
                this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            }

            // Bind Event Klik Tombol Sidebar Left Admin agar Sinkron & Tidak Freeze
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click);
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // Operasi Form Kontrol Bawah
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            if (this.btnBatal != null)
            {
                this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            }

            // Konfigurasi awal sifat kontrol input
            txtNama_Lengkap.ReadOnly = true;
            txtNo_Hp.ReadOnly = true;
            txtTgl_Pinjam.ReadOnly = true;

            // Mengunci DateTimePicker sebelum ada nota transaksi yang dipilih
            dtpTgl_Kembali.Enabled = false;
        }

        private void FormDataPengembalian_Load(object sender, EventArgs e)
        {
            // Menggunakan BeginInvoke untuk memisahkan UI Thread dengan query database KPI/SQL
            this.BeginInvoke(new MethodInvoker(SegarkanGridUtama));
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

                HitungKPIPengembalian();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data pengembalian: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HitungKPIPengembalian()
        {
            int totalPeminjamanAktif = 0;
            int totalJatuhTempoHariIni = 0;
            int totalKembaliSukses = 0;

            string queryKPI = @"SELECT status_peminjaman, 
                                       CASE WHEN DATE(tgl_kembali) <= CURDATE() THEN 1 ELSE 0 END as is_jatuh_tempo 
                                FROM peminjaman 
                                WHERE LOWER(status_peminjaman) IN ('disetujui', 'dipinjam', 'dikembalikan')";

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryKPI, conn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string status = dr["status_peminjaman"].ToString().ToLower().Trim();
                                int isJatuhTempo = Convert.ToInt32(dr["is_jatuh_tempo"]);

                                if (status == "dipinjam" || status == "disetujui")
                                {
                                    totalPeminjamanAktif++;

                                    if (isJatuhTempo == 1)
                                    {
                                        totalJatuhTempoHariIni++;
                                    }
                                }
                                else if (status == "dikembalikan")
                                {
                                    totalKembaliSukses++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("KPI Error: " + ex.Message);
            }

            var ctrlPeminjaman = this.Controls.Find("lblJumlahPeminjaman", true);
            if (ctrlPeminjaman.Length > 0) ctrlPeminjaman[0].Text = totalPeminjamanAktif.ToString();

            var ctrlJatuhTempo = this.Controls.Find("lblJumlahJatuhTempo", true);
            if (ctrlJatuhTempo.Length > 0) ctrlJatuhTempo[0].Text = totalJatuhTempoHariIni.ToString();

            var ctrlKembali = this.Controls.Find("lblJumlahKembali", true);
            if (ctrlKembali.Length > 0) ctrlKembali[0].Text = totalKembaliSukses.ToString();
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
                        string statusTransaksi = dr["status_peminjaman"].ToString().ToLower().Trim();

                        txtNama_Lengkap.Text = dr["nama_lengkap"].ToString();
                        txtNo_Hp.Text = dr["no_hp"].ToString();

                        if (dr["tgl_pinjam"] != DBNull.Value)
                        {
                            DateTime tPinjam = Convert.ToDateTime(dr["tgl_pinjam"]);
                            txtTgl_Pinjam.Text = tPinjam.ToString("dd/MM/yyyy");
                        }
                        else txtTgl_Pinjam.Clear();

                        // INTEGRASI: Selalu utamakan nilai tgl_kembali bawaan database terlebih dahulu
                        if (dr["tgl_kembali"] != DBNull.Value)
                        {
                            dtpTgl_Kembali.Value = Convert.ToDateTime(dr["tgl_kembali"]);
                        }
                        else
                        {
                            // Jika data tgl_kembali di database bernilai NULL, baru jadikan waktu sekarang sebagai default
                            dtpTgl_Kembali.Value = DateTime.Now;
                        }

                        txtDikembalikanOleh.Clear();
                        MuatDetailBarangBawah(kodeNotaAktif);

                        // Atur Hak Akses Input Berdasarkan Status Transaksi
                        if (statusTransaksi == "dikembalikan")
                        {
                            btnSimpan.Enabled = false;
                            txtDikembalikanOleh.ReadOnly = true;
                            dtpTgl_Kembali.Enabled = false; // Kunci jika sudah kembali
                        }
                        else
                        {
                            btnSimpan.Enabled = true;
                            txtDikembalikanOleh.ReadOnly = false;
                            txtDikembalikanOleh.Text = txtNama_Lengkap.Text;

                            dtpTgl_Kembali.Enabled = true;  // Buka kunci agar dapat diedit/disesuaikan admin
                            // Baris yang menimpa tanggal secara paksa menjadi DateTime.Now di sini sudah dihapus
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error CellClick: " + ex.Message);
                }
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
                    string queryDetail = @"SELECT b.id_barang, b.nama_barang, k.nama_kategori, dp.deskripsi_barang, dp.jumlah_pinjam,
                                                  dp.jumlah_kembali, dp.kondisi_bagus, dp.kondisi_rusak, dp.denda, dp.dikembalikan_oleh
                                           FROM detail_peminjaman dp
                                           INNER JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman
                                           INNER JOIN barang b ON dp.id_barang = b.id_barang
                                           LEFT JOIN kategori k ON b.id_kategori = k.id_kategori
                                           WHERE p.kode_peminjaman = @kode
                                             AND dp.status = 'disetujui'";

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
                                    if (dtDetail.Rows.Count > 0 && dtDetail.Rows[0]["dikembalikan_oleh"] != DBNull.Value)
                                    {
                                        txtDikembalikanOleh.Text = dtDetail.Rows[0]["dikembalikan_oleh"].ToString();
                                    }
                                    else
                                    {
                                        txtDikembalikanOleh.Text = "Warga/Admin";
                                    }
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

            DialogResult res = MessageBox.Show($"Simpan pengembalian untuk Nota {kodeNotaAktif}?", "Konfirmasi Logistik", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
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

                        // 1. Update Tabel Induk Peminjaman menggunakan parameter .Value dari dtpTgl_Kembali
                        string queryUpdateInduk = "UPDATE peminjaman SET status_peminjaman = 'dikembalikan', tgl_kembali = @tglKembali WHERE id_peminjaman = @id";
                        using (MySqlCommand cmdInduk = new MySqlCommand(queryUpdateInduk, conn, transaksi))
                        {
                            cmdInduk.Parameters.AddWithValue("@tglKembali", dtpTgl_Kembali.Value);
                            cmdInduk.Parameters.AddWithValue("@id", idPeminjamanAsli);
                            cmdInduk.ExecuteNonQuery();
                        }

                        // 2. Query Update Detail Peminjaman
                        string queryUpdateDetail = @"UPDATE detail_peminjaman 
                                                     SET jumlah_kembali = @jmlKembali, 
                                                         kondisi_bagus = @bagus, 
                                                         kondisi_rusak = @rusak, 
                                                         denda = @denda,
                                                         dikembalikan_oleh = @oleh
                                                     WHERE id_peminjaman = @id 
                                                     AND id_barang = @idBarang";

                        // 3. Query Update Master Barang
                        string queryUpdateStokBarang = @"UPDATE barang 
                                                         SET kondisi_rusak = kondisi_rusak + @rusak,
                                                             kondisi_bagus = kondisi_bagus - @rusak
                                                         WHERE id_barang = @idBarang";

                        foreach (DataGridViewRow baris in dgvDetail.Rows)
                        {
                            if (baris.Cells[0].Value != null && baris.DataBoundItem != null)
                            {
                                DataRowView drv = (DataRowView)baris.DataBoundItem;
                                int idBarangItem = Convert.ToInt32(drv["id_barang"]);

                                int jmlKembali = 0; int.TryParse(Convert.ToString(baris.Cells[4].Value), out jmlKembali);
                                int kondBagus = 0; int.TryParse(Convert.ToString(baris.Cells[5].Value), out kondBagus);
                                int kondRusak = 0; int.TryParse(Convert.ToString(baris.Cells[6].Value), out kondRusak);
                                int nominalDenda = 0; int.TryParse(Convert.ToString(baris.Cells[7].Value), out nominalDenda);

                                string penerimaSektor = txtDikembalikanOleh.Text.Trim();

                                // Validasi perhitungan matematika gridview rincian
                                if ((kondBagus + kondRusak) != jmlKembali)
                                {
                                    throw new Exception($"Jumlah kondisi Bagus ({kondBagus}) + Rusak ({kondRusak}) harus sama dengan total Jumlah Kembali ({jmlKembali})!");
                                }

                                // Eksekusi ke detail peminjaman
                                using (MySqlCommand cmdDetail = new MySqlCommand(queryUpdateDetail, conn, transaksi))
                                {
                                    cmdDetail.Parameters.AddWithValue("@jmlKembali", jmlKembali);
                                    cmdDetail.Parameters.AddWithValue("@bagus", kondBagus);
                                    cmdDetail.Parameters.AddWithValue("@rusak", kondRusak);
                                    cmdDetail.Parameters.AddWithValue("@denda", nominalDenda);
                                    cmdDetail.Parameters.AddWithValue("@oleh", penerimaSektor);
                                    cmdDetail.Parameters.AddWithValue("@id", idPeminjamanAsli);
                                    cmdDetail.Parameters.AddWithValue("@idBarang", idBarangItem);

                                    cmdDetail.ExecuteNonQuery();
                                }

                                // Eksekusi penyesuaian kondisi fisik aset ke tabel master barang
                                using (MySqlCommand cmdBarang = new MySqlCommand(queryUpdateStokBarang, conn, transaksi))
                                {
                                    cmdBarang.Parameters.AddWithValue("@rusak", kondRusak);
                                    cmdBarang.Parameters.AddWithValue("@idBarang", idBarangItem);

                                    cmdBarang.ExecuteNonQuery();
                                }
                            }
                        }

                        transaksi.Commit();
                        MessageBox.Show("Sukses! Status nota telah diperbarui menjadi DIKEMBALIKAN.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        BersihkanSeluruhForm();
                        SegarkanGridUtama();
                    }
                    catch (Exception ex)
                    {
                        if (transaksi != null) transaksi.Rollback();
                        MessageBox.Show("Gagal memproses transaksi pengembalian: " + ex.Message, "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open) conn.Close();
                    }
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

            // Reset state DateTimePicker ke default saat form dibersihkan
            dtpTgl_Kembali.Value = DateTime.Now;
            dtpTgl_Kembali.Enabled = false;

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

        // --- NAVIGATION SYSTEM ---
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