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

namespace fa22LBT.Seeding
{
    public static class SeedStockPortfolios
    {
        public static void SeedAllStockPortfolios(AppDbContext db)
        {
            //create some counters to help debug problems
            Int32 intStockPortfolioAdded = 0;
            String strStockPortfolioName = "Start";

            List<StockPortfolio> AllStockPortfolios = new List<StockPortfolio>();
            List<BankAccount> AllBankAccounts = new List<BankAccount>();
            StockPortfolio sp1 = new StockPortfolio()
            {
                AccountNo = 2290000001,
                AccountName = "Shan's Stock",
                CashBalance = 0m,
                IsApproved = true,
            };
            sp1.AppUser = db.Users.FirstOrDefault(u => u.UserName == "Dixon@aool.com");

            AllStockPortfolios.Add(sp1);

            StockPortfolio sp2 = new StockPortfolio()
            {
                AccountNo = 2290000009,
                AccountName = "Michelle's Stock",
                CashBalance = 8888.88m,
            };
            sp2.AppUser = db.Users.FirstOrDefault(u => u.UserName == "mb@aool.com");

            AllStockPortfolios.Add(sp2);

            StockPortfolio sp3 = new StockPortfolio()
            {
                AccountNo = 2290000011,
                AccountName = "Kelly's Stock",
                CashBalance = 420m,
            };
            sp3.AppUser = db.Users.FirstOrDefault(u => u.UserName == "nelson.Kelly@aool.com");

            AllStockPortfolios.Add(sp3);

            StockPortfolio sp4 = new StockPortfolio()
            {
                AccountNo = 2290000018,
                AccountName = "John's Stock",
                CashBalance = 0m,
            };
            sp4.AppUser = db.Users.FirstOrDefault(u => u.UserName == "johnsmith187@aool.com");

            AllStockPortfolios.Add(sp4);

            StockPortfolio sp5 = new StockPortfolio()
            {
                AccountNo = 2290000018,
                AccountName = "CBaker's Stock",
                CashBalance = 6900m
            };
            sp5.AppUser = db.Users.FirstOrDefault(u => u.UserName == "cbaker@freezing.co.uk");

            AllStockPortfolios.Add(sp5);


            try  //attempt to add or update the book
            {
                //loop through each of the books in the list
                foreach (StockPortfolio portfolioToAdd in AllStockPortfolios)
                {
                    //set the flag to the current title to help with debugging
                    strStockPortfolioName = portfolioToAdd.AccountName;

                    //look to see if the book is in the database - this assumes that no
                    //two books have the same title
                    StockPortfolio dbStockPortfolio = db.StockPortfolios.FirstOrDefault(b => b.AccountName == portfolioToAdd.AccountName);

                    //if the dbBook is null, this title is not in the database
                    if (dbStockPortfolio == null)
                    {
                        //add the book to the database and save changes
                        db.StockPortfolios.Add(portfolioToAdd);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intStockPortfolioAdded += 1;
                    }
                    else //dbBook is not null - this title *is* in the database
                    {
                        //update all of the book's properties
                        dbStockPortfolio.AccountName = portfolioToAdd.AccountName;
                        dbStockPortfolio.CashBalance = portfolioToAdd.CashBalance;
                        dbStockPortfolio.AppUser = portfolioToAdd.AppUser;
                        dbStockPortfolio.AccountNo = portfolioToAdd.AccountNo;

                        //update the database and save the changes
                        db.Update(dbStockPortfolio);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intStockPortfolioAdded += 1;
                    } //this is the end of the else

                    dbStockPortfolio = db.StockPortfolios.Include(b => b.AppUser).FirstOrDefault(b => b.AccountName == portfolioToAdd.AccountName);

                    BankAccount b1 = new BankAccount()
                    {
                        AccountNo = dbStockPortfolio.AccountNo,
                        AccountName = dbStockPortfolio.AccountName,
                        AccountType = AccountTypes.StockPortfolio,
                        AccountBalance = dbStockPortfolio.CashBalance,
                        Contribution = 0,
                        IsApproved = true,
                    };
                    b1.Customer = db.Users.FirstOrDefault(u => u.UserName == dbStockPortfolio.AppUser.Email);
                    b1.StockPortfolio = dbStockPortfolio;
                    db.BankAccounts.Add(b1);
                    db.SaveChanges();

                } //this is the end of the foreach loop for the books
            }//this is the end of the try block

            catch (Exception ex)//something went wrong with adding or updating
            {

                //Build a messsage using the flags we created
                String msg = "  Repositories added:" + intStockPortfolioAdded + "; Error on " + strStockPortfolioName;

                //throw the exception with the new message
                throw new InvalidOperationException(ex.Message + msg);
            }
        }
    }
}
