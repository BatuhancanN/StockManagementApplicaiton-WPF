using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StokTakipOtomasyonu
{
    public class MetinKontrolu
    {
        public static string BasHarfBuyut(string girdi)
        {
            girdi.Trim();
            string[] kelimeler = girdi.Split(' ');

            for (int i = 0; i < kelimeler.Length; i++)
            {
                if (!string.IsNullOrEmpty(kelimeler[i]))
                {
                    string ilkHarf = kelimeler[i][0].ToString().ToUpper();
                    kelimeler[i] = ilkHarf + kelimeler[i].Substring(1);
                }
            }
            string cikti = string.Join(" ", kelimeler);
            return cikti;
        }

        public static void Bosalt()
        {

        }
    }
}
