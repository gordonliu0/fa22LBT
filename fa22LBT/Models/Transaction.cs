using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace fa22LBT.Models
{
    public enum TransactionType { Deposit, Withdraw, Transfer }

	public class Transaction
	{
        [Key]
        public Int32 TransactionID { get; set; }

        [Display(Name = "Transaction Number")]
        public Int32 TransactionNumber { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; set; }

        [Display(Name = "Transaction Amount")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TransactionAmount { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Approved")]
        public Boolean TransactionApproved { get; set; }

        [Display(Name = "Transaction Comments")]
        public String? TransactionComments { get; set; }

        // account num
        [Display(Name = "To Account")]
        public Int32 ToAccount { get; set; }

        // account num
        [Display(Name = "From Account")]
        public Int32 FromAccount { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "User")]
        public AppUser User { get; set; }

        // account this transaction is displayed on
        [Display(Name = "Account")]
        public BankAccount BankAccount { get; set; }

        [Display(Name = "Dispute")]
        public List<Dispute> Dispute { get; set; }

        public Transaction()
		{
		}
	}
}

