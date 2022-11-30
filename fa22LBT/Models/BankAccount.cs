using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fa22LBT.Models
{
    public enum AccountTypes { Checking, Savings, IRA, StockPortfolio }

    public class BankAccount
	{
        const Decimal CONTRIBUTION_LIMIT = 5000m;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String AccountID { get; set; }

        [Display(Name = "Account Number")]
        public Int64 AccountNo { get; set; } = 10000000;

        [Display(Name = "Account Name")]
        public String? AccountName { get; set; }

        [Display(Name = "Account Type")]
        public AccountTypes AccountType { get; set; }

        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal AccountBalance { get; set; }

        [Display(Name = "Approved")]
        public Boolean IsApproved { get; set; }

        // IRA
        [Display(Name = "Yearly Contribution")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Contribution { get; set; }

        // CALCULATED PROPERTIES

        [Display(Name = "Hidden Account Number")]
        public String? HiddenAccountNo
        {
            get
            {
                return "******" + this.AccountNo.ToString().Substring(6);
            }
        }

        [Display(Name = "CompiledQuickInfo")]
        public String? AccountQuickInfo
        {
            get
            {
                return this.AccountName + " " + this.HiddenAccountNo + " " + this.AccountBalance;
            }
        }

            // NAVIGATIONAL PROPERTIES

            [Display(Name = "Customer")]
        public AppUser Customer { get; set; }

        [Display(Name = "Transactions")]
        public List<Transaction> Transactions { get; set; }

        public StockPortfolio StockPortfolio { get; set; }

        public BankAccount()
		{
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
        }
	}
}

