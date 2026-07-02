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
        private int _jumlah_barang;
        private int _kondisi_bagus;
        private int _kondisi_rusak;
        private string _status;

        public Barang_cls()
        {
            _id_barang = 0;
            _nama_barang = "";
            _id_kategori = 0;
            _jumlah_barang = 0;
            _kondisi_bagus = 0;
            _kondisi_rusak = 0;
            _status= "";
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

        public int Jumlah_barang
        {
            get { return _jumlah_barang; }
            set { _jumlah_barang = value; }
        }

        public int Kondisi_bagus
        {
            get { return _kondisi_bagus; }
            set { _kondisi_bagus = value; }
        }

        public int Kondisi_rusak
        {
            get { return _kondisi_rusak; }
            set { _kondisi_rusak = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}