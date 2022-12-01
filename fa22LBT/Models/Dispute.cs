using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace fa22LBT.Models
{
    public enum DisputeStatus { Submitted, Accepted, Rejected, Adjusted}

	public class Dispute
	{
        [Key]
        public Int32 DisputeID { get; set; }

        [Display(Name = "Dispute Description")]
        [Required]
        public String DisputeDescription { get; set; }

        [Display(Name = "Correct Amount")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CorrectAmount { get; set; }

        [Display(Name = "Dispute Status")]
        public DisputeStatus DisputeStatus { get; set; }

        [Display(Name = "Admin Email")]
        public String? AdminEmail { get; set; }

        [Display(Name = "Admin Comments")]
        public String? AdminComments { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Disputed Transaction")]
        public Transaction DisputeTransaction { get; set; }

        //public Dispute()
	}
}

