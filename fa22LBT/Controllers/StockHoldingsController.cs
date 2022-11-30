using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;

namespace fa22LBT.Controllers
{
    public class StockHoldingsController : Controller
    {
        private readonly AppDbContext _context;

        public StockHoldingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StockHoldings
        public async Task<IActionResult> Index()
        {
              return _context.StockHoldings != null ? 
                          View(await _context.StockHoldings.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.StockHoldings'  is null.");
        }

        // GET: StockHoldings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockHoldings == null)
            {
                return NotFound();
            }

            var stockHolding = await _context.StockHoldings
                .FirstOrDefaultAsync(m => m.StockHoldingID == id);
            if (stockHolding == null)
            {
                return NotFound();
            }

            return View(stockHolding);
        }

        // GET: StockHoldings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockHoldings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockHoldingID,QuantityShares")] StockHolding stockHolding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockHolding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockHolding);
        }

        // GET: StockHoldings/Sell/5
        public async Task<IActionResult> Sell(int? id)
        {
            if (id == null || _context.StockHoldings == null)
            {
                return NotFound();
            }

            StockHolding stockHolding = _context.StockHoldings.Include(sh => sh.Stock).FirstOrDefault(sh => sh.StockHoldingID == id);
            if (stockHolding == null)
            {
                return NotFound();
            }
            return View(stockHolding);
        }

        // POST: StockHoldings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell(int id, [Bind("StockHoldingID,QuantityShares")] StockHolding stockHolding, int numSold)
        {
            StockHolding dbStockHolding = _context.StockHoldings.Include(sh => sh.Stock).FirstOrDefault(sh => sh.StockHoldingID == id);
            Stock dbStock = _context.Stocks.Include(sh => sh.StockTransactions).FirstOrDefault(sh => sh.StockID == dbStockHolding.Stock.StockID);
            StockPortfolio stockPortfolio = _context.StockPortfolios.Include(sp => sp.StockTransactions).ThenInclude(st => st.Stock).Include(sp => sp.BankAccount).Include(sp => sp.StockHoldings).ThenInclude(sh => sh.Stock).ThenInclude(s => s.StockType).FirstOrDefault(o => o.AppUser.UserName == User.Identity.Name);
            BankAccount dbBankAccount = _context.BankAccounts.FirstOrDefault(ba => ba.AccountID == stockPortfolio.BankAccount.AccountID);
            IEnumerable<int> relevantStockTransactions =
                from st in stockPortfolio.StockTransactions
                where st.Stock.StockID == dbStockHolding.Stock.StockID
                orderby st.StockTransactionID
                select st.StockTransactionID;
            numSold = stockHolding.QuantityShares;

            if (numSold > dbStockHolding.QuantityShares)
            {
                ViewBag.Message = "You have attempted to sell more shares than you own. Please try again.";
                return View(dbStockHolding);
            }
            else if (numSold <= 0)
            {
                ViewBag.Message = "You must sell at least 1 share and it must be an integer. Please try again.";
                return View(dbStockHolding);
            }
            else
            {
                // Update StockHolding (reduce QuantityShares or Remove if 0)
                dbStockHolding.QuantityShares -= numSold;
                if (dbStockHolding.QuantityShares == 0)
                {
                    _context.StockHoldings.Remove(dbStockHolding);
                }
                else
                {
                    _context.Update(dbStockHolding);
                }

                // Calculate gain/loss and Update StockTransactions
                int numNeedSold = numSold;
                foreach (int stID in relevantStockTransactions)
                {
                    // Grab relevantStockTransaction in order of purchase
                    StockTransaction dbStockTransaction = _context.StockTransactions.Include(st => st.Stock).FirstOrDefault(st => st.StockTransactionID == stID);

                    // If the number that we want to sell is greater than this transaction's quantity,
                    // Create Sell Transaction,
                    if (numNeedSold > 0 && numNeedSold >= dbStockTransaction.QuantityShares)
                    {
                        Transaction t = new Transaction();
                        t.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
                        t.TransactionType = TransactionType.Deposit;
                        t.TransactionAmount = dbStockTransaction.QuantityShares * dbStock.StockPrice;
                        t.TransactionApproved = true;
                        t.ToAccount = stockPortfolio.BankAccount.AccountNo;
                        t.BankAccount = dbBankAccount;
                        Decimal gl = dbStockTransaction.CurrentValue - dbStockTransaction.InitialValue;
                        t.TransactionComments = dbStockTransaction.QuantityShares.ToString() + " of Stock " + dbStock.StockName + ", Initial Price: $" + dbStockTransaction.PricePerShare.ToString() + ", Current Price: $" + dbStock.StockPrice.ToString() + ", Gains/Losses: $" + gl.ToString();
                        numNeedSold -= dbStockTransaction.QuantityShares;
                        _context.StockTransactions.Remove(dbStockTransaction);
                        _context.Add(t);
                        await _context.SaveChangesAsync();
                    } else if (numNeedSold > 0 && numNeedSold < dbStockTransaction.QuantityShares)
                    {
                        Transaction t = new Transaction();
                        t.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
                        t.TransactionType = TransactionType.Deposit;
                        t.TransactionAmount = numNeedSold * dbStock.StockPrice;
                        t.TransactionApproved = true;
                        t.ToAccount = stockPortfolio.BankAccount.AccountNo;
                        t.BankAccount = dbBankAccount;
                        Decimal gl = (dbStock.StockPrice - dbStockTransaction.PricePerShare) * numNeedSold;
                        t.TransactionComments = numNeedSold.ToString() + " of Stock " + dbStock.StockName + ", Initial Price: $" + dbStockTransaction.PricePerShare.ToString() + ", Current Price: $" + dbStock.StockPrice.ToString() + ", Gains/Losses: $" + gl.ToString();
                        dbStockTransaction.QuantityShares -= numNeedSold;
                        numNeedSold = 0;
                        _context.Update(dbStockTransaction);
                        _context.Add(t);
                        await _context.SaveChangesAsync();
                    }
                }

                // Add Selling Fee of 15
                Transaction fee = new Transaction();
                fee.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
                fee.TransactionType = TransactionType.Fee;
                fee.TransactionAmount = 15;
                fee.TransactionApproved = true;
                fee.FromAccount = stockPortfolio.BankAccount.AccountNo;
                fee.BankAccount = dbBankAccount;
                fee.TransactionComments = "Fee for sale of " + dbStock.StockName;
                _context.Add(fee);
                await _context.SaveChangesAsync();

                // Change cash bank account balance
                dbBankAccount.AccountBalance += numSold * dbStock.StockPrice;
                _context.Update(dbBankAccount);

                // Recalculate stockPortfolio balanced state
                stockPortfolio.CalculateBalancedStatus();
                _context.Update(stockPortfolio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "StockPortfolios", new { id = stockPortfolio.AccountID});

        }

        // GET: StockHoldings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockHoldings == null)
            {
                return NotFound();
            }

            var stockHolding = await _context.StockHoldings
                .FirstOrDefaultAsync(m => m.StockHoldingID == id);
            if (stockHolding == null)
            {
                return NotFound();
            }

            return View(stockHolding);
        }

        // POST: StockHoldings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockHoldings == null)
            {
                return Problem("Entity set 'AppDbContext.StockHoldings'  is null.");
            }
            var stockHolding = await _context.StockHoldings.FindAsync(id);
            if (stockHolding != null)
            {
                _context.StockHoldings.Remove(stockHolding);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockHoldingExists(int id)
        {
          return (_context.StockHoldings?.Any(e => e.StockHoldingID == id)).GetValueOrDefault();
        }
    }
}
