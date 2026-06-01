using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simade_pbo.Model
{
    internal class Kategori_cls
    {
        private string _id_kategori;
        private string _nama_kategori;

        public Kategori_cls()
        {
            _id_kategori = "";
            _nama_kategori = "";
        }

        public string Id_kategori
        {
            get { return _id_kategori; }
            set { _id_kategori = value; }
        }

        public string Nama_kategori
        {
            get { return _nama_kategori; }
            set { _nama_kategori = value; }
        }
    }
}
