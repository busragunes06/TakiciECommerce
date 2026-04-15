using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCategoriesController : Controller
{
    private readonly AppDbContext _db;

    public AdminCategoriesController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        // Sadece okuma işlemi yapıldığı için AsNoTracking eklendi
        var categories = await _db.Categories
            .AsNoTracking()
            .OrderByDescending(c => c.CreatedAtUtc)
            .ToListAsync();
            
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Category category)
    {
        if (ModelState.IsValid)
        {
            // Aynı isimde kategori var mı kontrolü
            var isDuplicate = await _db.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());
            if (isDuplicate)
            {
                ModelState.AddModelError("Name", "Bu isimde bir kategori zaten mevcut.");
                return View(category);
            }

            category.Id = Guid.NewGuid();
            category.CreatedAtUtc = DateTime.UtcNow;
            
            _db.Add(category);
            await _db.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Kategori başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var category = await _db.Categories.FindAsync(id);
        if (category == null) return NotFound();
        
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Category category) // CreatedAtUtc'yi Bind'dan ÇIKARDIK
    {
        if (id != category.Id) return NotFound();

        if (ModelState.IsValid)
        {
            // Aynı isimde BAŞKA bir kategori var mı kontrolü (Kendi ID'si hariç)
            var isDuplicate = await _db.Categories
                .AnyAsync(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != id);
                
            if (isDuplicate)
            {
                ModelState.AddModelError("Name", "Bu isimde başka bir kategori zaten mevcut.");
                return View(category);
            }

            try
            {
                // Güvenli Güncelleme (Overposting koruması)
                // Mevcut datayı veritabanından çekiyoruz
                var existingCategory = await _db.Categories.FindAsync(id);
                if (existingCategory == null) return NotFound();

                // Sadece izin verilen alanları (Name) güncelliyoruz. 
                // CreatedAtUtc orjinal halinde kalıyor, dokunmuyoruz.
                existingCategory.Name = category.Name;

                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var category = await _db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category != null)
        {
            // Eğer kategoriye bağlı ürünler varsa silinmesini engellemek için bir kontrol ekleyebilirsiniz.
            // Sizin AppDbContext'te "Restrict" tanımlı olduğu için zaten DB bazında hata verecektir.
            // İsterseniz burada try-catch ile yakalayıp kullanıcıya "Bu kategoriye ait ürünler var, silemezsiniz" mesajı gösterebilirsiniz.
            
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(Guid id)
    {
        return _db.Categories.Any(e => e.Id == id);
    }
}