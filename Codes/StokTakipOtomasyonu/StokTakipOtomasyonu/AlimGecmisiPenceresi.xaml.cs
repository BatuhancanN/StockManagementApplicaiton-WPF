using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// AlimGecmisiPenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class AlimGecmisiPenceresi : Window
    {
        private ListeYukleyici listeYukleyici = new ListeYukleyici();
        public ObservableCollection<Alim> Alimlar { get; set; }

        private Urun seciliUrun;
        public AlimGecmisiPenceresi(Urun urun)
        {
            InitializeComponent();
            seciliUrun = urun;
            DataContext = this;
            listeYukleyici.AlimYukle(seciliUrun.Barkod);
            Alimlar = listeYukleyici.Alimlar;
            alimListesi.ItemsSource = Alimlar;

        }

        private void btnDonemIci_Click(object sender, RoutedEventArgs e)
        {
            listeYukleyici.DonemIciAlimAra(seciliUrun.Barkod);
        }

        private void btnOncekiDonem_Click(object sender, RoutedEventArgs e)
        {
            listeYukleyici.OncekiDonemAlimAra(seciliUrun.Barkod);
        }

        private void btnTumu_Click(object sender, RoutedEventArgs e)
        {
            listeYukleyici.AlimYukle(seciliUrun.Barkod);
        }
    }
}
