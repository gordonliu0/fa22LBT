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
    public static class SeedAccounts
    {
        public static void SeedAllAccounts(AppDbContext db)
        {
            //create some counters to help debug problems
            Int32 intBankAccountsAdded = 0;
            String strBankAccountName = "Start";

            List<BankAccount> AllBankAccounts = new List<BankAccount>();
            BankAccount b1 = new BankAccount()
            {
                AccountNo = 2290000002,
                AccountName = "William's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 40035.5m,
                Contribution = 0,
                IsApproved = true,
            };
            b1.Customer = db.Users.FirstOrDefault(u => u.UserName == "willsheff@email.com");

            AllBankAccounts.Add(b1);

            BankAccount b2 = new BankAccount()
            {
                AccountNo = 2290000003,
                AccountName = "Gregory's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 39779.49m,
                Contribution = 0,
                IsApproved = true,
            };
            b2.Customer = db.Users.FirstOrDefault(u => u.UserName == "smartinmartin.Martin@aool.com");

            AllBankAccounts.Add(b2);

            BankAccount b3 = new BankAccount()
            {
                AccountNo = 2290000004,
                AccountName = "Allen's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 47277.33m,
                Contribution = 0,
                IsApproved = true,
            };
            b3.Customer = db.Users.FirstOrDefault(u => u.UserName == "avelasco@yaho.com");

            AllBankAccounts.Add(b3);

            BankAccount b4 = new BankAccount()
            {
                AccountNo = 2290000005,
                AccountName = "Reagan's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 70812.15m,
                Contribution = 0,
                IsApproved = true,
            };
            b4.Customer = db.Users.FirstOrDefault(u => u.UserName == "rwood@voyager.net");

            AllBankAccounts.Add(b4);

            BankAccount b5 = new BankAccount()
            {
                AccountNo = 2290000006,
                AccountName = "Kelly's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 21901.97m,
                Contribution = 0,
                IsApproved = true,
            };
            b5.Customer = db.Users.FirstOrDefault(u => u.UserName == "nelson.Kelly@aool.com");

            AllBankAccounts.Add(b5);

            BankAccount b6 = new BankAccount()
            {
                AccountNo = 2290000007,
                AccountName = "Eryn's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 70480.99m,
                Contribution = 0,
                IsApproved = true,
            };
            b6.Customer = db.Users.FirstOrDefault(u => u.UserName == "erynrice@aool.com");

            AllBankAccounts.Add(b6);

            BankAccount b7 = new BankAccount()
            {
                AccountNo = 2290000008,
                AccountName = "Jake's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 7916.4m,
                Contribution = 0,
                IsApproved = true,
            };
            b7.Customer = db.Users.FirstOrDefault(u => u.UserName == "westj@pioneer.net");

            AllBankAccounts.Add(b7);

            BankAccount b8 = new BankAccount()
            {
                AccountNo = 2290000010,
                AccountName = "Jeffrey's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 69576.83m,
                Contribution = 0,
                IsApproved = true,
            };
            b8.Customer = db.Users.FirstOrDefault(u => u.UserName == "jeff@ggmail.com");

            AllBankAccounts.Add(b8);

            BankAccount b9 = new BankAccount()
            {
                AccountNo = 2290000012,
                AccountName = "Eryn's Checking 2",
                AccountType = AccountTypes.Checking,
                AccountBalance = 30279.33m,
                Contribution = 0,
                IsApproved = true,
            };
            b9.Customer = db.Users.FirstOrDefault(u => u.UserName == "erynrice@aool.com");

            AllBankAccounts.Add(b9);

            BankAccount b10 = new BankAccount()
            {
                AccountNo = 2290000013,
                AccountName = "Jennifer's IRA",
                AccountType = AccountTypes.IRA,
                AccountBalance = 53177.21m,
                Contribution = 0,
                IsApproved = true,
            };
            b10.Customer = db.Users.FirstOrDefault(u => u.UserName == "mackcloud@pimpdaddy.com");

            AllBankAccounts.Add(b10);

            BankAccount b11 = new BankAccount()
            {
                AccountNo = 2290000014,
                AccountName = "Sarah's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 11958.08m,
                Contribution = 0,
                IsApproved = true,
            };
            b11.Customer = db.Users.FirstOrDefault(u => u.UserName == "ss34@ggmail.com");

            AllBankAccounts.Add(b11);

            BankAccount b12 = new BankAccount()
            {
                AccountNo = 2290000015,
                AccountName = "Jeremy's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 72990.47m,
                Contribution = 0,
                IsApproved = true,
            };
            b12.Customer = db.Users.FirstOrDefault(u => u.UserName == "tanner@ggmail.com");

            AllBankAccounts.Add(b12);

            BankAccount b13 = new BankAccount()
            {
                AccountNo = 2290000016,
                AccountName = "Elizabeth's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 7417.20m,
                Contribution = 0,
                IsApproved = true,
            };
            b13.Customer = db.Users.FirstOrDefault(u => u.UserName == "liz@ggmail.com");

            AllBankAccounts.Add(b13);

            BankAccount b14 = new BankAccount()
            {
                AccountNo = 2290000017,
                AccountName = "Allen's IRA",
                AccountType = AccountTypes.IRA,
                AccountBalance = 75866.69m,
                Contribution = 0,
                IsApproved = true,
            };
            b14.Customer = db.Users.FirstOrDefault(u => u.UserName == "ra@aoo.com");

            AllBankAccounts.Add(b14);

            BankAccount b15 = new BankAccount()
            {
                AccountNo = 2290000019,
                AccountName = "Clarence's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 1642.82m,
                Contribution = 0,
                IsApproved = true,
            };
            b15.Customer = db.Users.FirstOrDefault(u => u.UserName == "mclarence@aool.com");

            AllBankAccounts.Add(b15);


            BankAccount b16 = new BankAccount()
            {
                AccountNo = 2290000020,
                AccountName = "Sarah's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 84421.45m,
                Contribution = 0,
                IsApproved = true,
            };
            b16.Customer = db.Users.FirstOrDefault(u => u.UserName == "ss34@ggmail.com");

            AllBankAccounts.Add(b16);


            BankAccount b17 = new BankAccount()
            {
                AccountNo = 2290000021,
                AccountName = "CBaker's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 4523.0m,
                Contribution = 0,
                IsApproved = true,
            };
            b17.Customer = db.Users.FirstOrDefault(u => u.UserName == "cbaker@freezing.co.uk");

            AllBankAccounts.Add(b17);


            BankAccount b18 = new BankAccount()
            {
                AccountNo = 2290000022,
                AccountName = "CBaker's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 1000.0m,
                Contribution = 0,
                IsApproved = true,
            };
            b18.Customer = db.Users.FirstOrDefault(u => u.UserName == "cbaker@freezing.co.uk");

            AllBankAccounts.Add(b18);


            BankAccount b19 = new BankAccount()
            {
                AccountNo = 2290000023,
                AccountName = "CBaker's IRA",
                AccountType = AccountTypes.IRA,
                AccountBalance = 1000.0m,
                Contribution = 0,
                IsApproved = true,
            };
            b19.Customer = db.Users.FirstOrDefault(u => u.UserName == "cbaker@freezing.co.uk");

            AllBankAccounts.Add(b19);

            BankAccount b20 = new BankAccount()
            {
                AccountNo = 2290000025,
                AccountName = "C-dawg's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 4.04m,
                Contribution = 0,
                IsApproved = true,
            };
            b20.Customer = db.Users.FirstOrDefault(u => u.UserName == "chaley@thug.com");

            AllBankAccounts.Add(b20);

            BankAccount b21 = new BankAccount()
            {
                AccountNo = 2290000026,
                AccountName = "C-dawg's Savings",
                AccountType = AccountTypes.Savings,
                AccountBalance = 350.0m,
                Contribution = 0,
                IsApproved = true,
            };
            b21.Customer = db.Users.FirstOrDefault(u => u.UserName == "chaley@thug.com");

            AllBankAccounts.Add(b21);


            BankAccount b22 = new BankAccount()
            {
                AccountNo = 2290000027,
                AccountName = "Margaret's IRA",
                AccountType = AccountTypes.IRA,
                AccountBalance = 3500.0m,
                Contribution = 0,
                IsApproved = true,
            };
            b22.Customer = db.Users.FirstOrDefault(u => u.UserName == "mgar@aool.com");

            AllBankAccounts.Add(b22);

            BankAccount b23 = new BankAccount()
            {
                AccountNo = 2290000028,
                AccountName = "Shan's Checking",
                AccountType = AccountTypes.Checking,
                AccountBalance = 2657.81m,
                Contribution = 0,
                IsApproved = true,
            };
            b23.Customer = db.Users.FirstOrDefault(u => u.UserName == "Dixon@aool.com");

            AllBankAccounts.Add(b23);



            try  //attempt to add or update the book
            {
                //loop through each of the books in the list
                foreach (BankAccount accountToAdd in AllBankAccounts)
                {
                    //set the flag to the current title to help with debugging
                    strBankAccountName = accountToAdd.AccountName;

                    //look to see if the book is in the database - this assumes that no
                    //two books have the same title
                    BankAccount dbBank = db.BankAccounts.FirstOrDefault(b => b.AccountName == accountToAdd.AccountName);

                    //if the dbBook is null, this title is not in the database
                    if (dbBank == null)
                    {
                        //add the book to the database and save changes
                        db.BankAccounts.Add(accountToAdd);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intBankAccountsAdded += 1;
                    }
                    else //dbBook is not null - this title *is* in the database
                    {
                        //update all of the book's properties
                        dbBank.AccountName = accountToAdd.AccountName;
                        dbBank.AccountType = accountToAdd.AccountType;
                        dbBank.AccountBalance = accountToAdd.AccountBalance;
                        dbBank.AccountNo = accountToAdd.AccountNo;
                        dbBank.Contribution = accountToAdd.Contribution;
                        dbBank.Customer = accountToAdd.Customer;

                        //update the database and save the changes
                        db.Update(dbBank);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intBankAccountsAdded += 1;
                    } //this is the end of the else
                } //this is the end of the foreach loop for the books
            }//this is the end of the try block

            catch (Exception ex)//something went wrong with adding or updating
            {

                //Build a messsage using the flags we created
                String msg = "  Repositories added:" + intBankAccountsAdded + "; Error on " + strBankAccountName;

                //throw the exception with the new message
                throw new InvalidOperationException(ex.Message + msg);
            }
        }
    }
}
