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
        }

        void muatKategori()
        {
            DataTable dt = kategoriService.tampilSemuaKategori();

            cmbKategoriBarang.DataSource = dt;
            cmbKategoriBarang.DisplayMember = "nama_kategori";
            cmbKategoriBarang.ValueMember = "id_kategori";
            cmbKategoriBarang.SelectedIndex = -1;
        }

        void bersihkan()
        {
            txtNamaBarang.Clear();
            cmbKategoriBarang.SelectedIndex = -1;
            if (txtKondisiBarang != null) txtKondisiBarang.Clear();
            txtCari.Clear();
            idBarangAktif = 0;
            if (txtKategoriBarang != null) txtKategoriBarang.Clear();
        }

        void tampilGrid()
        {
            DataTable dt = txtCari.Text.Length == 0 ? barangService.tampilSemua() : barangService.tampilByNama(txtCari.Text);
            dgvBarang.AutoGenerateColumns = false;

            dgvBarang.Columns[0].DataPropertyName = "nama_barang";
            dgvBarang.Columns[1].DataPropertyName = "nama_kategori";
            dgvBarang.Columns[2].DataPropertyName = "kondisi";
            dgvBarang.Columns[3].DataPropertyName = "status_ketersediaan";

            dgvBarang.DataSource = dt;

            for (int i = 0; i < dgvBarang.Rows.Count; i++)
            {
                if (dgvBarang.Rows[i].Cells[3].Value != null)
                {
                    string status = dgvBarang.Rows[i].Cells[3].Value.ToString().ToLower();

                    if (status == "tersedia")
                    {
                        dgvBarang.Rows[i].Cells[4].Value = "Pinjam";
                    }
                    else if (status == "dipinjam")
                    {
                        dgvBarang.Rows[i].Cells[4].Value = "Kembali";
                    }
                }
            }

            lblTotal.Text = barangService.hitungStatistik("TOTAL");
            lblDipinjam.Text = barangService.hitungStatistik("DIPINJAM");
            lblTersedia.Text = barangService.hitungStatistik("TERSEDIA");

            muatKategori();
        }

        private void FormDashboardAdmin_Load_1(object sender, EventArgs e)
        {
            tampilGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaBarang.Text) || cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtKondisiBarang.Text))
            {
                MessageBox.Show("Mohon lengkapi semua data barang sebelum menambahkan.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!barangService.isExist(txtNamaBarang.Text))
            {
                barangService.Nama_barang = txtNamaBarang.Text;
                barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
                barangService.Kondisi_barang = txtKondisiBarang.Text;
                barangService.Status_ketersediaan = "tersedia";

                if (barangService.simpan() > 0)
                {
                    MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bersihkan();
                    tampilGrid();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan data barang. Pastikan nama barang belum terdaftar.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNamaBarang.Focus();
                }
            }
            else
            {
                MessageBox.Show("Nama barang sudah terdaftar. Silakan gunakan nama lain.", "Duplikasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (idBarangAktif > 0)
            {
                if (cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtNamaBarang.Text) || string.IsNullOrWhiteSpace(txtKondisiBarang.Text))
                {
                    MessageBox.Show("Mohon lengkapi semua data barang sebelum mengubah.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                barangService.Nama_barang = txtNamaBarang.Text;
                barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
                barangService.Kondisi_barang = txtKondisiBarang.Text;

                if (MessageBox.Show("Yakin data akan diubah?", "KONFIRMASI", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (barangService.ubah(idBarangAktif) > 0)
                    {
                        MessageBox.Show("Data berhasil diubah", "UBAH DATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bersihkan();
                        tampilGrid();
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengubah data barang. Pastikan nama barang belum terdaftar.", "UBAH DATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNamaBarang.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Silahkan pilih data pada tabel terlebih dahulu!", "PERINGATAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    else
                    {
                        MessageBox.Show("Data gagal dihapus", "HAPUS DATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Silahkan pilih data tabel terlebih dahulu!", "PERINGATAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            bersihkan();
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            tampilGrid();
        }

        private void btnTambahKategori_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKategoriBarang.Text.Trim()))
            {
                MessageBox.Show("Nama kategori tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kategoriBaru = txtKategoriBarang.Text.Trim();

            if (!kategoriService.isExist(kategoriBaru))
            {
                if (kategoriService.simpanKategori(kategoriBaru) > 0)
                {
                    MessageBox.Show("Kategori '" + kategoriBaru + "' berhasil ditambahkan!", "SUKSES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtKategoriBarang.Clear();
                    tampilGrid();
                    cmbKategoriBarang.Text = kategoriBaru;
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan kategori baru", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kategori '" + kategoriBaru + "' sudah ada di database!", "PERINGATAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBatalKategori_Click(object sender, EventArgs e)
        {
            txtKategoriBarang.Clear();
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow baris = this.dgvBarang.Rows[e.RowIndex];

                DataTable dt = (DataTable)dgvBarang.DataSource;
                idBarangAktif = Convert.ToInt32(dt.Rows[e.RowIndex]["id_barang"]);

                txtNamaBarang.Text = baris.Cells[0].Value.ToString();

                txtKondisiBarang.Text = baris.Cells[2].Value.ToString();

                if (dt.Rows[e.RowIndex]["id_kategori"] != DBNull.Value)
                {
                    cmbKategoriBarang.SelectedValue = dt.Rows[e.RowIndex]["id_kategori"];
                }
            }
        }

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                DataTable dt = (DataTable)dgvBarang.DataSource;
                int idBarang = Convert.ToInt32(dt.Rows[e.RowIndex]["id_barang"]);
                string statusSekarang = dt.Rows[e.RowIndex]["status_ketersediaan"].ToString().ToLower();

                string statusBaru = "";
                string pesanKonfirmasi = "";

                if (statusSekarang == "tersedia")
                {
                    statusBaru = "dipinjam";
                    pesanKonfirmasi = "Apakah Anda yakin ingin meminjam barang ini?";
                }
                else if (statusSekarang == "dipinjam")
                {
                    statusBaru = "tersedia";
                    pesanKonfirmasi = "Apakah Anda yakin barang ini sudah dikembalikan?";
                }

                if (MessageBox.Show(pesanKonfirmasi, "KONFIRMASI AKSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    barangService.Nama_barang = dt.Rows[e.RowIndex]["nama_barang"].ToString();
                    barangService.Id_kategori = Convert.ToInt32(dt.Rows[e.RowIndex]["nama_kategori"]);
                    barangService.Kondisi_barang = dt.Rows[e.RowIndex]["kondisi"].ToString();
                    barangService.Status_ketersediaan = statusBaru;

                    if (barangService.ubah(idBarang) > 0)
                    {
                        MessageBox.Show("Status barang berhasil diperbarui menjadi: " + statusBaru.ToUpper(), "SUKSES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tampilGrid();
                    }
                    else
                    {
                        MessageBox.Show("Gagal memperbarui status barang.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin halamanLogin = new FormLogin();
                halamanLogin.Show();
                this.Hide();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Dialihkan secara presisi untuk memanggil Form pendaftaran khusus Admin Desa
            FormRegisterAdmin halamanRegisterAdmin = new FormRegisterAdmin();

            halamanRegisterAdmin.StartPosition = FormStartPosition.CenterScreen;

            halamanRegisterAdmin.ShowDialog();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
    }
}