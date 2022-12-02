using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fa22LBT.Models
{
    public class StockPortfolio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String AccountID { get; set; }

        [Display(Name = "Account Number")]
        public Int64 AccountNo { get; set; }

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

        [Display(Name = "Cash Account")]
        public BankAccount BankAccount { get; set; }

        [Display(Name = "Stock Transactions")]
        public List<StockTransaction> StockTransactions { get; set; }

        [Display(Name = "Stocks Owned")]
        public List<StockHolding> StockHoldings { get; set; }

        public void CalculateBalancedStatus()
        {
            int ordinaryCount = 0;
            int indexCount = 0;
            int mutualCount = 0;
            foreach (StockHolding sh in this.StockHoldings)
            {
                if (sh.Stock.StockType.StockTypeID == 1)
                {
                    ordinaryCount += 1;
                } else if (sh.Stock.StockType.StockTypeID == 5)
                {
                    indexCount += 1;
                } else if (sh.Stock.StockType.StockTypeID == 4)
                {
                    mutualCount += 1;
                }
            }
            if (ordinaryCount >= 2 && indexCount >= 1 && mutualCount >= 1)
            {
                this.IsBalanced = true;
            } else
            {
                this.IsBalanced = false;
            }
        }

        public StockPortfolio()
        { 
            if (StockTransactions == null)
            {
                StockTransactions = new List<StockTransaction>();
            }
            if (StockHoldings == null)
            {
                StockHoldings = new List<StockHolding>();
            }
        }
	}
}

