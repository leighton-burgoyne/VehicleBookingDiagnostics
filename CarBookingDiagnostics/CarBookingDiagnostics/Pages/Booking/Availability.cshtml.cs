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

namespace CarBookingDiagnostics.Pages
{
    public class AvailabilityModel : PageModel
    {
        public Vehicle Vehicle { get; set; }
        public List<string> SelectedOptions { get; set; }
        public Dictionary<string, List<string>> Solutions { get; set; }
        public List<string> Dates { get; set; }
        public List<string> Times { get; set; }

        public void OnGet()
        {
            // Retrieve the JSON string from TempData
            //var vehicleJson = TempData["VehicleJson"] as string;
            var vehicleJson = HttpContext.Session.GetString("VehicleJson");

            if (vehicleJson != null)
            {
                // Deserialize the JSON string into a Vehicle object
                Vehicle = JsonConvert.DeserializeObject<Vehicle>(vehicleJson);
            }

            Dates = GetDates();
            Times = GetTimes();
        }

        public IActionResult OnPost(string selectedDate, string selectedTime)
        {
            HttpContext.Session.SetString("SelectedDate", selectedDate);
            HttpContext.Session.SetString("SelectedTime", selectedTime);

            string issue = Request.Query["issue"].ToString();
            return RedirectToPage("/Booking/Details");
        }

        private List<string> GetDates()
        {
            List<string> dates = new List<string>();
            DateTime current = DateTime.Today;

            for (int i = 0; i < 30; i++)
            {
                DateTime date = current.AddDays(i);
                string formattedDate = $"{date.ToString("dd/MM/yyyy")}";
                dates.Add(formattedDate);
            }
            return dates;
        }

        private List<string> GetTimes()
        {
            List<string> times = new List<string>();
            DateTime start = DateTime.Today.AddHours(7);
            DateTime end = DateTime.Today.AddHours(19);

            while (start < end)
            {
                times.Add(start.ToString("HH:mm"));
                start = start.AddMinutes(30);
            }
            return times;
        }
    }
}