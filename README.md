# WPF ve MSSQL ile Stok/İş takibi uygulaması

Bu bir iş takibi otomasyonudur. SQL server ile tüm veriler güvenli bir şekilde tutulmaktadır. Sade, modern ve kullanıcı dostu tasarımıyla kullanması en basit uygulamadır.

Müşteri kayıtlarını ayrı, ürünler kayıtlarını ayrı ve satış bilgilerini ayrı tablolarda saklar ve ilişkisel veritabanı yöntemlerini içerir. "stored procedure" ile veritabanı tasarımını güçlendirdim.

Ürün yönetimi bölümünde ilk olarak ürünü bilgileriyle kayıt etmeniz ve ardından alım için diğer butona tıklamanız gerekmektedir. Yani her alımda aynı ürünü bir daha kaydederek performansa etki edecek basit veretabanı tasarımından kaçınılmış, sadece alımlarda önceden kaydedilmiş ürünün stok miktarı güncellenmiştir.

Satış bölümünde ilk olarak müşteri-ürün eşleştirmesi yapmanız gerekmektedir. Ardından satış miktarı girip tek tuşla satış işlemini tamamlayabilirsiniz. Daha sonrasında hem alım hem de satış işlemlerinin geriye dönük kayıtlarına bakabilirsiniz.

Uygulamanın tüm kodlarını repoya ekledim. Kendiniz denemek isterseniz öncelikle kodlardan sql bağlantısıyla ilhili class yapısını bulup içindeki bilgleri kendinize göre ayarlayın, sql server içerisinde o bilgilerle login oluşturun ve ardından setup projesini yeniden derleyin. Derleme sonucu elde edilen setup dosyasıyla uygulamayı kurun ve ".bak" dosyasını sql server ile çalıştırın. Uygulamayı incelemeye başlayabilirsiniz.

Bu uygulamayı kendimi geliştirmek ve potansiyelimi özgeçmişimde göstermek amacıyla öğrenerek aşama aşama geliştirdim. İlerleyen zamanlarda daha farklı teknolojilerle ve daha gelişmiş uygulamalarla karşınızda olacağım.

