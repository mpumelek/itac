using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITAC.DataAccess;
using ITAC.Models;

namespace ITAC.Web.Controllers
{
    public class ImportersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Importers
        public async Task<IActionResult> Index()
        {
            var importers = await _context.Importers.AsNoTracking().ToListAsync();
            return View(importers);
        }

        // GET: Importers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var importer = await _context.Importers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (importer == null) return NotFound();

            return View(importer);
        }

        // GET: Importers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Importers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BusinessName,TradeName,CustomsNumber")] Importer importer)
        {
            if (!ModelState.IsValid) return View(importer);

            _context.Add(importer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Importers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var importer = await _context.Importers.FindAsync(id);
            if (importer == null) return NotFound();

            return View(importer);
        }

        // POST: Importers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BusinessName,TradeName,CustomsNumber")] Importer importer)
        {
            if (id != importer.ID) return NotFound();
            if (!ModelState.IsValid) return View(importer);

            try
            {
                _context.Update(importer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Importers.AnyAsync(e => e.ID == importer.ID);
                if (!exists) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Importers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var importer = await _context.Importers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (importer == null) return NotFound();

            return View(importer);
        }

        // POST: Importers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importer = await _context.Importers.FindAsync(id);
            if (importer != null)
            {
                _context.Importers.Remove(importer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
