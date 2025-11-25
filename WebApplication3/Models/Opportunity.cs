using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Opportunity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Stage { get; set; } = "Prospecting";
        // Prospecting, Qualification, Needs Analysis, Value Proposition,
        // Proposal/Price Quote, Negotiation/Review, Closed Won, Closed Lost

        [Range(0, 100)]
        public int Probability { get; set; } = 10; // Percentage (0-100)

        [Required]
        public decimal Amount { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = "New Business"; // New Business, Existing Business, Renewal

        [StringLength(50)]
        public string LeadSource { get; set; } = string.Empty;

        public int? OwnerId { get; set; }

        public int? PrimaryContactId { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? NextSteps { get; set; }

        public bool IsClosed { get; set; } = false;

        public bool IsWon { get; set; } = false;

        public DateTime? ActualCloseDate { get; set; }

        public decimal? ForecastedRevenue { get; set; }

        public int? TerritoryId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }

        // Navigation properties
        public Account Account { get; set; } = null!;
        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    }
}
