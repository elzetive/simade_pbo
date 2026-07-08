using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;
using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace simade_pbo
{
    public partial class FormRiwayat : Form
    {
        // Membuat objek Barang_service untuk mengambil data riwayat dari database
        Barang_service barang = new Barang_service();

        // Menyimpan seluruh data riwayat peminjaman
        DataTable dtRiwayat = new DataTable();

        // Constructor FormRiwayat
        public FormRiwayat()
        {
            // Menginisialisasi seluruh komponen pada form
            InitializeComponent();

            // Mengatur posisi form agar muncul di tengah layar
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menampilkan data riwayat saat form pertama kali dibuka
            tampilRiwayat();

            // Menambahkan event klik pada DataGridView
            // sehingga ketika baris dipilih akan muncul detail peminjaman
            dgvRiwayat.CellClick += dgvRiwayat_CellClick;
        }

        // Method untuk menampilkan seluruh data riwayat peminjaman
        void tampilRiwayat()
        {
            // Mengambil data riwayat dari database melalui Barang_service
            dtRiwayat = barang.tampilRiwayat();

            // Menggunakan kolom yang sudah dibuat pada DataGridView Designer
            dgvRiwayat.AutoGenerateColumns = false;

            // Menghubungkan kolom DataGridView dengan nama field pada database
            colNama.DataPropertyName = "nama_barang";
            colPeminjam.DataPropertyName = "username";
            colTanggalPinjam.DataPropertyName = "tgl_pinjam";
            colTanggalKembali.DataPropertyName = "tgl_kembali";

            // Status yang ditampilkan berasal dari status_peminjaman
            colStatus.DataPropertyName = "status_peminjaman";

            // Menampilkan data ke DataGridView
            dgvRiwayat.DataSource = dtRiwayat;

            // Mengisi pilihan ComboBox status
            isiStatus();
        }

        // Mengisi ComboBox sesuai status_peminjaman pada database
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

        // Method untuk melakukan pencarian dan filter data
        void filterData()
        {
            // Mengambil DataView dari DataTable
            DataView dv = dtRiwayat.DefaultView;

            // Mengambil teks pencarian
            string cari = txtCari.Text.Trim().Replace("'", "''");

            // Mengambil status dari ComboBox
            string status = cmbStatus.Text.ToLower();

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

            // Menampilkan hasil filter
            dv.RowFilter = filter;

            dgvRiwayat.DataSource = dv;
        }

        // Tombol keluar dari Form Riwayat
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Tombol menuju Dashboard
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardKades frm = new FormDashboardKades();
            frm.Show();

            // Menyembunyikan form saat ini
            this.Hide();
        }

        // Tombol menuju halaman Data Barang
        private void btnBarang_Click_1(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();

            // Menyembunyikan form saat ini
            this.Hide();
        }

        // Tombol Riwayat
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            // Tidak melakukan apa-apa karena pengguna sudah berada di halaman Riwayat
        }

        // Tombol Logout
        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Menampilkan konfirmasi sebelum logout
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin Log Out?",
                "Konfirmasi Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Jika memilih Yes maka kembali ke halaman Login
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();

                this.Hide();
            }
        }

        // Event pencarian secara realtime ketika isi TextBox berubah
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // Event ketika ComboBox status berubah
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
        }

        // Event bawaan Visual Studio (belum digunakan)
        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ==================================================
        // Menampilkan detail riwayat ketika baris diklik
        // ==================================================
        private void dgvRiwayat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Memastikan baris yang dipilih adalah data, bukan header
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
                catch (Exception)
                {
                    // Mengabaikan error apabila data kosong
                }
            }
        }

        // Memberikan warna pada kolom Status
        private void dgvRiwayat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Hanya diterapkan pada kolom Status
            if (dgvRiwayat.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString().ToLower();

                    // Warna default
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.SelectionForeColor = Color.Black;

                    switch (status)
                    {
                        // Pengajuan masih diproses
                        case "pending":
                            e.CellStyle.BackColor = Color.Khaki;
                            break;

                        // Pengajuan sudah disetujui admin
                        case "disetujui":
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            break;

                        // Barang sedang dipinjam
                        case "dipinjam":
                            e.CellStyle.BackColor = Color.Orange;
                            break;

                        // Barang sudah dikembalikan
                        case "dikembalikan":
                            e.CellStyle.BackColor = Color.LightGreen;
                            break;

                        // Pengajuan ditolak
                        case "ditolak":
                            e.CellStyle.BackColor = Color.LightCoral;
                            break;

                        // Pengajuan hangus
                        case "hangus":
                            e.CellStyle.BackColor = Color.LightGray;
                            break;

                        // Status lainnya
                        default:
                            e.CellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        // Event pencarian kedua (jika TextBox memiliki dua event)
        private void txtCari_TextChanged_1(object sender, EventArgs e)
        {
            filterData();
        }

        // Tombol Detail
        private void btnDetail_Click(object sender, EventArgs e)
        {
            // Memastikan pengguna telah memilih salah satu baris
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

            // Mengambil data dari DataGridView
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