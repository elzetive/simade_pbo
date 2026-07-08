using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace simade_pbo
{
    class Koneksi
    {
        private static string connString = "server=192.168.19.95;port=3306;username=simade_team;password=fjrc5-]nemQlV[pb;database=simade_pbo;";
        public static MySqlConnection GetConn()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }
}