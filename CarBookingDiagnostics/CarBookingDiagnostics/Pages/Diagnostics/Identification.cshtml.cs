using Microsoft.AspNetCore.Mvc.RazorPages;
using CarBookingDiagnostics.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace CarBookingDiagnostics.Pages
{
    public class IdentificationModel : PageModel
    {
        public Vehicle Vehicle { get; set; }

        public List<string> SelectedQuestions { get; set; }

        public List<string> SelectedOptions { get; set; }

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

            string issue = Request.Query["issue"].ToString();
            SelectedQuestions = GetSymptoms(issue);

            HttpContext.Session.SetString("issue", issue);
        }
        
        public IActionResult OnPost(List<string> selectedOptions)
        {
            HttpContext.Session.SetString("SelectedOptions", JsonConvert.SerializeObject(selectedOptions));
            string issue = Request.Query["issue"].ToString();
            return RedirectToPage("/Diagnostics/Resolve", new { issue = issue });
        }

    // Get Symptoms
    private List<string> GetSymptoms(string issue)
        {
            Dictionary<string, List<string>> symptoms = new Dictionary<string, List<string>>
            {
                { "MOT", new List<string> { "MOT Expired/Expiring Soon" } },
                { "Service", new List<string> { "Service Light on Dashboard", "Service Date Due/Ready", "Engine Oil requires change" } },
                { "Brakes", new List<string> { "Too hard or soft to press brake pedal", "Handbrake failure", "Squeaky/noisy brakes", "ABS Fault Light" } },
                { "Battery", new List<string> { "Dead battery", "Start/Stop failure", "Burning smell" } },
                { "Tyres", new List<string> { "Tyre Pressure Low", "Tyre Alignment out" } },
                { "Engine", new List<string> { "Clutch Defective", "Engine Light on Dashboard", "Fluid Leaks", "Limp mode" } },
                { "Lights", new List<string> { "Headlight Bulb Failure", "Levelling System Failure", "Headlight Cleaning Mechanism inoperable" } },
                { "Windows", new List<string> { "Window visibly damaged", "Window stuck or inoperable", "Windscreen cracked or chipped" } },
                { "Electrical", new List<string> {"Intermittent faults", "Various Warning Lights on Dashboard", "Blown fuses" } },
                { "Air Conditioning", new List<string> { "Air Conditioning pumping out warm air", "Damp Smell", "Air Conditioning not working" } },
                { "Wipers", new List<string> { "Wiper Blade Deteriorated/Defective", "Screenwash not working" } },
                { "Other", new List<string> { "Transmission Problems", "Suspension Problems", "Infotainment Problems", "Number Plate Replacement" } },
            };

            return symptoms.ContainsKey(issue) ? symptoms[issue] : new List<string>();
        }
    }
}