using System.ComponentModel.DataAnnotations;

namespace CarBookingDiagnostics.Models
{
    public class Account
    {
        [Display(Name = "ID"), Key]
        public int AccountId { get; set; }
    }
}
