using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace fa22LBT.Models
{
    public enum DisputeStatus { Submitted, Approved}

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

        [Display(Name = "DisputeStatus")]
        public DisputeStatus DisputeStatus { get; set; }

        // NAVIGATIONAL PROPERTIES

        [Display(Name = "Disputed Transaction")]
        public Transaction DisputeTransaction { get; set; }

        //public Dispute()
	}
}

