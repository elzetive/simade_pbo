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
        bool isLoadingFilter = true;

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

        void muatFilter()
        {
            isLoadingFilter = true;

            DataTable dtBarang = barangService.tampilSemua();
            DataView view = new DataView(dtBarang);
            DataTable dtNamaUnik = view.ToTable(true, "nama_barang");

            cmbFilterNama.DataSource = dtNamaUnik;
            cmbFilterNama.DisplayMember = "nama_barang";
            cmbFilterNama.ValueMember = "nama_barang";
            cmbFilterNama.SelectedIndex = -1;

            cmbFilterKondisi.SelectedIndex = -1;

            isLoadingFilter = false;
        }

        void jalankanFilter()
        {
            if (isLoadingFilter) return;

            DataTable dt = barangService.tampilSemua();
            DataView dv = new DataView(dt);
            string queryFilter = "";

            bool namaTerpilih = cmbFilterNama.SelectedIndex != -1;
            bool kondisiTerpilih = cmbFilterKondisi.SelectedIndex != -1;

            string kondisiTeks = cmbFilterKondisi.SelectedItem != null ? cmbFilterKondisi.SelectedItem.ToString() : "";

            if (namaTerpilih && kondisiTerpilih)
            {
                queryFilter = $"nama_barang = '{cmbFilterNama.SelectedValue}' AND kondisi = '{kondisiTeks}'";
            }
            else if (namaTerpilih && !kondisiTerpilih)
            {
                queryFilter = $"nama_barang = '{cmbFilterNama.SelectedValue}'";
            }
            else if (!namaTerpilih && kondisiTerpilih)
            {
                queryFilter = $"kondisi = '{kondisiTeks}'";
            }

            if (!string.IsNullOrEmpty(queryFilter))
            {
                dv.RowFilter = queryFilter;
                dgvBarang.DataSource = dv;
                lblJumlahFilter.Text = dv.Count.ToString();
            }
            else
            {
                dgvBarang.DataSource = dt;
                lblJumlahFilter.Text = lblTotal.Text;
            }
        }

        void bersihkan()
        {
            txtNamaBarang.Clear();
            cmbKategoriBarang.SelectedIndex = -1;
            cmbKondisiBarang.SelectedIndex = -1;
            txtCari.Clear();
            idBarangAktif = 0;
            if (txtKategoriBarang != null) txtKategoriBarang.Clear();

            isLoadingFilter = true;
            cmbFilterNama.SelectedIndex = -1;
            cmbFilterKondisi.SelectedIndex = -1;
            isLoadingFilter = false;
            jalankanFilter();
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

            lblTotal.Text = barangService.hitungStatistik("TOTAL");
            lblDipinjam.Text = barangService.hitungStatistik("DIPINJAM");
            lblTersedia.Text = barangService.hitungStatistik("TERSEDIA");

            muatKategori();
        }

        private void FormDashboardAdmin_Load_1(object sender, EventArgs e)
        {
            tampilGrid();
            muatFilter();
            lblJumlahFilter.Text = lblTotal.Text;
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
            if (string.IsNullOrWhiteSpace(txtNamaBarang.Text) || cmbKategoriBarang.SelectedIndex == -1 || cmbKondisiBarang.SelectedIndex == -1)
            {
                MessageBox.Show("Mohon lengkapi semua data barang sebelum menambahkan.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            barangService.Nama_barang = txtNamaBarang.Text;
            barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
            barangService.Kondisi_barang = cmbKondisiBarang.Text;
            barangService.Status_ketersediaan = "tersedia";

            if (barangService.simpan() > 0)
            {
                MessageBox.Show("Data berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bersihkan();
                tampilGrid();
                muatFilter();
                jalankanFilter();
            }
            else
            {
                MessageBox.Show("Gagal menyimpan data barang ke database.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNamaBarang.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (idBarangAktif > 0)
            {
                if (cmbKategoriBarang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtNamaBarang.Text) || cmbKondisiBarang.SelectedIndex == -1)
                {
                    MessageBox.Show("Mohon lengkapi semua data barang sebelum mengubah.", "Data Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                barangService.Nama_barang = txtNamaBarang.Text;
                barangService.Id_kategori = Convert.ToInt32(cmbKategoriBarang.SelectedValue);
                barangService.Kondisi_barang = cmbKondisiBarang.Text;

                if (MessageBox.Show("Yakin data akan diubah?", "KONFIRMASI", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (barangService.ubah(idBarangAktif) > 0)
                    {
                        MessageBox.Show("Data berhasil diubah", "UBAH DATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bersihkan();
                        tampilGrid();
                        muatFilter();
                        jalankanFilter();
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
                        muatFilter();
                        jalankanFilter();
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

                if (baris.Cells[2].Value != null)
                {
                    cmbKondisiBarang.Text = baris.Cells[2].Value.ToString();
                }

                if (dt.Rows[e.RowIndex]["id_kategori"] != DBNull.Value)
                {
                    cmbKategoriBarang.SelectedValue = dt.Rows[e.RowIndex]["id_kategori"];
                }
            }
        }

        private void cmbFilterNama_SelectedIndexChanged(object sender, EventArgs e)
        {
            jalankanFilter();
        }

        private void cmbFilterKondisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            jalankanFilter();
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

        private void btnData_Barang_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin halamanDashboardAdmin = new FormDashboardAdmin();

            halamanDashboardAdmin.StartPosition = FormStartPosition.CenterScreen;

            halamanDashboardAdmin.ShowDialog();
        }

        private void btnData_Pinjam_Click_1(object sender, EventArgs e)
        {
            FormDataPinjam halamanDataPinjam = new FormDataPinjam();

            halamanDataPinjam.StartPosition = FormStartPosition.CenterScreen;

            halamanDataPinjam.ShowDialog();
        }

        private void btnData_Ambil_Click(object sender, EventArgs e)
        {
            FormDataPengambilan halamanDataPengambilan = new FormDataPengambilan();

            halamanDataPengambilan.StartPosition = FormStartPosition.CenterScreen;

            halamanDataPengambilan.ShowDialog();
        }

        private void btnData_Kembali_Click(object sender, EventArgs e)
        {
            FormDataPengembalian halamanDataPengembalian = new FormDataPengembalian();

            halamanDataPengembalian.StartPosition = FormStartPosition.CenterScreen;

            halamanDataPengembalian.ShowDialog();
        }

        private void btnTambah_Admin_Click(object sender, EventArgs e)
        {
            FormRegisterAdmin halamanRegisterAdmin = new FormRegisterAdmin();

            halamanRegisterAdmin.StartPosition = FormStartPosition.CenterScreen;

            halamanRegisterAdmin.ShowDialog();
        }
    }
}