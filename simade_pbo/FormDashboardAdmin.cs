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
            DataTable dt = txtCari.Text.Length == 0 ? barangService.tampilSemua() : barangService.tampilByNama(txtCari.Text);
            dgvBarang.AutoGenerateColumns = false;

            dgvBarang.Columns[0].DataPropertyName = "nama_barang";
            dgvBarang.Columns[1].DataPropertyName = "nama_kategori";
            dgvBarang.Columns[2].DataPropertyName = "jumlah_total";
            dgvBarang.Columns[3].DataPropertyName = "jumlah_tersedia";
            dgvBarang.Columns[4].DataPropertyName = "jumlah_dipinjam";

            dgvBarang.DataSource = dt;

            lblTotal.Text = barangService.hitungStatistik("TOTAL");
            lblDipinjam.Text = barangService.hitungStatistik("DIPINJAM");
            lblTersedia.Text = barangService.hitungStatistik("TERSEDIA");

            muatKategori();
            txtNamaDetail.ReadOnly = true;
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
            if (string.IsNullOrWhiteSpace(txtNamaBarang.Text) || cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtJumlahBarang.Text))
            {
                MessageBox.Show("Mohon lengkapi semua data barang sebelum menambahkan.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlahBarang.Text, out int jumlah))
            {
                MessageBox.Show("Jumlah barang harus berupa angka valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahBarang.Focus();
                return;
            }

            string namaBarangBaru = txtNamaBarang.Text.Trim();

            if (barangService.isExist(namaBarangBaru))
            {
                MessageBox.Show("Barang dengan nama '" + namaBarangBaru + "' sudah ada!\nSilakan gunakan tombol EDIT jika ingin menambah stok.",
                                "Barang Sudah Ada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaBarang.Focus();
                return;
            }

            barangService.Nama_barang = namaBarangBaru;
            barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
            barangService.Jumlah_barang = jumlah;

            if (barangService.simpan() > 0)
            {
                MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bersihkan();
                tampilGrid();
            }
            else
            {
                MessageBox.Show("Gagal menyimpan data barang!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNamaBarang.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (idBarangAktif > 0)
            {
                if (cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtNamaBarang.Text) || string.IsNullOrWhiteSpace(txtJumlahBarang.Text))
                {
                    MessageBox.Show("Mohon lengkapi semua data barang sebelum mengubah.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                barangService.Nama_barang = txtNamaBarang.Text;
                barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
                barangService.Jumlah_barang = Convert.ToInt32(txtJumlahBarang.Text);

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
                        MessageBox.Show("Gagal mengubah data barang.", "UBAH DATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (MessageBox.Show("Yakin data akan dihapus?", "KONFIRFINMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                MessageBox.Show("Silahkan pilih data pada tabel terlebih dahulu!", "PERINGATAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Kategori '" + kategoriBaru + "' sudah ada!", "PERINGATAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                DataRowView rowView = (DataRowView)baris.DataBoundItem;
                if (rowView != null)
                {
                    DataRow dr = rowView.Row;

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
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Konfirmasi Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                FormLogin halamanLogin = new FormLogin();
                halamanLogin.Show();
                this.Close();
            }
        }

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            tampilGrid();
        }

        private void btnData_Pinjam_Click_1(object sender, EventArgs e)
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

        private void btnBatalDetail_Click_1(object sender, EventArgs e)
        {
            bersihkan();
        }

        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            if (idBarangAktif == 0)
            {
                MessageBox.Show("Silakan pilih barang pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKondisiBagus.Text) || string.IsNullOrWhiteSpace(txtKondisiRusak.Text))
            {
                MessageBox.Show("Kolom kondisi bagus dan rusak tidak boleh kosong.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtKondisiBagus.Text, out int bagus) || !int.TryParse(txtKondisiRusak.Text, out int rusak))
            {
                MessageBox.Show("Input kondisi harus berupa angka yang valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalKondisiBaru = bagus + rusak;

            if (MessageBox.Show($"Yakin ingin memperbarui kondisi barang ini?\nTotal ketersediaan stok akan dihitung ulang secara otomatis.", "KONFIRMASI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                barangService.Nama_barang = txtNamaBarang.Text;
                barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
                barangService.Jumlah_barang = totalKondisiBaru;

                barangService.ubah(idBarangAktif);

                if (barangService.ubahDetailKondisi(idBarangAktif, bagus, rusak) > 0)
                {
                    MessageBox.Show("Detail kondisi dan total ketersediaan barang berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bersihkan();
                    tampilGrid();
                }
                else
                {
                    MessageBox.Show("Gagal memperbarui detail kondisi barang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}