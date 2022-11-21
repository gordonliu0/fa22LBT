using System;
using System.ComponentModel.DataAnnotations;

namespace fa22LBT.Models
{
    public enum StockTransactionType { Purchase, Sell }

	public class StockTransaction
	{
        [Key]
        public Int32 StockTransactionID { get; set; }

        [Display(Name = "Quantity of Shares")]
        public Int32 QuantityShares { get; set; }

        [Display(Name = "Stock Price at Purchase")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal PricePerShare { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Stock Portfolio Account")]
        public StockPortfolio StockPortfolio { get; set; }

        [Display(Name = "Stock")]
        public Stock Stock { get; set; }
	}
}

