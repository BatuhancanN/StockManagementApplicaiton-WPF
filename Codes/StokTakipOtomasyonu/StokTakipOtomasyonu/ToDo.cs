using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public class ToDo
    {
        public int ID {  get; set; }
        public string Metin { get; set; }

        public ToDo(int id, string metin) 
        { 
            ID = id;
            Metin = metin;
        }
    }
}
