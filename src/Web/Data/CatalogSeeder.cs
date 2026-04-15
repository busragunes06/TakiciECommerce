using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data;

public static class CatalogSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Kategorileri Name (İsim) alanına göre sözlüğe alıyoruz
        var categories = await db.Categories.ToDictionaryAsync(x => x.Name);

        // Slug parametrelerini kaldırdık, sadece İsim gönderiyoruz
        var kolye = GetOrCreateCategory(categories, db, "Kolye");
        var bileklik = GetOrCreateCategory(categories, db, "Bileklik");
        var yuzuk = GetOrCreateCategory(categories, db, "Yüzük");
        var kupe = GetOrCreateCategory(categories, db, "Küpe");

        var existingProductNames = await db.Products
            .AsNoTracking()
            .Select(x => x.Name)
            .ToHashSetAsync();

        // Kolyeler (5 Adet)
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Zarif Minimal Kalp Kolye",
            Description = "Gunluk kullanima uygun, zarif ve hafif tasarim.",
            Price = 499.90m,
            StockQuantity = 12,
            Category = kolye,
            ImageUrl = "https://images.unsplash.com/photo-1599643478524-fb66f7f41530?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "İnci Detaylı Zarif Kolye",
            Description = "Ozel gunler icin sade ama dikkat cekici bir model.",
            Price = 799.90m,
            StockQuantity = 6,
            Category = kolye,
            ImageUrl = "https://images.unsplash.com/photo-1611591437281-460bfbe1220a?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Rose Gold Işıltılı Kolye",
            Description = "Yüksek kaliteli rose gold kaplama, modern tasarim.",
            Price = 849.00m,
            StockQuantity = 9,
            Category = kolye,
            ImageUrl = "https://images.unsplash.com/photo-1601121141461-9d6647bca1ed?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Gümüş İğne Uçlu Kolye",
            Description = "Klasikten vazgecemeyenler icin ozel uretim ince narin kolye.",
            Price = 649.90m,
            StockQuantity = 15,
            Category = kolye,
            ImageUrl = "https://images.unsplash.com/photo-1535632066927-ab7c9ab60908?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Katmanlı Günlük Kolye",
            Description = "Modern kombinlere uyum saglayan katmanli dokusu.",
            Price = 949.90m,
            StockQuantity = 7,
            Category = kolye,
            ImageUrl = "https://images.unsplash.com/photo-1515562141207-7a8ea4114e17?w=600&h=600&fit=crop"
        });

        // Bileklikler (5 Adet)
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Charm Detaylı İnce Bileklik",
            Description = "Hareketli charm detaylariyla enerjik ve genc stil.",
            Price = 429.90m,
            StockQuantity = 14,
            Category = bileklik,
            ImageUrl = "https://images.unsplash.com/photo-1611591437198-d102a0a2faee?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Gümüş Örgü Bileklik",
            Description = "Paslanmaz celik govde uzerine zarif gumus isleme.",
            Price = 649.90m,
            StockQuantity = 18,
            Category = bileklik,
            ImageUrl = "https://images.unsplash.com/photo-1596944924616-7b38e7cfac36?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Klasik İnci Bileklik",
            Description = "Dogal tatli su incilerinden tasarlanmis premium kalite.",
            Price = 899.00m,
            StockQuantity = 5,
            Category = bileklik,
            ImageUrl = "https://images.unsplash.com/photo-1573408301145-b98c46544405?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Pembe Kuvars Taşlı Bileklik",
            Description = "Dogal pembe kuvars tasi ile gune enerji katan ozel tasarim.",
            Price = 349.90m,
            StockQuantity = 22,
            Category = bileklik,
            ImageUrl = "https://images.unsplash.com/photo-1537233214812-9c3a37e1ba5c?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Çelik Kelepçe Bileklik",
            Description = "Modern is kadinlari icin sofistike cizgili kelepce model.",
            Price = 479.90m,
            StockQuantity = 11,
            Category = bileklik,
            ImageUrl = "https://images.unsplash.com/photo-1628151015968-3a4429e9ef04?w=600&h=600&fit=crop"
        });

        // Yüzükler (5 Adet)
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Pırlanta Kesim Tektaş Yüzük",
            Description = "Ozel anlar icin parlak iscilikle hazirlanan premium model.",
            Price = 12500.00m,
            StockQuantity = 3,
            Category = yuzuk,
            ImageUrl = "https://images.unsplash.com/photo-1605100804763-247f67b4548e?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Minimalist Gümüş Alyans",
            Description = "Gundelik kullanim icin sadeligi sevenlere ince gumus cizgiler.",
            Price = 499.00m,
            StockQuantity = 25,
            Category = yuzuk,
            ImageUrl = "https://images.unsplash.com/photo-1589674781759-c21c37956a44?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Vintage Dokulu Yüzük",
            Description = "Eskitme gorunumlu yuzeyiyle nostaljik ve iddiali tasarim.",
            Price = 679.90m,
            StockQuantity = 7,
            Category = yuzuk,
            ImageUrl = "https://images.unsplash.com/photo-1611591437125-d069da217c2a?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Renkli Taşlı Tasarım Yüzük",
            Description = "Renkli zirkon taslar ile isiltiyi doruklarda yasayin.",
            Price = 850.00m,
            StockQuantity = 10,
            Category = yuzuk,
            ImageUrl = "https://images.unsplash.com/photo-1599643477874-c50ebe647206?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Altın Sarısı Nişan Yüzüğü",
            Description = "Aşkın simgesi zarif altin sarisi gorunum.",
            Price = 8990.00m,
            StockQuantity = 2,
            Category = yuzuk,
            ImageUrl = "https://images.unsplash.com/photo-1574634534894-89d7576c8259?w=600&h=600&fit=crop"
        });

        // Küpeler (5 Adet)
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Klasik Çizgili Halka Küpe",
            Description = "Klasik halka formunda, cizgili desenlerle suslenmis konforlu kullanim.",
            Price = 379.90m,
            StockQuantity = 15,
            Category = kupe,
            ImageUrl = "https://images.unsplash.com/photo-1535632787358-b0e4e9336e8b?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Rose Gold Damla Küpe",
            Description = "Damla seklindeki tasarimiyla zarafeti yansitan ince iscilik.",
            Price = 459.00m,
            StockQuantity = 12,
            Category = kupe,
            ImageUrl = "https://images.unsplash.com/photo-1630019852942-f89202989a59?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Gümüş Top Küpe Seti",
            Description = "Gundelik sikligi tamamlayan basic gumus kupe.",
            Price = 249.90m,
            StockQuantity = 30,
            Category = kupe,
            ImageUrl = "https://images.unsplash.com/photo-1588444837495-c6cfcb75f07c?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Zirkon Taşlı Sallantılı Küpe",
            Description = "Ozel davetlerinizde isilti sacacaginiz premium kupe modeli.",
            Price = 899.90m,
            StockQuantity = 8,
            Category = kupe,
            ImageUrl = "https://images.unsplash.com/photo-1617264789501-8fc2bd14ae25?w=600&h=600&fit=crop"
        });
        AddProductIfMissing(db, existingProductNames, new Product
        {
            Name = "Geometrik Modern Küpe",
            Description = "Keskin hatli geometrik formu ile dikkat ceken kupe.",
            Price = 389.90m,
            StockQuantity = 11,
            Category = kupe,
            ImageUrl = "https://images.unsplash.com/photo-1608042314453-ae338d80c427?w=600&h=600&fit=crop"
        });

        await db.SaveChangesAsync();
    }

    private static Category GetOrCreateCategory(
        IDictionary<string, Category> categories,
        AppDbContext db,
        string name)
    {
        // Artık sadece Name üzerinden Dictionary'de arama yapıyoruz
        if (categories.TryGetValue(name, out var category))
            return category;

        category = new Category
        {
            Name = name
            // Slug ataması tamamen silindi
        };

        categories[name] = category;
        db.Categories.Add(category);
        return category;
    }

    private static void AddProductIfMissing(AppDbContext db, ISet<string> existingProductNames, Product product)
    {
        if (!existingProductNames.Add(product.Name))
            return;

        db.Products.Add(product);
    }
}