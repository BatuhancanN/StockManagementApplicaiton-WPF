using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public class Musteri
    {
        public int ID {  get; set; }
        public string Tcvkn { get; set; }
        public string Unvan { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }

        public Musteri(int id, string tcvkn, string unvan, string telefon, string mail) 
        { 
            ID = id;
            Tcvkn = tcvkn;
            Unvan = unvan;
            Telefon = telefon;
            Mail = mail;
        }
    }
}
