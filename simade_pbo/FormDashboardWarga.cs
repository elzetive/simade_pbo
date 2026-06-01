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
        private int idBarangTerpilih = -1;

        public static int IdUserLogin { get; set; }
        public static string NamaUserLogin { get; set; }

        public FormDashboardWarga()
        {
            InitializeComponent();
        }

        private void FormDashboardWarga_Load(object sender, EventArgs e)
        {
            typeof(Form).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, this, new object[] { true });

            TampilkanDataBarang();

            lblJumlah.Visible = false;
            txtJumlah.Visible = false;

            lblNamaWarga.Text = "Halo, " + NamaUserLogin + "!";
            lblRole.Text = "User";
        }

        private void TampilkanDataBarang()
        {
            MySqlConnection conn = Koneksi.GetConn();
            try
            {
                conn.Open();

                string query = "SELECT b.id_barang, b.nama_barang, IFNULL(k.nama_kategori, 'Tanpa Kategori') AS nama_kategori, b.kondisi, b.status_ketersediaan " +
                               "FROM barang b " +
                               "LEFT JOIN kategori_barang k ON b.id_kategori = k.id_kategori " +
                               "WHERE b.status_ketersediaan = 'tersedia'";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvBarang.DataSource = dt;

                dgvBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBarang.Columns["id_barang"].HeaderText = "ID";
                dgvBarang.Columns["nama_barang"].HeaderText = "Nama Aset";
                dgvBarang.Columns["nama_kategori"].HeaderText = "Kategori";
                dgvBarang.Columns["kondisi"].HeaderText = "Kondisi";
                dgvBarang.Columns["status_ketersediaan"].HeaderText = "Status";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat aset desa: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];
                idBarangTerpilih = Convert.ToInt32(row.Cells["id_barang"].Value);
                txtNamaBarang.Text = row.Cells["nama_barang"].Value.ToString();
            }
        }

        private void btnPinjam_Click(object sender, EventArgs e)
        {
            if (idBarangTerpilih == -1)
            {
                MessageBox.Show("Silakan pilih aset desa pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MySqlConnection conn = Koneksi.GetConn();
            try
            {
                conn.Open();

                string query = "INSERT INTO peminjaman (id_user, id_barang, tgl_pinjam, tgl_kembali, status_peminjaman) " +
                               "VALUES (@id_user, @id_barang, @tgl_pinjam, @tgl_kembali, @status_peminjaman)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id_user", IdUserLogin);
                cmd.Parameters.AddWithValue("@id_barang", idBarangTerpilih);
                cmd.Parameters.AddWithValue("@tgl_pinjam", dtpPinjam.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@tgl_kembali", dtpKembali.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status_peminjaman", "pending");

                int hasil = cmd.ExecuteNonQuery();
                if (hasil > 0)
                {
                    MessageBox.Show("Pengajuan peminjaman berhasil dikirim! Menunggu konfirmasi Admin Desa.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNamaBarang.Clear();
                    idBarangTerpilih = -1;

                    TampilkanDataBarang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengirim draf peminjaman: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}