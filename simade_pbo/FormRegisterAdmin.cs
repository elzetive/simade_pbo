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
    // inheritence: Mewarisi karakteristik dari class induk 'Form' bawaan .NET Framework.
    public partial class FormRegisterAdmin : Form
    {
        public FormRegisterAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // EVENT HANDLER: Memproses pendaftaran entitas Admin Baru ketika tombol diklik.
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // FIELD VALIDATION: Menolak input kosong atau spasi kosong demi validitas data.
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text) ||
                string.IsNullOrWhiteSpace(txtNoHp.Text))
            {
                MessageBox.Show("Semua kolom data Admin wajib diisi dengan lengkap!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MySqlConnection conn = Koneksi.GetConn();
            try
            {
                conn.Open();

                // REDUNDANCY CHECK: Mencegah pendaftaran email admin kembar di database.
                string cekQuery = "SELECT COUNT(*) FROM user WHERE email=@email";
                MySqlCommand cekCmd = new MySqlCommand(cekQuery, conn);
                cekCmd.Parameters.AddWithValue("@email", txtEmail.Text);

                int emailSama = Convert.ToInt32(cekCmd.ExecuteScalar());
                if (emailSama > 0)
                {
                    MessageBox.Show("Email Admin tersebut sudah terdaftar di sistem! Gunakan email lain.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string email = txtEmail.Text;
                // STRING MANIPULATION: Otomatis memotong email sebelum '@' untuk dijadikan Username.
                string usernameStatis = email.Contains("@") ? email.Split('@')[0] : email;

                // CRUD - CREATE OPERATION: Menyisipkan record admin baru ke tabel user.
                string query = "INSERT INTO user (id_role, username, email, password, nama_lengkap, alamat, no_hp) " +
                               "VALUES (@id_role, @username, @email, @pass, @nama, @alamat, @nohp)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                // PARAMETERIZED QUERIES: Proteksi ketat untuk menangkal bahaya peretasan SQL Injection.
                cmd.Parameters.AddWithValue("@id_role", 1); // BUSINESS RULE: Dikunci '1' khusus untuk tingkat Admin.
                cmd.Parameters.AddWithValue("@username", usernameStatis);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@nohp", txtNoHp.Text);

                int hasil = cmd.ExecuteNonQuery();
                if (hasil > 0)
                {
                    MessageBox.Show("Registrasi Admin Baru berhasil disimpan ke database!", "Sukses Registrasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK; // Memberi tahu form induk bahwa operasi sukses
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mendaftarkan akun Admin: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // RESOURCE MANAGEMENT: Menutup gerbang koneksi secara higienis untuk mencegah memory leak.
                conn.Close();
            }
        }

        private void lblKeLogin_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}