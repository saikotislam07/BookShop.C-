using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context) => _context = context;

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.BookInShop)
                    .ThenInclude(bs => bs.Book)
                .Include(o => o.BookInShop)
                    .ThenInclude(bs => bs.Shop)
                .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Customers"] = await _context.Customers.ToListAsync();
            ViewData["BookInShops"] = await _context.BookInShops
                .Include(bs => bs.Book)
                .Include(bs => bs.Shop)
                .ToListAsync();

            return View();
        }

        // POST: Orders/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Customers"] = await _context.Customers.ToListAsync();
                ViewData["BookInShops"] = await _context.BookInShops
                    .Include(bs => bs.Book)
                    .Include(bs => bs.Shop)
                    .ToListAsync();
                return View(model);
            }

            // Calculate total price
            var bookInShop = await _context.BookInShops.FindAsync(model.BookInShopId);
            if (bookInShop == null) return BadRequest("Invalid book in shop.");

            model.TotalPrice = bookInShop.Book.Price * model.Quantity;
            model.OrderDate = DateTime.Now;

            _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
