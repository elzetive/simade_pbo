using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
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

            // Pasang event handler secara eksplisit agar berjalan kuat
            this.Load += new System.EventHandler(this.FormDataPengembalian_Load);

            // Pasang event klik pada tabel atas (Antrean)
            if (this.dgvTabelList != null)
            {
                this.dgvTabelList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellClick);
                this.dgvTabelList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelList_CellContentClick);
            }
        }

        private void FormDataPengembalian_Load(object sender, EventArgs e)
        {
            // 1. Pemetaan Kolom Tabel Atas (List yang sedang dipinjam)
            if (dgvTabelList != null)
            {
                dgvTabelList.AutoGenerateColumns = false;
                if (dgvTabelList.Columns.Count >= 5)
                {
                    dgvTabelList.Columns[0].DataPropertyName = "kode_peminjaman";
                    dgvTabelList.Columns[1].DataPropertyName = "nama_lengkap";
                    dgvTabelList.Columns[2].DataPropertyName = "tgl_pinjam";
                    dgvTabelList.Columns[3].DataPropertyName = "tgl_kembali";
                    dgvTabelList.Columns[4].DataPropertyName = "status_peminjaman";
                }
            }

            // 2. Bersihkan Grid Bawah & Paksa jadi TextBox murni agar terhindar dari ComboBox Error dialog
            ResetDanBangunUlangGridBawah();

            // 3. Tarik data dari database
            MuatBarangSedangDipinjam();
        }

        private void ResetDanBangunUlangGridBawah()
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
                            dgv.DataError += (s, anE) => {
                                anE.ThrowException = false;
                                anE.Cancel = true;
                            };

                            dgv.Columns.Clear();

                            // === BANGUN ULANG STRUKTUR KOLOM SECARA VISUAL ===
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "nama_barang", HeaderText = "Nama Barang", DataPropertyName = "nama_barang", Width = 150 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "kategori_barang", HeaderText = "Kategori Barang", DataPropertyName = "kategori_barang", Width = 130 });

                            // 1. Tambahkan kolom deskripsi secara visual
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "deskripsi_barang", HeaderText = "Deskripsi Barang", DataPropertyName = "deskripsi_barang", Width = 150 });

                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "jumlah_pinjam", HeaderText = "Jumlah Pinjam", DataPropertyName = "jumlah_pinjam", Width = 110 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "jumlah_kembali", HeaderText = "Jumlah Kembali", DataPropertyName = "jumlah_kembali", Width = 110 });

                            // 2. Tambahkan kolom kondisi & denda secara visual
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "kondisi_bagus", HeaderText = "Kondisi Bagus", DataPropertyName = "kondisi_bagus", Width = 110 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "kondisi_rusak", HeaderText = "Kondisi Rusak", DataPropertyName = "kondisi_rusak", Width = 110 });
                            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "denda", HeaderText = "Denda", DataPropertyName = "denda", Width = 100 });

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

        private void MuatBarangSedangDipinjam()
        {
            if (dgvTabelList == null) return;

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();

                    // Cek ketersediaan nama kolom nomor telepon secara dinamis di tabel user
                    string kolomTelepon = "u.nomor_telepon";
                    using (MySqlCommand checkCmd = new MySqlCommand("SHOW COLUMNS FROM user LIKE 'nomor_telepon'", conn))
                    {
                        if (checkCmd.ExecuteScalar() == null)
                        {
                            using (MySqlCommand checkCmd2 = new MySqlCommand("SHOW COLUMNS FROM user LIKE 'no_hp'", conn))
                            {
                                kolomTelepon = checkCmd2.ExecuteScalar() != null ? "u.no_hp" : "u.no_telp";
                            }
                        }
                    }

                    // Ambil berkas transaksi yang statusnya 'dipinjam'
                    string query = $@"SELECT DISTINCT p.kode_peminjaman, u.nama_lengkap, {kolomTelepon} AS nomor_telepon_fix,
                                             p.tgl_pinjam, p.tgl_kembali, p.status_peminjaman 
                                     FROM peminjaman p
                                     INNER JOIN user u ON p.id_user = u.id_user
                                     WHERE LOWER(p.status_peminjaman) = 'dipinjam'";

                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvTabelList.DataSource = dt;

                        // Perbarui Nilai Indikator Atas
                        UpdateKartuIndikatorAtas(dt.Rows.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat sirkulasi pinjam: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateKartuIndikatorAtas(int jumlahAktif)
        {
            try
            {
                foreach (Control c in this.Controls)
                {
                    if (c is Panel || c.HasChildren)
                    {
                        foreach (Control subC in c.Controls)
                        {
                            if (subC is Label lbl && (lbl.Text == "0" || lbl.Name.ToLower().Contains("pinjam")))
                            {
                                if (lbl.Size.Height > 15) lbl.Text = jumlahAktif.ToString();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void dgvTabelList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikAntrean(e.RowIndex);
        }

        private void dgvTabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EksekusiKlikAntrean(e.RowIndex);
        }

        private void EksekusiKlikAntrean(int rowIndex)
        {
            if (rowIndex < 0 || dgvTabelList.Rows.Count == 0 || rowIndex >= dgvTabelList.Rows.Count) return;

            try
            {
                DataGridViewRow baris = this.dgvTabelList.Rows[rowIndex];
                DataRowView rowView = baris.DataBoundItem as DataRowView;
                DataRow dr = (rowView != null) ? rowView.Row : null;

                if (dr != null)
                {
                    kodeNotaAktif = dr["kode_peminjaman"].ToString();
                    string namaPeminjam = dr["nama_lengkap"].ToString();
                    string tglPinjam = dr["tgl_pinjam"] != DBNull.Value ? Convert.ToDateTime(dr["tgl_pinjam"]).ToString("dd/MM/yyyy") : "-";
                    string tglKembali = dr["tgl_kembali"] != DBNull.Value ? Convert.ToDateTime(dr["tgl_kembali"]).ToString("dd/MM/yyyy") : "-";

                    string noTelp = "-";
                    if (dr.Table.Columns.Contains("nomor_telepon_fix")) noTelp = dr["nomor_telepon_fix"].ToString();
                    else if (dr.Table.Columns.Contains("nomor_telepon")) noTelp = dr["nomor_telepon"].ToString();
                    else if (dr.Table.Columns.Contains("no_hp")) noTelp = dr["no_hp"].ToString();

                    // Panggil fungsi pengisian TextBox
                    IsiTextBoxFormMaju(namaPeminjam, noTelp, tglPinjam, tglKembali);

                    // Muat Item Barang Milik Nota Ini ke Grid Bawah
                    MuatDetailBarangBawah(kodeNotaAktif);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca baris antrean: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IsiTextBoxFormMaju(string nama, string telp, string pinjam, string kembali)
        {
            try
            {
                List<Label> semuaLabel = new List<Label>();
                List<TextBox> semuaTextBox = new List<TextBox>();

                Action<Control.ControlCollection> dapatkanKontrol = null;
                dapatkanKontrol = (ctrls) => {
                    foreach (Control c in ctrls)
                    {
                        if (c is Label lbl) semuaLabel.Add(lbl);
                        if (c is TextBox txt) semuaTextBox.Add(txt);
                        if (c.HasChildren) dapatkanKontrol(c.Controls);
                    }
                    ;
                };
                dapatkanKontrol(this.Controls);

                foreach (var txt in semuaTextBox)
                {
                    Label labelTerdekat = null;
                    int jarakTerkecil = int.MaxValue;

                    foreach (var lbl in semuaLabel)
                    {
                        int selisihY = Math.Abs(lbl.PointToScreen(Point.Empty).Y - txt.PointToScreen(Point.Empty).Y);
                        if (lbl.PointToScreen(Point.Empty).X < txt.PointToScreen(Point.Empty).X && selisihY < 15)
                        {
                            int jarakX = txt.PointToScreen(Point.Empty).X - lbl.PointToScreen(Point.Empty).X;
                            if (jarakX < jarakTerkecil)
                            {
                                jarakTerkecil = jarakX;
                                labelTerdekat = lbl;
                            }
                        }
                    }

                    if (labelTerdekat != null)
                    {
                        string teksLabel = labelTerdekat.Text.ToLower();

                        if (teksLabel.Contains("nama") || teksLabel.Contains("peminjam") || teksLabel.Contains("lengkap"))
                        {
                            txt.Text = nama;
                        }
                        else if (teksLabel.Contains("telepon") || teksLabel.Contains("hp") || teksLabel.Contains("telp"))
                        {
                            txt.Text = telp;
                        }
                        else if (teksLabel.Contains("pinjam"))
                        {
                            txt.Text = pinjam;
                        }
                        else if (teksLabel.Contains("kembali"))
                        {
                            txt.Text = kembali;
                        }
                        else if (teksLabel.Contains("ambil") || teksLabel.Contains("oleh"))
                        {
                            txt.Text = "";
                        }
                    }
                    else
                    {
                        string namaTxt = txt.Name.ToLower();
                        if (namaTxt.Contains("nama") || namaTxt.Contains("peminjam")) txt.Text = nama;
                        else if (namaTxt.Contains("hp") || namaTxt.Contains("telp") || namaTxt.Contains("telepon")) txt.Text = telp;
                        else if (namaTxt.Contains("pinjam")) txt.Text = pinjam;
                        else if (namaTxt.Contains("kembali")) txt.Text = kembali;
                        else if (namaTxt.Contains("ambil") || namaTxt.Contains("oleh")) txt.Text = "";
                    }
                }
            }
            catch { }
        }

        private void MuatDetailBarangBawah(string kodePeminjaman)
        {
            // 1. Membuat penampung data (DataTable)
            DataTable dt = new DataTable();

            // WAJIB DIPERHATIKAN: Nama-nama kolom di bawah ini HARUS SAMA PERSIS dengan
            // nilai properti "DataPropertyName" pada tiap kolom di DataGridView bawah kamu!
            dt.Columns.Add("nama_barang");
            dt.Columns.Add("kategori_barang");
            dt.Columns.Add("deskripsi_barang"); // Tetap dibuat agar grid tidak error, diisi '-'
            dt.Columns.Add("jumlah_pinjam");
            dt.Columns.Add("jumlah_kembali");
            dt.Columns.Add("kondisi_bagus");    // Tetap dibuat untuk kompatibilitas desainer
            dt.Columns.Add("kondisi_rusak");    // Tetap dibuat untuk kompatibilitas desainer
            dt.Columns.Add("denda");            // Tetap dibuat untuk kompatibilitas desainer

            try
            {
                using (MySqlConnection conn = Koneksi.GetConn())
                {
                    conn.Open();

                    // 2. Query SQL yang sudah disesuaikan agar terhindar dari error b.deskripsi / b.deskripsi_barang
                    // Menggunakan LEFT JOIN ke kategori supaya jika kategori barang kosong, barang tetap muncul
                    string queryTembak = @"SELECT b.nama_barang, 
                                          IFNULL(k.nama_kategori, 'Umum') AS kategori_barang, 
                                          dp.jumlah_pinjam
                                   FROM detail_peminjaman dp 
                                   INNER JOIN barang b ON dp.id_barang = b.id_barang 
                                   LEFT JOIN kategori k ON b.id_kategori = k.id_kategori
                                   WHERE dp.id_peminjaman = @kode";

                    using (MySqlCommand cmd = new MySqlCommand(queryTembak, conn))
                    {
                        // Menggunakan parameter agar aman dan tidak memicu error casting tipe data
                        cmd.Parameters.AddWithValue("@kode", kodePeminjaman);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Ambil jumlah pinjam dari database
                                int jmlPinjam = Convert.ToInt32(reader["jumlah_pinjam"]);

                                // Sesuai permintaan: Jumlah Kembali OTOMATIS disamakan dengan Jumlah Pinjam saat dimuat
                                int jmlKembali = jmlPinjam;

                                // Memasukkan data baris demi baris ke DataTable
                                dt.Rows.Add(
                                    reader["nama_barang"].ToString(),
                                    reader["kategori_barang"].ToString(),
                                    "-", // Mengisi kolom Deskripsi Barang dengan strip agar aman
                                    jmlPinjam,
                                    jmlKembali,
                                    0, // default isi Kondisi Bagus
                                    0, // default isi Kondisi Rusak
                                    0  // default isi Denda
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Menampilkan pesan error jika koneksi atau kueri database bermasalah
                MessageBox.Show("Gagal memuat detail database: " + ex.Message, "Error Database");
            }

            // 3. Masukkan hasil DataTable ke DataGridView bawah menggunakan method bawaan project-mu
            TembakKeGridBawah(dt);
        }
        private void TembakKeGridBawah(DataTable dt)
        {
            Action<Control.ControlCollection> pasangDgv = null;
            pasangDgv = (controls) =>
            {
                foreach (Control c in controls)
                {
                    if (c is DataGridView dgv && dgv.Name != "dgvTabelList")
                    {
                        dgv.DataSource = null;
                        dgv.DataSource = dt;
                        return;
                    }
                    if (c.HasChildren) pasangDgv(c.Controls);
                }
            };
            pasangDgv(this.Controls);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kodeNotaAktif))
            {
                MessageBox.Show("Silakan pilih berkas transaksi yang akan dikembalikan pada tabel atas!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show($"Apakah Anda yakin ingin memproses pengembalian untuk Nota {kodeNotaAktif}?", "Konfirmasi Gudang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = Koneksi.GetConn())
                    {
                        conn.Open();
                        string queryUpdate = "UPDATE peminjaman SET status_peminjaman = 'dikembalikan', tgl_kembali = NOW() WHERE kode_peminjaman = @kode";
                        using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                        {
                            cmd.Parameters.AddWithValue("@kode", kodeNotaAktif);
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("Sukses! Siklus inventaris selesai, barang resmi masuk kembali ke dalam gudang.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                BersihkanSeluruhForm();
                                MuatBarangSedangDipinjam();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memproses sirkulasi balik: " + ex.Message, "Error");
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
            try
            {
                Action<Control.ControlCollection> clearTxt = null;
                clearTxt = (controls) => {
                    foreach (Control c in controls)
                    {
                        if (c is TextBox) c.Text = "";
                        if (c.HasChildren) clearTxt(c.Controls);
                    }
                };
                clearTxt(this.Controls);

                Action<Control.ControlCollection> clearDgv = null;
                clearDgv = (controls) => {
                    foreach (Control c in controls)
                    {
                        if (c is DataGridView dgv && dgv.Name != "dgvTabelList")
                        {
                            dgv.DataSource = null;
                            return;
                        }
                        if (c.HasChildren) clearDgv(c.Controls);
                    }
                };
                clearDgv(this.Controls);
            }
            catch { }
        }

        // --- Sistem Navigasi Dashboard SIMADE ---
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
            BersihkanSeluruhForm();
            MuatBarangSedangDipinjam();
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