using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
    /// YapilacakEkleSayfasi.xaml etkileşim mantığı
    /// </summary>
    public partial class YapilacakEkleSayfasi : Window
    {
        public YapilacakEkleSayfasi()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
           if(!string.IsNullOrEmpty(metinBox.Text))
            {

                string metin = metinBox.Text.Trim();

                if(metin.Length <= 200)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                        {
                            connection.Open();

                            string query = "INSERT INTO ToDo (metin) VALUES (@metin)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@metin", metin);

                                int result = command.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    this.Close();
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
                else MessageBox.Show("En fazla 200 karakterlik girişler kabul edilmektedir.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Lütfen TO-DO listesi için bir şeyler yazın.", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
