using Microsoft.AspNetCore.Mvc.RazorPages;
using CarBookingDiagnostics.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CarBookingDiagnostics.Pages
{
    public class VehicleDetailsModel : PageModel
    {
        public Vehicle Vehicle { get; set; }

        [BindProperty]
        public string SelectedModel { get; set; }  // Property to store selected Model

        public void OnGet()
        {
            // Retrieve the JSON string from TempData
            //var vehicleJson = TempData["VehicleJson"] as string;
            var vehicleJson = HttpContext.Session.GetString("VehicleJson");

            if (vehicleJson != null)
            {
                // Deserialize the JSON string into a Vehicle object
                Vehicle = JsonConvert.DeserializeObject<Vehicle>(vehicleJson);

                ViewData["VehicleModel"] = Vehicle.Model;

                // Get the Models for the provided Vehicle Make
                var models = GetModelsForMake(Vehicle.Make);
                ViewData["Models"] = models; // Set ViewData models to models for future access
            }
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(SelectedModel))
            {
                var vehicleJson = HttpContext.Session.GetString("VehicleJson");

                if (vehicleJson != null)
                {
                    // Deserialize the JSON string into a Vehicle object
                    Vehicle = JsonConvert.DeserializeObject<Vehicle>(vehicleJson);

                    Vehicle.Model = SelectedModel;

                    // Serialize the updated Vehicle object and store it back into session
                    HttpContext.Session.SetString("VehicleJson", JsonConvert.SerializeObject(Vehicle));
                }
            }

            return RedirectToPage("/VehicleDetails");
        }

        private List<string> GetModelsForMake(string make)
        {
            // Read the JSON File
            string jsonFilePath = "Data/CarMakesAndModels.json";
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            // Deserialize JSON into CarMakesAndModels object
            CarMakesAndModels carMakesAndModels = JsonConvert.DeserializeObject<CarMakesAndModels>(jsonData);

            // Change case of the Vehicle Make received from JSON to Capitalised (eg. FORD to Ford)
            make = char.ToUpper(make[0]) + make.Substring(1).ToLowerInvariant();

            // If the carMakesAndModels dictionary is not null and it contains the make
            if (carMakesAndModels != null && carMakesAndModels.MakesAndModels.ContainsKey(make))
            {
                // Return the models for that particular make
                return carMakesAndModels.MakesAndModels[make];
            }
            else
            {
                // Returns empty list (FIX FOR NULLPTR)
                return new List<string>();
            }
        }
    }
}