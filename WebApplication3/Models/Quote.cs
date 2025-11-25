using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string QuoteNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int OpportunityId { get; set; }

        public int? AccountId { get; set; }

        public int? ContactId { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Draft"; // Draft, In Review, Presented, Accepted, Rejected

        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpirationDate { get; set; }

        public decimal Subtotal { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalPrice { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(2000)]
        public string? Terms { get; set; }

        [StringLength(200)]
        public string? BillingStreet { get; set; }

        [StringLength(100)]
        public string? BillingCity { get; set; }

        [StringLength(50)]
        public string? BillingState { get; set; }

        [StringLength(20)]
        public string? BillingPostalCode { get; set; }

        [StringLength(100)]
        public string? BillingCountry { get; set; }

        [StringLength(200)]
        public string? ShippingStreet { get; set; }

        [StringLength(100)]
        public string? ShippingCity { get; set; }

        [StringLength(50)]
        public string? ShippingState { get; set; }

        [StringLength(20)]
        public string? ShippingPostalCode { get; set; }

        [StringLength(100)]
        public string? ShippingCountry { get; set; }

        public int? OwnerId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }

        // Navigation properties
        public Opportunity Opportunity { get; set; } = null!;
        public ICollection<QuoteLineItem> LineItems { get; set; } = new List<QuoteLineItem>();
    }
}
