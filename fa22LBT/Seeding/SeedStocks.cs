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
using fa22LBT.Controllers;

namespace fa22LBT.Seeding
{
    public static class SeedStocks
    {
        public static void SeedAllStocks(AppDbContext db)
        {
            //create some counters to help debug problems
            Int32 intStockAdded = 0;
            String strStockName = "Start";

            List<Stock> AllStocks = new List<Stock>();
            Stock s1 = new Stock()
            {
                TickerSymbol = "GOOG",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Alphabet Inc.",
                StockPrice = 87.07m,
            };
            AllStocks.Add(s1);

            Stock s2 = new Stock()
            {
                TickerSymbol = "AAPL",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Apple Inc.",
                StockPrice = 145.03m,
            };
            AllStocks.Add(s2);

            Stock s3 = new Stock()
            {
                TickerSymbol = "AMZN",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Amazon.com Inc.",
                StockPrice = 92.12m,
            };
            AllStocks.Add(s3);

            Stock s4 = new Stock()
            {
                TickerSymbol = "LUV",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Southwest Airlines",
                StockPrice = 36.50m,
            };
            AllStocks.Add(s4);

            Stock s5 = new Stock()
            {
                TickerSymbol = "TXN",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Texas Instruments",
                StockPrice = 158.49m,
            };
            AllStocks.Add(s5);

            Stock s6 = new Stock()
            {
                TickerSymbol = "HSY",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "The Hershey Company",
                StockPrice = 235.11m,
            };
            AllStocks.Add(s6);

            Stock s7 = new Stock()
            {
                TickerSymbol = "V",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Visa Inc.",
                StockPrice = 200.95m,
            };
            AllStocks.Add(s7);

            Stock s8 = new Stock()
            {
                TickerSymbol = "NKE",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Nike",
                StockPrice = 90.30m,
            };
            AllStocks.Add(s8);

            Stock s9 = new Stock()
            {
                TickerSymbol = "VWO",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("ETF")),
                StockName = "Vanguard Emerging Markets ETF",
                StockPrice = 35.77m,
            };
            AllStocks.Add(s9);

            Stock s10 = new Stock()
            {
                TickerSymbol = "CORN",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Futures")),
                StockName = "Corn",
                StockPrice = 27.35m,
            };
            AllStocks.Add(s10);

            Stock s11 = new Stock()
            {
                TickerSymbol = "FXAIX",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Mutual Fund")),
                StockName = "Fidelity 500 Index Fund",
                StockPrice = 133.88m,
            };
            AllStocks.Add(s11);

            Stock s12 = new Stock()
            {
                TickerSymbol = "F",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Ford Motor Company",
                StockPrice = 13.06m,
            };
            AllStocks.Add(s12);

            Stock s13 = new Stock()
            {
                TickerSymbol = "BAC",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Bank of America Corporation",
                StockPrice = 36.09m,
            };
            AllStocks.Add(s13);

            Stock s14 = new Stock()
            {
                TickerSymbol = "VNQ",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("ETF")),
                StockName = "Vanguard REIT ETF",
                StockPrice = 80.67m,
            };
            AllStocks.Add(s14);

            Stock s15 = new Stock()
            {
                TickerSymbol = "NSDQ",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Index Fund")),
                StockName = "Nasdaq Index Fund",
                StockPrice = 10524.80m,
            };
            AllStocks.Add(s15);

            Stock s16 = new Stock()
            {
                TickerSymbol = "KMX",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "CarMax, Inc.",
                StockPrice = 62.36m,
            };
            AllStocks.Add(s16);

            Stock s17 = new Stock()
            {
                TickerSymbol = "DIA",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Index Fund")),
                StockName = "Dow Jones Industrial Average Index Fund",
                StockPrice = 321.36m,
            };
            AllStocks.Add(s17);

            Stock s18 = new Stock()
            {
                TickerSymbol = "SPY",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Index Fund")),
                StockName = "S&P 500 Index Fund",
                StockPrice = 374.87m,
            };
            AllStocks.Add(s18);

            Stock s19 = new Stock()
            {
                TickerSymbol = "BEN",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Ordinary")),
                StockName = "Franklin Resources, Inc.",
                StockPrice = 22.56m,
            };
            AllStocks.Add(s19);

            Stock s20 = new Stock()
            {
                TickerSymbol = "FLCEX",
                StockType = db.StockTypes.FirstOrDefault(s => s.StockTypeName.Equals("Mutual Fund")),
                StockName = "Fidelity Large Cap Core Enhanced Index Fund",
                StockPrice = 18.10m,
            };
            AllStocks.Add(s20);

            //try  //attempt to add or update the book
            //{
            //loop through each of the books in the list
            foreach (Stock stockToAdd in AllStocks)
            {
                //set the flag to the current title to help with debugging
                strStockName = stockToAdd.StockName;

                //look to see if the book is in the database - this assumes that no
                //two books have the same title
                Stock dbStock = db.Stocks.FirstOrDefault(s => s.StockName == stockToAdd.StockName);

                //if the dbBook is null, this title is not in the database
                if (dbStock == null)
                {
                    //add the book to the database and save changes
                    db.Stocks.Add(stockToAdd);
                    db.SaveChanges();

                    //update the counter to help with debugging
                    intStockAdded += 1;
                }
                else //dbBook is not null - this title *is* in the database
                {
                    //update all of the book's properties
                    dbStock.TickerSymbol = stockToAdd.TickerSymbol;
                    dbStock.StockType = stockToAdd.StockType;
                    dbStock.StockName = stockToAdd.StockName;
                    dbStock.StockPrice = stockToAdd.StockPrice;

                    //update the database and save the changes
                    db.Update(dbStock);
                    db.SaveChanges();

                    //update the counter to help with debugging
                    intStockAdded += 1;
                } //this is the end of the else
            } //this is the end of the foreach loop for the books
              //} //this is the end of the try block

            /*catch (Exception ex)//something went wrong with adding or updating
            {

                //Build a messsage using the flags we created
                String msg = "  Repositories added:" + intStockAdded + "; Error on " + strStockName;

                //throw the exception with the new message
                throw new InvalidOperationException(ex.Message + msg);
            }*/
        }
    }
}





