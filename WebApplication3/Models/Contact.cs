using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Contact
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

        [StringLength(50)]
        public string? MobilePhone { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [Required]
        public int AccountId { get; set; }

        public int? ReportsToContactId { get; set; }

        [StringLength(200)]
        public string? MailingStreet { get; set; }

        [StringLength(100)]
        public string? MailingCity { get; set; }

        [StringLength(50)]
        public string? MailingState { get; set; }

        [StringLength(20)]
        public string? MailingPostalCode { get; set; }

        [StringLength(100)]
        public string? MailingCountry { get; set; }

        public DateTime? Birthdate { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsPrimaryContact { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }

        // Navigation properties
        public Account Account { get; set; } = null!;
    }
}
