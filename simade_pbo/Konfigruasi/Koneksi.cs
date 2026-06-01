using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace simade_pbo
{
    class Koneksi
    {
        private static string connString = "server=localhost;port=3306;username=root;password=;database=simade_pbo;";

        public static MySqlConnection GetConn()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }
}