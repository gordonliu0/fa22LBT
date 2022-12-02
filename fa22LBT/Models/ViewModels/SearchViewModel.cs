using System;
using System.ComponentModel.DataAnnotations;

namespace fa22LBT.Models.ViewModels
{

    public class SearchViewModel
	{

        [Display(Name = "Ascending = Checked, Descending = Unchecked")]
        public Boolean Ascending { get; set; }

        [Display(Name = "Order Search By:")]
        public SearchOrderBy? SearchOrderBy { get; set; }

        [Display(Name = "Search by Description:")]
        public String? SearchDescription { get; set; }

        [Display(Name = "Search by Type:")]
        public TransactionType? SearchType { get; set; }

        [Display(Name = "Search by Amount, Upper Bound")]
        public Decimal? SearchAmountUpper { get; set; }

        [Display(Name = "Search by Amount, Lower Bound")]
        public Decimal? SearchAmountLower { get; set; }

        [Display(Name = "Search by Transaction Number")]
        public String? SearchTNumber { get; set; }

        [Display(Name = "Search by Transactions From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime? SearchDateFrom { get; set; }

        [Display(Name = "Search by Transaction To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime? SearchDateTo { get; set; }

        public SearchViewModel()
        {

        }

    }
}

