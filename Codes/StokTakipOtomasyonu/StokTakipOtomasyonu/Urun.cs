using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public class Urun
    {
        public int ID {  get; set; }
        public string Barkod { get; set; }
        public string Kategori { get; set; }
        public string Marka { get; set; }
        public string UrunAdi { get; set; }
        public int Miktar { get; set; }
        public decimal Maliyet { get; set; }
        public decimal Satis { get; set; }

        public Urun(int id, string barkod, string kategori, string marka, string urunAdi, int miktar, decimal maliyet, decimal satis)
        {
            ID = id;
            Barkod = barkod;
            Kategori = kategori;
            Marka = marka;
            UrunAdi = urunAdi;
            Miktar = miktar;
            Maliyet = maliyet;
            Satis = satis;
        }
    }
}
