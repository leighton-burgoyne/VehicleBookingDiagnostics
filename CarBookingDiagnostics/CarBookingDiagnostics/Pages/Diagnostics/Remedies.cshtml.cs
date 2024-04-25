using Microsoft.AspNetCore.Mvc.RazorPages;
using CarBookingDiagnostics.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingDiagnostics.Pages
{
    public class RemediesModel : PageModel
    {
        public Vehicle Vehicle { get; set; }

        public List<string> SelectedQuestions { get; set; }

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

            string buttonId = Request.Query["issue"].ToString();
            SelectedQuestions = GetOptions(buttonId);
        }

        private List<string> GetOptions(string issue)
        {
            Dictionary<string, List<string>> remedyQuestions = new Dictionary<string, List<string>>
            {
                { "MOT", new List<string> { "Book MOT Test" } },
                { "Service", new List<string> { "Book Service Appointment" } },
                { "Brakes", new List<string> { "Checked Brake Fluid Levels", "Checked Brake Pads and Discs" } },
                { "Battery", new List<string> { "Replaced Battery", "Checked Start-Stop Mode", "Checked Connections for signs of scorching or loose connections" } },
                { "Tyres", new List<string> { "Tried inflating tyres", "Realigned Tyres" } },
                { "Engine", new List<string> { "Tried cycling Engine", "Checked for any other Fault Lights", "Verified correct type of fuel used" } },
                { "Lights", new List<string> { "Replaced bulb(s)", "Added Windscreen Fluid" } },
                { "Windows", new List<string> { "Applied a Repair Kit to the crack or chip", "Checked for obstructions near window" } },
                { "Electrical", new List<string> {"Checked fuses and wiring", "Various Warning Lights on Dashboard" } },
                { "AirCon", new List<string> { "Cleaned Cabin Filters", "Had system recharged" } },
                { "Wipers", new List<string> { "Tried replacing Wiper Blade/Motor", "Checked for obstructions around wipers", "Topped up screenwash" } },
                { "Other", new List<string> { "Infotainment faulty/inoperative", "No display and/or audio", "Tried checking gear selection" } },
            };

            return remedyQuestions.ContainsKey(issue) ? remedyQuestions[issue] : new List<string>();
        }
    }
}