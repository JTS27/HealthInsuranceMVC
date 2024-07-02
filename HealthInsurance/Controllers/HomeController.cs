using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Diagnostics;

namespace HealthInsurance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        
        public HomeController(ILogger<HomeController> logger,ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            
            var admins = _context.Users.Where(x=>x.Roleid==1).ToList();
            var test = _context.Testimonials.Where(x => x.Status == 1).Include(t => t.User);
            var users = _context.Users.Where(x=>x.Issub==1).ToList();

            var sub = _context.Subscriptions.Include(x => x.User).Count();

            ViewBag.AdminCount = admins.Count();
            ViewBag.SubCount = sub;
            ViewBag.RegUser = _context.Users.Where(x => x.Roleid == 2).Count();

            var model = Tuple.Create<IEnumerable<User>, IEnumerable<Testimonial>>(admins, test);

            return View(model);
        }

        public IActionResult Services()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            
            return View();
        }
        public IActionResult notLogged()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            return View();
        } 
        public IActionResult isSub()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            return View();
        }
        public IActionResult isNotSub()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            return View();
        }
        public IActionResult paymentSucceed()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}