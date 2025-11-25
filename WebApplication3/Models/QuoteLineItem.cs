using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class QuoteLineItem
    {
        public int Id { get; set; }

        [Required]
        public int QuoteId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal TotalPrice { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int LineNumber { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Quote Quote { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
