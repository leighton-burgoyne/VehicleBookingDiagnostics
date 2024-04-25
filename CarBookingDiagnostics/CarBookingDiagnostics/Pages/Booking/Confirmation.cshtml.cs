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
    public class ConfirmationModel : PageModel
    {
        public Vehicle Vehicle { get; set; }
        public List<string> SelectedOptionsList { get; set; }
        public string SelectedOptions { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }
        public string Issue { get; set; }
        public int BookingId { get; set; }

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

            SelectedOptionsList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("SelectedOptions"));
            SelectedOptions = string.Join(",", SelectedOptionsList);
            SelectedDate = HttpContext.Session.GetString("SelectedDate");
            SelectedTime = HttpContext.Session.GetString("SelectedTime");
            Issue = HttpContext.Session.GetString("issue");
            BookingId = (int)HttpContext.Session.GetInt32("BookingId");
        }
    }
}