using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipOtomasyonu
{
    public static class SqlServerBaglanti
    {
        // Sql Server Management Studio 18 ve üstü sürümler geçerlidir.
        // Sql Server üzerinde bir kullanıcı oluşturun; bu kullanıcıya yazma ve okuma iznini, proje içerisindeki veri tabanı için verin.
        // İster aşağıdaki bilgilerle aynı yapın, ister farklı yapıp aşağıya bilgileri girin. Sorunsuz şekilde çalışacaktır.

        public static string connectionString = "Data Source=localhost;Initial Catalog=StokTakip;User ID=StokTakipLogin;Password=batuhanbatuhan";

        public static string baglanti()
        {
            return connectionString;
        }
    }
}