using CarBookingDiagnostics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using RestSharp;

namespace CarBookingDiagnostics.Pages
{
    public class IndexModel : PageModel
    {
        public Vehicle Vehicle { get; set; }

        [BindProperty]
        public string VehicleRegistration { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create the Rest Client Instance
            var client = new RestClient("https://driver-vehicle-licensing.api.gov.uk");

            // Create the Rest Request Instance
            var request = new RestRequest("vehicle-enquiry/v1/vehicles", RestSharp.Method.Post);

            // Create the Request Headers and Paramaters (suchb as passing in the Registration Number)
            request.AddHeader("x-api-key", "zDXrAbbvV88xrnVQqAWQHNehZfeIWgN1fEro2qb7"); // Reg Plate
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(new { registrationNumber = VehicleRegistration }), ParameterType.RequestBody);

            // Execute RestResponse Request
            var response = await client.ExecuteAsync(request);

            // If the request is successful
            if (response.IsSuccessful)
            {
                // Split the JSON up (deserialize) into Vehicle Object (was previously var vehicle)
                var vehicle = JsonConvert.DeserializeObject<Vehicle>(response.Content);

                // TO DO: LOAD MODEL IN HERE

                // Serialize the Vehicle Object into a JSON string (was previously vehicle)
                var vehicleJson = JsonConvert.SerializeObject(vehicle);

                // Store the JSON string in a session state
                HttpContext.Session.SetString("VehicleJson", vehicleJson);
                Vehicle = vehicle;
                //TempData["VehicleJson"] = vehicleJson;

                // TO DO: PASS MODEL INTO VEHICLE OBJECT

                // Redirect to the VehicleDetails page
                return RedirectToPage("/VehicleDetails");
            }

            // If the request is not successful
            else
            {
                ModelState.AddModelError("", "Failed to retrieve Vehicle Details. Please check the Registration Number is valid.");
                return Page();
            }
        }
    }
}