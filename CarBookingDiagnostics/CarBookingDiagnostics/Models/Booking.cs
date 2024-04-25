using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarBookingDiagnostics.Models
{
    public class Booking
    {
        [Display(Name = "ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "Time")]
        public string Time { get; set; }

        [Display(Name = "Problems")]
        public string Problems { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
    }
}