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
    /// MusteriYonetimiPenceresi.xaml etkileşim mantığı
    /// </summary>
    public partial class MusteriYonetimiPenceresi : Window
    {
        private ListeYukleyici listeYukleyici = new ListeYukleyici();
        public ObservableCollection<Musteri> Musteriler { get; set; }

        public MusteriYonetimiPenceresi()
        {
            InitializeComponent();
            DataContext = this;
            listeYukleyici.MusteriYukle();
            Musteriler = listeYukleyici.Musteriler;
            musteriListesi.ItemsSource = Musteriler;
        }

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            MusteriEklemePenceresi musteriEklemePenceresi = new MusteriEklemePenceresi();
            musteriEklemePenceresi.ShowDialog();
            listeYukleyici.MusteriYukle();
        }


        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if(SeciliMusteri != null)
            {
                if (!string.IsNullOrEmpty(unvanBox.Text))
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                        {
                            connection.Open();

                            string query = "UPDATE Musteriler SET tcvkn=@tcvkn, unvan=@unvan, telefon=@telefon, mail=@mail WHERE ID=@id";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@id", SeciliMusteri.ID);
                                command.Parameters.AddWithValue("@tcvkn", tcvknBox.Text.Trim().Replace(" ", ""));
                                command.Parameters.AddWithValue("@unvan", MetinKontrolu.BasHarfBuyut(unvanBox.Text));
                                command.Parameters.AddWithValue("@telefon", telefonBox.Text.Trim().Replace(" ", ""));
                                command.Parameters.AddWithValue("@mail", mailBox.Text.Trim().Replace(" ", ""));

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    listeYukleyici.MusteriYukle();
                                    MessageBox.Show("Müşteri bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Müşteri bilgileri güncellenirken bir hata oluştu.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("Lütfen unvan alanını doldurun.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Lütfen bir müşteri seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            listeYukleyici.MusteriYukle();
            tcvknBox.Text = string.Empty;
            unvanBox.Text = string.Empty;
            telefonBox.Text = string.Empty;
            mailBox.Text = string.Empty;
        }

        public Musteri SeciliMusteri { get; set; }
        private void musteriListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (musteriListesi.SelectedItem != null)
            {
                SeciliMusteri = (Musteri)musteriListesi.SelectedItem;

                tcvknBox.Text = SeciliMusteri.Tcvkn;
                unvanBox.Text = SeciliMusteri.Unvan;
                telefonBox.Text = SeciliMusteri.Telefon;
                mailBox.Text = SeciliMusteri.Mail;
            }
            else
            {
                SeciliMusteri = null;
                tcvknBox.Text = "";
                unvanBox.Text = "";
                telefonBox.Text = "";
                mailBox.Text = "";
            }
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (SeciliMusteri != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                    {
                        connection.Open();

                        string query = "delete from Musteriler where ID = @ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", SeciliMusteri.ID);

                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                listeYukleyici.MusteriYukle();
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
            else MessageBox.Show("Lütfen bir müşteri seçin.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(unvanBox.Text))
            {
                listeYukleyici.MusteriAra("Unvan", unvanBox.Text.Trim());
            }
            else MessageBox.Show("Lütfen aramak istediğiniz unvanı yazın.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
