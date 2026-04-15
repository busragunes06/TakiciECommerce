using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels.Profile;

namespace Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(AppDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var orderCount = await _db.Orders
            .Where(o => o.UserId == user.Id)
            .CountAsync();

        var vm = new ProfileIndexVm
        {
            Email = user.Email,
            UserName = user.UserName,
            TotalOrders = orderCount
        };

        return View(vm);
    }
}
