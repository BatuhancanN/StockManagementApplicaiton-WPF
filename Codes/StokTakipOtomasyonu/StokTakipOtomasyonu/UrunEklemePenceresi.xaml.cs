using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// UrunEklemePenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunEklemePenceresi : Window
    {
        ListeYukleyici listeYukleyici = new ListeYukleyici();
        public UrunEklemePenceresi()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(barkodBox.Text) && !string.IsNullOrEmpty(kategoriBox.Text) && !string.IsNullOrEmpty(urunAdiBox.Text))
            {
                if (!string.IsNullOrEmpty(markaBox.Text))
                {
                    MarkaliKaydet();
                }
                else MarkasizKaydet();
            }
            else MessageBox.Show("Lütfen zorunlu alanını doldurun.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void MarkaliKaydet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "INSERT INTO Urunler (barkod, kategori, marka, urunAdi) VALUES (@barkod, @kategori, @marka, @urunAdi)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@barkod", barkodBox.Text.Trim().Replace(" ", ""));
                        command.Parameters.AddWithValue("@kategori", MetinKontrolu.BasHarfBuyut(kategoriBox.Text));
                        command.Parameters.AddWithValue("@marka", MetinKontrolu.BasHarfBuyut(markaBox.Text));
                        command.Parameters.AddWithValue("@urunAdi", MetinKontrolu.BasHarfBuyut(urunAdiBox.Text));

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Close();
                            listeYukleyici.UrunYukle();
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
        public void MarkasizKaydet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "INSERT INTO Urunler (barkod, kategori, urunAdi) VALUES (@barkod, @kategori, @urunAdi)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@barkod", barkodBox.Text.Trim().Replace(" ", ""));
                        command.Parameters.AddWithValue("@kategori", MetinKontrolu.BasHarfBuyut(kategoriBox.Text));
                        command.Parameters.AddWithValue("@urunAdi", MetinKontrolu.BasHarfBuyut(urunAdiBox.Text));

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Close();
                            listeYukleyici.UrunYukle();
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
    }
}
