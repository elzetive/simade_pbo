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
    //inheritance : mewarisi class form dari .net framework
    public partial class FormRegisterAdmin : Form
    {
        public FormRegisterAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //create event handler untuk button register
        private void btnRegister_Click(object sender, EventArgs e)
        {
            //menolak input kosong pada form register admin
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

                //mencegah duplikasi email admin yang sudah terdaftar di database
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
                //membuat username dari email 
                string usernameStatis = email.Contains("@") ? email.Split('@')[0] : email;

                string query = "INSERT INTO user (id_role, username, email, password, nama_lengkap, alamat, no_hp) " +
                               "VALUES (@id_role, @username, @email, @pass, @nama, @alamat, @nohp)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                // mencegah SQL Injection dengan menggunakan parameterized query
                cmd.Parameters.AddWithValue("@id_role", 1); //otomatis memberikan id_role 1 untuk admin
                cmd.Parameters.AddWithValue("@username", usernameStatis);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@nohp", txtNoHp.Text);

                int hasil = cmd.ExecuteNonQuery();
                if (hasil > 0)
                {
                    MessageBox.Show("Registrasi Admin Baru berhasil disimpan ke database!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // menutup form register
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mendaftarkan akun Admin: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // menutup koneksi database untuk mencegah memory leak
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