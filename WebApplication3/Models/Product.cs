using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string ProductCode { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(50)]
        public string ProductFamily { get; set; } = string.Empty;

        public decimal StandardPrice { get; set; }

        public decimal? CostPrice { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        public int QuantityInStock { get; set; } = 0;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }

        // Navigation properties
        public ICollection<QuoteLineItem> QuoteLineItems { get; set; } = new List<QuoteLineItem>();
    }
}
