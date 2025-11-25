using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Lead
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string Company { get; set; } = string.Empty;

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "New"; // New, Contacted, Qualified, Unqualified

        [StringLength(50)]
        public string Source { get; set; } = "Website"; // Website, Referral, Cold Call, Event, etc.

        public int LeadScore { get; set; } = 0; // 0-100 scoring system

        [StringLength(50)]
        public string Industry { get; set; } = string.Empty;

        public decimal? EstimatedBudget { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public int? AssignedToUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastContactedDate { get; set; }

        public DateTime? ConvertedDate { get; set; }

        public int? ConvertedToAccountId { get; set; }

        public int? ConvertedToOpportunityId { get; set; }

        public bool IsConverted { get; set; } = false;
    }
}
