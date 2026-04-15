using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels.Admin;

namespace Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUsersController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminUsersController(AppDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var adminRoleId = await _db.Roles
            .Where(r => r.Name == "Admin")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        var adminIdSet = string.IsNullOrWhiteSpace(adminRoleId)
            ? new HashSet<string>()
            : (await _db.UserRoles
                .Where(ur => ur.RoleId == adminRoleId)
                .Select(ur => ur.UserId)
                .ToListAsync())
                .ToHashSet();

        var users = await _db.Users
            .OrderBy(u => u.Email)
            .Select(u => new AdminUserListItemVm
            {
                Id = u.Id,
                Email = u.Email ?? "-",
                PhoneNumber = u.PhoneNumber,
                EmailConfirmed = u.EmailConfirmed,
                OrderCount = _db.Orders.Count(o => o.UserId == u.Id),
                TotalSpent = _db.Orders
                    .Where(o => o.UserId == u.Id)
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0m,
                LastOrderUtc = _db.Orders
                    .Where(o => o.UserId == u.Id)
                    .OrderByDescending(o => o.CreatedAtUtc)
                    .Select(o => (DateTime?)o.CreatedAtUtc)
                    .FirstOrDefault()
            })
            .ToListAsync();

        foreach (var user in users)
            user.IsAdmin = adminIdSet.Contains(user.Id);

        return View(users);
    }

    public async Task<IActionResult> Details(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return NotFound();

        var userEntity = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userEntity is null)
            return NotFound();

        var vm = new AdminUserDetailsVm
        {
            Id = userEntity.Id,
            Email = userEntity.Email ?? "-",
            UserName = userEntity.UserName,
            PhoneNumber = userEntity.PhoneNumber,
            EmailConfirmed = userEntity.EmailConfirmed,
            IsAdmin = await _userManager.IsInRoleAsync(userEntity, "Admin"),
            OrderCount = await _db.Orders.CountAsync(o => o.UserId == userEntity.Id),
            TotalSpent = await _db.Orders
                .Where(o => o.UserId == userEntity.Id)
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0m,
            LastOrderUtc = await _db.Orders
                .Where(o => o.UserId == userEntity.Id)
                .OrderByDescending(o => o.CreatedAtUtc)
                .Select(o => (DateTime?)o.CreatedAtUtc)
                .FirstOrDefaultAsync(),
            Orders = await _db.Orders
                .Where(o => o.UserId == userEntity.Id)
                .OrderByDescending(o => o.CreatedAtUtc)
                .Select(o => new AdminUserOrderSummaryVm
                {
                    Id = o.Id,
                    CreatedAtUtc = o.CreatedAtUtc,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    ItemCount = o.Items.Sum(i => i.Quantity)
                })
                .ToListAsync()
        };

        return View(vm);
    }
}
