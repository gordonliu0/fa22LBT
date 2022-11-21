using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace fa22LBT.Models
{
    public enum AccountTypes { Savings, Checking, IRA }

    public class BankAccount
	{
        [Key]
        public String AccountID { get; set; }

        [Display(Name = "Account Number")]
        public Int32 AccountNo { get; set; }

        [Display(Name = "Account Name")]
        public String AccountName { get; set; }

        [Display(Name = "Account Type")]
        public AccountTypes AccountType { get; set; }

        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal AccountBalance { get; set; }

        [Display(Name = "Approved")]
        public Boolean IsApproved { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Customer")]
        public AppUser Customer { get; set; }

        [Display(Name = "Transactions")]
        public List<Transaction> Transactions { get; set; }

        public BankAccount()
		{
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
        }
	}
}

