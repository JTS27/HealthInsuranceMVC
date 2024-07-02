using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Models;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Configuration.UserSecrets;
using MailKit.Net.Smtp;
using MimeKit;

namespace HealthInsurance.Controllers
{
    public class BanksController : Controller
    {
        private readonly ModelContext _context;

        public BanksController(ModelContext context)
        {
            _context = context;
        }

        // GET: Banks
        public async Task<IActionResult> Index()
        {
            return _context.Banks != null ?
                        View(await _context.Banks.ToListAsync()) :
                        Problem("Entity set 'ModelContext.Banks'  is null.");
        }

        // GET: Banks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Banks/Create
        public IActionResult Create()
        {
            ViewBag.Fname = HttpContext.Session.GetString("Fname");
            ViewBag.Lname = HttpContext.Session.GetString("Lname");
            ViewBag.Imagepath = HttpContext.Session.GetString("Imagepath");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("RoleId");

            var userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(x=>x.Id == userID);
            var sub = _context.Subscriptions.FirstOrDefault(x=>x.Userid == userID);

            if (userID == null)
            {
                return RedirectToAction("notLogged", "Home");
            }
            if (user != null && sub != null &&  user.Issub == 1)
            {
                return RedirectToAction("isSub", "Home");
            }

            return View();
        }

        // POST: Banks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Balance,Cardnumber,Cvv")] Bank bank)
        {
            var userID = HttpContext.Session.GetInt32("UserId");
            var Email = HttpContext.Session.GetString("Email");

            var check = _context.Banks.FirstOrDefault(x => x.Cardnumber == bank.Cardnumber && x.Cvv == bank.Cvv);
            var user = _context.Users.FirstOrDefault(x => x.Id == userID);

            if(check != null)
            {
                if (check.Balance < 500)
                {
                    ViewBag.Error = "Not enough money in the bank";
                    return View();
                }
                    
                var balance = check.Balance;
                check.Balance = balance - 500;
                user.Issub = 1;

                var sup = new Subscription
                {
                    Userid = userID,
                    Subscriptiondate = DateTime.Now,
                };

                //using (var client = new SmtpClient())
                //{
                //    client.Connect("smtp.gmail.com");
                //    client.Authenticate("jihadlord2016@gamil.com", "23491200");

                //    var message = new MimeMessage
                //    {
                //    };

                //    message.From.Add(new MailboxAddress("Jihad Tawfiq", "jihadlord2016@gmail.com"));
                //    message.To.Add(new MailboxAddress("Subscriber", Email));
                //    message.Subject = "New subscription invoice";

                //    client.Disconnect(true);
                //}

                _context.Subscriptions.Add(sup);
                await _context.SaveChangesAsync();
            }
            else 
            {
                ViewBag.Error = "Wrong card information, please try agian";
                return View(); 
            }

            return RedirectToAction("paymentSucceed", "Home");


            //if (ModelState.IsValid)
            //{
            //    _context.Add(bank);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction("Home", "Services");
            //}
            //return View(bank);
        }

        // GET: Banks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            return View(bank);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Username,Balance,Cardnumber,Cvv")] Bank bank)
        {
            if (id != bank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.Id))
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
            return View(bank);
        }

        // GET: Banks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Banks == null)
            {
                return Problem("Entity set 'ModelContext.Banks'  is null.");
            }
            var bank = await _context.Banks.FindAsync(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(decimal id)
        {
          return (_context.Banks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
