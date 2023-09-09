using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StokTakipOtomasyonu
{
    public partial class SatisGecmisiPenceresi : Window
    {

        public SatisGecmisiPenceresi()
        {
            InitializeComponent();
            ListeYukleyici.SatisGecmisiYukle(2, satisListesi);
        }

        private void btnDonemIci_Click(object sender, RoutedEventArgs e)
        {
            ListeYukleyici.SatisGecmisiYukle(2, satisListesi);
        }

        private void btnOncekiDonem_Click(object sender, RoutedEventArgs e)
        {
            ListeYukleyici.SatisGecmisiYukle(3, satisListesi);
        }

        private void btnTumu_Click(object sender, RoutedEventArgs e)
        {
            ListeYukleyici.SatisGecmisiYukle(1, satisListesi);
        }
    }
}
