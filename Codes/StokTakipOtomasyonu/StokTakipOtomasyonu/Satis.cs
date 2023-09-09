using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public class Satis
    {
        public int ID {get; set;}
        public int MusteriID { get; set; }
        public int UrunID { get; set; }
        public string Barkod { get; set; }
        public int Adet { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime Tarih { get; set; }

        public Satis(int id, int musteriID, int urunID, string barkod, int adet, decimal toplamtutar, DateTime tarih)
        {
            ID = id;
            MusteriID = musteriID;
            UrunID = urunID;
            Barkod = barkod;
            Adet = adet;
            ToplamTutar = toplamtutar;
            Tarih = tarih;
        }
    }
}
