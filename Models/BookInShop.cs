using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class BookInShop
    {
        [Key]
        public int BookInShopId { get; set; }  // PK

        [Required]
        public int BookId { get; set; }

        [Required]
        public int ShopId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        // Navigation
        public Book Book { get; set; } = null!;
        public Shop Shop { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
