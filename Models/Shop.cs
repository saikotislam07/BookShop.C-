using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        public ICollection<BookInShop> BookInShops { get; set; } = new List<BookInShop>();
    }
}
