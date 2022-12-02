    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;
using fa22LBT.Models.ViewModels;

namespace fa22LBT.Controllers
{
    [Authorize]
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

        public ActionResult DetailedSearch(string id)
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult DisplaySearchResults(SearchViewModel searchViewModel, string id)
        {
            // create LINQ query
            var query = from r in _context.Transactions.Where(m => m.BankAccount.AccountID == id)
                        select r;
            
            // FILTER
            if (searchViewModel.SearchDescription != null)
            {
                query = query.Where(r => r.TransactionComments.Contains(searchViewModel.SearchDescription));
            }

            if (searchViewModel.SearchType != null)
            {
                query = query.Where(r => r.TransactionType.Equals(searchViewModel.SearchType));
            }

            if (searchViewModel.SearchAmountLower != null)
            {
                query = query.Where(r => r.TransactionAmount >= searchViewModel.SearchAmountLower);
            }

            if (searchViewModel.SearchAmountUpper != null)
            {
                query = query.Where(r => r.TransactionAmount <= searchViewModel.SearchAmountUpper);
            }

            if (searchViewModel.SearchTNumber != null)
            {
                query = query.Where(r => r.TransactionNumber.Equals(searchViewModel.SearchTNumber));
            }

            if (searchViewModel.SearchDateFrom != null)
            {
                query = query.Where(r => r.OrderDate >= searchViewModel.SearchDateFrom);
            }

            if (searchViewModel.SearchDateTo != null)
            {
                query = query.Where(r => r.OrderDate <= searchViewModel.SearchDateTo);
            }

            // TODO: NEED TO FILL IN SEARCH LANGUAGE

            //Convert query into list with type Repository
            List<Transaction> SelectedTransactions = query.ToList();

            //Populate the view bag with a count of all repositories 
            ViewBag.AllRepositories = _context.Transactions.Where(m => m.BankAccount.AccountID == id).Count();

            //Populate the view bag with a count of selected repositories 
            ViewBag.SelectedRepositories = SelectedTransactions.Count;

            // ORDER, ASCENDING, TYPE
            if (searchViewModel.Ascending)
            {
                if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.num)
                {
                    SelectedTransactions = SelectedTransactions.OrderBy(r => r.TransactionNumber).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.type)
                {
                    SelectedTransactions = SelectedTransactions.OrderBy(r => r.TransactionType).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.description)
                {
                    SelectedTransactions = SelectedTransactions.OrderBy(r => r.TransactionComments).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.amount)
                {
                    SelectedTransactions = SelectedTransactions.OrderBy(r => r.TransactionAmount).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.date)
                {
                    SelectedTransactions = SelectedTransactions.OrderBy(r => r.OrderDate).ToList();
                }
            }
            else
            {
                if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.num)
                {
                    SelectedTransactions = SelectedTransactions.OrderByDescending(r => r.TransactionNumber).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.type)
                {
                    SelectedTransactions = SelectedTransactions.OrderByDescending(r => r.TransactionType).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.description)
                {
                    SelectedTransactions = SelectedTransactions.OrderByDescending(r => r.TransactionComments).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.amount)
                {
                    SelectedTransactions = SelectedTransactions.OrderByDescending(r => r.TransactionAmount).ToList();
                }
                else if (searchViewModel.SearchOrderBy.Value == SearchOrderBy.date)
                {
                    SelectedTransactions = SelectedTransactions.OrderByDescending(r => r.OrderDate).ToList();
                }
            }

            return View("Index", SelectedTransactions);
        }

        public TransactionsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(string? id)
        {
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.ID = id;
            List<Transaction> t = _context.Transactions.Where(t => t.BankAccount.Customer == userLoggedIn).ToList();
            ViewBag.Message = "Showing Transactions from all your Bank Accounts.";
            if (id != null)
            {
                BankAccount dbBankAccount = _context.BankAccounts.FirstOrDefault(ba => ba.AccountID == id);
                t = _context.Transactions.Where(t => t.BankAccount.AccountID == id).ToList();
                ViewBag.Message = "Showing Transactions from Bank Account: [" + dbBankAccount.HiddenAccountNo.ToString() + "] " + dbBankAccount.AccountName;
            }
            ViewBag.AllRepositories = t.Count();
            ViewBag.SelectedRepositories = t.Count;

            return _context.Transactions != null ? 
                View(t) :
                Problem("Entity set 'AppDbContext.Transactions'  is null.");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageDeposits()
        {
            List<Transaction> transactions = _context.Transactions.Where(o => o.TransactionApproved == false).ToList();
            if (transactions.Count() == 0)
            {
                ViewBag.Message = "Congratulations, you have cleared your deposit request inbox!";
            }
            return _context.Transactions != null ? View(transactions) :
                        Problem("Entity set 'AppDbContext.Transactions'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> ManageDeposits(int[] approvedDeposits, int[] rejectedDeposits)
        {
            int overlappingElements = approvedDeposits.Intersect(rejectedDeposits).Count();
            if (overlappingElements > 0)
            {
                ViewBag.Message = "You cannot accept and reject the same transaction. Please try again.";
                List<Transaction> transactions = _context.Transactions.Where(o => o.TransactionApproved == false).ToList();
                return _context.Transactions != null ? View(transactions) :
                            Problem("Entity set 'AppDbContext.Transactions'  is null.");
            }
            foreach (int i in approvedDeposits)
            {
                Transaction dbTransaction = _context.Transactions.Include(o => o.BankAccount).FirstOrDefault(o => o.TransactionID == i);
                dbTransaction.TransactionApproved = true;
                BankAccount dbBankAccount = _context.BankAccounts.Include(ba => ba.Customer).FirstOrDefault(o => o.AccountID == dbTransaction.BankAccount.AccountID);
                dbBankAccount.AccountBalance += dbTransaction.TransactionAmount;
                _context.Update(dbTransaction);
                _context.Update(dbBankAccount);
                if (dbBankAccount.StockPortfolio != null)
                {
                    StockPortfolio dbStockPortfolio = _context.StockPortfolios.FirstOrDefault(o => o.AccountID == dbBankAccount.StockPortfolio.AccountID);
                    dbStockPortfolio.CashBalance += dbTransaction.TransactionAmount;
                    _context.Update(dbStockPortfolio);
                }
                await _context.SaveChangesAsync();

                // TODO: sends an email that a customer's bank account has been made
                String emailbody = "Your deposit of $" + dbTransaction.TransactionAmount + " have been approved!";
                String emailsubject = "Deposit Status Update";
                Utilities.EmailMessaging.SendEmail(dbBankAccount.Customer.Email, emailsubject, emailbody);
            }

            foreach (int i in rejectedDeposits)
            {
                Transaction dbTransaction = _context.Transactions.Include(o => o.BankAccount).FirstOrDefault(o => o.TransactionID == i);
                _context.Transactions.Remove(dbTransaction);
                await _context.SaveChangesAsync();
                BankAccount dbBankAccount = _context.BankAccounts.Include(ba => ba.Customer).FirstOrDefault(o => o.AccountID == dbTransaction.BankAccount.AccountID);

                String emailbody = "Your deposit of $" + dbTransaction.TransactionAmount + " have been rejected. The transaction was deleted.";
                String emailsubject = "Deposit Status Update";
                Utilities.EmailMessaging.SendEmail(dbBankAccount.Customer.Email, emailsubject, emailbody);
            }

            return RedirectToAction("ManageDeposits");
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Disputes)
                .Include(t => t.BankAccount)
                .ThenInclude(ba => ba.Customer)
                .FirstOrDefaultAsync(m => m.TransactionID == id);

            if (User.IsInRole("Customer") && transaction.BankAccount.Customer.Email != User.Identity.Name)
            {
                return View("Error", new string[] { "Access Denied" });
            }


            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/CreateTransfer
        public async Task<IActionResult> CreateTransfer()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                List<BankAccount> bas = _context.BankAccounts.Where(ba => ba.Customer.Email == userLoggedIn.Email).ToList();
                if (bas.Count() == 0)
                {
                    return View("Error", new string[] { "You don't have any accounts. Please apply for one!" });
                }

                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }

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
            tTo.TransactionComments = "Transfer from Account [" + dbFromBankAccount.HiddenAccountNo.ToString() + "]" + dbFromBankAccount.AccountName;
            tTo.TransactionType = TransactionType.Deposit;
            tTo.TransactionApproved = true;
            tTo.OrderDate = transaction.OrderDate;
            Int32 tToTransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            await Create(tTo, ToBankAccount);


            // CREATE FROM TRANSACTION
            Transaction tFrom = new Transaction();
            tFrom.TransactionAmount = transaction.TransactionAmount;
            tFrom.TransactionComments = "Transfer to Account [" + dbToBankAccount.HiddenAccountNo.ToString() + "]" + dbToBankAccount.AccountName;
            tFrom.BankAccount = dbFromBankAccount;
            tFrom.TransactionType = TransactionType.Withdraw;
            tFrom.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context)-1;
            tFrom.TransactionApproved = true;
            tFrom.OrderDate = transaction.OrderDate;
            tFrom.FromAccount = dbFromBankAccount.AccountNo;
            dbFromBankAccount.AccountBalance -= transaction.TransactionAmount;
            _context.Add(tFrom);
            _context.Update(dbFromBankAccount);
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
            tTo.TransactionComments = "Transfer from Account [" + dbFromBankAccount.HiddenAccountNo.ToString() + "]" + dbFromBankAccount.AccountName;
            tTo.TransactionType = TransactionType.Deposit;
            Int32 tToTransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            tTo.TransactionNumber = tToTransactionNumber;
            tTo.TransactionApproved = true;
            tTo.BankAccount = dbToBankAccount;
            tTo.ToAccount = dbToBankAccount.AccountNo;
            tTo.FromAccount = dbFromBankAccount.AccountNo;
            tTo.OrderDate = transaction.OrderDate;
            dbToBankAccount.AccountBalance += tTo.TransactionAmount;
            _context.Add(tTo);
            _context.Update(dbToBankAccount);
            await _context.SaveChangesAsync();

            Transaction tFrom = new Transaction();
            tFrom.TransactionAmount = transaction.TransactionAmount;
            tFrom.TransactionComments = "Transfer to Account [" + dbToBankAccount.HiddenAccountNo.ToString() + "]" + dbToBankAccount.AccountName;
            tFrom.TransactionType = TransactionType.Withdraw;
            tFrom.TransactionNumber = tToTransactionNumber;
            tFrom.TransactionApproved = true;
            tFrom.BankAccount = dbFromBankAccount;
            tFrom.ToAccount = dbToBankAccount.AccountNo;
            tFrom.FromAccount = dbFromBankAccount.AccountNo;
            tFrom.OrderDate = transaction.OrderDate;
            dbFromBankAccount.AccountBalance -= tTo.TransactionAmount;
            _context.Add(tFrom);
            _context.Update(dbFromBankAccount);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransfer(Transaction transaction, string ToBankAccount, string FromBankAccount)
        {
            if (ToBankAccount == FromBankAccount)
            {
                ViewBag.CustomerAccounts = GetAllAccountsSelectList();
                ViewBag.Overdrawn = "You cannot transfer between the same account.";
                return View(transaction);
            }
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
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                List<BankAccount> bas = _context.BankAccounts.Where(ba => ba.Customer.Email == userLoggedIn.Email).ToList();
                if (bas.Count() == 0)
                {
                    return View("Error", new string[] { "You don't have any accounts. Please apply for one!" });
                }

                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }

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

        public async Task<IActionResult> InitialDepositStockPortfolio(string SelectedBankAccount, int AccountBalance)
        {
            Transaction transaction = new Transaction();
            ViewBag.IsInitial = true;
            transaction.TransactionNumber = Utilities.GenerateNumbers.GetTransactionNumber(_context);
            BankAccount dbBankAccount = _context.BankAccounts.Include(db => db.StockPortfolio).FirstOrDefault(db => db.AccountID == SelectedBankAccount);
            StockPortfolio dbStockPortfolio = dbBankAccount.StockPortfolio;
            transaction.BankAccount = dbBankAccount;
            transaction.ToAccount = dbBankAccount.AccountNo;
            transaction.TransactionComments = "Initial Deposit";
            transaction.TransactionType = TransactionType.Deposit;
            dbBankAccount.AccountBalance = 0;
            _context.Update(dbBankAccount);

            transaction.TransactionAmount += AccountBalance;

            if (AccountBalance > 5000)
            {
                transaction.TransactionApproved = false;
            }
            else
            {
                transaction.TransactionApproved = true;
                
                dbBankAccount.AccountBalance += AccountBalance;
                dbStockPortfolio.CashBalance += AccountBalance;
                _context.Update(dbBankAccount);
                _context.Update(dbStockPortfolio);
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
            BankAccount dbBankAccount = await _context.BankAccounts.Include(db => db.Customer).FirstOrDefaultAsync(db => db.AccountID == SelectedBankAccount);
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
            transaction.TransactionApproved = true;
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
