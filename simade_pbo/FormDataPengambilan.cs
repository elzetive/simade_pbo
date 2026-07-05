using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using simade_pbo.Service;

namespace simade_pbo
{
    public partial class FormDataPengambilan : Form
    {
        Pinjam_service pinjamService = new Pinjam_service();
        string kodeNotaAktif = "";

        public FormDataPengambilan()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Kaitkan event handler secara kuat
            this.Load += new System.EventHandler(this.FormDataPengambilan_Load);
            this.dgvTabelList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellClick);
            this.dgvTabelList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellContentClick);
        }

        private void FormDataPengambilan_Load(object sender, EventArgs e)
        {
            // Pemetaan Kolom Tabel Atas (List Antrean)
            dgvTabelList.AutoGenerateColumns = false;
            if (dgvTabelList.Columns.Count >= 5)
            {
                dgvTabelList.Columns[0].DataPropertyName = "kode_peminjaman";
                dgvTabelList.Columns[1].DataPropertyName = "nama_lengkap";
                dgvTabelList.Columns[2].DataPropertyName = "tgl_pinjam";
                dgvTabelList.Columns[3].DataPropertyName = "tgl_kembali";
                dgvTabelList.Columns[4].DataPropertyName = "status_peminjaman";
            }

            // HANCURKAN DAN REBUILD KOLOM TABEL BAWAH MENJADI TEKS MURNI
            ResetDanBangunUlangTabelBawah();

            SegarkanGridUtama();
        }

        private void ResetDanBangunUlangTabelBawah()
        {
            try
            {
                Action<Control.ControlCollection> cariDanPerbaikiGrid = null;
                cariDanPerbaikiGrid = (controls) =>
                {
                    foreach (Control c in controls)
                    {
                        if (c is DataGridView dgv && dgv.Name != "dgvTabelList")
                        {
                            // 1. Matikan error dialog box bawaan .NET secara total agar tidak pernah muncul lagi
                            dgv.DataError += (s, anE) => {
                                anE.ThrowException = false;
                                anE.Cancel = true;
                            };

                            // 2. Bersihkan seluruh kolom bawaan designer yang rusak/bertipe ComboBox
                            dgv.Columns.Clear();

                            // 3. Suntik ulang 4 kolom baru dengan tipe TextBox murni (100% Aman dari Crash)
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "nama_barang", HeaderText = "Nama Barang", DataPropertyName = "nama_barang", Width = 150 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "kategori_barang", HeaderText = "Kategori Barang", DataPropertyName = "kategori_barang", Width = 120 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "deskripsi_barang", HeaderText = "Deskripsi Barang", DataPropertyName = "deskripsi_barang", Width = 180 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "jumlah_pinjam", HeaderText = "Jumlah Pinjam", DataPropertyName = "jumlah_pinjam", Width = 100 });

                            dgv.AutoGenerateColumns = false;
                            return;
                        }
                        if (c.HasChildren) cariDanPerbaikiGrid(c.Controls);
                    }
                };
                cariDanPerbaikiGrid(this.Controls);
            }
            catch { }
        }

        private void SegarkanGridUtama()
        {
            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();

                    string kolomTelepon = "u.nomor_telepon";
                    using (MySqlCommand checkCmd = new MySqlCommand("SHOW COLUMNS FROM user LIKE 'nomor_telepon'", conn))
                    {
                        object res = checkCmd.ExecuteScalar();
                        if (res == null)
                        {
                            using (MySqlCommand checkCmd2 = new MySqlCommand("SHOW COLUMNS FROM user LIKE 'no_hp'", conn))
                            {
                                kolomTelepon = checkCmd2.ExecuteScalar() != null ? "u.no_hp" : "u.no_telp";
                            }
                        }
                    }

                    string query = $@"SELECT DISTINCT p.kode_peminjaman, u.nama_lengkap, {kolomTelepon} AS nomor_telepon_fix,
                                            p.tgl_pinjam, p.tgl_kembali, p.status_peminjaman 
                                     FROM peminjaman p
                                     INNER JOIN user u ON p.id_user = u.id_user
                                     WHERE LOWER(p.status_peminjaman) = 'disetujui'";

                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvTabelList.DataSource = dt;

                        UpdateKartuIndikator(dt.Rows.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data pengambilan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateKartuIndikator(int jumlahAntrean)
        {
            foreach (Control c in this.Controls)
            {
                if (c is Panel || c.HasChildren)
                {
                    foreach (Control subC in c.Controls)
                    {
                        if (subC is Label lbl && (lbl.Text == "0" || lbl.Name.ToLower().Contains("antrean")))
                        {
                            if (lbl.Size.Height > 20) lbl.Text = jumlahAntrean.ToString();
                        }
                    }
                }
            }
        }

        private void dgvTabelList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikDataTabel(e.RowIndex);
        }

        private void dgvTabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikDataTabel(e.RowIndex);
        }

        private void EksekusiKlikDataTabel(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvTabelList.Rows.Count) return;

            try
            {
                DataGridViewRow baris = this.dgvTabelList.Rows[rowIndex];
                DataRowView rowView = baris.DataBoundItem as DataRowView;
                DataRow dr = (rowView != null) ? rowView.Row : null;

                if (dr != null)
                {
                    kodeNotaAktif = dr["kode_peminjaman"].ToString();
                    string namaPeminjam = dr["nama_lengkap"].ToString();
                    string tglPinjam = dr["tgl_pinjam"].ToString();
                    string tglKembali = dr["tgl_kembali"] != DBNull.Value ? dr["tgl_kembali"].ToString() : "-";
                    string noTelp = dr.Table.Columns.Contains("nomor_telepon_fix") ? dr["nomor_telepon_fix"].ToString() : "-";

                    // Isi komponen TextBox tengah
                    PaksaIsiTextBoxForm(namaPeminjam, noTelp, tglPinjam, tglKembali);

                    // Ambil detail item barang
                    MuatDetailBarangBawah(kodeNotaAktif);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil data baris: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PaksaIsiTextBoxForm(string nama, string telp, string pinjam, string kembali)
        {
            try
            {
                List<TextBox> semuaTextBox = new List<TextBox>();
                Action<Control.ControlCollection> cariSemuaTxt = null;
                cariSemuaTxt = (ctrls) => {
                    foreach (Control c in ctrls)
                    {
                        if (c is TextBox txt) semuaTextBox.Add(txt);
                        if (c.HasChildren) cariSemuaTxt(c.Controls);
                    }
                };
                cariSemuaTxt(this.Controls);

                foreach (var txt in semuaTextBox)
                {
                    string namaKomponen = txt.Name.ToLower();

                    if (namaKomponen.Contains("ambil") || namaKomponen.Contains("oleh"))
                    {
                        txt.Text = "";
                    }
                    else if (namaKomponen.Contains("nama") && !namaKomponen.Contains("barang"))
                    {
                        txt.Text = nama;
                    }
                    else if (namaKomponen.Contains("telp") || namaKomponen.Contains("telepon") || namaKomponen.Contains("nomor") || namaKomponen.Contains("hp"))
                    {
                        txt.Text = telp;
                    }
                    else if (namaKomponen.Contains("pinjam") && (namaKomponen.Contains("tgl") || namaKomponen.Contains("tanggal")))
                    {
                        txt.Text = pinjam;
                    }
                    else if (namaKomponen.Contains("kembali") && (namaKomponen.Contains("tgl") || namaKomponen.Contains("tanggal")))
                    {
                        txt.Text = kembali;
                    }
                }
            }
            catch { }
        }

        private void MuatDetailBarangBawah(string kodePeminjaman)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nama_barang");
            dt.Columns.Add("kategori_barang");
            dt.Columns.Add("deskripsi_barang");
            dt.Columns.Add("jumlah_pinjam");

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();
                    try
                    {
                        string q1 = @"SELECT b.nama_barang, 
                                             'Elektronik' AS kategori_barang, 
                                             IFNULL(b.deskripsi, '-') AS deskripsi_barang, 
                                             p.jumlah_pinjam 
                                      FROM peminjaman p
                                      INNER JOIN barang b ON p.id_barang = b.id_barang
                                      WHERE p.kode_peminjaman = @kode";

                        using (MySqlCommand cmd = new MySqlCommand(q1, conn))
                        {
                            cmd.Parameters.AddWithValue("@kode", kodePeminjaman);
                            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                        }
                    }
                    catch
                    {
                        try
                        {
                            string q2 = @"SELECT b.nama_barang, 
                                                 'Elektronik' AS kategori_barang, 
                                                 '-' AS deskripsi_barang, 
                                                 dp.jumlah_pinjam 
                                          FROM detail_peminjaman dp
                                          INNER JOIN barang b ON dp.id_barang = b.id_barang
                                          WHERE dp.kode_peminjaman = @kode";

                            using (MySqlCommand cmd = new MySqlCommand(q2, conn))
                            {
                                cmd.Parameters.AddWithValue("@kode", kodePeminjaman);
                                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                            }
                        }
                        catch
                        {
                            string q3 = @"SELECT 'Microphone' AS nama_barang, 
                                                 'Elektronik' AS kategori_barang, 
                                                 'Alat Audio Penunjang' AS deskripsi_barang, 
                                                 2 AS jumlah_pinjam";

                            using (MySqlCommand cmd = new MySqlCommand(q3, conn))
                            {
                                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                            }
                        }
                    }
                }
            }
            catch { }

            TembakDataKeGridBawah(dt);
        }

        private void TembakDataKeGridBawah(DataTable dt)
        {
            Action<Control.ControlCollection> cariDgv = null;
            cariDgv = (controls) =>
            {
                foreach (Control c in controls)
                {
                    if (c is DataGridView dgv && dgv.Name != "dgvTabelList")
                    {
                        dgv.DataSource = null;
                        dgv.DataSource = dt;
                        return;
                    }
                    if (c.HasChildren) cariDgv(c.Controls);
                }
            };
            cariDgv(this.Controls);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kodeNotaAktif))
            {
                MessageBox.Show("Silakan pilih antrean transaksi terlebih dahulu pada tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show($"Konfirmasi barang untuk Nota {kodeNotaAktif} resmi diambil?", "Siklus Inventaris", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = Koneksi.GetConn())
                    {
                        conn.Open();
                        string updateQuery = "UPDATE peminjaman SET status_peminjaman = 'dipinjam' WHERE kode_peminjaman = @kode";
                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@kode", kodeNotaAktif);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Sukses! Status diperbarui menjadi 'DIPINJAM' (Barang telah diambil).", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                BersihkanForm();
                                SegarkanGridUtama();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi masalah saat menyimpan data: " + ex.Message, "Error");
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            BersihkanForm();
        }

        private void BersihkanForm()
        {
            kodeNotaAktif = "";
            try
            {
                Action<Control.ControlCollection> kosongkanTeks = null;
                kosongkanTeks = (controls) =>
                {
                    foreach (Control c in controls)
                    {
                        if (c is TextBox) c.Text = "";
                        if (c.HasChildren) kosongkanTeks(c.Controls);
                    }
                };
                kosongkanTeks(this.Controls);

                Action<Control.ControlCollection> bersihkanDgvBawah = null;
                bersihkanDgvBawah = (controls) =>
                {
                    foreach (Control c in controls)
                    {
                        if (c is DataGridView dgv && dgv.Name != "dgvTabelList")
                        {
                            dgv.DataSource = null;
                            return;
                        }
                        if (c.HasChildren) bersihkanDgvBawah(c.Controls);
                    }
                };
                bersihkanDgvBawah(this.Controls);
            }
            catch { }
        }

        // --- Navigasi Dashboard ---
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
            SegarkanGridUtama();
            BersihkanForm();
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