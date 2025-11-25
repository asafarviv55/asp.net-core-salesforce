using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; } = "Call"; // Call, Email, Meeting, Task

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;

        [StringLength(50)]
        public string Status { get; set; } = "Planned"; // Planned, Completed, Cancelled

        [StringLength(50)]
        public string Priority { get; set; } = "Normal"; // High, Normal, Low

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int? DurationMinutes { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        // Related to entities
        public int? LeadId { get; set; }

        public int? AccountId { get; set; }

        public int? ContactId { get; set; }

        public int? OpportunityId { get; set; }

        public int? OwnerId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }
    }
}
