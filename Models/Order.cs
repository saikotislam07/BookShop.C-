using System;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int BookInShopId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public Customer Customer { get; set; } = null!;
        public BookInShop BookInShop { get; set; } = null!;
    }
}
