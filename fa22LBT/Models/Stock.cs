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

        // CALCULATED PROPERTIES
        [Display(Name = "Stock Type")]
        public String StockQuickInfo { get
            {
                if (this.StockType != null)
                {
                    return this.TickerSymbol + " " + this.StockName + " " + this.StockType.StockTypeName + " $" + this.StockPrice + ", Purchase Fee: $10";
                }
                return this.TickerSymbol + " " + this.StockName + " " + " $" + this.StockPrice + ", Purchase Fee: $10";
            }
        }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }

        [Display(Name = "Transactions")]
        public List<StockTransaction> StockTransactions { get; set; }

        [Display(Name = "Holdings")]
        public List<StockHolding> StockHoldings { get; set; }

        public Stock()
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

