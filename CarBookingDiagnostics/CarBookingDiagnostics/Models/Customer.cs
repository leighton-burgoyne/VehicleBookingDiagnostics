using System.ComponentModel.DataAnnotations;

namespace CarBookingDiagnostics.Models
{
    public class Customer
    {
        [Display(Name = "ID"), Key]
        public int CustomerId { get; set; }
        
        [Display(Name = "First Name"), Required]
        public string FirstName {  get; set; }
        
        [Display(Name = "Last Name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Address Line 1"), Required]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Town / City"), Required]
        public string TownCity { get; set; }

        [Display(Name = "Postcode"), Required]
        public string Postcode { get; set; }

        [Display(Name = "County"), Required]
        public string County { get; set; }

        [Display(Name = "Email Address"), Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number"), Required]
        public string PhoneNumber { get; set; }
    }
}
