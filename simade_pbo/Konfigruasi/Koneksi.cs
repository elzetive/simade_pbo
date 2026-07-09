using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace simade_pbo
{
    //abstraksi class koneksi database mysql
    class Koneksi
    {
        //enkapsulasi string koneksi database mysql agar tidak bisa diakses langsung dari luar class
        private static string connString = "server=localhost;port=3306;username=root;password=;database=simade_pbo;";

        //abstraksi method untuk mendapatkan koneksi database mysql 
        public static MySqlConnection GetConn()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }
}