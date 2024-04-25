using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace CarBookingDiagnostics.Models
{
    public class Vehicle
    {
        [Display(Name = "ID")]
        public int VehicleId { get; set; }

        [Display(Name = "Registration Plate")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Colour")]
        public string Colour { get; set; }

        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }

        [Display(Name = "First Registered")]
        public string MonthOfFirstRegistration { get; set; }

        [Display(Name = "Manufacture Year")]
        public string YearOfManufacture { get; set; }

        [Display(Name = "Wheelplan")]
        public string Wheelplan { get; set; }

        [Display(Name = "MOT Status")]
        public string MOTStatus { get; set;  }

        [Display(Name = "MOT Expiry Date")]
        public string MOTExpiryDate { get; set; }
    }
}
