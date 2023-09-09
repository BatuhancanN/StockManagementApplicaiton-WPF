using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;

namespace StokTakipOtomasyonu
{
    public class ListeYukleyici
    {
        public ObservableCollection<Musteri> Musteriler { get; set; }
        public ObservableCollection<ToDo> Yapilacaklar { get; set; }
        public ObservableCollection<Urun> Urunler { get; set; }
        public ObservableCollection<Alim> Alimlar { get; set; }
        public ObservableCollection<Satis> Satislar { get; set; }
        public ObservableCollection<Ozet> Ozetler { get; set; }
        SatisGecmisiPenceresi satisGecmisiPenceresi = new SatisGecmisiPenceresi();

        public ListeYukleyici()
        {
            Musteriler = new ObservableCollection<Musteri>();
            Yapilacaklar = new ObservableCollection<ToDo>();
            Urunler = new ObservableCollection<Urun>();
            Alimlar = new ObservableCollection<Alim>();
            Satislar = new ObservableCollection<Satis>();
            Ozetler = new ObservableCollection<Ozet>();

        }

        public void ToDoYukle()
        {
            Yapilacaklar.Clear();

            using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
            {
                connection.Open();

                string query = "SELECT * FROM ToDo";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string metin = reader.GetString(1);

                            ToDo toDo = new ToDo(id, metin);
                            Yapilacaklar.Add(toDo);
                        }
                    }
                }
            }
        }

        public void MusteriYukle()
        {
            Musteriler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Musteriler";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string tcvkn = reader.GetString(1);
                                string unvan = reader.GetString(2);
                                string telefon = reader.GetString(3);
                                string mail = reader.GetString(4);

                                Musteri musteri = new Musteri(id, tcvkn, unvan, telefon, mail);
                                Musteriler.Add(musteri);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        public void UrunYukle()
        {
            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM urunler";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
        public void SatisUrunYukle()
        {
            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM urunler where miktar > 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        public void AlimYukle(string seciliBarkod)
        {
            Alimlar.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM alimlar where barkod = @seciliBarkod order by tarih desc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@seciliBarkod", seciliBarkod);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                int alimMiktari = reader.GetInt32(2);
                                decimal alisFiyati = reader.GetDecimal(3);
                                string mensei = reader.GetString(4);
                                DateTime tarih = reader.GetDateTime(5);

                                Alim alim = new Alim(id, barkod, alimMiktari, alisFiyati, mensei, tarih);
                                Alimlar.Add(alim);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        public static void SatisGecmisiYukle(int secim, ListView satisListesi)
        {
            try
            {
                satisListesi.Items.Clear();

                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SatisGecmisi", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Secim", secim);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Tcvkn = reader["tcvkn"], Unvan = reader["unvan"], Barkod = reader["barkod"], Kategori = reader["kategori"], UrunAdi = reader["urunAdi"], Adet = reader["adet"], ToplamTutar = reader["toplamTutar"], Tarih = reader["tarih"] };
                            satisListesi.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hatalı İşlem Tespit Edildi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void MusteriAra(string kriter, string metin)
        {
            switch (kriter)
            {
            case "Tc/VKN": TcvknMusteriArama(metin); break;
            case "Unvan": UnvanlaMusteriArama(metin); break;
            }
        }
        public void UrunAra(string kriter, string metin)
        {
            switch (kriter)
            {
                case "Barkod": BarkodlaUrunArama(metin); break;
                case "Kategori": KategoriyleUrunArama(metin); break;
                case "Ürün Adı": AdlaUrunArama(metin); break;
            }
        }
        public void SatisUrunAra(string kriter, string metin)
        {
            switch (kriter)
            {
                case "Barkod": SatisBarkodlaUrunArama(metin); break;
                case "Kategori": SatisKategoriyleUrunArama(metin); break;
                case "Ürün Adı": SatisAdlaUrunArama(metin); break;
            }
        }
        public void DonemIciAlimAra(string seciliBarkod)
        {
            Alimlar.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();
                    DateTime arananTarih = DateTime.Now;

                    string query = "DECLARE @ay INT = MONTH(@tarih); SELECT * FROM alimlar where barkod = @seciliBarkod and month(tarih) = month(@tarih) order by tarih desc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@seciliBarkod", seciliBarkod);
                        command.Parameters.AddWithValue("@tarih", arananTarih);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                int alimMiktari = reader.GetInt32(2);
                                decimal alisFiyati = reader.GetDecimal(3);
                                string mensei = reader.GetString(4);
                                DateTime tarih = reader.GetDateTime(5);

                                Alim alim = new Alim(id, barkod, alimMiktari, alisFiyati, mensei, tarih);
                                Alimlar.Add(alim);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
        public void OncekiDonemAlimAra(string seciliBarkod)
        {
            Alimlar.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();
                    DateTime guncelTarih = DateTime.Now;
                    DateTime arananTarih = guncelTarih.AddMonths(-1);

                    string query = "DECLARE @ay INT = MONTH(@tarih); SELECT * FROM alimlar where barkod = @seciliBarkod and month(tarih) = month(@tarih) order by tarih desc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@seciliBarkod", seciliBarkod);
                        command.Parameters.AddWithValue("@tarih", arananTarih);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                int alimMiktari = reader.GetInt32(2);
                                decimal alisFiyati = reader.GetDecimal(3);
                                string mensei = reader.GetString(4);
                                DateTime tarih = reader.GetDateTime(5);

                                Alim alim = new Alim(id, barkod, alimMiktari, alisFiyati, mensei, tarih);
                                Alimlar.Add(alim);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        public void OzetYukle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("OzetHesapla", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // SqlCommand ile çalıştırılan stored procedure'ün sadece içinde bulunulan ay için hesaplama yapmasını istiyorsanız
                        // burada geçerli ayın başlangıç ve bitiş tarihlerini parametre olarak vermeniz gerekebilir
                        // Örnek olarak:
                        // command.Parameters.AddWithValue("@BaslangicTarihi", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                        // command.Parameters.AddWithValue("@BitisTarihi", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal maliyet = Convert.ToDecimal(reader["maliyet"]);
                                decimal kazanc = Convert.ToDecimal(reader["kazanc"]);
                                decimal net = Convert.ToDecimal(reader["net"]);

                                Ozet ozet = new Ozet(maliyet, kazanc, net);
                                Ozetler.Add(ozet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void TcvknMusteriArama(string aranan)
            {
                Musteriler.Clear();

                try
                {
                    using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                    {
                        connection.Open();

                        string query = "SELECT * FROM Musteriler WHERE tcvkn LIKE '%' + @tcvkn + '%'";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@tcvkn", aranan);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string tcvkn = reader.GetString(1);
                                    string unvan = reader.GetString(2);
                                    string telefon = reader.GetString(3);
                                    string mail = reader.GetString(4);

                                    Musteri musteri = new Musteri(id, tcvkn, unvan, telefon, mail);
                                    Musteriler.Add(musteri);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
        private void UnvanlaMusteriArama(string aranan)
        {
            Musteriler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Musteriler WHERE unvan LIKE '%' + @unvan + '%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@unvan", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string tcvkn = reader.GetString(1);
                                string unvan = reader.GetString(2);
                                string telefon = reader.GetString(3);
                                string mail = reader.GetString(4);

                                Musteri musteri = new Musteri(id, tcvkn, unvan, telefon, mail);
                                Musteriler.Add(musteri);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        private void BarkodlaUrunArama(string aranan)
        {

            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Urunler WHERE barkod LIKE '%' + @barkod + '%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@barkod", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

        }
        private void SatisBarkodlaUrunArama(string aranan)
        {

            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Urunler WHERE barkod LIKE '%' + @barkod + '%' and miktar > 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@barkod", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

        }
        private void KategoriyleUrunArama(string aranan)
        {

            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Urunler WHERE kategori LIKE '%' + @kategori + '%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@kategori", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
        private void SatisKategoriyleUrunArama(string aranan)
        {

            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Urunler WHERE kategori LIKE '%' + @kategori + '%' and miktar > 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@kategori", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
        private void AdlaUrunArama(string aranan)
        {

            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Urunler WHERE urunAdi LIKE '%' + @urunAdi + '%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@urunAdi", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

        }
        private void SatisAdlaUrunArama(string aranan)
        {

            Urunler.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlServerBaglanti.baglanti()))
                {
                    connection.Open();

                    string query = "SELECT * FROM Urunler WHERE urunAdi LIKE '%' + @urunAdi + '%' and miktar > 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@urunAdi", aranan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string barkod = reader.GetString(1);
                                string kategori = reader.GetString(2);
                                string marka = reader.GetString(3);
                                string urunAdi = reader.GetString(4);
                                int miktar = reader.GetInt32(5);
                                decimal maliyet = reader.GetDecimal(6);
                                decimal satis = reader.GetDecimal(7);

                                Urun urun = new Urun(id, barkod, kategori, marka, urunAdi, miktar, maliyet, satis);
                                Urunler.Add(urun);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

        }

    }
}