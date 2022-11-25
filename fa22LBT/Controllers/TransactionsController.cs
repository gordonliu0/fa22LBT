using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;

namespace fa22LBT.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        private SelectList GetAllAccountsSelectList()
        {
            List<BankAccount> bankAccountList = _context.BankAccounts.Where(o => o.Customer.UserName == User.Identity.Name).ToList();

            //convert the list to a SelectList by calling SelectList constructor
            //MonthID and MonthName are the names of the properties on the Month class
            //MonthID is the primary key
            SelectList monthSelectList = new SelectList(bankAccountList.OrderBy(m => m.AccountNo), "AccountID", "AccountQuickInfo");

            //return the SelectList
            return monthSelectList;
        }

        public TransactionsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
              return _context.Transactions != null ? 
                          View(await _context.Transactions.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Transactions'  is null.");
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/CreateTransfer
        public IActionResult CreateTransfer()
        {
            ViewBag.CustomerAccounts = GetAllAccountsSelectList();
            Transaction t = new Transaction();
            return View(t);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferIRAUnder65([Bind("TransactionID,TransactionNumber,TransactionType,TransactionAmount,OrderDate,TransactionApproved,TransactionComments,BankAccount")] Transaction transaction, string ToBankAccount, string FromBankAccount)
        {
            ViewBag.IsInitial = false;
            BankAccount dbToBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == ToBankAccount);
            BankAccount dbFromBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == FromBankAccount);

            // CREATE TO TRANSACTION
            Transaction tTo = new Transaction();
            tTo.TransactionAmount = transaction.TransactionAmount;
            tTo.TransactionComments = "Transfer to " + ToBankAccount.ToString();
            tTo.TransactionType = TransactionType.Deposit;
            tTo.TransactionApproved = true;
            tTo.OrderDate = transaction.OrderDate;
            Int32 tToTransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            await Create(tTo, ToBankAccount);


            // CREATE FROM TRANSACTION
            Transaction tFrom = new Transaction();
            tFrom.TransactionAmount = transaction.TransactionAmount;
            tFrom.TransactionComments = "Transfer from " + ToBankAccount.ToString();
            tFrom.BankAccount = dbFromBankAccount;
            tFrom.TransactionType = TransactionType.Withdraw;
            tFrom.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context)-1;
            tFrom.TransactionApproved = true;
            tFrom.OrderDate = transaction.OrderDate;
            tFrom.FromAccount = dbFromBankAccount.AccountNo;
            dbFromBankAccount.AccountBalance -= transaction.TransactionAmount;
            _context.Add(tFrom);
            await _context.SaveChangesAsync();

            // CREATE FEE TRANSACTION
            await this.AddIRAFee(FromBankAccount, tFrom.TransactionNumber);
            return RedirectToAction("Index");
        }
        

        public async Task<IActionResult> FinishCreateTransfer(Transaction transaction, string ToBankAccount, string FromBankAccount)
        {
            BankAccount dbToBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == ToBankAccount);
            BankAccount dbFromBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == FromBankAccount);

            Transaction tTo = new Transaction();
            tTo.TransactionAmount = transaction.TransactionAmount;
            tTo.TransactionComments = "Transfer to " + ToBankAccount.ToString();
            tTo.TransactionType = TransactionType.Deposit;
            Int32 tToTransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            await Create(tTo, ToBankAccount);
            await _context.SaveChangesAsync();

            Transaction dbDeposit = _context.Transactions.FirstOrDefault(db => db.TransactionNumber == tToTransactionNumber);

            // If deposit is not approved, approve it and update that bank account
            if (dbDeposit.TransactionAmount > 5000){
                dbDeposit.TransactionApproved = true;
                dbToBankAccount.AccountBalance += dbDeposit.TransactionAmount;
                _context.Update(dbDeposit);
                await _context.SaveChangesAsync();
            }

            Transaction tFrom = new Transaction();
            tFrom.TransactionAmount = transaction.TransactionAmount;
            tFrom.TransactionComments = "Transfer from " + ToBankAccount.ToString();
            tFrom.TransactionType = TransactionType.Withdraw;
            Int32 tFromTransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            await Create(tFrom, FromBankAccount);

            Transaction dbWithdraw = _context.Transactions.FirstOrDefault(db => db.TransactionNumber == tFromTransactionNumber);
            dbWithdraw.TransactionNumber -= 1;
            _context.Update(dbWithdraw);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransfer(Transaction transaction, string ToBankAccount, string FromBankAccount)
        {
            ViewBag.CustomerAccounts = GetAllAccountsSelectList();
            ViewBag.ToBankAccount = ToBankAccount;
            ViewBag.FromBankAccount = FromBankAccount;

            BankAccount dbToBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == ToBankAccount);
            BankAccount dbFromBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == FromBankAccount);

            if (dbToBankAccount.AccountType == AccountTypes.IRA)
            {
                if (dbToBankAccount.Customer.Age >= 70)
                {
                    ViewBag.CanContribute = false;
                    ViewBag.Message = "You are too old to make a contribution to your IRA.";
                    return View("ContributionModificationTransfer", transaction);
                }
                else
                {
                    if (dbToBankAccount.Contribution == 5000)
                    {
                        ViewBag.Message = "You have made your maximum contribution this year.";
                        ViewBag.CanContribute = false;
                        return View("ContributionModificationTransfer", transaction);
                    }
                    else if (dbToBankAccount.Contribution + transaction.TransactionAmount > 5000)
                    {
                        Decimal allowedContribution = 5000m - dbToBankAccount.Contribution;
                        ViewBag.Message = "You can contribute an additional " + allowedContribution.ToString();
                        ViewBag.CanContribute = true;
                        ViewBag.IsInitial = false;
                        ViewBag.allowedContribution = allowedContribution;
                        return View("ContributionModificationTransfer", transaction);
                    }
                    //else
                    //{
                    //    transaction.TransactionApproved = true;
                    //    dbToBankAccount.AccountBalance += transaction.TransactionAmount;
                    //    dbToBankAccount.Contribution += transaction.TransactionAmount;
                    //    _context.Update(dbToBankAccount);
                    //}
                } 
            } else
            {
                transaction.ToAccount = dbToBankAccount.AccountNo;
                if (transaction.TransactionAmount > 5000)
                {
                    transaction.TransactionApproved = false;
                }
                else
                {
                    transaction.TransactionApproved = true;
                }
            }

            if (dbFromBankAccount.AccountType == AccountTypes.IRA)
            {
                if (dbFromBankAccount.Customer.Age > 65)
                {
                    if (transaction.TransactionAmount > dbFromBankAccount.AccountBalance)
                    {
                        ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                        ViewBag.Overdrawn = "You do not have sufficient funds for this withdrawal.";
                        return View(transaction);
                    }
                    else
                    {
                        transaction.TransactionApproved = true;
                        dbFromBankAccount.AccountBalance -= transaction.TransactionAmount;
                        _context.Update(dbFromBankAccount);
                    }
                }
                else
                {
                    if (transaction.TransactionAmount > dbFromBankAccount.AccountBalance)
                    {
                        ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                        ViewBag.Overdrawn = "You do not have sufficient funds for this withdrawal.";
                        return View(transaction);
                    }
                    else if (transaction.TransactionAmount > 3000)
                    {
                        ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                        ViewBag.Overdrawn = "You cannot withdraw more than 3000 from IRA if you are 65 or under.";
                        return View(transaction);
                    }
                    else
                    {
                        ViewBag.SelectedBankAccount = dbFromBankAccount;
                        ViewBag.LowerOption = transaction.TransactionAmount - 30;

                        if (transaction.TransactionAmount + 30 > dbFromBankAccount.AccountBalance)
                        {
                            ViewBag.UpperOption = dbFromBankAccount.AccountBalance - 30;
                        }
                        else
                        {
                            ViewBag.UpperOption = transaction.TransactionAmount;
                        }

                        return View("IRAWithdrawOptionsTransfer", transaction);
                    }
                }
            } else
            {
                if (transaction.TransactionAmount > dbFromBankAccount.AccountBalance)
                {
                    ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                    ViewBag.Overdrawn = "You do not have sufficient funds for this withdrawal.";
                    return View(transaction);
                }
                //else
                //{
                //    transaction.TransactionApproved = true;
                //    dbFromBankAccount.AccountBalance -= transaction.TransactionAmount;
                //    _context.Update(dbFromBankAccount);
                //}
            }

            await FinishCreateTransfer(transaction, ToBankAccount, FromBankAccount);
            return RedirectToAction("Index");
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewBag.CustomerAccounts = GetAllAccountsSelectList();
            Transaction t = new Transaction();
            return View(t);
        }

        public async Task<IActionResult> InitialDepositIRA(Transaction transaction, string SelectedBankAccount)
        {
            ViewBag.IsInitial = true;
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            BankAccount dbBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == SelectedBankAccount);
            transaction.BankAccount = dbBankAccount;
            transaction.ToAccount = dbBankAccount.AccountNo;
            transaction.TransactionComments = "Initial Deposit";
            transaction.TransactionType = TransactionType.Deposit;
            dbBankAccount.AccountBalance = 0;
            _context.Update(dbBankAccount);

            if (dbBankAccount.Customer.Age >= 70)
            {
                ViewBag.CanContribute = false;
                ViewBag.Message = "You are too old to make a contribution to your IRA.";
            }
            else
            {
                if (dbBankAccount.Contribution == 5000)
                {
                    ViewBag.Message = "You have made your maximum contribution this year.";
                    ViewBag.CanContribute = false;
                    ViewBag.SelectedBankAccount = SelectedBankAccount;
                    return View("ContributionModification", transaction);
                }
                else if (dbBankAccount.Contribution + transaction.TransactionAmount > 5000)
                {
                    Decimal allowedContribution = 5000m - dbBankAccount.Contribution;
                    ViewBag.Message = "You can contribute an additional " + allowedContribution.ToString();
                    ViewBag.CanContribute = true;
                    ViewBag.SelectedBankAccount = SelectedBankAccount;
                    ViewBag.allowedContribution = allowedContribution;
                    return View("ContributionModification", transaction);
                }
                else
                {
                    transaction.TransactionApproved = true;
                    dbBankAccount.AccountBalance += transaction.TransactionAmount;
                    dbBankAccount.Contribution += transaction.TransactionAmount;
                    _context.Update(dbBankAccount);
                }
            }

            if (transaction.TransactionApproved == false)
            {
                ViewBag.Message2 = "Welcome to your new account! Your initial deposit is pending approval, but feel free to take a look around. ";
            }
            else
            {
                ViewBag.Message2 = "Welcome to your new account! Your initial deposit has been added.";
            }

            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return View("~/Views/BankAccounts/Confirmation.cshtml", dbBankAccount);

        }

        public async Task<IActionResult> InitialDeposit(string SelectedBankAccount)
        {
            ViewBag.IsInitial = true;
            Transaction transaction = new Transaction();
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            BankAccount dbBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == SelectedBankAccount);
            transaction.BankAccount = dbBankAccount;
            transaction.ToAccount = dbBankAccount.AccountNo;
            transaction.TransactionComments = "Initial Deposit";
            transaction.TransactionType = TransactionType.Deposit;
            transaction.TransactionAmount = dbBankAccount.AccountBalance;
            dbBankAccount.AccountBalance = 0;
            _context.Update(dbBankAccount);
            await _context.SaveChangesAsync();

            if (dbBankAccount.AccountType == AccountTypes.IRA)
            {
                if (dbBankAccount.Customer.Age >= 70)
                {
                    ViewBag.CanContribute = false;
                    ViewBag.Message = "You are too old to make a contribution to your IRA.";
                }
                else
                {
                    if (dbBankAccount.Contribution == 5000)
                    {
                        ViewBag.Message = "You have made your maximum contribution this year.";
                        ViewBag.CanContribute = false;
                        ViewBag.SelectedBankAccount = SelectedBankAccount;
                        return View("ContributionModification", transaction);
                    }
                    else if (dbBankAccount.Contribution + transaction.TransactionAmount > 5000)
                    {
                        Decimal allowedContribution = 5000m - dbBankAccount.Contribution;
                        ViewBag.Message = "You can contribute an additional " + allowedContribution.ToString();
                        ViewBag.CanContribute = true;
                        ViewBag.SelectedBankAccount = SelectedBankAccount;
                        ViewBag.allowedContribution = allowedContribution;
                        return View("ContributionModification", transaction);
                    }
                    else
                    {
                        transaction.TransactionApproved = true;
                        dbBankAccount.AccountBalance += transaction.TransactionAmount;
                        dbBankAccount.Contribution += transaction.TransactionAmount;
                        _context.Update(dbBankAccount);
                    }
                }
            }
            else
            {
                if (transaction.TransactionType == TransactionType.Deposit)
                {
                    if (transaction.TransactionAmount > 5000)
                    {
                        transaction.TransactionApproved = false;
                    }
                    else
                    {
                        transaction.TransactionApproved = true;
                        dbBankAccount.AccountBalance += transaction.TransactionAmount;
                        _context.Update(dbBankAccount);
                    }
                }
            }

            // Update confirmation message
            if (transaction.TransactionApproved == false)
            {
                ViewBag.Message2 = "Welcome to your new account! Your initial deposit is pending approval, but feel free to take a look around. ";
            } else
            {
                ViewBag.Message2 = "Welcome to your new account! Your initial deposit has been added.";
            }

            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return View("~/Views/BankAccounts/Confirmation.cshtml", dbBankAccount);

        }

        public async Task AddIRAFee(String SelectedBankAccount, Int32 TransactionNumber)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            BankAccount dbBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == SelectedBankAccount);
            transaction.BankAccount = dbBankAccount;
            transaction.FromAccount = dbBankAccount.AccountNo;
            transaction.TransactionComments = "Fee for IRA Withdrawal: " + TransactionNumber.ToString();
            transaction.TransactionType = TransactionType.Fee;
            transaction.TransactionAmount = 30;
            dbBankAccount.AccountBalance -= 30;
            _context.Update(dbBankAccount);
            _context.Add(transaction);
            await _context.SaveChangesAsync();
        }
        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WithdrawIRAUnder65([Bind("TransactionID,TransactionNumber,TransactionType,TransactionAmount,OrderDate,TransactionApproved,TransactionComments,BankAccount")] Transaction transaction, String SelectedBankAccount)
        {
            ViewBag.IsInitial = false;
            BankAccount dbBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == SelectedBankAccount);
            transaction.BankAccount = dbBankAccount;
            transaction.TransactionType = TransactionType.Withdraw;
            dbBankAccount.AccountBalance -= transaction.TransactionAmount;
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            transaction.FromAccount = dbBankAccount.AccountNo;
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            await this.AddIRAFee(SelectedBankAccount, transaction.TransactionNumber);
            return RedirectToAction(nameof(Index));
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,TransactionNumber,TransactionType,TransactionAmount,OrderDate,TransactionApproved,TransactionComments,BankAccount")] Transaction transaction, String SelectedBankAccount)
        {
            ViewBag.IsInitial = false;
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            BankAccount dbBankAccount = _context.BankAccounts.Include(db => db.Customer).FirstOrDefault(db => db.AccountID == SelectedBankAccount) ;
            transaction.BankAccount = dbBankAccount;

            if (dbBankAccount.AccountType == AccountTypes.IRA)
            {
                if (transaction.TransactionType == TransactionType.Deposit)
                {
                    transaction.ToAccount = dbBankAccount.AccountNo;
                    if  (dbBankAccount.Customer.Age >= 70)
                    {
                        ViewBag.CanContribute = false;
                        ViewBag.Message = "You are too old to make a contribution to your IRA.";
                        return View("ContributionModification", transaction);
                    } else
                    {
                        if (dbBankAccount.Contribution == 5000)
                        {
                            ViewBag.Message = "You have made your maximum contribution this year.";
                            ViewBag.CanContribute = false;
                            ViewBag.SelectedBankAccount = SelectedBankAccount;
                            return View("ContributionModification", transaction);
                        }
                        else if (dbBankAccount.Contribution + transaction.TransactionAmount > 5000)
                        {
                            Decimal allowedContribution = 5000m - dbBankAccount.Contribution;
                            ViewBag.Message = "You can contribute an additional " + allowedContribution.ToString();
                            ViewBag.CanContribute = true;
                            ViewBag.SelectedBankAccount = SelectedBankAccount;
                            ViewBag.allowedContribution = allowedContribution;
                            return View("ContributionModification", transaction);
                        }
                        else
                        {
                            transaction.TransactionApproved = true;
                            dbBankAccount.AccountBalance += transaction.TransactionAmount;
                            dbBankAccount.Contribution += transaction.TransactionAmount;
                            _context.Update(dbBankAccount);
                        }
                    }
                    
                }
                else if (transaction.TransactionType == TransactionType.Withdraw)
                {
                    transaction.FromAccount = dbBankAccount.AccountNo;
                    if (dbBankAccount.Customer.Age > 65)
                    {
                        if (transaction.TransactionAmount > dbBankAccount.AccountBalance)
                        {
                            ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                            ViewBag.Overdrawn = "You do not have sufficient funds for this withdrawal.";
                            return View(transaction);
                        }
                        else
                        {
                            transaction.TransactionApproved = true;
                            dbBankAccount.AccountBalance -= transaction.TransactionAmount;
                            _context.Update(dbBankAccount);
                        }
                    } else
                    {
                        if (transaction.TransactionAmount > dbBankAccount.AccountBalance)
                        {
                            ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                            ViewBag.Overdrawn = "You do not have sufficient funds for this withdrawal.";
                            return View(transaction);
                        } else if (transaction.TransactionAmount > 3000)
                        {
                            ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                            ViewBag.Overdrawn = "You cannot withdraw more than 3000 from IRA if you are 65 or under.";
                            return View(transaction);
                            
                        } else
                        {
                            ViewBag.SelectedBankAccount = SelectedBankAccount;
                            ViewBag.LowerOption = transaction.TransactionAmount - 30;

                            if (transaction.TransactionAmount + 30 > dbBankAccount.AccountBalance)
                            {
                                ViewBag.UpperOption = dbBankAccount.AccountBalance - 30;
                            } else
                            {
                                ViewBag.UpperOption = transaction.TransactionAmount;
                            }
                            
                            return View("IRAWithdrawOptions", transaction);
                        }
                    }
                    
                }
            } else
            {
                if (transaction.TransactionType == TransactionType.Deposit)
                {
                    transaction.ToAccount = dbBankAccount.AccountNo;
                    if (transaction.TransactionAmount > 5000)
                    {
                        transaction.TransactionApproved = false;
                    }
                    else
                    {
                        transaction.TransactionApproved = true;
                        dbBankAccount.AccountBalance += transaction.TransactionAmount;
                        _context.Update(dbBankAccount);
                    }
                }
                else if (transaction.TransactionType == TransactionType.Withdraw)
                {
                    transaction.FromAccount = dbBankAccount.AccountNo;
                    if (transaction.TransactionAmount > dbBankAccount.AccountBalance)
                    {
                        ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                        ViewBag.Overdrawn = "You do not have sufficient funds for this withdrawal.";
                        return View(transaction);
                    }
                    else
                    {
                        transaction.TransactionApproved = true;
                        dbBankAccount.AccountBalance -= transaction.TransactionAmount;
                        _context.Update(dbBankAccount);
                    }
                }
            }
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionID,TransactionNumber,TransactionType,TransactionAmount,OrderDate,TransactionApproved,TransactionComments,ToAccount,FromAccount")] Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionID))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'AppDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return (_context.Transactions?.Any(e => e.TransactionID == id)).GetValueOrDefault();
        }
    }
}
