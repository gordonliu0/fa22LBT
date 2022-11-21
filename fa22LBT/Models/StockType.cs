using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace fa22LBT.Models
{
	public class StockType
	{
		[Key]
		public Int32 StockTypeID { get; set; }

        [Display(Name = "Stock Type")]
        public String StockTypeName { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Stocks")]
        public List<Stock> Stocks { get; set; }
    }
}

