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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Koneksi.GetConn();
            try
            {
                conn.Open();

                string query = "SELECT id_user, id_role, nama_lengkap FROM user WHERE (email=@userAtauEmail OR username=@userAtauEmail) AND password=@pass";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userAtauEmail", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int idRole = Convert.ToInt32(reader["id_role"]);
                    int idUser = Convert.ToInt32(reader["id_user"]);
                    string namaLengkap = reader["nama_lengkap"].ToString();

                    MessageBox.Show("Login berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    if (idRole == 1)
                    {
                        FormDashboardAdmin admin = new FormDashboardAdmin();
                        admin.Show();
                    }
                    else if (idRole == 2)
                    {
                        FormDashboardKades kades = new FormDashboardKades();
                        kades.Show();
                    }
                    else if (idRole == 3)
                    {
                        FormDashboardWarga warga = new FormDashboardWarga();
                        // Mengirim data session ke property public milik FormDashboardWarga
                        warga.IdUserLogin = idUser.ToString();
                        warga.NamaUserLogin = namaLengkap;
                        warga.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Username/Email atau Password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi Gagal: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            TemaSistem.TerapkanTema(this, overlay, btnLogin, button1);
            lblKeRegister.Font = new Font(lblKeRegister.Font, FontStyle.Underline);
            lblKeRegister.Cursor = Cursors.Hand;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblKeRegister_Click(object sender, EventArgs e)
        {
            FormRegister register = new FormRegister();
            register.Show();
            this.Hide();
        }

        // Kosongan event handler agar tidak error desainer
        private void label4_Click(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void judul_Click(object sender, EventArgs e) { }
    }
}