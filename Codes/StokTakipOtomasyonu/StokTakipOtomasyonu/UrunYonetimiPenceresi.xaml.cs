using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// UrunYonetimiPenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunYonetimiPenceresi : Window
    {
        private ListeYukleyici listeYukleyici = new ListeYukleyici();
        public ObservableCollection<Urun> Urunler { get; set; }
        public Urun SeciliUrun { get; set; }
        public UrunYonetimiPenceresi()
        {
            InitializeComponent();
            DataContext = this;
            listeYukleyici.UrunYukle();
            Urunler = listeYukleyici.Urunler;
            urunListesi.ItemsSource = Urunler;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGuncelle_Click(null, null);
            }
        }
        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            UrunEklemePenceresi urunEklemePenceresi = new UrunEklemePenceresi();
            urunEklemePenceresi.ShowDialog();
            listeYukleyici.UrunYukle();
        }

        private void urunListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (urunListesi.SelectedItem != null)
            {

                SeciliUrun = (Urun)urunListesi.SelectedItem;

                barkodBox.Text = SeciliUrun.Barkod;
                kategoriBox.Text = SeciliUrun.Kategori;
                markaBox.Text = SeciliUrun.Marka;
                urunAdiBox.Text = SeciliUrun.UrunAdi;
                miktarBox.Text = SeciliUrun.Miktar.ToString("N2");
                maliyetBox.Text = SeciliUrun.Maliyet.ToString("N2");
                //satisBox.Text = SeciliUrun.Satis.ToString();

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    satisBox.Focus();
                    satisBox.SelectAll();
                }));
            }
            else
            {
                SeciliUrun = null;
                barkodBox.Text = "";
                kategoriBox.Text = "";
                markaBox.Text = "";
                urunAdiBox.Text = "";
                miktarBox.Text = "";
                maliyetBox.Text = "";
                satisBox.Text = "";
            }
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (SeciliUrun != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                    {
                        connection.Open();

                        string query = "delete from Urunler where ID = @ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", SeciliUrun.ID);

                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                listeYukleyici.UrunYukle();
                                MessageBox.Show("Başarıyla silindi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("Lütfen bir ürün seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            listeYukleyici.UrunYukle();
            barkodBox.Text = string.Empty;
            kategoriBox.Text = string.Empty;
            markaBox.Text = string.Empty;
            urunAdiBox.Text = string.Empty;
            miktarBox.Text = string.Empty;
            maliyetBox.Text = string.Empty;
            satisBox.Text = string.Empty;
            aramaBox.Text = string.Empty;
            cmbAra.SelectedIndex = 0;
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if(SeciliUrun.Miktar > 0)
            {
                if (SeciliUrun != null)
                {
                    if (markaBox.Text == null || markaBox.Text == "")
                    {
                        MarkasizGuncelle();
                    }
                    else MarkaliGuncelle();
                }
                else MessageBox.Show("Lütfen bir ürün seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Elinizde olmayan bir ürüne değer biçemezsiniz. Öncelikle tedarik sağlayın ve maliyete göre değer biçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void MarkaliGuncelle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "UPDATE Urunler SET barkod=@barkod, kategori=@kategori, marka=@marka, urunAdi=@urunAdi, satis=@satis WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", SeciliUrun.ID);
                        command.Parameters.AddWithValue("@barkod", barkodBox.Text.Trim().Replace(" ", ""));
                        command.Parameters.AddWithValue("@kategori", MetinKontrolu.BasHarfBuyut(kategoriBox.Text));
                        command.Parameters.AddWithValue("@marka", MetinKontrolu.BasHarfBuyut(markaBox.Text));
                        command.Parameters.AddWithValue("@urunAdi", MetinKontrolu.BasHarfBuyut(urunAdiBox.Text));
                        command.Parameters.AddWithValue("@satis", Decimal.Parse(satisBox.Text, CultureInfo.InvariantCulture));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            listeYukleyici.UrunYukle();
                            MessageBox.Show("Ürün bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ürün bilgileri güncellenirken bir hata oluştu.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MarkasizGuncelle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "UPDATE Urunler SET barkod=@barkod, kategori=@kategori, marka='BELİRTİLMEMİŞ' , urunAdi=@urunAdi, satis=@satis WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", SeciliUrun.ID);
                        command.Parameters.AddWithValue("@barkod", barkodBox.Text.Trim().Replace(" ", ""));
                        command.Parameters.AddWithValue("@kategori", MetinKontrolu.BasHarfBuyut(kategoriBox.Text));
                        command.Parameters.AddWithValue("@urunAdi", MetinKontrolu.BasHarfBuyut(urunAdiBox.Text));
                        command.Parameters.AddWithValue("@satis", Decimal.Parse(satisBox.Text, CultureInfo.InvariantCulture));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            if (decimal.Parse(satisBox.Text) / decimal.Parse(maliyetBox.Text) > 5)
                            {
                                MessageBox.Show("Alış fiyatına 5 kattan fazla koymak nedir be vicdansız!!!.", " AHLAKLI ESNAF (!)", MessageBoxButton.OK, MessageBoxImage.Information);
                            }

                            listeYukleyici.UrunYukle();
                            MessageBox.Show("Ürün bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ürün bilgileri güncellenirken bir hata oluştu.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbAra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cmbAra.SelectedItem;
            string selectedText = selectedItem.Content.ToString();
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
                if(cmbAra.SelectedIndex == 0)
                {
                    MessageBox.Show("Lütfen bir arama kriteri seçin.", "Hatalı işlem tespit edildi.", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if(string.IsNullOrEmpty(aramaBox.Text))
                {
                    MessageBox.Show("Lütfen bir arama metni girin.", "Hatalı işlem tespit edildi.", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return;
            }

            string aramaKriteri = ((ComboBoxItem)cmbAra.SelectedItem).Content.ToString();
            string aramaMetni = aramaBox.Text.Trim();

            listeYukleyici.UrunAra(aramaKriteri, aramaMetni);
        }

        private void btnUrunAlimi_Click(object sender, RoutedEventArgs e)
        {
            if(SeciliUrun != null)
            {
                UrunAlimiPenceresi urunAlimiPenceresi = new UrunAlimiPenceresi(SeciliUrun);
                urunAlimiPenceresi.ShowDialog();
                listeYukleyici.UrunYukle();
            }
            else MessageBox.Show("Lütfen bir ürün seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnAlimlar_Click(object sender, RoutedEventArgs e)
        {
            if(SeciliUrun !=null)
            {
                AlimGecmisiPenceresi alimGecmisiPenceresi = new AlimGecmisiPenceresi(SeciliUrun);
                alimGecmisiPenceresi.ShowDialog();
            }
            else MessageBox.Show("Lütfen bir ürün seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
