using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class TerritoriesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TerritoriesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Territories
        public async Task<IActionResult> Index()
        {
            var territories = await _context.Territories
                .OrderBy(t => t.Region)
                .ThenBy(t => t.Name)
                .ToListAsync();
            return View(territories);
        }

        // GET: Territories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _context.Territories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (territory == null)
            {
                return NotFound();
            }

            return View(territory);
        }

        // GET: Territories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Territories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TerritoryCode,Description,IsActive,Region,Country,State,City,PostalCodeRange,AnnualQuota")] Territory territory)
        {
            if (ModelState.IsValid)
            {
                territory.CreatedDate = DateTime.UtcNow;
                _context.Add(territory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(territory);
        }

        // GET: Territories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _context.Territories.FindAsync(id);
            if (territory == null)
            {
                return NotFound();
            }
            return View(territory);
        }

        // POST: Territories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TerritoryCode,Description,IsActive,Region,Country,State,City,PostalCodeRange,AnnualQuota,AccountCount,OpportunityCount,TotalRevenue")] Territory territory)
        {
            if (id != territory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    territory.LastModifiedDate = DateTime.UtcNow;
                    _context.Update(territory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerritoryExists(territory.Id))
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
            return View(territory);
        }

        // GET: Territories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var territory = await _context.Territories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (territory == null)
            {
                return NotFound();
            }

            return View(territory);
        }

        // POST: Territories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var territory = await _context.Territories.FindAsync(id);
            if (territory != null)
            {
                _context.Territories.Remove(territory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerritoryExists(int id)
        {
            return _context.Territories.Any(e => e.Id == id);
        }
    }
}
