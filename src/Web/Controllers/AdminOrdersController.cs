using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminOrdersController : Controller
{
    private readonly AppDbContext _db;

    public AdminOrdersController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _db.Orders
            .OrderByDescending(o => o.CreatedAtUtc)
            .ToListAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var order = await _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (order == null) return NotFound();

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(Guid id, string status)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
            
        if (order == null) return NotFound();

        // Stok İade ve Düşme Kontrolü
        if (order.Status != "İptal Edildi" && status == "İptal Edildi")
        {
            // İptal edildi, stokları iade et
            foreach (var item in order.Items)
            {
                if (item.Product != null)
                {
                    item.Product.StockQuantity += item.Quantity;
                }
            }
        }
        else if (order.Status == "İptal Edildi" && status != "İptal Edildi")
        {
            // İptalden geri döndü, stokları tekrar düş
            foreach (var item in order.Items)
            {
                if (item.Product != null)
                {
                    item.Product.StockQuantity -= item.Quantity;
                }
            }
        }

        order.Status = status;
        _db.Update(order);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Sipariş durumu güncellendi.";
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }
}
