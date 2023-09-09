using System;
using System.Collections.Generic;
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
    /// UrunAlimiPenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunAlimiPenceresi : Window
    {
        MetinKontrolu metinKontrolu = new MetinKontrolu();
        string baglantiString = SqlServerBaglanti.baglanti();
        ListeYukleyici listeYukleyici = new ListeYukleyici();
        private Urun seciliUrun;
        public UrunAlimiPenceresi(Urun urun)
        {
            InitializeComponent();
            seciliUrun = urun;
            DataContext = urun;

            barkodBox.Text = seciliUrun.Barkod;
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(barkodBox.Text) || !string.IsNullOrEmpty(alimMiktariBox.Text) || !string.IsNullOrEmpty(alisFiyatiBox.Text) || !string.IsNullOrEmpty(menseiBox.Text))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(baglantiString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("UrunAlim", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Barkod", seciliUrun.Barkod);
                            command.Parameters.AddWithValue("@alimMiktari", Decimal.Parse(alimMiktariBox.Text, CultureInfo.InvariantCulture));
                            command.Parameters.AddWithValue("@alisFiyati", Decimal.Parse(alisFiyatiBox.Text, CultureInfo.InvariantCulture));
                            command.Parameters.AddWithValue("@mensei", MetinKontrolu.BasHarfBuyut(menseiBox.Text));
                            command.Parameters.AddWithValue("@tarih", DateTime.Now);

                            int result = command.ExecuteNonQuery();
                            if (result == -1)
                            {
                                Close();
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
            else MessageBox.Show("Lütfen tüm girişleri doldurun.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
