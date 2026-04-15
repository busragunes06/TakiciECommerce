using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels.Catalog;

namespace Web.Controllers;

public class CategoriesController : Controller
{
    private readonly AppDbContext _db;

    public CategoriesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("/kategoriler")]
    public async Task<IActionResult> Index()
    {
        var categories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryListItemVm
            {
                Id = c.Id,
                Name = c.Name,
                // Slug ataması tamamen silindi
                ProductCount = c.Products.Count(p => p.IsActive)
            })
            .ToListAsync();

        return View(categories);
    }

    // BySlug metodu yerine Details (veya ById) kullanıyoruz
    // URL artık site.com/kategori/5c4d3b... şeklinde Guid kabul edecek
    [HttpGet("/kategori/{id:guid}")]
    public IActionResult Details(Guid id)
    {
        // Products controller'ının Index metoduna 'slug' yerine 'categoryId' (Guid) gönderiyoruz
        return RedirectToAction("Index", "Products", new { categoryId = id });
    }
}