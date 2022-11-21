using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace fa22LBT.Models
{
    public class AppUser : IdentityUser
    {
        // COMMON USER FIELDS
        [Display(Name="First Name")]
        [Required]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        public String? MiddleInitial { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public String LastName { get; set; }

        [Display(Name = "Address")]
        [Required]
        public String Address { get; set; }

        [Display(Name = "City")]
        [Required]
        public String City { get; set; }

        [Display(Name = "State")]
        [Required]
        public String State { get; set; }

        [Display(Name = "Zip Code")]
        [Required]
        public String ZipCode { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }

        [Display(Name = "User Status")]
        public Boolean IsActive { get; set; }

        // EMPLOYEE FIELDS
        [Display(Name = "Social Security")]
        [MinLength(9)]
        [MaxLength(9)]
        public String? SSN { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Accounts")]
        public List<BankAccount> BankAccounts { get; set; }

        [Display(Name = "Stock Portfolios")]
        public List<StockPortfolio> StockPortfolios { get; set; }

        // INITIALIZE
        public AppUser()
        {
            if (BankAccounts == null)
            {
                BankAccounts = new List<BankAccount>();
            }

            if (StockPortfolios == null)
            {
                StockPortfolios = new List<StockPortfolio>();
            }
        }
    }
}
