using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminMessagesController : Controller
{
    private readonly AppDbContext _db;

    public AdminMessagesController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _db.ContactMessages
            .OrderBy(m => m.IsRead)
            .ThenByDescending(m => m.CreatedAtUtc)
            .ToListAsync();

        return View(messages);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var message = await _db.ContactMessages.FindAsync(id);
        if (message == null) return NotFound();

        if (!message.IsRead)
        {
            message.IsRead = true;
            await _db.SaveChangesAsync();
        }

        return View(message);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var message = await _db.ContactMessages.FindAsync(id);
        if (message != null)
        {
            _db.ContactMessages.Remove(message);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Mesaj kalıcı olarak silindi.";
        }

        return RedirectToAction(nameof(Index));
    }
}
