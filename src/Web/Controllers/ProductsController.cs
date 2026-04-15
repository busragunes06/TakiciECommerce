using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels.Catalog;

namespace Web.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("/urunler")]
    // Parametre string? category yerine Guid? categoryId olarak değiştirildi
    public async Task<IActionResult> Index(string? q = null, Guid? categoryId = null, string? sort = null)
    {
        var productsQuery = _db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(term) ||
                (p.Description != null && p.Description.Contains(term)));
        }

        // Slug yerine artık ID'ye göre filtreleme yapıyoruz
        if (categoryId.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
        }

        productsQuery = sort switch
        {
            "price_asc" => productsQuery.OrderBy(p => p.Price),
            "price_desc" => productsQuery.OrderByDescending(p => p.Price),
            "newest" => productsQuery.OrderByDescending(p => p.CreatedAtUtc),
            _ => productsQuery.OrderBy(p => p.Name)
        };

        var items = await productsQuery
            .Select(p => new ProductListItemVm
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category!.Name,
                CategoryId = p.Category.Id // CategorySlug yerine CategoryId gönderiyoruz
            })
            .ToListAsync();

        var vm = new ProductListVm
        {
            Query = q,
            CategoryId = categoryId, // Category (string) yerine CategoryId (Guid?) kullanıyoruz
            Sort = sort,
            Items = items
        };

        return View(vm);
    }

    [HttpGet("/urunler/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.Id == id)
            .Select(p => new ProductDetailsVm
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                IsActive = p.IsActive,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category!.Name,
                CategoryId = p.Category.Id // CategorySlug yerine CategoryId eklendi
            })
            .FirstOrDefaultAsync();

        if (product is null) return NotFound();
        return View(product);
    }
}