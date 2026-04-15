using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels.Admin;

namespace Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new AdminDashboardVm
        {
            ProductCount = await _db.Products.CountAsync(),
            OrderCount = await _db.Orders.CountAsync(o => o.Status != "İptal Edildi"),
            UserCount = await _db.Users.CountAsync(),
            CategoryCount = await _db.Categories.CountAsync(),
            LowStockProductCount = await _db.Products.CountAsync(p => p.StockQuantity > 0 && p.StockQuantity < 5),
            TotalRevenue = await _db.Orders.Where(o => o.Status != "İptal Edildi").SumAsync(o => (decimal?)o.TotalAmount) ?? 0m,
            RecentOrders = await _db.Orders
                .OrderByDescending(o => o.CreatedAtUtc)
                .Take(5)
                .Select(o => new AdminRecentOrderVm
                {
                    Id = o.Id,
                    CreatedAtUtc = o.CreatedAtUtc,
                    UserId = o.UserId,
                    UserEmail = _db.Users
                        .Where(u => u.Id == o.UserId)
                        .Select(u => u.Email)
                        .FirstOrDefault(),
                    TotalAmount = o.TotalAmount,
                    Status = o.Status
                })
                .ToListAsync()
        };

        return View(vm);
    }
}
