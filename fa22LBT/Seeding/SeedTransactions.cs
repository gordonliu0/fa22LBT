using System;
using System.Linq;
using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.SqlServer.Server;
using System.Text;
using NuGet.Protocol.Core.Types;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace fa22LBT.Seeding
{
    public static class SeedTransactions
    {
        public static void SeedAllTransactions(AppDbContext db)
        {
            //create some counters to help debug problems
            Int32 intTransactionAdded = 0;
            String strTransactionName = "Start";

            List<Transaction> AllTransactions = new List<Transaction>();
            Transaction t1 = new Transaction()
            {
                TransactionNumber = 1,
                TransactionType = TransactionType.Deposit,
                ToAccount = 2290000021,
                TransactionAmount = 4000m,
                OrderDate = new DateTime(2022, 1, 15),
                TransactionApproved = true,
            };
            t1.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000021);
            AllTransactions.Add(t1);

            Transaction t2 = new Transaction()
            {
                TransactionNumber = 2,
                TransactionType = TransactionType.Deposit,
                ToAccount = 2290000022,
                TransactionAmount = 2200m,
                OrderDate = new DateTime(2022, 3, 5),
                TransactionApproved = true,
                TransactionComments = "This transaction went so well! I will be using this bank again for sure!!",
            };
            t2.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000022);
            AllTransactions.Add(t2);

            Transaction t3 = new Transaction()
            {
                TransactionNumber = 3,
                TransactionType = TransactionType.Deposit,
                ToAccount = 2290000022,
                TransactionAmount = 6000m,
                OrderDate = new DateTime(2022, 3, 9),
                TransactionApproved = true,
            };
            t3.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000022);
            AllTransactions.Add(t3);

            Transaction t4a = new Transaction()
            {
                TransactionNumber = 4,
                TransactionType = TransactionType.Transfer,
                ToAccount = 2290000021,
                FromAccount = 2290000022,
                TransactionAmount = 1200m,
                OrderDate = new DateTime(2022, 4, 14),
                TransactionApproved = true,
                TransactionComments = "Jacob Foster has a GPA of 1.92. LOL",
            };
            t4a.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000021);
            AllTransactions.Add(t4a);

            Transaction t4b = new Transaction()
            {
                TransactionNumber = 4,
                TransactionType = TransactionType.Transfer,
                ToAccount = 2290000021,
                FromAccount = 2290000022,
                TransactionAmount = 1200m,
                OrderDate = new DateTime(2022, 4, 14),
                TransactionApproved = true,
                TransactionComments = "Jacob Foster has a GPA of 1.92. LOL",
            };
            t4b.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000022);
            AllTransactions.Add(t4b);

            Transaction t5 = new Transaction()
            {
                TransactionNumber = 5,
                TransactionType = TransactionType.Withdraw,
                FromAccount = 2290000022,
                TransactionAmount = 352m,
                OrderDate = new DateTime(2022, 4, 21),
                TransactionApproved = true,
                TransactionComments = "Longhorn Bank is my favorite bank! I couldn't dream of putting my money anywhere else.",
            };
            t5.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000022);
            AllTransactions.Add(t5);

            Transaction t6 = new Transaction()
            {
                TransactionNumber = 6,
                TransactionType = TransactionType.Deposit,
                ToAccount = 2290000023,
                TransactionAmount = 1500m,
                OrderDate = new DateTime(2022, 3, 8),
                TransactionApproved = true,
            };
            t6.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000023);
            AllTransactions.Add(t6);

            Transaction t7a = new Transaction()
            {
                TransactionNumber = 7,
                TransactionType = TransactionType.Transfer,
                ToAccount = 2290000021,
                FromAccount = 2290000024,
                TransactionAmount = 3000m,
                OrderDate = new DateTime(2022, 4, 20),
                TransactionApproved = true,
            };
            t7a.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000021);
            AllTransactions.Add(t7a);

            Transaction t7b = new Transaction()
            {
                TransactionNumber = 7,
                TransactionType = TransactionType.Transfer,
                ToAccount = 2290000021,
                FromAccount = 2290000024,
                TransactionAmount = 3000m,
                OrderDate = new DateTime(2022, 4, 20),
                TransactionApproved = true,
            };
            t7b.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000024);
            AllTransactions.Add(t7b);

            Transaction t8 = new Transaction()
            {
                TransactionNumber = 8,
                TransactionType = TransactionType.Withdraw,
                FromAccount = 2290000021,
                TransactionAmount = 578.12m,
                OrderDate = new DateTime(2022, 4, 19),
                TransactionApproved = true,
                TransactionComments = "K project snack expenses",
            };
            t8.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000021);
            AllTransactions.Add(t8);

            Transaction t9a = new Transaction()
            {
                TransactionNumber = 9,
                TransactionType = TransactionType.Transfer,
                FromAccount = 2290000026,
                ToAccount = 2290000025,
                TransactionAmount = 52m,
                OrderDate = new DateTime(2022, 4, 29),
                TransactionApproved = true,
            };
            t9a.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000026);
            AllTransactions.Add(t9a);

            Transaction t9b = new Transaction()
            {
                TransactionNumber = 9,
                TransactionType = TransactionType.Transfer,
                FromAccount = 2290000026,
                ToAccount = 2290000025,
                TransactionAmount = 52m,
                OrderDate = new DateTime(2022, 4, 29),
                TransactionApproved = true,
            };
            t9b.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000025);
            AllTransactions.Add(t9b);

            Transaction t10 = new Transaction()
            {
                TransactionNumber = 10,
                TransactionType = TransactionType.Withdraw,
                FromAccount = 2290000020,
                TransactionAmount = 4000m,
                OrderDate = new DateTime(2022, 3, 7),
                TransactionApproved = true,
                TransactionComments = "This is totally not fraudulent 0_o",
            };
            t10.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000020);
            AllTransactions.Add(t10);

            Transaction t11 = new Transaction()
            {
                TransactionNumber = 11,
                TransactionType = TransactionType.Deposit,
                ToAccount = 2290000016,
                TransactionAmount = 6000m,
                OrderDate = new DateTime(2022, 5, 1),
                TransactionApproved = false,
                TransactionComments = "I got this money from my new business at the Blue Cat Lodge",
            };
            t11.BankAccount = db.BankAccounts.FirstOrDefault(u => u.AccountNo == 2290000016);
            AllTransactions.Add(t11);


            //try  //attempt to add or update the book
            //{
                //loop through each of the books in the list
                foreach (Transaction transactionToAdd in AllTransactions)
                {
                    //set the flag to the current title to help with debugging
                    strTransactionName = transactionToAdd.TransactionComments;

                    //look to see if the book is in the database - this assumes that no
                    //two books have the same title
                    Transaction dbTransaction = db.Transactions.FirstOrDefault(b => b.TransactionID == transactionToAdd.TransactionID);

                    //if the dbBook is null, this title is not in the database
                    if (dbTransaction == null)
                    {
                        //add the book to the database and save changes
                        db.Transactions.Add(transactionToAdd);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intTransactionAdded += 1;
                    }
                    else //dbBook is not null - this title *is* in the database
                    {
                        //update all of the book's properties
                        dbTransaction.TransactionNumber = transactionToAdd.TransactionNumber;
                        dbTransaction.BankAccount = transactionToAdd.BankAccount;
                        dbTransaction.TransactionType = transactionToAdd.TransactionType;
                        dbTransaction.ToAccount = transactionToAdd.ToAccount;
                        dbTransaction.FromAccount = transactionToAdd.FromAccount;
                        dbTransaction.TransactionAmount = transactionToAdd.TransactionAmount;
                        dbTransaction.OrderDate = transactionToAdd.OrderDate;
                        dbTransaction.TransactionApproved = transactionToAdd.TransactionApproved;
                        dbTransaction.TransactionComments = transactionToAdd.TransactionComments;

                        //update the database and save the changes
                        db.Update(dbTransaction);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intTransactionAdded += 1;
                    } //this is the end of the else
                } //this is the end of the foreach loop for the books
            //}//this is the end of the try block

            //catch (Exception ex)//something went wrong with adding or updating
            //{

            //    //Build a messsage using the flags we created
            //    String msg = "  Repositories added:" + intTransactionAdded + "; Error on " + strTransactionName;

            //    //throw the exception with the new message
            //    throw new InvalidOperationException(ex.Message + msg);
            //}
        }
    }
}
