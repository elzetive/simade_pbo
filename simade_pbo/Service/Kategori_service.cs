using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using simade_pbo.Model;

namespace simade_pbo.Service
{
    internal class Kategori_service
    {
        string Query;

        public Kategori_service()
        {
            Query = "";
        }

        private int jalankanNonQuery(string query)
        {
            int retVal = -1;
            using (MySql.Data.MySqlClient.MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        retVal = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ) { }
            }
            return retVal;
        }

        private DataTable jalankanQuery(string query)
        {
            DataTable dt = new DataTable();
            using (MySql.Data.MySqlClient.MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        using (MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ) { }
            }
            return dt;
        }

        public bool isExist(string nama_kategori)
        {
            bool cek = false;
            Query = "SELECT * FROM kategori WHERE nama_kategori = '" + nama_kategori + "'";

            if (jalankanQuery(Query).Rows.Count > 0)
            {
                cek = true;
            }
            return cek;
        }

        public int simpanKategori(string nama_kategori)
        {
            Query = "INSERT INTO kategori (nama_kategori) VALUES ('" + nama_kategori + "')";
            return jalankanNonQuery(Query);
        }

        public DataTable tampilSemuaKategori()
        {
            Query = "SELECT * FROM kategori";
            return jalankanQuery(Query);
        }
    }
}
