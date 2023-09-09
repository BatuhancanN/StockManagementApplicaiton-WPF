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
    /// MusteriEklemePenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class MusteriEklemePenceresi : Window
    {
        string baglantiString = SqlServerBaglanti.baglanti();
        MetinKontrolu metinKontrolu = new MetinKontrolu();
        ListeYukleyici musteriYukleyici = new ListeYukleyici();
        public MusteriEklemePenceresi()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(unvanBox.Text))
            {
                if (!string.IsNullOrEmpty(tcvknBox.Text) || !string.IsNullOrEmpty(telefonBox.Text) || !string.IsNullOrEmpty(mailBox.Text))
                {
                    Kaydet();
                }
                else
                {
                   MessageBoxResult secim = MessageBox.Show("Zorunlu olmayan alanlarda boşluk var. Devam etmek istiyor musunuz?", "Eksik İşlem Tespit Edildi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (secim == MessageBoxResult.Yes) Kaydet();
                }
            }
            else MessageBox.Show("Lütfen unvan alanını doldurun.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public void Kaydet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(baglantiString))
                {
                    connection.Open();

                    string query = "INSERT INTO Musteriler (tcvkn, unvan, telefon, mail) VALUES (@tcvkn, @unvan, @telefon, @mail)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tcvkn", tcvknBox.Text.Trim().Replace(" ", ""));
                        command.Parameters.AddWithValue("@unvan", MetinKontrolu.BasHarfBuyut(unvanBox.Text));
                        command.Parameters.AddWithValue("@telefon", telefonBox.Text.Trim().Replace(" ", ""));
                        command.Parameters.AddWithValue("@mail", mailBox.Text.Trim().Replace(" ", ""));

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Close();
                            musteriYukleyici.MusteriYukle();
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
