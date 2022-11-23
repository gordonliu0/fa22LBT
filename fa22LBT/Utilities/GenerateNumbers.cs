using System;
using fa22LBT.DAL;

namespace fa22LBT.Utilities
{
	public class GenerateNumbers
	{
        public static Int64 GetAccountNumber(AppDbContext _context)
        {
            //set a constant to designate where the registration numbers 
            //should start
            const Int64 START_NUMBER = 2290000000;

            Int64 intMaxNumber; //the current maximum course number
            Int64 intNextNumber; //the course number for the next class

            if (_context.BankAccounts.Count() == 0) //there are no registrations in the database yet
            {
                intMaxNumber = START_NUMBER; //registration numbers start at 101
            }
            else
            {
                intMaxNumber = _context.BankAccounts.Max(c => c.AccountNo); //this is the highest number in the database right now
            }

            //You added records to the datbase before you realized 
            //that you needed this and now you have numbers less than 100 
            //in the database
            if (intMaxNumber < START_NUMBER)
            {
                intMaxNumber = START_NUMBER;
            }

            //add one to the current max to find the next one
            intNextNumber = intMaxNumber + 1;

            //return the value
            return intNextNumber;
        }

        public static Int32 GetTransactionNumber(AppDbContext _context)
        {
            //set a constant to designate where the registration numbers 
            //should start
            const Int32 START_NUMBER = 0;

            Int32 intMaxNumber; //the current maximum course number
            Int32 intNextNumber; //the course number for the next class

            if (_context.Transactions.Count() == 0) //there are no registrations in the database yet
            {
                intMaxNumber = START_NUMBER; //registration numbers start at 101
            }
            else
            {
                intMaxNumber = _context.Transactions.Max(c => c.TransactionNumber); //this is the highest number in the database right now
            }

            //You added records to the datbase before you realized 
            //that you needed this and now you have numbers less than 100 
            //in the database
            if (intMaxNumber < START_NUMBER)
            {
                intMaxNumber = START_NUMBER;
            }

            //add one to the current max to find the next one
            intNextNumber = intMaxNumber + 1;

            //return the value
            return intNextNumber;
        }
    }
}
