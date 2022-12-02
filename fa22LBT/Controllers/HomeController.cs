using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;

namespace fa22LBT.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    Int32 disputeCount = _context.Disputes.Where(d => d.DisputeStatus == DisputeStatus.Submitted).ToList().Count();
                    ViewBag.Task1 = disputeCount;
                    
                    Int32 deposit = _context.Transactions.Where(d => d.TransactionApproved == false).ToList().Count();
                    if (deposit != 0)
                    {

                        ViewBag.Task2 = "Attention! You have " + deposit.ToString() + " deposits that need to be resolved";
                    }
                    else
                    {
                        ViewBag.Task2 = "You have no deposits needing attention.";
                    }
                }
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                List<BankAccount> bas = _context.BankAccounts.Where(ba => ba.Customer.Email == userLoggedIn.Email).ToList();
                if (bas.Count() == 0)
                {
                    ViewBag.Prompt = "PLEASE APPLY FOR A BANKING ACCOUNT USING ONE OF THE TWO BLUE BUTTONS BELOW";
                    return View();
                }

                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }
            return View("Index");
        }
    }
}