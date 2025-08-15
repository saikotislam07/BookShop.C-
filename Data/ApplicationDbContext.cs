using Microsoft.EntityFrameworkCore;
using BookShop.Models;

namespace BookShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<BookInShop> BookInShops => Set<BookInShop>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Junction: BookInShop (own PK, FKs to Book & Shop)
            modelBuilder.Entity<BookInShop>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookInShops)
                .HasForeignKey(bs => bs.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookInShop>()
                .HasOne(bs => bs.Shop)
                .WithMany(s => s.BookInShops)
                .HasForeignKey(bs => bs.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            // Orders link to Customer (1..many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Orders link to BookInShop (1..many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.BookInShop)
                .WithMany(bs => bs.Orders)
                .HasForeignKey(o => o.BookInShopId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
