using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace simade_pbo
{
    // Form untuk menampilkan riwayat peminjaman barang
    public partial class FormRiwayat : Form
    {
        // Service untuk mengambil data riwayat dari database
        Barang_service barang = new Barang_service();

        // Menyimpan data riwayat sementara
        DataTable dtRiwayat = new DataTable();

        // Inisialisasi form dan memuat data riwayat
        public FormRiwayat()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            tampilRiwayat();

            // Event ketika baris DataGridView dipilih
            dgvRiwayat.CellClick += dgvRiwayat_CellClick;
        }

        // Menampilkan seluruh data riwayat
        void tampilRiwayat()
        {
            // Mengambil data dari service
            dtRiwayat = barang.tampilRiwayat();

            // Menghubungkan kolom DataGridView dengan database
            colNama.DataPropertyName = "nama_barang";
            colPeminjam.DataPropertyName = "username";
            colTanggalPinjam.DataPropertyName = "tgl_pinjam";
            colTanggalKembali.DataPropertyName = "tgl_kembali";
            colStatus.DataPropertyName = "status_peminjaman";

            // Menampilkan data ke DataGridView
            dgvRiwayat.DataSource = dtRiwayat;

            // Mengisi daftar status pada ComboBox
            isiStatus();
        }

        // ==========================================================
        // Mengisi pilihan status peminjaman pada ComboBox
        // ==========================================================
        void isiStatus()
        {
            cmbStatus.Items.Clear();

            cmbStatus.Items.Add("Semua");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Disetujui");
            cmbStatus.Items.Add("Dipinjam");
            cmbStatus.Items.Add("Dikembalikan");
            cmbStatus.Items.Add("Ditolak");
            cmbStatus.Items.Add("Hangus");

            cmbStatus.SelectedIndex = 0;
        }

        // ==========================================================
        // Melakukan pencarian dan filter data
        // ==========================================================
        void filterData()
        {
            // Mengambil data sementara dalam bentuk DataView
            DataView dv = dtRiwayat.DefaultView;

            // Mengambil kata kunci pencarian
            string cari = txtCari.Text.Trim().Replace("'", "''");

            // Mengambil status yang dipilih pengguna
            string status = cmbStatus.Text.ToLower();

            // Menyimpan filter yang akan diterapkan
            string filter = "";

            // Filter berdasarkan nama barang
            if (!string.IsNullOrEmpty(cari))
            {
                filter = $"nama_barang LIKE '%{cari}%'";
            }

            // Filter berdasarkan status peminjaman
            if (status != "semua" && !string.IsNullOrEmpty(status))
            {
                if (filter != "")
                {
                    filter += " AND ";
                }

                filter += $"status_peminjaman = '{status}'";
            }

            // Menerapkan filter ke DataView
            dv.RowFilter = filter;

            // Menampilkan hasil filter ke DataGridView
            dgvRiwayat.DataSource = dv;
        }

        // ==========================================================
        // Tombol Keluar
        // Menutup halaman Riwayat
        // ==========================================================
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==========================================================
        // Tombol Dashboard
        // Berpindah ke halaman Dashboard Kepala Desa
        // ==========================================================
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();

            // Menyembunyikan Form Riwayat
            this.Hide();
        }

        // ==========================================================
        // Tombol Data Barang
        // Berpindah ke halaman Data Barang
        // ==========================================================
        private void btnBarang_Click_1(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();

            // Menyembunyikan Form Riwayat
            this.Hide();
        }

        // ==========================================================
        // Tombol Riwayat
        // Tidak melakukan proses karena pengguna sedang
        // berada di halaman Riwayat
        // ==========================================================
        private void btnRiwayat_Click(object sender, EventArgs e)
        {

        }

        // ==========================================================
        // Tombol Logout
        // Keluar dari akun dan kembali ke halaman Login
        // ==========================================================
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi sebelum logout
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin Log Out?",
                "Konfirmasi Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Jika memilih Ya maka kembali ke Form Login
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();

                this.Hide();
            }
        }

        // ==========================================================
        // Event TextBox Pencarian
        // Menjalankan proses filter setiap kali pengguna mengetik
        // ==========================================================
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // ==========================================================
        // Event ComboBox Status
        // Menampilkan data sesuai status yang dipilih
        // ==========================================================
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // ==========================================================
        // Event bawaan DataGridView
        // Belum digunakan
        // ==========================================================
        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ==========================================================
        // Menampilkan detail riwayat peminjaman
        // ketika salah satu baris dipilih
        // ==========================================================
        private void dgvRiwayat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Memastikan pengguna memilih baris data
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Mengambil data dari baris yang dipilih
                    string barang = dgvRiwayat.Rows[e.RowIndex].Cells["colNama"].Value?.ToString();
                    string peminjam = dgvRiwayat.Rows[e.RowIndex].Cells["colPeminjam"].Value?.ToString();
                    string tglPinjam = dgvRiwayat.Rows[e.RowIndex].Cells["colTanggalPinjam"].Value?.ToString();
                    string tglKembali = dgvRiwayat.Rows[e.RowIndex].Cells["colTanggalKembali"].Value?.ToString();
                    string status = dgvRiwayat.Rows[e.RowIndex].Cells["colStatus"].Value?.ToString();

                    // Menampilkan informasi detail peminjaman
                    MessageBox.Show(
                        this,
                        "Nama Barang : " + barang +
                        "\nPeminjam : " + peminjam +
                        "\nTanggal Pinjam : " + tglPinjam +
                        "\nTanggal Kembali : " + tglKembali +
                        "\nStatus : " + status,
                        "Detail Riwayat Peminjaman",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception)
                {
                    // Mengabaikan error apabila data tidak dapat dibaca
                }
            }
        }

        // ==========================================================
        // Memberikan warna pada kolom Status
        // agar setiap status mudah dibedakan oleh pengguna
        // ==========================================================
        private void dgvRiwayat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Hanya diterapkan pada kolom Status
            if (dgvRiwayat.Columns[e.ColumnIndex].Name == "colStatus")
            {
                // Memastikan nilai status tidak kosong
                if (e.Value != null)
                {
                    // Mengambil nilai status kemudian diubah menjadi huruf kecil
                    string status = e.Value.ToString().ToLower();

                    // Warna tulisan default
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.SelectionForeColor = Color.Black;

                    // Memberikan warna sesuai status peminjaman
                    switch (status)
                    {
                        // Pengajuan masih menunggu persetujuan Admin
                        case "pending":
                            e.CellStyle.BackColor = Color.Khaki;
                            break;

                        // Pengajuan telah disetujui Admin
                        case "disetujui":
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            break;

                        // Barang sedang dipinjam oleh Warga
                        case "dipinjam":
                            e.CellStyle.BackColor = Color.Orange;
                            break;

                        // Barang sudah dikembalikan
                        case "dikembalikan":
                            e.CellStyle.BackColor = Color.LightGreen;
                            break;

                        // Pengajuan ditolak Admin
                        case "ditolak":
                            e.CellStyle.BackColor = Color.LightCoral;
                            break;

                        // Pengajuan hangus karena melewati batas waktu
                        case "hangus":
                            e.CellStyle.BackColor = Color.LightGray;
                            break;

                        // Status selain yang telah ditentukan
                        default:
                            e.CellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        // ==========================================================
        // Event pencarian kedua
        // Tetap menjalankan proses filter data
        // ==========================================================
        private void txtCari_TextChanged_1(object sender, EventArgs e)
        {
            filterData();
        }

        // ==========================================================
        // Tombol Detail
        // Menampilkan informasi lengkap dari riwayat
        // yang sedang dipilih pengguna
        // ==========================================================
        private void btnDetail_Click(object sender, EventArgs e)
        {
            // Memastikan pengguna sudah memilih salah satu data
            if (dgvRiwayat.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Silakan pilih salah satu data riwayat terlebih dahulu.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // Mengambil index baris yang dipilih
            int row = dgvRiwayat.SelectedRows[0].Index;

            // Mengambil informasi dari DataGridView
            string barang = dgvRiwayat.Rows[row].Cells["colNama"].Value?.ToString();
            string peminjam = dgvRiwayat.Rows[row].Cells["colPeminjam"].Value?.ToString();
            string tglPinjam = dgvRiwayat.Rows[row].Cells["colTanggalPinjam"].Value?.ToString();
            string tglKembali = dgvRiwayat.Rows[row].Cells["colTanggalKembali"].Value?.ToString();
            string status = dgvRiwayat.Rows[row].Cells["colStatus"].Value?.ToString();

            // Menampilkan detail riwayat peminjaman
            MessageBox.Show(
                this,
                "Nama Barang : " + barang +
                "\nPeminjam : " + peminjam +
                "\nTanggal Pinjam : " + tglPinjam +
                "\nTanggal Kembali : " + tglKembali +
                "\nStatus : " + status,
                "Detail Riwayat Peminjaman",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}