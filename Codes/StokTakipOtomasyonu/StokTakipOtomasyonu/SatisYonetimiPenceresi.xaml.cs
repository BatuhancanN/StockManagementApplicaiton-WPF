using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
    /// SatisYonetimiPenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class SatisYonetimiPenceresi : Window
    {
        private ListeYukleyici listeYukleyici = new ListeYukleyici();
        public ObservableCollection<Urun> Urunler { get; set; }
        public ObservableCollection<Musteri> Musteriler { get; set; }
        public Urun SeciliUrun { get; set; }
        public Musteri SeciliMusteri { get; set; }
        public SatisYonetimiPenceresi()
        {
            InitializeComponent();
            DataContext = this;

            listeYukleyici.SatisUrunYukle();
            listeYukleyici.MusteriYukle();

            Urunler = listeYukleyici.Urunler;
            Musteriler = listeYukleyici.Musteriler;

            urunListesi.ItemsSource = Urunler;
            musteriListesi.ItemsSource = Musteriler;
        }

        private void musteriListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (musteriListesi.SelectedItem != null)
            {

                SeciliMusteri = (Musteri)musteriListesi.SelectedItem;

                tblMusteri.Text = SeciliMusteri.Unvan;
                tblMusteriX.Text = "Müşteri     :  ";

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    miktarBox.Focus();
                    miktarBox.SelectAll();
                }));
            }
            else
            {
                SeciliMusteri = null;
                tblMusteri.Text = "";
            }
        }

        private void urunListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (urunListesi.SelectedItem != null)
            {

                SeciliUrun = (Urun)urunListesi.SelectedItem;

                tblUrun.Text = SeciliUrun.UrunAdi;
                tblUrunX.Text = "Ürün   :  ";

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    miktarBox.Focus();
                    miktarBox.SelectAll();
                }));
            }
            else
            {
                SeciliUrun = null;
                tblUrun.Text = "";
            }
        }

        private void btnSat_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(miktarBox.Text))
            {
                if (SeciliUrun != null && SeciliMusteri != null)
                {
                    if (SeciliUrun.Miktar >= int.Parse(miktarBox.Text))
                    {
                        if (SeciliUrun.Satis > 0)
                        {
                            try
                            {
                                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                                {
                                    connection.Open();

                                    using (SqlCommand command = new SqlCommand("UrunSatis", connection))
                                    {
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.AddWithValue("@UrunID", SeciliUrun.ID);
                                        command.Parameters.AddWithValue("@Barkod", SeciliUrun.Barkod);
                                        command.Parameters.AddWithValue("@MusteriID", SeciliMusteri.ID);
                                        command.Parameters.AddWithValue("@Adet", int.Parse(miktarBox.Text));
                                        command.Parameters.AddWithValue("@Fiyat", SeciliUrun.Satis);

                                        int result = command.ExecuteNonQuery();
                                        if (result > 0)
                                        {
                                            Temizle();

                                            MessageBox.Show("Başarıyla kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Hata: " + ex.Message, "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else MessageBox.Show("Lütfen ürüne bir fiyat belirleyin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Stok limitlerinin üzerinde satış yapılamaz.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Lütfen ürün ve müşteri seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Lütfen satış miktarı girin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            if (cmbAra.SelectedIndex == 0 && string.IsNullOrEmpty(aramaBox.Text))
            {
                MessageBox.Show("Lütfen bir arama kriteri seçin ve arama metni girin.", "Hatalı işlem tespit edildi.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (cmbAra.SelectedIndex == 0 || string.IsNullOrEmpty(aramaBox.Text))
            {
                if (cmbAra.SelectedIndex == 0)
                {
                    MessageBox.Show("Lütfen bir arama kriteri seçin.", "Hatalı işlem tespit edildi.", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (string.IsNullOrEmpty(aramaBox.Text))
                {
                    MessageBox.Show("Lütfen bir arama metni girin.", "Hatalı işlem tespit edildi.", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return;
            }
            else
            {
                if(cmbAra.SelectedIndex >= 4)
                {
                    string aramaKriteri = ((ComboBoxItem)cmbAra.SelectedItem).Content.ToString();
                    string aramaMetni = aramaBox.Text.Trim();

                    listeYukleyici.MusteriAra(aramaKriteri, aramaMetni);
                    aramaBox.Text = null;
                }
                else
                {
                    string aramaKriteri = ((ComboBoxItem)cmbAra.SelectedItem).Content.ToString();
                    string aramaMetni = aramaBox.Text.Trim();

                    listeYukleyici.SatisUrunAra(aramaKriteri, aramaMetni);
                    aramaBox.Text = null;
                }
            }
        }

        private void Temizle()
        {
            SeciliUrun = null;
            tblUrun.Text = string.Empty;

            SeciliMusteri = null;
            tblMusteri.Text = string.Empty;

            miktarBox.Text = string.Empty;

            urunListesi.SelectedItem = null;
            musteriListesi.SelectedItem = null;

            tblMusteriX.Text = string.Empty;
            tblUrunX.Text = string.Empty;

            listeYukleyici.SatisUrunYukle();
            listeYukleyici.MusteriYukle();

            aramaBox.Text = string.Empty;

            cmbAra.SelectedIndex = 0;
        }

        private void btnSatislar_Click(object sender, RoutedEventArgs e)
        {
            SatisGecmisiPenceresi satisGecmisiPenceresi = new SatisGecmisiPenceresi();
            satisGecmisiPenceresi.ShowDialog();
        }
    }
}
