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

        [Display(Name = "Transaction Number")]
        public Int32 STransactionNo { get; set; }

        [Display(Name = "Stock Price at Purchase")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal PricePerShare { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // CALCULATED PROPERTIES
        [Display(Name = "Total Value")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal InitialValue { get { return this.QuantityShares * this.PricePerShare; } }

        [Display(Name = "Total Value")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CurrentValue { get
            {
                if (this.Stock != null)
                {
                    return this.QuantityShares * this.Stock.StockPrice;
                }
                return this.PricePerShare;
            }
        }

        [Display(Name = "Difference in share price (+ is gain, - is loss)")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal StockPriceDifference
        {
            get
            {
                if (this.Stock != null)
                {
                    return this.Stock.StockPrice - this.PricePerShare;
                }
                return this.PricePerShare;
            }
        }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Stock Portfolio Account")]
        public StockPortfolio StockPortfolio { get; set; }

        [Display(Name = "Stock")]
        public Stock Stock { get; set; }
	}
}

