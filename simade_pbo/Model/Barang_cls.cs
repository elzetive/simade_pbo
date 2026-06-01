using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simade_pbo.Model
{
    internal class Barang_cls
    {
        private int _id_barang;
        private string _nama_barang;
        private int _id_kategori;
        private string _kondisi;
        private string _status_ketersediaan;

        public Barang_cls()
        {
            _id_barang = 0;
            _nama_barang = "";
            _id_kategori = 0;
            _kondisi = "";
            _status_ketersediaan = "";
        }

        public int Id_barang
        {
            get { return _id_barang; }
            set { _id_barang = value; }
        }

        public string Nama_barang 
        {
            get { return _nama_barang; }
            set { _nama_barang = value; }
        }

        public int Id_kategori
        {
            get { return _id_kategori; }
            set { _id_kategori = value; }
        }

        public string Kondisi_barang
        {
            get { return _kondisi; }
            set { _kondisi = value; }
        }

        public string Status_ketersediaan
        {
            get { return _status_ketersediaan; }
            set { _status_ketersediaan = value; }
        }
    }
}
