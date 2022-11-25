using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using fa22LBT.DAL;
using fa22LBT.Models;

namespace fa22LBT.Controllers
{
    public class StocksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StocksController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private SelectList GetAllStockTypesSelectList()
        {
            List<StockType> stockTypeList = _context.StockTypes.ToList();

            //convert the list to a SelectList by calling SelectList constructor
            //MonthID and MonthName are the names of the properties on the Month class
            //MonthID is the primary key
            SelectList monthSelectList = new SelectList(stockTypeList.OrderBy(m => m.StockTypeName), "StockTypeID", "StockTypeName");

            //return the SelectList
            return monthSelectList;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
              return _context.Stocks != null ? 
                          View(await _context.Stocks.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Stocks'  is null.");
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewBag.AllStockTypes = GetAllStockTypesSelectList();
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockID,TickerSymbol,StockName,StockPrice")] Stock stock, Int32 SelectedStockType)
        {
            StockType dbStockType = _context.StockTypes.FirstOrDefault(m => m.StockTypeID == SelectedStockType);
            stock.StockType = dbStockType;
            ModelState.Remove("StockID");
            ModelState.Remove("StockType");
            ModelState.Remove("StockTransactions");
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllStockTypes = GetAllStockTypesSelectList();
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockID,TickerSymbol,StockName,StockPrice")] Stock stock)
        {
            if (id != stock.StockID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.StockID))
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
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stocks == null)
            {
                return Problem("Entity set 'AppDbContext.Stocks'  is null.");
            }
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
          return (_context.Stocks?.Any(e => e.StockID == id)).GetValueOrDefault();
        }
    }
}
