# 💎 Takıcı E-Commerce

Modern, şık ve kullanıcı dostu bir takı e-ticaret platformu. ASP.NET Core MVC ile geliştirilmiş bu proje, müşterileriniz için benzersiz bir alışveriş deneyimi sunarken, güçlü admin paneli ile mağaza ve sipariş yönetimini kolaylaştırır. 

![Takıcı E-Commerce](https://raw.githubusercontent.com/kullaniciadiniz/TakiciECommerce/main/docs/banner.png) 

## 🌟 Özellikler

### 🛍️ Müşteri Paneli
- **Şık Tasarım:** Kadın giyim ve takı konseptine uygun, rose gold ve pastel tonlarında modern, minimalist tasarım.
- **Kategori Bazlı Gezinme:** Kolye, bileklik, küpe, yüzük gibi kategoriler arasında kolayca gezinin.
- **Ürün Detayları & Sepet:** Detaylı ürün görünümleri, stok durumunu izleme ve akıcı sepet deneyimi.
- **Güvenli Kimlik Doğrulama:** ASP.NET Core Identity ile güvenli üyelik ve giriş işlemleri.
- **Sipariş Takibi:** Kullanıcıların sipariş durumlarını anlık öğrenmesi ve geçmiş siparişleri listeleyebilmesi.

### ⚙️ Yönetici (Admin) Paneli
- **Gelişmiş Dashboard:** Kolay kullanılabilen arayüz, satışları ve sipariş durumlarını özetleme.
- **Ürün & Kategori Yönetimi:** Yeni ürün ekleme, görsel yükleme, kategori düzenleme ve stok kontrolü.
- **Sipariş Yönetimi:** Müşteri siparişlerini kontrol etme ve durumlarını (`Yeni`, `Hazırlanıyor`, `Kargolandı`, `İptal Edildi` vb.) güncelleme.
- **Mesajlaşma & Bildirim:** Admin paneli içerisinde gerçek zamanlı bildirimler ve yetkililer arası iletişim modülü.

## 🛠️ Kullanılan Teknolojiler

- **Backend:** ASP.NET Core 8.0 MVC (C#)
- **Veritabanı:** Entity Framework Core & SQL Server
- **Kimlik Yönetimi:** ASP.NET Core Identity
- **Frontend:** HTML5, CSS3 (Modern ve duyarlı tasarım, animasyonlar), JavaScript, Bootstrap 5

## 🚀 Kurulum & Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz.

### Gereksinimler
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) veya daha güncel bir sürüm
- [SQL Server](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads) (veya LocalDB)
- Geliştirme ortamı (Visual Studio 2022, Rider veya VS Code)

### Adımlar

1. **Projeyi Klonlayın**
   ```bash
   git clone https://github.com/kullaniciadiniz/TakiciECommerce.git
   cd TakiciECommerce
   ```

2. **Veritabanı Bağlantısını Yapılandırın**
   `src/Web/appsettings.json` dosyasını açın ve `DefaultConnection` dizesini kendi veritabanınıza göre güncelleyin. *(Varsayılan olarak LocalDB ayarlanmıştır)*
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TakiciECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

3. **Veritabanını Oluşturun (Migrations)**
   Proje dizininde veya Packet Manager Console üzerinde EF Core Migration komutunu çalıştırın:
   ```bash
   cd src/Web
   dotnet ef database update
   ```
   *(Eğer seed data mevcutsa, veritabanı oluşturulurken otomatik olarak örnek kategoriler veya yönetici hesabı eklenebilir).*

4. **Projeyi Başlatın**
   ```bash
   dotnet run
   ```
   Uygulama çalıştıktan sonra tarayıcınızdan `http://localhost:5051` (veya belirtilen port üzerinden) siteye erişim sağlayabilirsiniz.

---



**Geliştirici:** Büşra Güneş / [GitHub Profili](https://github.com/busragunes06)  
*Lisans, geri bildirim veya sorularınız için projeyi inceleyebilir, issues sekmesinden takip edebilirsiniz.*
