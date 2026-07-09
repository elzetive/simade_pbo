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
    internal class Kategori_service //(OOP)
    {
        //atribut untuk menyimpan perintah SQL
        string Query; //enkapsulasi

        //constructor (OOP)
        public Kategori_service()
        {
            Query = "";
        }

        //method untuk menghubah kondisi data di database
        private int jalankanNonQuery(string query) //enkapsulasi
        {
            int retVal = -1; //untuk mengembalikan nilai -1 jika operasi gagal
            using (MySql.Data.MySqlClient.MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        retVal = cmd.ExecuteNonQuery(); //untuk menjalankan query apabila operasi sukses
                    }
                }
                catch (Exception) { }
            }
            return retVal;
        }

        //method untuk meminta dan melihat data dari database
        private DataTable jalankanQuery(string query) //enkapsulasi
        {
            DataTable dt = new DataTable(); //wadah kosong
            using (MySql.Data.MySqlClient.MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        using (MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd)) //untuk mengambil data berupa tabel
                        {
                            adapter.Fill(dt); //mengambil hasil query dan mengisi baris-baris data ke dalam wadah kosong (dt)
                        }
                    }
                }
                catch (Exception ) { }
            }
            return dt; //menampilkan datatable yang sudah terisi atau wadah kosong
        }

        //method untuk mengecek atau memvalidasi apakah sudah ada nama kategori yang diinputkan di database atau belum
        public bool isExist(string nama_kategori) //enkapsulaso
        {
            bool cek = false; //status awalnya data belum ada
            Query = "SELECT * FROM kategori WHERE nama_kategori = '" + nama_kategori + "'";

            if (jalankanQuery(Query).Rows.Count > 0) //jika data dengan nama yang diinputkan lebih dari 0
            {
                cek = true; //status datanya ada
            }
            return cek;
        }

        //method untuk menyimpan kategori ke dalam database
        public int simpanKategori(string nama_kategori) //enkapsulasi
        {
            Query = "INSERT INTO kategori (nama_kategori) VALUES ('" + nama_kategori + "')";
            return jalankanNonQuery(Query);
        }

        //method untuk menampilkan kategori dari database
        public DataTable tampilSemuaKategori() //enkapsulasi
        {
            Query = "SELECT * FROM kategori";
            return jalankanQuery(Query);
        }
    }
}

//FormDataBarang menggunakan semua method