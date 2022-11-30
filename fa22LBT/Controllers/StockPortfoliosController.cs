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

namespace fa22LBT.Controllers
{
    public class StockPortfoliosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StockPortfoliosController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StockPortfolios
        public async Task<IActionResult> Index()
        {
              return _context.StockPortfolios != null ? 
                          View(await _context.StockPortfolios.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.StockPortfolios'  is null.");
        }

        // GET: StockPortfolios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.StockPortfolios == null)
            {
                return NotFound();
            }

            var stockPortfolio = await _context.StockPortfolios
                .Include(m => m.StockHoldings)
                .ThenInclude(sh => sh.Stock)
                .Include(m => m.StockTransactions)
                .ThenInclude(st => st.Stock)
                .Include(m => m.BankAccount)
                .ThenInclude(ba => ba.Transactions)
                .FirstOrDefaultAsync(m => m.AccountID == id);

            if (stockPortfolio == null)
            {
                return NotFound();
            }

            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Create
        public async Task<IActionResult> Create()
        {
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountID,AccountNo,AccountName,CashBalance,IsBalanced,IsApproved,AppUser")] StockPortfolio stockPortfolio, int AccountBalance)
        {
            BankAccount cashAccount = new BankAccount();
            cashAccount.AccountType = AccountTypes.StockPortfolio;

            if (stockPortfolio.AccountName == null)
            {
                stockPortfolio.AccountName = "Longhorn Stock Portfolio";
            }

            cashAccount.AccountName = stockPortfolio.AccountName;

            cashAccount.AccountNo = Utilities.GenerateNumbers.GetAccountNumber(_context);
            stockPortfolio.AccountNo = Utilities.GenerateNumbers.GetAccountNumber(_context);

            cashAccount.Customer = await _userManager.FindByNameAsync(stockPortfolio.AppUser.UserName);
            stockPortfolio.AppUser = cashAccount.Customer;

            _context.Add(cashAccount);

            stockPortfolio.BankAccount = cashAccount;
            _context.Add(stockPortfolio);

            await _context.SaveChangesAsync();
            return RedirectToAction("InitialDepositStockPortfolio", "Transactions", new { SelectedBankAccount = cashAccount.AccountID, AccountBalance = AccountBalance});
        }

        // GET: StockPortfolios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StockPortfolios == null)
            {
                return NotFound();
            }

            var stockPortfolio = await _context.StockPortfolios.FindAsync(id);
            if (stockPortfolio == null)
            {
                return NotFound();
            }
            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AccountID,AccountNo,AccountName,CashBalance,IsBalanced,IsApproved")] StockPortfolio stockPortfolio)
        {
            if (id != stockPortfolio.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockPortfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockPortfolioExists(stockPortfolio.AccountID))
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
            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StockPortfolios == null)
            {
                return NotFound();
            }

            var stockPortfolio = await _context.StockPortfolios
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (stockPortfolio == null)
            {
                return NotFound();
            }

            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StockPortfolios == null)
            {
                return Problem("Entity set 'AppDbContext.StockPortfolios'  is null.");
            }
            var stockPortfolio = await _context.StockPortfolios.FindAsync(id);
            if (stockPortfolio != null)
            {
                _context.StockPortfolios.Remove(stockPortfolio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockPortfolioExists(string id)
        {
          return (_context.StockPortfolios?.Any(e => e.AccountID == id)).GetValueOrDefault();
        }
    }
}
