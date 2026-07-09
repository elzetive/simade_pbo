using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    //inheritance : mewarisi class form dari .net framework
    public partial class FormRiwayatWarga : Form
    {
        //enkapsulasi data agar tidak bisa diakses langsung dari luar class
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        private int idUserWargaFix = 4; //atribut private pelindung enkapsulasi data internal.

        public FormRiwayatWarga()
        {
            InitializeComponent();

            //mendaftarkan event handler untuk event load form dan klik tombol menu
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

            //mengunci tabel DataGridView agar tidak bisa diedit langsung oleh pengguna.
            dgvRiwayat.ReadOnly = true;
            dgvDetailBarang.ReadOnly = true;

            //memastikan kolom DataGridView tidak dibuat otomatis, melainkan diatur secara manual.
            dgvRiwayat.AutoGenerateColumns = false;
            colNo.DataPropertyName = "no_urut";
            colHistId.DataPropertyName = "kode_peminjaman";
            colHistTglPinjam.DataPropertyName = "tgl_pinjam";
            colHistTglKembali.DataPropertyName = "tgl_kembali";

            //memastikan kolom DataGridView detail barang tidak dibuat otomatis, melainkan diatur secara manual.
            dgvDetailBarang.AutoGenerateColumns = false;
            colDetailNama.DataPropertyName = "nama_aset";
            colDetailJumlah.DataPropertyName = "jumlah";
            //tambahin status

            tampilDataSelesai();
        }

        //read : menampilkan data peminjaman yang sudah selesai (dikembalikan) ke dalam DataGridView dgvRiwayat.
        private void tampilDataSelesai()
        {
            // abstraksi koneksi database MySQL menggunakan kelas Koneksi untuk mendapatkan objek MySqlConnection.
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();

                    //hanya menampilkan tanggal pinjam dan tanggal kembali dalam format dd/MM/yyyy, serta mengurutkan data berdasarkan tanggal kembali terbaru.
                    string query = @"SELECT kode_peminjaman, 
                                            DATE_FORMAT(tgl_pinjam, '%d/%m/%Y') AS tgl_pinjam, 
                                            DATE_FORMAT(updated_at, '%d/%m/%Y') AS tgl_kembali
                                     FROM peminjaman
                                     WHERE id_user = @idUser AND status_peminjaman = 'dikembalikan'
                                     ORDER BY updated_at DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        //mencegah SQL Injection dengan menggunakan parameterized query untuk idUser.
                        cmd.Parameters.AddWithValue("@idUser", idUserWargaFix);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // logika menambahkan nomor urut
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

        //menampilkan rincian barang yang dipinjam berdasarkan baris yang diklik di DataGridView dgvRiwayat.
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
                        // mengambil nama barang dan jumlah pinjam dari tabel detail_peminjaman yang terkait dengan kode peminjaman yang dipilih.
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
                                dgvDetailBarang.DataSource = dtDetail; // menampilkan rincian barang di DataGridView dgvDetailBarang
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

        //menavigasi ke form dashboard warga
        private void btnMenuDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardWarga frm = new FormDashboardWarga();
            frm.IdUserLogin = this.IdUserLogin;
            frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        //menavigasi ke form status pengajuan warga
        private void btnMenuStatus_Click(object sender, EventArgs e)
        {
            FormStatusPengajuan frm = new FormStatusPengajuan();
            frm.IdUserLogin = this.IdUserLogin;
            frm.NamaUserLogin = this.NamaUserLogin;
            frm.Show();
            this.Close();
        }

        //menavigasi ke form riwayat warga
        private void btnMenuRiwayat_Click(object sender, EventArgs e)
        {
            tampilDataSelesai();
        }

        //menampilkan konfirmasi log out dan menutup form saat pengguna memilih "Yes"
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}