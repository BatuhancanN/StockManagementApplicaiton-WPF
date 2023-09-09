using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public class Alim
    {
        public int ID { get; set; }
        public string Barkod { get; set; }
        public int AlimMiktari { get; set; }
        public decimal AlisFiyati { get; set; }
        public string Mensei { get; set; }
        public DateTime Tarih { get; set; }

        public Alim(int id, string barkod, int alimMiktari, decimal alisFiyati, string mensei, DateTime tarih)
        {
            ID = id;
            Barkod = barkod;
            AlimMiktari = alimMiktari;
            AlisFiyati = alisFiyati;
            Mensei = mensei;
            Tarih = tarih;
        }
    }
}
