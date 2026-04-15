#Takıcı E-Commerce

#1. Giriş
Günümüzde internetin yaygınlaşmasıyla ticaret anlayışı dijital ortama taşınmıştır. Bu proje kapsamında, bir takı işletmesi için kullanıcı dostu, güvenli, modern ve işlevsel bir e-ticaret web sitesi geliştirilmesi hedeflenmektedir.

Hazırlanan web sitesi sayesinde müşteriler mağazaya gitmeden ürünleri inceleyebilecek, sepetlerine ekleyebilecek, sipariş verebilecek ve işletme ile kolayca iletişime geçebilecektir.

#2. Proje Tanımı
Bu proje; kolye, yüzük, bileklik, küpe ve benzeri takı ile aksesuar ürünlerinin internet üzerinden satışını gerçekleştirmek amacıyla tasarlanmış bir web uygulamasıdır.

Sistem iki temel bölümden oluşmaktadır:

Kullanıcı (Müşteri) paneli

Yönetici (Admin) paneli

Kullanıcılar ürünleri görüntüleyip satın alma sürecini yönetirken, yöneticiler ürün, kategori, sipariş ve stok işlemlerini kontrol edebilmektedir.

#3. Projenin Amacı
Projenin temel amacı; modern, güvenli ve yönetilebilir bir takı e-ticaret platformu oluşturarak müşteri memnuniyetini artırmak ve işletmenin satış sürecini dijital ortama taşımaktır.

Bu kapsamda hedeflenen başlıca kazanımlar şunlardır:

Takı ürünlerinin online ortamda satışa sunulması

İşletmenin satış potansiyelinin artırılması

Müşterilere 7/24 erişilebilir satış imkanı sağlanması

Marka bilinirliğinin güçlendirilmesi

Sipariş ve stok süreçlerinin kolaylaştırılması

#4. Projenin Kapsamı
Proje aşağıdaki ana başlıkları kapsamaktadır:

Web sitesi arayüz tasarımı

Kullanıcı kayıt ve giriş sistemi

Ürün listeleme ve ürün detay sayfaları

Sepet ve sipariş işlemleri

Admin paneli oluşturulması

Veritabanı tasarımı

Güvenlik ve yetkilendirme mekanizmaları

#5. Hedef Kitle
Bu web sitesinin hedef kitlesi şu şekilde belirlenmiştir:

Takı ve aksesuar kullanmayı seven kadın kullanıcılar

Erkek aksesuarlarıyla ilgilenen kullanıcılar

Doğum günü, sevgililer günü, yıl dönümü gibi özel günler için hediye arayan kişiler

Online alışverişi tercih eden kullanıcılar

#6. Web Sitesi Genel Özellikleri
Mobil, tablet ve masaüstü cihazlarla uyumlu tasarım

Kullanıcı dostu arayüz

Görsel ağırlıklı modern ürün sunumu

Hızlı sayfa geçişleri ve basit menü yapısı

Güvenli giriş ve yetkilendirme altyapısı

Ürün listeleme, kategori bazlı gezinti ve detay sayfaları

#7. Kullanıcı (Müşteri) Paneli Özellikleri
#7.1 Üyelik ve Giriş Sistemi
Kullanıcı kaydı

E-posta ve şifre ile giriş

Oturum kapatma işlemi

#7.2 Ana Sayfa
Öne çıkan ürünler

Yeni eklenen ürünler

Kategorilere hızlı yönlendirme

#7.3 Ürün Kategorileri
Kolye

Bileklik

Yüzük

Küpe

Toka

Set ürünler

#7.4 Ürün Detay Sayfası
Ürün görselleri

Ürün adı ve açıklaması

Fiyat bilgisi

Stok durumu

Sepete ekleme işlemi

#7.5 Sepet ve Sipariş
Sepette ürün görüntüleme

Ürün adet artırma ve azaltma

Sepetten ürün silme

Sipariş oluşturma

Sipariş detaylarını görüntüleme

#7.6 Sipariş Geçmişi
Önceki siparişleri listeleme

Sipariş durumunu takip etme

#7.7 İletişim Sayfası
İletişim bilgileri

Sosyal medya ve mağaza tanıtım alanı için uygun yapı

İşletmeye doğrudan mesaj gönderme formu

#8. Admin (Yönetici) Paneli Özellikleri
#8.1 Admin Giriş Sistemi
Yetkilendirilmiş admin girişi

Rol bazlı erişim kontrolü

#8.2 Ürün Yönetimi
Yeni ürün ekleme

Ürün güncelleme

Ürün silme

Ürün görseli ekleme

#8.3 Kategori Yönetimi
Kategori ekleme

Kategori güncelleme

Kategori silme

#8.4 Sipariş Yönetimi
Tüm siparişleri görüntüleme

Sipariş detayını inceleme

Sipariş durumu güncelleme

#8.5 Stok Yönetimi
Ürün stok bilgisini takip etme

Sipariş oluştuğunda stok düşürme

#8.6 Kullanıcı Yönetimi
Rol bazlı yönetim altyapısı bulunmaktadır

Sistem, kullanıcı bazlı sipariş ve sepet ilişkilerini desteklemektedir

#8.7 İletişim Mesajları Yönetimi
Kullanıcılardan iletişim formu aracılığıyla gelen mesajların listelenmesi ve okunması

#9. Veritabanı Tasarımı
Projede SQL Server ve Entity Framework Core kullanılmaktadır. Temel tablolar/sınıflar şu yapıdadır:

Kullanıcılar (ApplicationUser)

Kategoriler (Category)

Ürünler (Product)

Sepetler (Cart)

Sepet Ürünleri (CartItem)

Siparişler (Order)

Sipariş Ürünleri (OrderItem)

İletişim Mesajları (ContactMessage)

#10. Güvenlik Önlemleri
ASP.NET Core Identity ile kimlik doğrulama

Admin ve kullanıcı için yetkilendirme (Authorization) kontrolü

E-posta bazlı benzersiz kullanıcı kaydı

HTTPS yönlendirmesi

Verilerin ilişkisel ve kontrollü biçimde (Cascade/Restrict) saklanması

Anti-Forgery token ile CSRF koruması

#11. Performans ve Kullanılabilirlik
Optimize ürün listeleme yapısı

Basit ve anlaşılır sayfa akışı

Görsel destekli ürün sunumu

Mobil uyumluluğu destekleyen arayüz yapısı

#12. Projenin Sağlayacağı Faydalar
İşletme için daha geniş müşteri kitlesine ulaşma

Satışların dijital kanaldan artırılması

Marka gücünün dijital ortamda desteklenmesi

Zaman ve maliyet tasarrufu

Ürün, sipariş ve kategori yönetiminde kolaylık

#13. Kullanılan Teknolojiler
ASP.NET Core MVC (.NET 8/9)

Entity Framework Core

SQL Server

ASP.NET Core Identity

Razor View yapısı

Bootstrap ve özel CSS

#14. Mevcut Kod Tabanında Bulunan Modüller
Bu repository içinde şu modüller aktif olarak yer almaktadır:

Kullanıcı kayıt ve giriş işlemleri

Ürün listeleme ve ürün detay sayfaları

Kategori bazlı (Guid altyapılı) ürün gezintisi

Sepet işlemleri

Sipariş oluşturma ve sipariş geçmişi

Admin ürün yönetimi

Admin kategori yönetimi

Admin sipariş yönetimi

Admin gelen kutusu (İletişim formları)

İletişim sayfası

#15. Çalıştırma Bilgisi
Uygulama varsayılan olarak SQL Server LocalDB bağlantısı ile çalışacak şekilde ayarlanmıştır.

Örnek bağlantı dizesi (Connection String):

JSON
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TakiciECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
Genel çalıştırma adımları:

Veritabanı bağlantısını src/Web/appsettings.json dosyasında kontrol edin.

Çözümü Visual Studio veya terminal üzerinden (dotnet build) açın.

Migration ve veritabanı oluşturma işlemlerini (dotnet ef database update) uygulayın.

Projeyi başlatıp (dotnet run) kullanıcı ve admin akışlarını test edin.
