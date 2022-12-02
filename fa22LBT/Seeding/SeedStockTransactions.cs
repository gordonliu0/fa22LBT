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
    public static class SeedStockTransactions
    {
        public static void SeedAllStockTransactions(AppDbContext db)
        {
            //create some counters to help debug problems
            Int32 intStockTransactionsAdded = 0;
            //String strStockTransactionName = "Start";

            StockPortfolio dbSP = db.StockPortfolios.Include(s => s.AppUser).FirstOrDefault(s => s.AppUser.Email == "cbaker@freezing.co.uk");

            List<StockTransaction> AllStockTransactions = new List<StockTransaction>();
            StockTransaction st1 = new StockTransaction()
            {
                StockPortfolio = dbSP,
                QuantityShares = 10,
                PricePerShare = 145.03m,
                Stock = db.Stocks.FirstOrDefault(s => s.TickerSymbol.Equals("AAPL")),
                OrderDate = new DateTime(2022, 4, 1),
                STransactionNo = 0,

            };

            AllStockTransactions.Add(st1);

            StockTransaction st2 = new StockTransaction()
            {
                StockPortfolio = dbSP,
                QuantityShares = 5,
                PricePerShare = 321.36m,
                Stock = db.Stocks.FirstOrDefault(s => s.TickerSymbol.Equals("DIA")),
                OrderDate = new DateTime(2022, 4, 3),
                STransactionNo = 0,
            };

            AllStockTransactions.Add(st2);

            StockTransaction st3 = new StockTransaction()
            {
                StockPortfolio = dbSP,
                QuantityShares = 2,
                PricePerShare = 18.10m,
                Stock = db.Stocks.FirstOrDefault(s => s.TickerSymbol.Equals("FLCEX")),
                OrderDate = new DateTime(2022, 4, 28),
                STransactionNo = 0,
            };

            AllStockTransactions.Add(st3);



            //try  //attempt to add or update the book
            //{
                //loop through each of the books in the list
                foreach (StockTransaction stockTransactionToAdd in AllStockTransactions)
                {
                    //set the flag to the current title to help with debugging
                    //strStockTransactionName = stockTransactionToAdd.;

                    //look to see if the book is in the database - this assumes that no
                    //two books have the same title
                    StockTransaction dbStockTransaction = db.StockTransactions.FirstOrDefault(b => b.StockTransactionID == stockTransactionToAdd.StockTransactionID);

                    //if the dbBook is null, this title is not in the database
                    if (dbStockTransaction == null)
                    {
                        //add the book to the database and save changes
                        db.StockTransactions.Add(stockTransactionToAdd);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intStockTransactionsAdded += 1;
                    }
                    else //dbBook is not null - this title *is* in the database
                    {
                        //update all of the book's properties
                        dbStockTransaction.QuantityShares = stockTransactionToAdd.QuantityShares;
                        dbStockTransaction.PricePerShare = stockTransactionToAdd.PricePerShare;
                        dbStockTransaction.Stock = stockTransactionToAdd.Stock;
                        dbStockTransaction.OrderDate = stockTransactionToAdd.OrderDate;
                        dbStockTransaction.STransactionNo = stockTransactionToAdd.STransactionNo;
                        dbStockTransaction.StockPortfolio = stockTransactionToAdd.StockPortfolio;

                        //update the database and save the changes
                        db.Update(dbStockTransaction);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intStockTransactionsAdded += 1;
                    } //this is the end of the else
                } //this is the end of the foreach loop for the books
            //}//this is the end of the try block

            //catch (Exception ex)//something went wrong with adding or updating
            //{

            //    //Build a messsage using the flags we created
            //    String msg = "  Repositories added:" + intBankAccountsAdded + "; Error on " + strBankAccountName;

            //    //throw the exception with the new message
            //    throw new InvalidOperationException(ex.Message + msg);
            //}
        }
    }
}