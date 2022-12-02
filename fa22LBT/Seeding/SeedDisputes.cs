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
    public static class SeedDisputes
    {
        public static void SeedAllDisputes(AppDbContext db)
        {
            //create some counters to help debug problems
            Int32 intDisputeAdded = 0;
            String strDisputeName = "Start";

            List<Dispute> AllDisputes = new List<Dispute>();
            Dispute d1 = new Dispute()
            {
                DisputeTransaction = db.Transactions.FirstOrDefault(t => t.TransactionNumber.Equals(8)),
                CorrectAmount = 300m,
                DisputeDescription = "I don’t remember buying so many snacks",
                DisputeStatus = DisputeStatus.Submitted,
            };
            
            AllDisputes.Add(d1);

            Dispute d2 = new Dispute()
            {

                DisputeTransaction = db.Transactions.FirstOrDefault(t => t.TransactionNumber.Equals(10)),
                CorrectAmount = 0m,
                DisputeDescription = "You rapscallions are trying to steal my money!!!",
                DisputeStatus = DisputeStatus.Submitted,
            };
            
            AllDisputes.Add(d2);



            //try  //attempt to add or update the book
            //{
            //loop through each of the books in the list
            foreach (Dispute disputeToAdd in AllDisputes)
                {
                    //set the flag to the current title to help with debugging
                    strDisputeName = disputeToAdd.DisputeDescription;

                    //look to see if the book is in the database - this assumes that no
                    //two books have the same title
                    Dispute dbDispute = db.Disputes.FirstOrDefault(b => b.DisputeTransaction == disputeToAdd.DisputeTransaction);

                    //if the dbBook is null, this title is not in the database
                    if (dbDispute == null)
                    {
                        //add the book to the database and save changes
                        db.Disputes.Add(disputeToAdd);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intDisputeAdded += 1;
                    }
                    else //dbBook is not null - this title *is* in the database
                    {
                        //update all of the book's properties
                        dbDispute.DisputeTransaction = disputeToAdd.DisputeTransaction;
                        dbDispute.CorrectAmount = disputeToAdd.CorrectAmount;
                        dbDispute.DisputeDescription = disputeToAdd.DisputeDescription;
                        dbDispute.DisputeStatus = disputeToAdd.DisputeStatus;

                        //update the database and save the changes
                        db.Update(dbDispute);
                        db.SaveChanges();

                        //update the counter to help with debugging
                        intDisputeAdded += 1;
                    } //this is the end of the else
                } //this is the end of the foreach loop for the books
            //}//this is the end of the try block

            //catch (Exception ex)//something went wrong with adding or updating
            //{

            //    //Build a messsage using the flags we created
            //    String msg = "  Repositories added:" + intDisputeAdded + "; Error on " + strDisputeName;

            //    //throw the exception with the new message
            //    throw new InvalidOperationException(ex.Message + msg);
            //}
        }
    }
}
