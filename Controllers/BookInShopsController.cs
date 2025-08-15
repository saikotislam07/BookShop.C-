using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class BookInShopsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookInShopsController(ApplicationDbContext context) => _context = context;

        // GET: BookInShops
        public async Task<IActionResult> Index()
        {
            var items = await _context.BookInShops
                .Include(bs => bs.Book)
                .Include(bs => bs.Shop)
                .ToListAsync();

            return View(items);
        }

        // GET: BookInShops/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Books"] = new SelectList(await _context.Books.ToListAsync(), "BookId", "Title");
            ViewData["Shops"] = new SelectList(await _context.Shops.ToListAsync(), "ShopId", "Name");
            return View(new BookInShop()); // single instance
        }

        // POST: BookInShops/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookInShop model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Books"] = new SelectList(await _context.Books.ToListAsync(), "BookId", "Title");
                ViewData["Shops"] = new SelectList(await _context.Shops.ToListAsync(), "ShopId", "Name");
                return View(model);
            }

            _context.BookInShops.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: BookInShops/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.BookInShops.FindAsync(id);
            if (item == null) return NotFound();

            ViewData["Books"] = new SelectList(await _context.Books.ToListAsync(), "BookId", "Title", item.BookId);
            ViewData["Shops"] = new SelectList(await _context.Shops.ToListAsync(), "ShopId", "Name", item.ShopId);

            return View(item);
        }

        // POST: BookInShops/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookInShop model)
        {
            if (id != model.BookInShopId) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewData["Books"] = new SelectList(await _context.Books.ToListAsync(), "BookId", "Title", model.BookId);
                ViewData["Shops"] = new SelectList(await _context.Shops.ToListAsync(), "ShopId", "Name", model.ShopId);
                return View(model);
            }

            var item = await _context.BookInShops.FindAsync(id);
            if (item == null) return NotFound();

            item.BookId = model.BookId;
            item.ShopId = model.ShopId;
            item.Quantity = model.Quantity;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: BookInShops/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.BookInShops
                .Include(bs => bs.Book)
                .Include(bs => bs.Shop)
                .FirstOrDefaultAsync(bs => bs.BookInShopId == id);

            if (item == null) return NotFound();
            return View(item);
        }

        // POST: BookInShops/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.BookInShops.FindAsync(id);
            if (item != null)
            {
                _context.BookInShops.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
