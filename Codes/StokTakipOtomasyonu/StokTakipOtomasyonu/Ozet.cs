using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public class Ozet
    {
        public decimal Maliyet { get; set; }
        public decimal Kazanc { get; set; }
        public decimal Net { get; set; }

        public Ozet(decimal maliyet, decimal kazanc, decimal net)
        {
            Maliyet = maliyet;
            Kazanc = kazanc;
            Net = net;
        }
    }
}
