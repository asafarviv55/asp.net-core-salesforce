using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string AccountType { get; set; } = "Prospect"; // Prospect, Customer, Partner, Vendor

        [StringLength(50)]
        public string Industry { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Website { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

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

        public int? NumberOfEmployees { get; set; }

        public decimal? AnnualRevenue { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? OwnerId { get; set; }

        public int? TerritoryId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }

        // Navigation properties
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
    }
}
