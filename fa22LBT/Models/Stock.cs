using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace fa22LBT.Models
{
	public class Stock
	{
        [Key]
        public Int32 StockID { get; set; }

        [Display(Name = "Ticker Symbol")]
        public String TickerSymbol { get; set; }

        [Display(Name = "Stock Name")]
        public String StockName { get; set; }

        [Display(Name = "Stock Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal StockPrice { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }

        [Display(Name = "Transactions")]
        public List<StockTransaction> StockTransactions { get; set; }

        public Stock()
		{
            if (StockTransactions == null)
            {
                StockTransactions = new List<StockTransaction>();
            }
        }
	}
}

