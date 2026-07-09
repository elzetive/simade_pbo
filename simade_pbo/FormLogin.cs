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
    //inheritence : mewarisi class Form dari .net framework agar bisa menampilkan form login                                                                
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        //polymorphism: override method CreateParams untuk menambahkan gaya jendela khusus (WS_EX_COMPOSITED) agar mengurangi layar kedip hitam saat form di-render.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        //read : mencari data di tabel user berdasarkan username/email dan password yang diinputkan, lalu memvalidasi hasilnya untuk login.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Koneksi.GetConn();
            try
            {
                conn.Open();

                //logika agar bisa login dengan username atau email, menggunakan parameterized query untuk mencegah SQL Injection.
                string query = "SELECT id_user, id_role, nama_lengkap FROM user WHERE (email=@userAtauEmail OR username=@userAtauEmail) AND password=@pass";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //memblokir celah SQL Injection dengan parameterized query, sehingga input user tidak langsung dieksekusi sebagai perintah SQL.
                cmd.Parameters.AddWithValue("@userAtauEmail", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //menangkap data session aktif dari database untuk dioper ke form dashboard.
                    int idRole = Convert.ToInt32(reader["id_role"]);
                    int idUser = Convert.ToInt32(reader["id_user"]);
                    string namaLengkap = reader["nama_lengkap"].ToString();

                    MessageBox.Show("Login berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    //logika if-else untuk memisahkan hak akses menu berdasarkan id_role.
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
                        //melempar data session (ID & Nama) ke properti public target form.
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
                //memastikan gerbang koneksi database ditutup kembali agar server tidak overload.
                conn.Close();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //abstraksi : menerapkan tema yang sudah di definisikan
            TemaSistem.TerapkanTema(this, overlay, btnLogin, button1);
            lblKeRegister.Font = new Font(lblKeRegister.Font, FontStyle.Underline);
            lblKeRegister.Cursor = Cursors.Hand;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Membunuh seluruh thread proses aplikasi dari sistem operasi Windows.
        }

        private void lblKeRegister_Click(object sender, EventArgs e)
        {
            FormRegister register = new FormRegister();
            register.Show();
            this.Hide();
        }
    }
}