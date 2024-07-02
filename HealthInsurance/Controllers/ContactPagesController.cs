using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Controllers
{
    public class ContactPagesController : Controller
    {
        private readonly ModelContext _context;

        public ContactPagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactPages
        public async Task<IActionResult> Index()
        {
            return _context.ContactPages != null ?
                        View(await _context.ContactPages.ToListAsync()) :
                        Problem("Entity set 'ModelContext.ContactPages'  is null.");
        }

        // GET: ContactPages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.ContactPages == null)
            {
                return NotFound();
            }

            var contactPage = await _context.ContactPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPage == null)
            {
                return NotFound();
            }

            return View(contactPage);
        }

        // GET: ContactPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Background,Text1,Text2,Text3")] ContactPage contactPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactPage);
        }

        // GET: ContactPages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.ContactPages == null)
            {
                return NotFound();
            }

            var contactPage = await _context.ContactPages.FindAsync(id);
            if (contactPage == null)
            {
                return NotFound();
            }
            return View(contactPage);
        }

        // POST: ContactPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Background,Text1,Text2,Text3")] ContactPage contactPage)
        {
            if (id != contactPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactPageExists(contactPage.Id))
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
            return View(contactPage);
        }

        // GET: ContactPages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.ContactPages == null)
            {
                return NotFound();
            }

            var contactPage = await _context.ContactPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPage == null)
            {
                return NotFound();
            }

            return View(contactPage);
        }

        // POST: ContactPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.ContactPages == null)
            {
                return Problem("Entity set 'ModelContext.ContactPages'  is null.");
            }
            var contactPage = await _context.ContactPages.FindAsync(id);
            if (contactPage != null)
            {
                _context.ContactPages.Remove(contactPage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactPageExists(decimal id)
        {
            return (_context.ContactPages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
