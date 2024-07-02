using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace HealthInsurance.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext _context)
        {
            this._context = _context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var users = _context.Users.Where(x=>x.Roleid==2).Count();
            ViewBag.regUsers = users;
            var sub = _context.Users.Where(x=>x.Issub==1).Count();
            ViewBag.subUsers = sub;
            var prof = _context.Users.Where(x => x.Issub == 1).Count();
            ViewBag.profit = prof * 500;

            var modelContext = _context.Users;
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> Testimonial()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var modelContext = _context.Testimonials.Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> Beneficiary()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var modelContext = _context.Beneficiaries.Include(b => b.Subscription).Include(x => x.Subscription.User);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> RegUsers()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var modelContext = _context.Users;
            return View(await modelContext.ToListAsync());
        }
        public IActionResult Search()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var model = _context.Subscriptions.Include(x=>x.User).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(DateTime? StartDate, DateTime? EndDate)
        {
            var model = _context.Subscriptions.Include(x => x.User).ToList();


            if (StartDate == null && EndDate == null)
            {
                return View(model);
            }
            else if (StartDate != null && EndDate == null)
            {
                var result = model.Where(x => x.Subscriptiondate.Value.Date >= StartDate);
                return View(result);
            }
            else if (StartDate == null && EndDate != null)
            {
                var result = model.Where(x => x.Subscriptiondate.Value.Date <= EndDate);
                return View(result);
            }
            else
            {
                var result = model.Where(x => x.Subscriptiondate.Value.Date >= StartDate && x.Subscriptiondate.Value.Date <= EndDate);
                return View(result);
            }



            return View();

        }
        public IActionResult Reports()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var result = _context.Subscriptions.Include(x => x.User).ToList();
            ViewBag.subNum = result.Count();
            ViewBag.profit = result.Count() * 500;
            return View(result);
        }

        [HttpPost]
        public IActionResult Reports(int? month, int? year)
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            int s = DateTime.Now.Date.Month;
            var model = _context.Subscriptions.Include(x => x.User).ToList();


            if (month == 0 && year != null)
            {
                var result = model.Where(x => x.Subscriptiondate.Value.Year == year);
                ViewBag.subNum = model.Count();
                ViewBag.profit = model.Count() * 500;
                return View(result);
            }
            else if (month != 0 && year != null)
            {
                var result = model.Where(x => x.Subscriptiondate.Value.Year == year && x.Subscriptiondate.Value.Month == month);
                if(result.Count() != 0)
                {
                    ViewBag.subNum = result.Count();
                    ViewBag.profit = result.Count() * 500;
                }
                
                return View(result);
            }
            return View(model);
        }
    }
}
