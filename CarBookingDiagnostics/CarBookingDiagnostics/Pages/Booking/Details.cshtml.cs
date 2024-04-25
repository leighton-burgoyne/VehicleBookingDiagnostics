using Microsoft.AspNetCore.Mvc.RazorPages;
using CarBookingDiagnostics.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http;
using CarBookingDiagnostics.Data;

namespace CarBookingDiagnostics.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly SystemData _context;
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Vehicle Vehicle { get; set; }
        public List<string> SelectedOptionsList { get; set; }
        public string SelectedOptions { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }
        public string Issue { get; set; }
        public Dictionary<string, List<string>> Solutions { get; set; }

        public DetailsModel(SystemData context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Customer = new Customer();
            // Retrieve the JSON string from TempData
            //var vehicleJson = TempData["VehicleJson"] as string;
            var vehicleJson = HttpContext.Session.GetString("VehicleJson");

            Debug.WriteLine(vehicleJson);
            if (vehicleJson != null)
            {
                // Deserialize the JSON string into a Vehicle object
                Vehicle = JsonConvert.DeserializeObject<Vehicle>(vehicleJson);
            }

            else
            {
                Debug.WriteLine("Veh detected null");
            }
            SelectedOptionsList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("SelectedOptions"));
            SelectedOptions = string.Join(",", SelectedOptionsList);
            Issue = HttpContext.Session.GetString("issue");
            SelectedDate = HttpContext.Session.GetString("SelectedDate");
            SelectedTime = HttpContext.Session.GetString("SelectedTime");
        }

        public async Task<IActionResult> OnPost()
        {
            var vehicleJson = HttpContext.Session.GetString("VehicleJson");

            if (vehicleJson != null)
            {
                // Deserialize the JSON string into a Vehicle object
                Vehicle = JsonConvert.DeserializeObject<Vehicle>(vehicleJson);
            }

            SelectedOptionsList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("SelectedOptions"));
            SelectedOptions = string.Join(",", SelectedOptionsList);
            Issue = HttpContext.Session.GetString("issue");
            SelectedDate = HttpContext.Session.GetString("SelectedDate");
            SelectedTime = HttpContext.Session.GetString("SelectedTime");

            // Add Customer Details to Database
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            // Add Vehicle Details to Database
            _context.Vehicles.Add(Vehicle);
            await _context.SaveChangesAsync();

            // Create new Booking
            var booking = new CarBookingDiagnostics.Models.Booking
            {
                CustomerId = Customer.CustomerId,
                VehicleId = Vehicle.VehicleId,
                Type = Issue,
                Date = SelectedDate,
                Time = SelectedTime,
                Problems = SelectedOptions,
            };

            // Add Booking Details to Database
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32("BookingId", booking.BookingId);

            // Go to Confirmation Page
            return RedirectToPage("/Booking/Confirmation", new { bookingId = booking.BookingId });
        }
    }
}