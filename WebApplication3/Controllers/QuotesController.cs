using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class QuotesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public QuotesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Quotes
        public async Task<IActionResult> Index()
        {
            var quotes = await _context.Quotes
                .Include(q => q.Opportunity)
                .Include(q => q.LineItems)
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();
            return View(quotes);
        }

        // GET: Quotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .Include(q => q.Opportunity)
                .Include(q => q.LineItems)
                    .ThenInclude(li => li.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // GET: Quotes/Create
        public IActionResult Create()
        {
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Name");
            return View();
        }

        // POST: Quotes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuoteNumber,Name,OpportunityId,AccountId,ContactId,Status,QuoteDate,ExpirationDate,Subtotal,TaxAmount,ShippingAmount,DiscountAmount,Description,Terms,BillingStreet,BillingCity,BillingState,BillingPostalCode,BillingCountry,ShippingStreet,ShippingCity,ShippingState,ShippingPostalCode,ShippingCountry")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                quote.CreatedDate = DateTime.UtcNow;
                quote.TotalPrice = quote.Subtotal + quote.TaxAmount + quote.ShippingAmount - quote.DiscountAmount;
                _context.Add(quote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Name", quote.OpportunityId);
            return View(quote);
        }

        // GET: Quotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Name", quote.OpportunityId);
            return View(quote);
        }

        // POST: Quotes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuoteNumber,Name,OpportunityId,AccountId,ContactId,Status,QuoteDate,ExpirationDate,Subtotal,TaxAmount,ShippingAmount,DiscountAmount,Description,Terms,BillingStreet,BillingCity,BillingState,BillingPostalCode,BillingCountry,ShippingStreet,ShippingCity,ShippingState,ShippingPostalCode,ShippingCountry")] Quote quote)
        {
            if (id != quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    quote.LastModifiedDate = DateTime.UtcNow;
                    quote.TotalPrice = quote.Subtotal + quote.TaxAmount + quote.ShippingAmount - quote.DiscountAmount;
                    _context.Update(quote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.Id))
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
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Name", quote.OpportunityId);
            return View(quote);
        }

        // GET: Quotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .Include(q => q.Opportunity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote != null)
            {
                _context.Quotes.Remove(quote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
