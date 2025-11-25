using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class SalesForecast
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ForecastName { get; set; } = string.Empty;

        [Required]
        public int PeriodYear { get; set; }

        [Required]
        public int PeriodMonth { get; set; } // 1-12

        [Required]
        public int PeriodQuarter { get; set; } // 1-4

        public int? OwnerId { get; set; }

        public int? TerritoryId { get; set; }

        public decimal PipelineAmount { get; set; } // Sum of all open opportunities

        public decimal BestCaseAmount { get; set; } // High probability deals

        public decimal CommittedAmount { get; set; } // Very high probability deals

        public decimal ClosedAmount { get; set; } // Actual closed/won deals

        public decimal QuotaAmount { get; set; } // Target amount for the period

        public int OpportunityCount { get; set; }

        public int WonOpportunityCount { get; set; }

        public decimal WinRate { get; set; } // Percentage

        public decimal AverageDealSize { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastCalculatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
