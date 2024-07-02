using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthInsurance.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AuthenticationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            _context = context;
        }



        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,Password,Email,Roleid,Imagepath,ImageFile,Subid")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + user.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/Users/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.Imagepath = fileName;
                }
                var check = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

                if (check == null)
                {
                    user.Roleid = 2;
                    user.Issub = 0;

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    ViewBag.Error = "Email is already used, please try another one.";
                }

            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Id", "Id", user.Roleid);
            return View(user);
        }


        [HttpPost]

        public async Task<IActionResult> Login([Bind("Id,Fname,Lname,Password,Email,Imagepath")] User user)
        {
            var auth = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();

            if (auth != null)
            {
                var u = _context.Users.Where(x => x.Id == auth.Id).FirstOrDefault();
                switch (auth.Roleid)
                {
                    case 1:

                        HttpContext.Session.SetString("Fname", u.Fname);
                        HttpContext.Session.SetString("Lname", u.Lname);
                        HttpContext.Session.SetString("Email", u.Email);
                        HttpContext.Session.SetString("Imagepath", u.Imagepath);
                        HttpContext.Session.SetInt32("UserId", (int)u.Id);
                        HttpContext.Session.SetInt32("RoleId", (int)u.Roleid);
                        return RedirectToAction("Index", "Admin");
                    case 2:

                        HttpContext.Session.SetString("Fname", u.Fname);
                        HttpContext.Session.SetString("Lname", u.Lname);
                        HttpContext.Session.SetString("Email", u.Email);
                        HttpContext.Session.SetString("Imagepath", u.Imagepath);
                        HttpContext.Session.SetInt32("UserId", (int)u.Id);
                        HttpContext.Session.SetInt32("RoleId", (int)u.Roleid);
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Error = "Wrong credentials";
            }



            return View();
        }



    }
}
