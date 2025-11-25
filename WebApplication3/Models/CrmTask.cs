using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class CrmTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;

        [StringLength(50)]
        public string Status { get; set; } = "Not Started"; // Not Started, In Progress, Completed, Waiting, Deferred

        [StringLength(50)]
        public string Priority { get; set; } = "Normal"; // High, Normal, Low

        public DateTime? DueDate { get; set; }

        public DateTime? ReminderDate { get; set; }

        public bool ReminderSent { get; set; } = false;

        [StringLength(2000)]
        public string? Description { get; set; }

        // Related to entities
        public int? LeadId { get; set; }

        public int? AccountId { get; set; }

        public int? ContactId { get; set; }

        public int? OpportunityId { get; set; }

        public int? AssignedToUserId { get; set; }

        public int? CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
