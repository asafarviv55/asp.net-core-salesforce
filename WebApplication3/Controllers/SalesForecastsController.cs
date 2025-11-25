using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SalesForecastsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SalesForecastsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: SalesForecasts
        public async Task<IActionResult> Index()
        {
            var forecasts = await _context.SalesForecasts
                .OrderByDescending(sf => sf.PeriodYear)
                .ThenByDescending(sf => sf.PeriodQuarter)
                .ToListAsync();
            return View(forecasts);
        }

        // GET: SalesForecasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forecast = await _context.SalesForecasts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forecast == null)
            {
                return NotFound();
            }

            return View(forecast);
        }

        // GET: SalesForecasts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesForecasts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForecastName,PeriodYear,PeriodMonth,PeriodQuarter,TerritoryId,PipelineAmount,BestCaseAmount,CommittedAmount,ClosedAmount,QuotaAmount,OpportunityCount,WonOpportunityCount,WinRate,AverageDealSize,Notes")] SalesForecast forecast)
        {
            if (ModelState.IsValid)
            {
                forecast.CreatedDate = DateTime.UtcNow;
                forecast.LastCalculatedDate = DateTime.UtcNow;
                _context.Add(forecast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forecast);
        }

        // GET: SalesForecasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forecast = await _context.SalesForecasts.FindAsync(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return View(forecast);
        }

        // POST: SalesForecasts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ForecastName,PeriodYear,PeriodMonth,PeriodQuarter,TerritoryId,PipelineAmount,BestCaseAmount,CommittedAmount,ClosedAmount,QuotaAmount,OpportunityCount,WonOpportunityCount,WinRate,AverageDealSize,Notes")] SalesForecast forecast)
        {
            if (id != forecast.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    forecast.LastModifiedDate = DateTime.UtcNow;
                    forecast.LastCalculatedDate = DateTime.UtcNow;
                    _context.Update(forecast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForecastExists(forecast.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(forecast);
        }

        // GET: SalesForecasts/Calculate
        public async Task<IActionResult> Calculate()
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;
            var currentQuarter = (currentMonth - 1) / 3 + 1;

            var opportunities = await _context.Opportunities
                .Where(o => o.CloseDate.HasValue &&
                            o.CloseDate.Value.Year == currentYear &&
                            ((o.CloseDate.Value.Month - 1) / 3 + 1) == currentQuarter)
                .ToListAsync();

            var forecast = new SalesForecast
            {
                ForecastName = $"Q{currentQuarter} {currentYear} Forecast",
                PeriodYear = currentYear,
                PeriodMonth = currentMonth,
                PeriodQuarter = currentQuarter,
                OpportunityCount = opportunities.Count,
                WonOpportunityCount = opportunities.Count(o => o.IsWon),
                ClosedAmount = opportunities.Where(o => o.IsClosed && o.IsWon).Sum(o => o.Amount),
                PipelineAmount = opportunities.Where(o => !o.IsClosed).Sum(o => o.Amount),
                BestCaseAmount = opportunities.Where(o => !o.IsClosed && o.Probability >= 70).Sum(o => o.Amount),
                CommittedAmount = opportunities.Where(o => !o.IsClosed && o.Probability >= 90).Sum(o => o.Amount),
                WinRate = opportunities.Count > 0 ? (decimal)opportunities.Count(o => o.IsWon) / opportunities.Count * 100 : 0,
                AverageDealSize = opportunities.Any() ? opportunities.Average(o => o.Amount) : 0,
                CreatedDate = DateTime.UtcNow,
                LastCalculatedDate = DateTime.UtcNow
            };

            return View(forecast);
        }

        // POST: SalesForecasts/Calculate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate([Bind("ForecastName,PeriodYear,PeriodMonth,PeriodQuarter,QuotaAmount")] SalesForecast forecast)
        {
            var opportunities = await _context.Opportunities
                .Where(o => o.CloseDate.HasValue &&
                            o.CloseDate.Value.Year == forecast.PeriodYear &&
                            ((o.CloseDate.Value.Month - 1) / 3 + 1) == forecast.PeriodQuarter)
                .ToListAsync();

            forecast.OpportunityCount = opportunities.Count;
            forecast.WonOpportunityCount = opportunities.Count(o => o.IsWon);
            forecast.ClosedAmount = opportunities.Where(o => o.IsClosed && o.IsWon).Sum(o => o.Amount);
            forecast.PipelineAmount = opportunities.Where(o => !o.IsClosed).Sum(o => o.Amount);
            forecast.BestCaseAmount = opportunities.Where(o => !o.IsClosed && o.Probability >= 70).Sum(o => o.Amount);
            forecast.CommittedAmount = opportunities.Where(o => !o.IsClosed && o.Probability >= 90).Sum(o => o.Amount);
            forecast.WinRate = opportunities.Count > 0 ? (decimal)opportunities.Count(o => o.IsWon) / opportunities.Count * 100 : 0;
            forecast.AverageDealSize = opportunities.Any() ? opportunities.Average(o => o.Amount) : 0;
            forecast.CreatedDate = DateTime.UtcNow;
            forecast.LastCalculatedDate = DateTime.UtcNow;

            _context.Add(forecast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SalesForecasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forecast = await _context.SalesForecasts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forecast == null)
            {
                return NotFound();
            }

            return View(forecast);
        }

        // POST: SalesForecasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forecast = await _context.SalesForecasts.FindAsync(id);
            if (forecast != null)
            {
                _context.SalesForecasts.Remove(forecast);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForecastExists(int id)
        {
            return _context.SalesForecasts.Any(e => e.Id == id);
        }
    }
}
