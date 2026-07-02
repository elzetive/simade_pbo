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
    public partial class FormDashboardWarga : Form
    {
        // Deklarasi Service dan Variabel Global
        Barang_service barangService = new Barang_service();
        DataTable dtAntrean = new DataTable();

        int idBarangTerpilih = 0;
        int stokTersediaTerpilih = 0;

        // Properti publik penampung data kiriman dari FormLogin
        public string IdUserLogin { get; set; }
        public string NamaUserLogin { get; set; }

        // Variabel penampung ID User berwujud angka int untuk query database
        int idUserLoginAngka = 1;

        public FormDashboardWarga()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            tampilDaftarAset();
            inisialisasiTabelAntrean();
        }

        private void FormDashboardWarga_Load(object sender, EventArgs e)
        {
            // Amankan konversi ID User dari Login ke tipe data integer
            if (!string.IsNullOrEmpty(IdUserLogin))
            {
                int.TryParse(IdUserLogin, out idUserLoginAngka);
            }

            // Mengunci Textbox Nama Barang agar tidak bisa diketik manual oleh warga
            txtNamaBarang.ReadOnly = true;
        }

        // 🔄 MENYAMAKAN CARA DENGAN DASHBOARD ADMIN (MENGGUNAKAN INDEKS ANGKA GRID)
        void tampilDaftarAset()
        {
            // Ambil data langsung dari service
            DataTable dt = barangService.tampilSemua();
            dgvBarang.AutoGenerateColumns = false;

            // Masukkan datanya ke Grid
            dgvBarang.DataSource = dt;
        }

        void inisialisasiTabelAntrean()
        {
            // Hapus total kolom lama agar tidak bentrok atau sisa dari desainer
            dtAntrean.Reset();
            dtAntrean.Columns.Clear();

            // Buat 3 kolom secara tegas sesuai urutan inputan saat tombol diklik
            dtAntrean.Columns.Add("id_barang", typeof(int));
            dtAntrean.Columns.Add("nama_barang", typeof(string));
            dtAntrean.Columns.Add("jumlah_pinjam", typeof(int));

            // Cegah dgv membuat kolom baru secara otomatis
            dgvAntrean.AutoGenerateColumns = false;

            // Hubungkan kolom visual DataGridView ke data tabel (sesuaikan indeks kolom dgvAntrean kamu)
            if (dgvAntrean.Columns.Count >= 2)
            {
                dgvAntrean.Columns[0].DataPropertyName = "nama_barang";
                dgvAntrean.Columns[1].DataPropertyName = "jumlah_pinjam";
            }

            // Sambungkan data source-nya
            dgvAntrean.DataSource = dtAntrean;
        }

        void bersihkanInputanBarang()
        {
            txtNamaBarang.Clear();
            txtJumlahPinjam.Clear();
            idBarangTerpilih = 0;
            stokTersediaTerpilih = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin menutup aplikasi?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow baris = this.dgvBarang.Rows[e.RowIndex];
                DataRowView rowView = (DataRowView)baris.DataBoundItem;
                if (rowView != null)
                {
                    DataRow dr = rowView.Row;

                    idBarangTerpilih = Convert.ToInt32(dr["id_barang"]);
                    stokTersediaTerpilih = Convert.ToInt32(dr["jumlah_tersedia"]);

                    idBarangTerpilih = Convert.ToInt32(dr["id_barang"]);
                    stokTersediaTerpilih = Convert.ToInt32(dr["jumlah_tersedia"]);

                    txtNamaBarang.Text = dr["nama_barang"].ToString();
                    txtJumlahPinjam.Text = stokTersediaTerpilih.ToString();
                }
            }
        }

        private void btnTambahKeList_Click(object sender, EventArgs e)
        {
            if (idBarangTerpilih == 0 || string.IsNullOrEmpty(txtNamaBarang.Text))
            {
                MessageBox.Show("Silakan pilih barang dari daftar aset desa terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlahPinjam.Text, out int jumlahInput) || jumlahInput <= 0)
            {
                MessageBox.Show("Masukkan jumlah pinjam yang valid (angka harus di atas 0)!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jumlahInput > stokTersediaTerpilih)
            {
                MessageBox.Show($"Gagal menambahkan! Jumlah pinjam ({jumlahInput} unit) melebihi jumlah tersedia ({stokTersediaTerpilih} unit).",
                                "Stok Tidak Mencukupi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtJumlahPinjam.Focus();
                return;
            }

            foreach (DataRow row in dtAntrean.Rows)
            {
                if (Convert.ToInt32(row["id_barang"]) == idBarangTerpilih)
                {
                    MessageBox.Show("Barang ini sudah ada di dalam list antrean!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            dtAntrean.Rows.Add(idBarangTerpilih, txtNamaBarang.Text, jumlahInput);
            bersihkanInputanBarang();
        }

        private void btnKirimPengajuan_Click(object sender, EventArgs e)
        {
            if (dtAntrean.Rows.Count == 0)
            {
                MessageBox.Show("List antrean peminjaman masih kosong! Silakan tambahkan barang terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kodePeminjamanOtomatis = "AD_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string tglPinjam = dtpTanggalPinjam.Value.ToString("yyyy-MM-dd");
            string tglKembali = dtpTanggalKembali.Value.ToString("yyyy-MM-dd");

            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=simade_pbo;password="))
                {
                    conn.Open();

                    // Menggunakan idUserLoginAngka yang sudah valid didapat dari FormLogin
                    string queryMaster = $@"INSERT INTO peminjaman (kode_peminjaman, id_user, tgl_pinjam, tgl_kembali, status_peminjaman)
                                           VALUES ('{kodePeminjamanOtomatis}', {idUserLoginAngka}, '{tglPinjam}', '{tglKembali}', 'Pending')";

                    MySqlCommand cmdMaster = new MySqlCommand(queryMaster, conn);
                    int hasilMaster = cmdMaster.ExecuteNonQuery();

                    if (hasilMaster > 0)
                    {
                        long idPeminjamanTerbuat = cmdMaster.LastInsertedId;
                        int suksesDetailCount = 0;

                        foreach (DataRow barisAntrean in dtAntrean.Rows)
                        {
                            int idBarang = Convert.ToInt32(barisAntrean["id_barang"]);
                            int qtyPinjam = Convert.ToInt32(barisAntrean["jumlah_pinjam"]);

                            string queryDetail = $@"INSERT INTO detail_peminjaman (id_peminjaman, id_barang, jumlah_pinjam, status, keterangan) 
                                                   VALUES ({idPeminjamanTerbuat}, {idBarang}, {qtyPinjam}, 'Pending', '-')";

                            MySqlCommand cmdDetail = new MySqlCommand(queryDetail, conn);
                            suksesDetailCount += cmdDetail.ExecuteNonQuery();
                        }

                        if (suksesDetailCount > 0)
                        {
                            MessageBox.Show($"Pengajuan peminjaman berhasil dikirim ke Admin!\nKode Nota Anda: {kodePeminjamanOtomatis}",
                                            "Sukses Pengajuan", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dtAntrean.Rows.Clear();
                            bersihkanInputanBarang();
                            tampilDaftarAset();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kendala saat menyimpan data ke database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bersihkanInputanBarang();
        }

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

        private void btnMenuDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardWarga dashboard = new FormDashboardWarga();
            dashboard.Show();
            this.Hide();
        }

        private void btnMenuStatus_Click(object sender, EventArgs e)
        {
            FormStatusPengajuan status = new FormStatusPengajuan();
            status.Show();
            this.Hide();
        }

        private void btnMenuRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayatWarga riwayat = new FormRiwayatWarga();
            riwayat.Show();
            this.Hide();
        }
    }
}