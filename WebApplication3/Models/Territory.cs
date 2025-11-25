using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Territory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string TerritoryCode { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int? ParentTerritoryId { get; set; }

        [StringLength(100)]
        public string Region { get; set; } = string.Empty; // North, South, East, West, etc.

        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? PostalCodeRange { get; set; }

        public int? ManagerUserId { get; set; }

        public decimal? AnnualQuota { get; set; }

        public int AccountCount { get; set; } = 0;

        public int OpportunityCount { get; set; } = 0;

        public decimal TotalRevenue { get; set; } = 0;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }
    }
}
