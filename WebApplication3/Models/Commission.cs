using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Commission
    {
        public int Id { get; set; }

        [Required]
        public int SalesRepUserId { get; set; }

        [Required]
        public int OpportunityId { get; set; }

        [Required]
        [StringLength(200)]
        public string CommissionName { get; set; } = string.Empty;

        [Required]
        public decimal DealAmount { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal CommissionRate { get; set; } // Percentage (0-100)

        [Required]
        public decimal CommissionAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Paid, Rejected

        [Required]
        public int PeriodYear { get; set; }

        [Required]
        public int PeriodMonth { get; set; } // 1-12

        [Required]
        public int PeriodQuarter { get; set; } // 1-4

        public DateTime? ApprovedDate { get; set; }

        public int? ApprovedByUserId { get; set; }

        public DateTime? PaidDate { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [StringLength(100)]
        public string? PaymentReference { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public int? TerritoryId { get; set; }

        public bool IsSplit { get; set; } = false;

        public decimal SplitPercentage { get; set; } = 100; // Default 100% if not split

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }
    }
}
