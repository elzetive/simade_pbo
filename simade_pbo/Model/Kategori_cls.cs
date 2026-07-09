using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simade_pbo.Model
{
    //class (OOP)
    internal class Kategori_cls
    {
        //atribut
        //menggunakan string pada _id_kategori untuk kemudahan penanganan data bernilai null
        private string _id_kategori; //(enkapsulasi)
        private string _nama_kategori; //(enkapsulasi)

        //constructor (OOP)
        public Kategori_cls()
        {
            //default untuk menghindari error nilai null
            _id_kategori = "";
            _nama_kategori = "";
        }

        //property
        //get untuk membaca nilai atribut tersebut agar bisa digunakan oleh komponen lain
        //set untuk untuk menerima data baru dari luar
        public string Id_kategori //enkapsulasi
        {
            get { return _id_kategori; }
            set { _id_kategori = value; }
        }

        public string Nama_kategori //enkapsulasi
        {
            get { return _nama_kategori; }
            set { _nama_kategori = value; }
        }
    }
}
