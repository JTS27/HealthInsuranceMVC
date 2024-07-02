using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Hosting;

namespace HealthInsurance.Controllers
{
    public class BeneficiariesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public BeneficiariesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Beneficiaries
        public async Task<IActionResult> Index()
        {
            ViewBag.msg = HttpContext.Session.GetString("TestMsg");
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var userId = HttpContext.Session.GetInt32("UserId");
            var sub = _context.Subscriptions.FirstOrDefault(x => x.User.Id == HttpContext.Session.GetInt32("UserId"));
            if (sub == null)
            {
                return RedirectToAction("isNotSub", "Home");
            }

            var modelContext = _context.Beneficiaries.Where(x=>x.Subscription.User.Id == userId).Include(b => b.Subscription).Include(x=>x.Subscription.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Beneficiaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public IActionResult Create()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "Id", "Id");
            return View();
        }

        // POST: Beneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subscriptionid,Name,Proofimage,ImageFile,Status,Relative")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                if (beneficiary.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + beneficiary.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/Beneficiaries/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await beneficiary.ImageFile.CopyToAsync(fileStream);
                    }

                    beneficiary.Proofimage = fileName;
                }
                var userID = HttpContext.Session.GetInt32("UserId");

                var sub = _context.Subscriptions.FirstOrDefault(x => x.User.Id == userID);
                beneficiary.Subscriptionid = sub.Id;
                beneficiary.Status = 2;
                HttpContext.Session.SetString("TestMsg", "Your testimonial is on pending, please wait the admins to check it out");

                _context.Add(beneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "Id", "Id", beneficiary.Subscriptionid);
            return View(beneficiary);
        }

        // GET: Beneficiaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "Id", "Id", beneficiary.Subscriptionid);
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Subscriptionid,Name,Proofimage,ImageFile,Status,Relative")] Beneficiary beneficiary)
        {
            if (id != beneficiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (beneficiary.ImageFile != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + beneficiary.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/Beneficiaries/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await beneficiary.ImageFile.CopyToAsync(fileStream);
                        }

                        beneficiary.Proofimage = fileName;
                    }
                    _context.Update(beneficiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryExists(beneficiary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Beneficiary", "Admin");
            }
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "Id", "Id", beneficiary.Subscriptionid);
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Beneficiaries == null)
            {
                return Problem("Entity set 'ModelContext.Beneficiaries'  is null.");
            }
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Beneficiary", "Admin");
        }

        private bool BeneficiaryExists(decimal id)
        {
          return (_context.Beneficiaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
