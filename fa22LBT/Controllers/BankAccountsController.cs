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
    public class BankAccountsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BankAccountsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
              return _context.BankAccounts != null ? 
                          View(await _context.BankAccounts.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.BankAccounts'  is null.");
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BankAccounts == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts
                .Include(m => m.Transactions)
                .FirstOrDefaultAsync(m => m.AccountID == id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public async Task<IActionResult> Create()
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.Customer = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(bankAccount);
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Customer,AccountName,AccountType,AccountBalance")] BankAccount bankAccount)
        {
            bankAccount.AccountNo = Utilities.GenerateNumbers.GetAccountNumber(_context);

            // Default AccountNames
            if (bankAccount.AccountName == null)
            {
                if (bankAccount.AccountType == AccountTypes.Savings)
                {
                    bankAccount.AccountName = "Longhorn Savings";
                }
                else if (bankAccount.AccountType == AccountTypes.Checking)
                {
                    bankAccount.AccountName = "Longhorn Checking";
                }
                else
                {
                    bankAccount.AccountName = "Longhorn IRA";
                }
            }
            // bank account balance should create new transaction
            bankAccount.Customer = await _userManager.FindByNameAsync(bankAccount.Customer.UserName);
            ModelState.Remove("AccountID");
            ModelState.Remove("AccountNo");
            _context.Add(bankAccount);
            await _context.SaveChangesAsync();

            return RedirectToAction("InitialDeposit", "Transactions", new { SelectedBankAccount = bankAccount.AccountID });

            //return View("Confirmation", bankAccount);
            //return RedirectToAction("Details", "BankAccounts", new { id = bankAccount.AccountID });
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BankAccounts == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AccountID,AccountNo,AccountName,AccountType,AccountBalance,IsApproved")] BankAccount bankAccount)
        {
            if (id != bankAccount.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.AccountID))
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
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.BankAccounts == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.BankAccounts == null)
            {
                return Problem("Entity set 'AppDbContext.BankAccounts'  is null.");
            }
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount != null)
            {
                _context.BankAccounts.Remove(bankAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(string id)
        {
          return (_context.BankAccounts?.Any(e => e.AccountID == id)).GetValueOrDefault();
        }
    }
}
