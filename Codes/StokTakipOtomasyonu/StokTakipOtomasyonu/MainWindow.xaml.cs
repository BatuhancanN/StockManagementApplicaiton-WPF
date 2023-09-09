using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StokTakipOtomasyonu
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        string baglantiString = SqlServerBaglanti.baglanti();
        ListeYukleyici listeYukleyici = new ListeYukleyici();
        public ObservableCollection<ToDo> Yapilacaklar { get; set; }
        public ObservableCollection<Ozet> Ozetler { get; set; } = new ObservableCollection<Ozet>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            listeYukleyici.ToDoYukle();
            Yapilacaklar = listeYukleyici.Yapilacaklar;
            yapilacaklarListesi.ItemsSource = Yapilacaklar;

        }

        private void btnYapilacakEkle_Click(object sender, RoutedEventArgs e)
        {
            YapilacakEkleSayfasi yapilacakEkleSayfasi = new YapilacakEkleSayfasi();
            yapilacakEkleSayfasi.ShowDialog();
            listeYukleyici.ToDoYukle();
        }


        public ToDo SeciliToDo { get; set; }
        private void yapilacaklarListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeciliToDo = (ToDo)yapilacaklarListesi.SelectedItem;
        }

        private void btnYapilacakSil_Click(object sender, RoutedEventArgs e)
        {
            if(SeciliToDo != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(baglantiString))
                    {
                        connection.Open();

                        string query = "delete from ToDo where ID = @ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", SeciliToDo.ID);
                            command.Parameters.AddWithValue("@metin", SeciliToDo.Metin);

                            int result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                listeYukleyici.ToDoYukle();
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
            else MessageBox.Show("Lütfen bir ToDo seçin", "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnMusteriYonetim_Click(object sender, RoutedEventArgs e)
        {
            MusteriYonetimiPenceresi musteriYonetimiPenceresi = new MusteriYonetimiPenceresi();
            musteriYonetimiPenceresi.ShowDialog();
        }

        private void btnUrunYonetim_Click(object sender, RoutedEventArgs e)
        {
            UrunYonetimiPenceresi urunYonetimiPenceresi = new UrunYonetimiPenceresi();
            urunYonetimiPenceresi.ShowDialog();
        }

        private void btnSatisYonetim_Click(object sender, RoutedEventArgs e)
        {
            SatisYonetimiPenceresi satisYonetimiPenceresi = new SatisYonetimiPenceresi();
            satisYonetimiPenceresi.ShowDialog();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string webSiteURL = "https://batuhancann.github.io/";

            Process.Start(webSiteURL);
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
