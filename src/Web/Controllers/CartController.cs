using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models; // Cart ve CartItem modellerine erişim için
using Web.ViewModels.Cart;

namespace Web.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(AppDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet("/sepet")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId)) return Challenge();

        var cart = await _db.Carts
            .Include(c => c.Items) // Ürünleri dahil et
            .ThenInclude(i => i.Product) // Ürün detaylarını dahil et
             .Where(c => c.UserId == userId)
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .Select(c => new CartVm
            {
                Items = c.Items
                    .OrderBy(i => i.Product!.Name)
                    .Select(i => new CartItemVm
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product!.Name,
                        UnitPrice = i.Product.Price,
                        StockQuantity = i.Product.StockQuantity,
                        Quantity = i.Quantity
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return View(cart ?? new CartVm());
    }

  [HttpPost("/sepet/ekle")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Add(Guid productId, int quantity = 1)
{
    var userId = _userManager.GetUserId(User);
    if (string.IsNullOrEmpty(userId)) return Challenge();

    // 1. Kullanıcının sepetini bul (Yoksa oluştur)
    var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
    {
        cart = new Cart { UserId = userId, UpdatedAtUtc = DateTime.UtcNow };
        _db.Carts.Add(cart);
        await _db.SaveChangesAsync(); // CartId oluşması için şart
    }

    // 2. CartItem'ı doğrudan veritabanından sorgula (Tracking açık kalsın)
    var cartItem = await _db.CartItems
        .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

    if (cartItem == null)
    {
        // Yeni öğe oluştur
        var newItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = productId,
            Quantity = quantity
        };
        _db.CartItems.Add(newItem);
    }
    else
    {
        // Mevcut öğeyi güncelle
        cartItem.Quantity += quantity;
        _db.Entry(cartItem).State = EntityState.Modified; 
    }

    // 3. Sepet güncelleme tarihini yenile
    cart.UpdatedAtUtc = DateTime.UtcNow;
    _db.Entry(cart).State = EntityState.Modified;

    try 
    {
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    catch (DbUpdateConcurrencyException)
    {
        // Çakışma olursa takip edilen verileri temizle ve tekrar dene mantığı
        _db.ChangeTracker.Clear();
        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        return BadRequest("Hata ayrıntısı: " + ex.InnerException?.Message ?? ex.Message);
    }
}
    [HttpPost("/sepet/artir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Increase(Guid productId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId)) return Challenge();

        var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
        var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);
        
        if (item == null) return RedirectToAction("Index");

        var stock = await _db.Products.Where(p => p.Id == productId).Select(p => p.StockQuantity).FirstOrDefaultAsync();
        
        if (item.Quantity < stock)
        {
            item.Quantity++;
            cart!.UpdatedAtUtc = DateTime.UtcNow;
            _db.Entry(item).State = EntityState.Modified;
            await SafeSaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    [HttpPost("/sepet/azalt")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Decrease(Guid productId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId)) return Challenge();

        var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
        var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                _db.Entry(item).State = EntityState.Modified;
            }
            else
            {
                cart!.Items.Remove(item);
            }
            cart!.UpdatedAtUtc = DateTime.UtcNow;
            await SafeSaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    [HttpPost("/sepet/sil")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(Guid productId)
    {
        var userId = _userManager.GetUserId(User);
        var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
        var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            cart!.Items.Remove(item);
            cart.UpdatedAtUtc = DateTime.UtcNow;
            await SafeSaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    // Helper metod: Concurrency hatalarını yönetmek için
    private async Task<IActionResult> SafeSaveChangesAsync()
    {
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Veritabanı ile Context uyuşmazlığı olursa ChangeTracker'ı temizle
            _db.ChangeTracker.Clear();
        }
        return RedirectToAction("Index");
    }

    [HttpGet("/sepet/ekle")]
    [HttpGet("/sepet/artir")]
    [HttpGet("/sepet/azalt")]
    [HttpGet("/sepet/sil")]
    public IActionResult RedirectToCart() => RedirectToAction("Index");
}