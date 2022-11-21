using System;
using System.ComponentModel.DataAnnotations;

namespace fa22LBT.Models
{
	public class StockPortfolio
	{
        [Key]
        public String AccountID { get; set; }

        [Display(Name = "Account Number")]
        public Int32 AccountNo { get; set; }

        [Display(Name = "Account Name")]
        public String AccountName { get; set; }

        [Display(Name = "Cash Balance")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CashBalance { get; set; }

        [Display(Name = "IsBalanced")]
        public Boolean IsBalanced { get; set; } = false;

        [Display(Name = "Approved")]
        public Boolean IsApproved { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Customer")]
        public AppUser AppUser { get; set; }

        [Display(Name = "Transactions")]
        public List<Transaction> Transactions { get; set; }

        [Display(Name = "Stock Transactions")]
        public List<StockTransaction> StockTransactions { get; set; }

        public StockPortfolio()
		{
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }

            if (StockTransactions == null)
            {
                StockTransactions = new List<StockTransaction>();
            }
        }
	}
}

