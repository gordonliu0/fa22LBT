using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace fa22LBT.Models
{ 
    //NOTE: This is the view model used to allow the user to login
    //The user only needs teh email and password to login
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    //NOTE: This is the view model used to register a user
    //When the user registers, they only need to specify the
    //properties listed in this model
    public class RegisterViewModel
    {   
        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
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
        public String State { get; set; }

        [Display(Name = "Zip Code")]
        public String ZipCode { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }

        [Display(Name = "User Status")]
        public Boolean IsActive { get; set; }

        // EMPLOYEE FIELDS
        [Display(Name = "Social Security")]
        [MinLength(9)]
        [MaxLength(9)]
        public String? SSN { get; set; }


        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    //NOTE: This is the view model used to allow the user to 
    //change their password
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserViewModel
    {
        public String Email { get; set; }

        [Display(Name = "First Name")]
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

        [Display(Name = "Phone Number")]
        [Required]
        public String PhoneNumber { get; set; }
    }

    //NOTE: This is the view model used to display basic user information
    //on the index page
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
        public String FullName { get; set; }
        public String FullAddress { get; set; }
        public String PhoneNumber { get; set; }
        public Int32 Age { get; set; }
        public Boolean IsActive { get; set; }
    }
}
