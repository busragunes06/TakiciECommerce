using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.ViewModels.Orders;
using Web.ViewModels.Checkout;

namespace Web.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrdersController(AppDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet("/siparisler")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId)) return Challenge();

        var orders = await _db.Orders
            .AsNoTracking()
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAtUtc)
            .Select(o => new OrderListItemVm
            {
                Id = o.Id,
                CreatedAtUtc = o.CreatedAtUtc,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                ItemCount = o.Items.Sum(i => i.Quantity)
            })
            .ToListAsync();

        return View(orders);
    }

    [HttpGet("/siparisler/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId)) return Challenge();

        var order = await _db.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Where(o => o.UserId == userId && o.Id == id)
            .Select(o => new OrderDetailsVm
            {
                Id = o.Id,
                CreatedAtUtc = o.CreatedAtUtc,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                ContactName = o.ContactName,
                PhoneNumber = o.PhoneNumber,
                ShippingAddress = o.ShippingAddress,
                City = o.City,
                PaymentMethod = o.PaymentMethod,
                Items = o.Items.Select(i => new OrderDetailsItemVm
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product != null ? i.Product.Name : "Ürün",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (order is null) return NotFound();
        return View(order);
    }

    [HttpPost("/siparis/olustur")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFromCart()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId)) return Challenge();

        await using var tx = await _db.Database.BeginTransactionAsync();

        var cart = await _db.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart is null || cart.Items.Count == 0)
            return RedirectToAction("Index", "Cart");

        // Validate stock & compute totals
        foreach (var item in cart.Items)
        {
            if (item.Product is null || !item.Product.IsActive)
                return BadRequest("Sepette geçersiz ürün var.");

            if (item.Product.StockQuantity < item.Quantity)
                return BadRequest("Stok değişti. Sepeti güncelleyin.");
        }

        var order = new Order
        {
            UserId = userId,
            Status = "Hazirlaniyor",
        };

        decimal total = 0m;
        foreach (var item in cart.Items)
        {
            var product = item.Product!;
            product.StockQuantity -= item.Quantity;

            var line = new OrderItem
            {
                Order = order,
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };

            order.Items.Add(line);
            total += line.UnitPrice * line.Quantity;
        }

        order.TotalAmount = total;

        _db.Orders.Add(order);
        cart.Items.Clear();
        cart.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        return RedirectToAction("Details", new { id = order.Id });
    }

    // Gerekli namespace'leri (Web.ViewModels.Checkout vb.) yukarıya eklemeyi unutma.

[HttpGet("/siparis-tamamla")]
public async Task<IActionResult> Checkout()
{
    var userId = _userManager.GetUserId(User);
    if (string.IsNullOrEmpty(userId)) return Challenge();
    
    // Sepeti ve içindeki ürünleri getir
    var cart = await _db.Carts
        .Include(c => c.Items)
        .ThenInclude(i => i.Product)
        .FirstOrDefaultAsync(c => c.UserId == userId);

    // Sepet boşsa sepete geri yolla
    if (cart == null || !cart.Items.Any()) 
        return RedirectToAction("Index", "Cart");
        
    var model = new CheckoutVm
    {
        // Sepetteki ürünlerin fiyatı x miktarını toplayıp CartTotal'a atıyoruz
        CartTotal = cart.Items.Sum(i => i.Quantity * i.Product!.Price)
    };
    
    return View(model); // <--- DÜZELTİLEN SATIR
}
[HttpPost("/siparis-tamamla")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Checkout(CheckoutVm model)
{
    if (!ModelState.IsValid)
        return View(model);

    var userId = _userManager.GetUserId(User);
    if (string.IsNullOrEmpty(userId)) return Challenge();

    var cart = await _db.Carts
        .Include(c => c.Items)
        .ThenInclude(i => i.Product)
        .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null || !cart.Items.Any()) 
        return RedirectToAction("Index", "Cart");
        
    // Stok düşme işlemi
    foreach (var item in cart.Items)
    {
        if (item.Product != null)
        {
            if (item.Product.StockQuantity >= item.Quantity)
            {
                item.Product.StockQuantity -= item.Quantity;
            }
            else
            {
                ModelState.AddModelError("", $"{item.Product.Name} isimli ürün için yeterli stok bulunmuyor. Kalan stok: {item.Product.StockQuantity}");
                return View(model);
            }
        }
    }

    // Formdan gelen bilgilerle yeni siparişi oluştur
    var order = new Order
    {
        UserId = userId,
        Status = "Hazirlaniyor",
        TotalAmount = cart.Items.Sum(i => i.Quantity * i.Product!.Price),
        ContactName = model.ContactName,
        PhoneNumber = model.PhoneNumber,
        ShippingAddress = model.ShippingAddress,
        City = model.City,
        PaymentMethod = model.PaymentMethod,
        Items = cart.Items.Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            UnitPrice = i.Product!.Price
        }).ToList()
    };

    _db.Orders.Add(order);

    // Sepeti siparişe dönüştürdüğümüz için sepetin içini boşaltıyoruz
    _db.CartItems.RemoveRange(cart.Items);
    
    await _db.SaveChangesAsync();

    // Veriler DB'ye kaydedildi! Şimdi sipariş detay sayfasına (faturaya) gidebiliriz.
    return RedirectToAction("Details", new { id = order.Id });
}
}

