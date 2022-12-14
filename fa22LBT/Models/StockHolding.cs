using System;
using System.ComponentModel.DataAnnotations;

namespace fa22LBT.Models
{
	public class StockHolding
	{
        [Key]
        public Int32 StockHoldingID { get; set; }

        [Display(Name = "Quantity of Shares")]
        [Required]
        [Range(1, 2147483646, ErrorMessage = "You must sell at least one share.")]
        [RegularExpression(@"^[\d]*$", ErrorMessage = "Please input an integer value!")]
        public Int32 QuantityShares { get; set; }

        // CALCULATED PROPERTIES
        [Display(Name = "Total Value")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TotalValue { get
            {
                if (this.Stock != null)
                {
                    return QuantityShares * Stock.StockPrice;
                }
                return -1m;
            }
        }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Stock Portfolio Account")]
        public StockPortfolio StockPortfolio { get; set; }

        [Display(Name = "Stock")]
        public Stock Stock { get; set; }
	}
}

