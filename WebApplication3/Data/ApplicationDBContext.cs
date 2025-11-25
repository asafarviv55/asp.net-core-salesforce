using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<SfUser> SfUsers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CrmTask> CrmTasks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteLineItem> QuoteLineItems { get; set; }
        public DbSet<SalesForecast> SalesForecasts { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<Commission> Commissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Account)
                .WithMany(a => a.Contacts)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Opportunity>()
                .HasOne(o => o.Account)
                .WithMany(a => a.Opportunities)
                .HasForeignKey(o => o.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Opportunity)
                .WithMany(o => o.Quotes)
                .HasForeignKey(q => q.OpportunityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuoteLineItem>()
                .HasOne(qli => qli.Quote)
                .WithMany(q => q.LineItems)
                .HasForeignKey(qli => qli.QuoteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuoteLineItem>()
                .HasOne(qli => qli.Product)
                .WithMany(p => p.QuoteLineItems)
                .HasForeignKey(qli => qli.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal precision
            modelBuilder.Entity<Lead>()
                .Property(l => l.EstimatedBudget)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Account>()
                .Property(a => a.AnnualRevenue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Opportunity>()
                .Property(o => o.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Opportunity>()
                .Property(o => o.ForecastedRevenue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.StandardPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.CostPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Quote>()
                .Property(q => q.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Quote>()
                .Property(q => q.TaxAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Quote>()
                .Property(q => q.ShippingAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Quote>()
                .Property(q => q.DiscountAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Quote>()
                .Property(q => q.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<QuoteLineItem>()
                .Property(qli => qli.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<QuoteLineItem>()
                .Property(qli => qli.Discount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<QuoteLineItem>()
                .Property(qli => qli.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.PipelineAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.BestCaseAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.CommittedAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.ClosedAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.QuotaAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.WinRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.AverageDealSize)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Territory>()
                .Property(t => t.AnnualQuota)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Territory>()
                .Property(t => t.TotalRevenue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Commission>()
                .Property(c => c.DealAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Commission>()
                .Property(c => c.CommissionRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Commission>()
                .Property(c => c.CommissionAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Commission>()
                .Property(c => c.SplitPercentage)
                .HasPrecision(5, 2);
        }

    }
}
