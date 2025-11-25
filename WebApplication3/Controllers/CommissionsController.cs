using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CommissionsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CommissionsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Commissions
        public async Task<IActionResult> Index()
        {
            var commissions = await _context.Commissions
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
            return View(commissions);
        }

        // GET: Commissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commission == null)
            {
                return NotFound();
            }

            return View(commission);
        }

        // GET: Commissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commissions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesRepUserId,OpportunityId,CommissionName,DealAmount,CommissionRate,Status,PeriodYear,PeriodMonth,PeriodQuarter,TerritoryId,IsSplit,SplitPercentage,Notes")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                commission.CommissionAmount = commission.DealAmount * (commission.CommissionRate / 100m) * (commission.SplitPercentage / 100m);
                commission.CreatedDate = DateTime.UtcNow;
                _context.Add(commission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commission);
        }

        // GET: Commissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions.FindAsync(id);
            if (commission == null)
            {
                return NotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SalesRepUserId,OpportunityId,CommissionName,DealAmount,CommissionRate,Status,PeriodYear,PeriodMonth,PeriodQuarter,ApprovedDate,PaidDate,PaymentMethod,PaymentReference,TerritoryId,IsSplit,SplitPercentage,Notes")] Commission commission)
        {
            if (id != commission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commission.CommissionAmount = commission.DealAmount * (commission.CommissionRate / 100m) * (commission.SplitPercentage / 100m);
                    commission.LastModifiedDate = DateTime.UtcNow;

                    if (commission.Status == "Approved" && commission.ApprovedDate == null)
                    {
                        commission.ApprovedDate = DateTime.UtcNow;
                    }

                    if (commission.Status == "Paid" && commission.PaidDate == null)
                    {
                        commission.PaidDate = DateTime.UtcNow;
                    }

                    _context.Update(commission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommissionExists(commission.Id))
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
            return View(commission);
        }

        // GET: Commissions/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions.FindAsync(id);
            if (commission == null)
            {
                return NotFound();
            }

            commission.Status = "Approved";
            commission.ApprovedDate = DateTime.UtcNow;
            commission.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Commissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commission == null)
            {
                return NotFound();
            }

            return View(commission);
        }

        // POST: Commissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commission = await _context.Commissions.FindAsync(id);
            if (commission != null)
            {
                _context.Commissions.Remove(commission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommissionExists(int id)
        {
            return _context.Commissions.Any(e => e.Id == id);
        }
    }
}
