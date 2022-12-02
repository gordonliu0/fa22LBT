using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;

namespace fa22LBT.Controllers
{
    public class StockTransactionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StockTransactionsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private SelectList GetAllStocksSelectList()
        {
            List<Stock> stockList = _context.Stocks.Include(s => s.StockType).ToList();

            //convert the list to a SelectList by calling SelectList constructor
            //MonthID and MonthName are the names of the properties on the Month class
            //MonthID is the primary key
            SelectList selectList = new SelectList(stockList.OrderBy(m => m.TickerSymbol), "StockID", "StockQuickInfo");

            //return the SelectList
            return selectList;
        }

        // GET: StockTransactions
        public async Task<IActionResult> Index()
        {
              return _context.StockTransactions != null ? 
                          View(await _context.StockTransactions.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.StockTransactions'  is null.");
        }

        // GET: StockTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockTransactions == null)
            {
                return NotFound();
            }

            var stockTransaction = await _context.StockTransactions
                .Include(st => st.Stock)
                .FirstOrDefaultAsync(m => m.StockTransactionID == id);
            if (stockTransaction == null)
            {
                return NotFound();
            }

            return View(stockTransaction);
        }

        // GET: StockTransactions/Create
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }

            ViewBag.AllStocks = GetAllStocksSelectList();
            StockTransaction t = new StockTransaction();
            return View(t);
        }

        // POST: StockTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockTransactionID,QuantityShares,PricePerShare,OrderDate,Stock,Stock.StockPrice")] StockTransaction stockTransaction, int SelectedStock)
        {
            // Gather Selected Stock and Associated StockPortfolio, CashPortfolio 
            Stock dbStock = _context.Stocks.FirstOrDefault(o => o.StockID == SelectedStock);
            StockPortfolio dbStockPortfolio = _context.StockPortfolios.Include(o => o.BankAccount).Include(o => o.StockHoldings).ThenInclude(sh => sh.Stock).ThenInclude(s => s.StockType).FirstOrDefault(o => o.AppUser.UserName == User.Identity.Name);
            BankAccount dbBankAccount = _context.BankAccounts.FirstOrDefault(o => o.AccountID == dbStockPortfolio.BankAccount.AccountID);
            stockTransaction.PricePerShare = dbStock.StockPrice;
            stockTransaction.STransactionNo = Utilities.GenerateNumbers.GetTransactionNumber(_context);

            // Check if User has enough money in StockPortfolio Cash Account to pay for stocks
            if (stockTransaction.QuantityShares * stockTransaction.PricePerShare + 10 > dbBankAccount.AccountBalance)
            {
                ViewBag.AllStocks = GetAllStocksSelectList();
                int maxPurchase = (int)((dbBankAccount.AccountBalance - 10)/stockTransaction.PricePerShare);
                ViewBag.Message = "You do not have sufficient funds for this purchase. You can purchase a maximum of " + maxPurchase.ToString() + " of " + dbStock.TickerSymbol;
                return View(stockTransaction);
            }

            // Check if User has already bought this type of stock before
            StockHolding dbStockHolding = dbStockPortfolio.StockHoldings.FirstOrDefault(o => o.Stock.StockID == SelectedStock);
            // If no: create new stock holding and add the number of shares
            // If yes: add number of shares to old stock holding
            if (dbStockHolding == null)
            {
                StockHolding stockHolding = new StockHolding();
                stockHolding.Stock = dbStock;
                stockHolding.StockPortfolio = dbStockPortfolio;
                stockHolding.QuantityShares = stockTransaction.QuantityShares;
                _context.Add(stockHolding);
            } else
            {
                dbStockHolding.QuantityShares += stockTransaction.QuantityShares;
                _context.Update(dbStockHolding);
            }
            await _context.SaveChangesAsync();

            // Create and add new Transaction
            Transaction transaction = new Transaction();
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            transaction.TransactionType = TransactionType.Withdraw;
            transaction.TransactionAmount = stockTransaction.QuantityShares * stockTransaction.PricePerShare;
            transaction.OrderDate = stockTransaction.OrderDate;
            transaction.TransactionApproved = true;
            transaction.TransactionComments = "Stock Purchase - Account " + dbBankAccount.AccountNo;
            transaction.FromAccount = dbBankAccount.AccountNo;
            transaction.BankAccount = dbBankAccount;
            _context.Add(transaction);
            await _context.SaveChangesAsync();

            // Create and add new Fee
            Transaction transactionFee = new Transaction();
            transactionFee.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            transactionFee.TransactionType = TransactionType.Fee;
            transactionFee.TransactionAmount = 10;
            transactionFee.OrderDate = stockTransaction.OrderDate;
            transactionFee.TransactionApproved = true;
            transactionFee.TransactionComments = "Fee for purchase of " + dbStock.StockName;
            transactionFee.FromAccount = dbBankAccount.AccountNo;
            transactionFee.BankAccount = dbBankAccount;
            _context.Add(transactionFee);
            await _context.SaveChangesAsync();

            // Update CashBalances for dbBankAccount and dbStockPortfolio
            dbBankAccount.AccountBalance -= stockTransaction.QuantityShares * stockTransaction.PricePerShare + 10;
            dbStockPortfolio.CashBalance = dbBankAccount.AccountBalance;
            _context.Update(dbBankAccount);

            // Update dbStockPortfolio balanced status
            dbStockPortfolio.CalculateBalancedStatus();
            _context.Update(dbStockPortfolio);
            await _context.SaveChangesAsync();

            // Create and Add new StockTransaction
            stockTransaction.StockPortfolio = dbStockPortfolio;
            stockTransaction.Stock = dbStock;
            _context.Add(stockTransaction);
            await _context.SaveChangesAsync();

            return View("PurchaseConfirmation", stockTransaction);
        }

        //// GET: StockTransactions/Sell/5
        //public async Task<IActionResult> Sell(int? id)
        //{
        //    if (id == null || _context.StockTransactions == null)
        //    {
        //        return NotFound();
        //    }

        //    var stockTransaction = await _context.StockTransactions.FindAsync(id);
        //    if (stockTransaction == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(stockTransaction);
        //}

        //// POST: StockTransactions/Sell/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Sell(int id, [Bind("StockTransactionID,QuantityShares,PricePerShare,OrderDate")] StockTransaction stockTransaction)
        //{
        //    if (id != stockTransaction.StockTransactionID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(stockTransaction);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StockTransactionExists(stockTransaction.StockTransactionID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(stockTransaction);
        //}

        //// GET: StockTransactions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.StockTransactions == null)
        //    {
        //        return NotFound();
        //    }

        //    var stockTransaction = await _context.StockTransactions
        //        .FirstOrDefaultAsync(m => m.StockTransactionID == id);
        //    if (stockTransaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(stockTransaction);
        //}

        //// POST: StockTransactions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.StockTransactions == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.StockTransactions'  is null.");
        //    }
        //    var stockTransaction = await _context.StockTransactions.FindAsync(id);
        //    if (stockTransaction != null)
        //    {
        //        _context.StockTransactions.Remove(stockTransaction);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool StockTransactionExists(int id)
        {
          return (_context.StockTransactions?.Any(e => e.StockTransactionID == id)).GetValueOrDefault();
        }
    }
}
