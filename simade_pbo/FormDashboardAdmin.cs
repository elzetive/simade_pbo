using simade_pbo.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simade_pbo
{
    public partial class FormDashboardAdmin : Form
    {
        Barang_service barangService = new Barang_service();
        Kategori_service kategoriService = new Kategori_service();

        int idBarangAktif = 0;

        public FormDashboardAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // SOLUSI SINKRONISASI RUNTIME EVENT:
            // Mengikat semua event handler secara eksplisit agar form responsif dan data langsung muncul saat diklik
            this.Load += new System.EventHandler(this.FormDashboardAdmin_Load_1);
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
            this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);

            // Navigasi Sidebar Left
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click_1);
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // Operasi Form Kontrol
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            this.btnTambahKategori.Click += new System.EventHandler(this.btnTambahKategori_Click);
            this.btnBatalKategori.Click += new System.EventHandler(this.btnBatalKategori_Click);
            this.btnEditDetail.Click += new System.EventHandler(this.btnEditDetail_Click);
            this.btnBatalDetail.Click += new System.EventHandler(this.btnBatalDetail_Click_1);
        }

        private void FormDashboardAdmin_Load_1(object sender, EventArgs e)
        {
            // Memanfaatkan BeginInvoke untuk mencegah pembekuan UI thread saat form pertama kali dibuka
            this.BeginInvoke(new MethodInvoker(tampilGrid));
        }

        void muatKategori()
        {
            DataTable dt = kategoriService.tampilSemuaKategori();

            if (dt == null || !dt.Columns.Contains("nama_kategori") || !dt.Columns.Contains("id_kategori"))
            {
                cmbKategoriBarang.DataSource = null;
                cmbKategoriBarang.Items.Clear();
                return;
            }

            cmbKategoriBarang.DisplayMember = "nama_kategori";
            cmbKategoriBarang.ValueMember = "id_kategori";
            cmbKategoriBarang.DataSource = dt;
            cmbKategoriBarang.SelectedIndex = -1;
        }

        void bersihkan()
        {
            txtNamaBarang.Clear();
            cmbKategoriBarang.SelectedIndex = -1;
            txtJumlahBarang.Clear();
            txtCari.Clear();
            idBarangAktif = 0;
            if (txtKategoriBarang != null) txtKategoriBarang.Clear();
            txtNamaDetail.Clear();
            txtKondisiBagus.Text = "0";
            txtKondisiRusak.Text = "0";
        }

        void tampilGrid()
        {
            string queryDinamis = @"
                SELECT 
                    b.id_barang,
                    b.nama_barang,
                    b.id_kategori,
                    k.nama_kategori,
                    b.jumlah_barang AS jumlah_total,
                    b.kondisi_bagus,
                    b.kondisi_rusak,
                    IFNULL(SUM(CASE WHEN p.status_peminjaman = 'dipinjam' THEN dp.jumlah_pinjam ELSE 0 END), 0) AS jumlah_dipinjam,
                    IFNULL(SUM(CASE WHEN p.status_peminjaman = 'pending' THEN dp.jumlah_pinjam ELSE 0 END), 0) AS jumlah_booking,
                    (b.jumlah_barang - IFNULL(SUM(CASE WHEN p.status_peminjaman IN ('pending', 'dipinjam') THEN dp.jumlah_pinjam ELSE 0 END), 0)) AS jumlah_tersedia
                FROM barang b
                INNER JOIN kategori k ON b.id_kategori = k.id_kategori
                LEFT JOIN detail_peminjaman dp ON b.id_barang = dp.id_barang
                LEFT JOIN peminjaman p ON dp.id_peminjaman = p.id_peminjaman ";

            if (txtCari.Text.Length > 0)
            {
                queryDinamis += " WHERE b.nama_barang LIKE @cari ";
            }

            queryDinamis += @" GROUP BY b.id_barang, b.nama_barang, b.id_kategori, k.nama_kategori, b.jumlah_barang, b.kondisi_bagus, b.kondisi_rusak
                               ORDER BY b.nama_barang ASC";

            DataTable dt = new DataTable();

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryDinamis, conn))
                    {
                        if (txtCari.Text.Length > 0)
                        {
                            cmd.Parameters.AddWithValue("@cari", "%" + txtCari.Text.Trim() + "%");
                        }

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat kalkulasi sirkulasi dinamis: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dgvBarang.AutoGenerateColumns = false;
            dgvBarang.Columns[0].DataPropertyName = "nama_barang";
            dgvBarang.Columns[1].DataPropertyName = "nama_kategori";
            dgvBarang.Columns[2].DataPropertyName = "jumlah_total";
            dgvBarang.Columns[3].DataPropertyName = "jumlah_tersedia";
            dgvBarang.Columns[4].DataPropertyName = "jumlah_dipinjam";

            dgvBarang.DataSource = dt;

            int totalBarangSemua = 0;
            int totalSedangDipinjam = 0;
            int totalMenungguPersetujuan = 0;
            int totalSiapTersedia = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["jumlah_total"] != DBNull.Value) totalBarangSemua += Convert.ToInt32(row["jumlah_total"]);
                if (row["jumlah_dipinjam"] != DBNull.Value) totalSedangDipinjam += Convert.ToInt32(row["jumlah_dipinjam"]);
                if (row["jumlah_booking"] != DBNull.Value) totalMenungguPersetujuan += Convert.ToInt32(row["jumlah_booking"]);
                if (row["jumlah_tersedia"] != DBNull.Value) totalSiapTersedia += Convert.ToInt32(row["jumlah_tersedia"]);
            }

            lblTotal.Text = totalBarangSemua.ToString();
            lblDipinjam.Text = totalSedangDipinjam.ToString();
            lblMenunggu.Text = totalMenungguPersetujuan.ToString();
            lblTersedia.Text = totalSiapTersedia.ToString();

            muatKategori();
            txtNamaDetail.ReadOnly = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes) Application.Exit();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaBarang.Text) || cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtJumlahBarang.Text))
            {
                MessageBox.Show("Mohon lengkapi semua data barang sebelum menambahkan.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlahBarang.Text, out int jumlah))
            {
                MessageBox.Show("Jumlah barang harus berupa angka valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string namaBarangBaru = txtNamaBarang.Text.Trim();

            if (barangService.isExist(namaBarangBaru))
            {
                MessageBox.Show("Barang sudah terdaftar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string queryInsert = "INSERT INTO barang (nama_barang, id_kategori, jumlah_barang, kondisi_bagus, kondisi_rusak) VALUES (@nama, @kat, @qty, @bagus, 0)";

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", namaBarangBaru);
                        cmd.Parameters.AddWithValue("@kat", Convert.ToInt32(cmbKategoriBarang.SelectedValue));
                        cmd.Parameters.AddWithValue("@qty", jumlah);
                        cmd.Parameters.AddWithValue("@bagus", jumlah);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bersihkan();
                    tampilGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal simpan database: " + ex.Message, "Error");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (idBarangAktif == 0) return;

            if (!int.TryParse(txtJumlahBarang.Text, out int totalInput)) return;

            string queryUpdate = "UPDATE barang SET nama_barang = @nama, id_kategori = @kat, jumlah_barang = @total, kondisi_bagus = (@total - kondisi_rusak) WHERE id_barang = @id";

            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", txtNamaBarang.Text.Trim());
                        cmd.Parameters.AddWithValue("@kat", Convert.ToInt32(cmbKategoriBarang.SelectedValue));
                        cmd.Parameters.AddWithValue("@total", totalInput);
                        cmd.Parameters.AddWithValue("@id", idBarangAktif);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data induk logistik berhasil diperbarui!", "Sukses");
                    bersihkan();
                    tampilGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengubah data: " + ex.Message, "Error");
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (idBarangAktif > 0)
            {
                if (MessageBox.Show("Yakin data akan dihapus?", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (barangService.hapus(idBarangAktif) > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus", "HAPUS DATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bersihkan();
                        tampilGrid();
                    }
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e) { bersihkan(); }

        private void txtCari_TextChanged(object sender, EventArgs e) { tampilGrid(); }

        private void btnTambahKategori_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKategoriBarang.Text.Trim())) return;

            string kategoriBaru = txtKategoriBarang.Text.Trim();

            if (!kategoriService.isExist(kategoriBaru))
            {
                if (kategoriService.simpanKategori(kategoriBaru) > 0)
                {
                    MessageBox.Show("Kategori berhasil ditambahkan!", "SUKSES");
                    txtKategoriBarang.Clear();
                    tampilGrid();
                }
            }
        }

        private void btnBatalKategori_Click(object sender, EventArgs e) { txtKategoriBarang.Clear(); }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataTable dt = (DataTable)dgvBarang.DataSource;
                    if (dt != null && dt.Rows.Count > e.RowIndex)
                    {
                        DataRow dr = dt.Rows[e.RowIndex];

                        idBarangAktif = Convert.ToInt32(dr["id_barang"]);
                        txtNamaBarang.Text = dr["nama_barang"].ToString();
                        txtJumlahBarang.Text = dr["jumlah_total"].ToString();

                        if (dr["id_kategori"] != DBNull.Value)
                        {
                            cmbKategoriBarang.SelectedValue = dr["id_kategori"];
                        }

                        txtNamaDetail.Text = dr["nama_barang"].ToString();
                        txtKondisiBagus.Text = dr["kondisi_bagus"].ToString();
                        txtKondisiRusak.Text = dr["kondisi_rusak"].ToString();
                    }
                }
                catch (Exception) { }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Close();
        }

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            bersihkan();
            tampilGrid();
        }

        private void btnData_Pinjam_Click_1(object sender, EventArgs e)
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
            FormDataPengembalian frm = new FormDataPengembalian();
            frm.Show();
            this.Close();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin frm = new FormRegisterAdmin();
            frm.Show();
            this.Close();
        }

        private void btnBatalDetail_Click_1(object sender, EventArgs e) { bersihkan(); }

        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            if (idBarangAktif == 0) return;

            if (!int.TryParse(txtKondisiBagus.Text, out int bagus) || !int.TryParse(txtKondisiRusak.Text, out int rusak))
            {
                MessageBox.Show("Input kondisi harus berupa angka valid!", "Peringatan");
                return;
            }

            int totalKondisiBaru = bagus + rusak;

            string queryCariTotal = "SELECT jumlah_barang FROM barang WHERE id_barang = @id";
            int jumlahBarangAsli = 0;
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryCariTotal, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idBarangAktif);
                    object res = cmd.ExecuteScalar();
                    if (res != null) jumlahBarangAsli = Convert.ToInt32(res);
                }
            }

            if (totalKondisiBaru != jumlahBarangAsli)
            {
                MessageBox.Show($"Validasi Gagal! Total kondisi baru tidak sama dengan Total Stok Barang ({jumlahBarangAsli}).", "Inkonsistensi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string queryUpdateDetail = "UPDATE barang SET kondisi_bagus = @bagus, condition_rusak = @rusak WHERE id_barang = @id";
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryUpdateDetail, conn))
                    {
                        cmd.Parameters.AddWithValue("@bagus", bagus);
                        cmd.Parameters.AddWithValue("@rusak", rusak);
                        cmd.Parameters.AddWithValue("@id", idBarangAktif);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Detail kondisi berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bersihkan();
                    tampilGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memperbarui detail kondisi: " + ex.Message, "Error");
                }
            }
        }
    }
}