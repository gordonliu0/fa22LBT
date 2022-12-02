using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace fa22LBT.Models
{

    public enum SearchOrderBy { num, type, description, amount, date }
    public enum TransactionType { Deposit, Withdraw, Transfer, Fee, Bonus }

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
        [Required]
        [RegularExpression(@"(^[0-9]+)?(\.[0-9]{0,2})?$", ErrorMessage = "Please input a maximum of 2 decimal places.")]
        [Range(0.01, 2147483646, ErrorMessage = "The amount should be greater than 0!")]
        public Decimal TransactionAmount { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Approved")]
        public Boolean TransactionApproved { get; set; }

        [Display(Name = "Transaction Comments")]
        public String? TransactionComments { get; set; }

        // account num
        [Display(Name = "To Account")]
        public Int64 ToAccount { get; set; }

        // account num
        [Display(Name = "From Account")]
        public Int64 FromAccount { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "User")]
        public AppUser User { get; set; }

        // account this transaction is displayed on
        [Display(Name = "Account")]
        public BankAccount BankAccount { get; set; }

        [Display(Name = "Dispute")]
        public List<Dispute> Disputes { get; set; }

        public Transaction()
		{
            if (Disputes == null)
            {
                Disputes = new List<Dispute>();
            }
		}
	}
}

