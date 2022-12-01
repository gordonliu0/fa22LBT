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
    public class DisputesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DisputesController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        // GET: Disputes
        public async Task<IActionResult> Index(Boolean? all)
        {
            List<Dispute> disputeList = await _context.Disputes.ToListAsync();
            if (all == null)
            {
                disputeList = _context.Disputes.Include(d => d.DisputeTransaction).ThenInclude(t => t.BankAccount).ThenInclude(ba => ba.Customer).Where(d => d.DisputeStatus == DisputeStatus.Submitted).ToList();
            }
            else if (all == true)
            {
                disputeList = _context.Disputes.Include(d => d.DisputeTransaction).ThenInclude(t => t.BankAccount).ThenInclude(ba => ba.Customer).ToList();
            }
            return _context.Disputes != null ? 
                        View(disputeList) :
                        Problem("Entity set 'AppDbContext.Disputes'  is null.");
        }

        // GET: Disputes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disputes == null)
            {
                return NotFound();
            }

            var dispute = await _context.Disputes
                .FirstOrDefaultAsync(m => m.DisputeID == id);
            if (dispute == null)
            {
                return NotFound();
            }

            return View(dispute);
        }

        // GET: Disputes/Create
        public async Task<IActionResult> Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }

            Dispute dispute = new Dispute();
            Transaction dbTransaction = _context.Transactions.FirstOrDefault(t => t.TransactionID == id);
            dispute.CorrectAmount = dbTransaction.TransactionAmount;
            dispute.DisputeTransaction = dbTransaction;
            return View(dispute);
        }

        // POST: Disputes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisputeID,DisputeDescription,CorrectAmount,DisputeStatus,DisputeTransaction.TransactionID")] Dispute dispute, int transactionID)
        {

            Transaction dbTransaction = _context.Transactions.Include(t => t.Disputes).FirstOrDefault(t => t.TransactionID == transactionID);
            foreach (Dispute dis in dbTransaction.Disputes)
            {
                if (dis.DisputeStatus == DisputeStatus.Accepted)
                {
                    return View("NoDisputes");
                }
            }
            dispute.DisputeStatus = DisputeStatus.Submitted;
            dispute.DisputeTransaction = dbTransaction;
            _context.Add(dispute);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Transactions", new { id = dbTransaction.TransactionID} );
        }

        public async Task<IActionResult> AcceptDispute(int id)
        {
            Dispute dbDispute = _context.Disputes.FirstOrDefault(t => t.DisputeID == id);
            return View(dbDispute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptDispute(Dispute dispute, int disputeID)
        {
            // GRAB RELEVANT
            Dispute dbDispute = _context.Disputes.Include(t => t.DisputeTransaction).FirstOrDefault(t => t.DisputeID == disputeID);
            Transaction dbTransaction = _context.Transactions.Include(t => t.BankAccount).FirstOrDefault(t => t.TransactionID == dbDispute.DisputeTransaction.TransactionID);
            BankAccount dbBankAccount = _context.BankAccounts.FirstOrDefault(t => t.AccountID == dbTransaction.BankAccount.AccountID);

            // UPDATE DISPUTE
            if (dispute.DisputeStatus == DisputeStatus.Accepted)
            {
                dbDispute.DisputeStatus = DisputeStatus.Accepted;
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                dbDispute.AdminEmail = user.Email;
                dbDispute.AdminComments = dispute.AdminComments;
                _context.Update(dbDispute);

                // UPDATE BANK ACCOUt
                if (dbTransaction.TransactionType == TransactionType.Deposit)
                {
                    dbBankAccount.AccountBalance += dbDispute.CorrectAmount - dbTransaction.TransactionAmount;
                }
                else if (dbTransaction.TransactionType == TransactionType.Withdraw || dbTransaction.TransactionType == TransactionType.Fee)
                {
                    dbBankAccount.AccountBalance += dbTransaction.TransactionAmount - dbDispute.CorrectAmount;
                }
                _context.Update(dbBankAccount);

                // UPDATE
                dbTransaction.TransactionAmount = dbDispute.CorrectAmount;
                string? originalComment = dbTransaction.TransactionComments;
                if (originalComment != null)
                {
                    dbTransaction.TransactionComments = "Dispute [Accepted] " + originalComment;
                }
                else
                {
                    dbTransaction.TransactionComments = "Dispute [Accepted]";
                }
                _context.Update(dbTransaction);

                await _context.SaveChangesAsync();
            } else if (dispute.DisputeStatus == DisputeStatus.Rejected)
            {
                dbDispute.DisputeStatus = DisputeStatus.Rejected;
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                dbDispute.AdminEmail = user.Email;
                dbDispute.AdminComments = dispute.AdminComments;
                _context.Update(dbDispute);
                string? originalComment = dbTransaction.TransactionComments;
                if (originalComment != null)
                {
                    dbTransaction.TransactionComments = "Dispute [Rejected] " + originalComment;
                }
                else
                {
                    dbTransaction.TransactionComments = "Dispute [Rejected]";
                }
                _context.Update(dbTransaction);
                await _context.SaveChangesAsync();
            }
            else
            {
                dbDispute.DisputeStatus = DisputeStatus.Adjusted;
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                dbDispute.AdminEmail = user.Email;
                dbDispute.AdminComments = dispute.AdminComments;
                return View("AdjustDispute", dbDispute);
            }
            
            return RedirectToAction("Details", "Transactions", new { id = dbTransaction.TransactionID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustDispute(Dispute dispute)
        {
            // GRAB RELEVANT
            Dispute dbDispute = _context.Disputes.Include(t => t.DisputeTransaction).FirstOrDefault(t => t.DisputeID == dispute.DisputeID);
            Transaction dbTransaction = _context.Transactions.Include(t => t.BankAccount).FirstOrDefault(t => t.TransactionID == dbDispute.DisputeTransaction.TransactionID);
            BankAccount dbBankAccount = _context.BankAccounts.FirstOrDefault(t => t.AccountID == dbTransaction.BankAccount.AccountID);

            dbDispute.DisputeStatus = DisputeStatus.Adjusted;
            dbDispute.AdminEmail = dispute.AdminEmail;
            dbDispute.AdminComments = dispute.AdminComments;
            dbDispute.CorrectAmount = dispute.CorrectAmount;
            _context.Update(dbDispute);

            // UPDATE BANK ACCOUt
            if (dbTransaction.TransactionType == TransactionType.Deposit)
            {
                dbBankAccount.AccountBalance += dbDispute.CorrectAmount - dbTransaction.TransactionAmount;
            } else if (dbTransaction.TransactionType == TransactionType.Withdraw || dbTransaction.TransactionType == TransactionType.Fee)
            {
                dbBankAccount.AccountBalance += dbTransaction.TransactionAmount - dbDispute.CorrectAmount;
            }
            _context.Update(dbBankAccount);

            // UPDATE TRANSSACTION
            dbTransaction.TransactionAmount = dbDispute.CorrectAmount;
            string? originalComment = dbTransaction.TransactionComments;
            if (originalComment != null)
            {
                dbTransaction.TransactionComments = "Dispute [Adjusted] " + originalComment;
            } else
            {
                dbTransaction.TransactionComments = "Dispute [Adjusted]";
            }
            _context.Update(dbTransaction);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Transactions", new { id = dbTransaction.TransactionID });
        }

        // GET: Disputes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disputes == null)
            {
                return NotFound();
            }

            var dispute = await _context.Disputes.FindAsync(id);
            if (dispute == null)
            {
                return NotFound();
            }
            return View(dispute);
        }

        // POST: Disputes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisputeID,DisputeDescription,CorrectAmount,DisputeStatus")] Dispute dispute)
        {
            if (id != dispute.DisputeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisputeExists(dispute.DisputeID))
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
            return View(dispute);
        }

        // GET: Disputes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disputes == null)
            {
                return NotFound();
            }

            var dispute = await _context.Disputes
                .FirstOrDefaultAsync(m => m.DisputeID == id);
            if (dispute == null)
            {
                return NotFound();
            }

            return View(dispute);
        }

        // POST: Disputes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disputes == null)
            {
                return Problem("Entity set 'AppDbContext.Disputes'  is null.");
            }
            var dispute = await _context.Disputes.FindAsync(id);
            if (dispute != null)
            {
                _context.Disputes.Remove(dispute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisputeExists(int id)
        {
          return (_context.Disputes?.Any(e => e.DisputeID == id)).GetValueOrDefault();
        }
    }
}
