using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShopsController(ApplicationDbContext context) => _context = context;

        // GET: /Shops
        public async Task<IActionResult> Index()
        {
            var shops = await _context.Shops.AsNoTracking().ToListAsync();
            return View(shops);
        }

        // GET: /Shops/Create
        public IActionResult Create() => View();

        // POST: /Shops/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shop model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.Shops.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Shops/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null) return NotFound();
            return View(shop);
        }

        // POST: /Shops/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shop model)
        {
            if (id != model.ShopId) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var shop = await _context.Shops.FindAsync(id);
            if (shop == null) return NotFound();

            shop.Name = model.Name;
            shop.Location = model.Location;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Shops/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null) return NotFound();
            return View(shop);
        }

        // POST: /Shops/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop != null)
            {
                _context.Shops.Remove(shop);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
