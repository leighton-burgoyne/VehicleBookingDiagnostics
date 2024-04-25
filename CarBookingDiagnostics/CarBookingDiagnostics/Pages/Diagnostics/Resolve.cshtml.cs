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
    public class ResolveModel : PageModel
    {
        // Vehicle Property
        public Vehicle Vehicle { get; set; }

        // List for Selected Checkbox Options
        public List<string> SelectedOptions { get; set; }

        // Dictionary for Solution Lists
        public Dictionary<string, List<string>> Solutions { get; set; }

        public void OnGet()
        {

            // Retrieve Vehicle Json from Session State
            var vehicleJson = HttpContext.Session.GetString("VehicleJson");

            // Check if not null
            if (vehicleJson != null)
            {
                // Deserialize the JSON string into the Vehicle object
                Vehicle = JsonConvert.DeserializeObject<Vehicle>(vehicleJson);
            }

            // Deserialize the Selected Options list into the SelectedOptions string
            SelectedOptions = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("SelectedOptions"));

            // Dictionary for Solution Lists
            Solutions = new Dictionary<string, List<string>>();

            // Get solutions for each Selected Option
            foreach (var selectedOption in SelectedOptions)
            {
                if (Solutions.ContainsKey(selectedOption))
                {
                    continue;
                }

                Solutions[selectedOption] = GetSolutions(selectedOption); // Get Solutions for Selected Option
            }
        }

        // Get Solutions
        private List<string> GetSolutions(string selectedOption)
        {
            Dictionary<string, List<string>> identifiedSolutions = new Dictionary<string, List<string>>
            {
                // MOT Solutions
                { "MOT Expired/Expiring Soon", new List<string> { "Your Vehicle requires an MOT conducted before your MOT Date once a year to ensure that it is legal, safe and roadworthy in the United Kingdom. Driving without an MOT can result in a fine up to £1000 and points on your licence." } },
                
                // Service Solutions
                { "Service Light on Dashboard", new List<string> { "Your Vehicle is displaying a Service Light on the Dashboard, this means that your Vehicle requires a Service to keep your car running smoothly." } },
                { "Service Date Due/Ready", new List<string> { "Your Vehicle's annual service is due or ready, we can complete a Service on the Vehicle for you, and provide the stamped logbook." } },
                { "Engine Oil requires change", new List<string> { "Your Engine Oil requires a change, it is important you get your Engine Oil changed annually or every 7000 miles (whichever comes sooner). This can be completed as part of a basic service. " } },
                
                // Brake Solutions
                { "Too hard or soft to press brake pedal", new List<string> { "There could be several reasons why your Brake Pedal is hard to press, this is a serious safety issue which needs resolving quickly. It could be down to low fluid levels, worn brake pads and/or calipers or another issue. " } },
                { "Handbrake failure", new List<string> { "Your Handbrake Cable likely needs replacing, this is a common issue on Vehicles which will lead to the complete failure of the Parking Brake, you should get this resolved quickly for safety reasons." } },
                { "Squeaky/noisy brakes", new List<string> { "Squeaky or Noisy brakes is usually because of worn-out brake pads or discs, it is important you get these fixed as soon as possible as they will impact your braking performance. " } },
                { "ABS Fault Light", new List<string> { "An ABS Fault Light usually signifies a fault with the anti-lock braking system, this means your brakes could not work as expected, which needs fixing as soon as possible." } },
                
                // Battery Solutions
                { "Dead battery", new List<string> { "A Dead Battery may be able to be jump-started by using jump leads and another Vehicle to attempt to recharge the battery to a good voltage, even if temporary. Please try jump-starting and get a Battery Replacement." } },
                { "Start/Stop failure", new List<string> { "There is multiple possible causes with Start-Stop System failure in a Vehicle - whether weather-related or battery issues. If the issue is persistent you should get your Vehicle checked out." } },
                { "Burning smell", new List<string> { "This is a serious issue, if there is a burning smell coming from your Engine Compartment, you should firstly assess whether it is on fire - call 999 if it is. Otherwise, do not drive your Vehicle and get it checked out. " } },
            
                // Tyre Solutions
                { "Tyre Pressure Low", new List<string> { "If your Tyre Pressure is low, you should firstly check the Tyre for any damage such as nails or deflation. You should then try to inflate the Tyre using a Tyre Pump, if it does not hold pressure then your Tyre will need replacement." } },
                { "Tyre Alignment out", new List<string> { "Tyre Alignment can usually be diagnosed if your Vehicle handling seems different, and the wheel doesn't seem to steer in the right direction. We offer a Tyre Alignment service in these situations." } },

                // Engine Solutions
                { "Clutch Defective", new List<string> { "If your clutch is defective then you will require a check of the Clutch and Flywheel - these may need replacing in their entirety or in some cases we can replace certain parts." } },
                { "Engine Light on Dashboard", new List<string> { "If your Engine Management Light is on, there is many possible causes and you should get your Vehicle checked immediately. Some possible causes can include engine misfires and emission faults. Specific diagnostic tests will be undertaken." } },
                { "Fluid Leaks", new List<string> { "Fluid Leaks are often visible by seeing damp spots inside or under your Vehicle, these fluids could look like Oil or various fluids such as your Brake Fluid or Screenwash. We will inspect the leak and resolve it." } },
                { "Limp mode", new List<string> { "Limp Mode often happens when your Battery or Transmission has a fault detected. You will notice that your Vehicle disables the less important parts of the system, such as Climate Control and Multimedia. You should get a Mechanic to look at this to find out the cause." } },

                // Lights Solutions
                { "Headlight Bulb Failure", new List<string> { "Headlight Bulbs have a limited lifespan, and require replacement when they no longer work or start going dim. You should regularly inspect the condition of your Headlights. We stock and can fit replacement bulbs if you require it. " } },
                { "Levelling System Failure", new List<string> { "Some headlights that use Xenon or LED Technology feature a levelling system where the headlights re-level each time the Engine is started to ensure safety of other road users. You may notice that your headlights are no longer levelling, or are not functioning properly." } },
                { "Headlight Cleaning Mechanism inoperable", new List<string> { "Headlights can feature cleaning mechanisms to keep them clean. Sometimes these mechanisms can fail or block up, and they require repair to make them operable again." } },

                // Window Solutions
                { "Window visibly damaged", new List<string> { "There is no real fix for a damaged window, and it will require replacement to keep your Vehicle safe and secure. We are able to order in and replace most window parts." } },
                { "Window stuck or inoperable", new List<string> { "Window Mechanisms can often break or become faulty. You will notice this if your Window suddenly becomes inoperable and will not go up or down." } },
                { "Windscreen cracked or chipped", new List<string> { "Windscreens that are cracked or chipped can sometimes be fixed by gluing the crack so that the windscreen is safe again. In some cases we will need to replace the windscreen in it's entirety but you will be advised about this on further inspection." } },

                // Electrical Solutions
                { "Intermittent faults", new List<string> { "Intermittent electrical problems is usually down to a problem with the sources powering the car - such as the Battery, Fuses or Cables. We can carry out a full diagnostics test to find and resolve the system." } },
                { "Various Warning Lights on Dashboard", new List<string> { "Multiple Lights showing on the Dashboard at one time usually signifies a Battery or Alternator fault, you should get this looked at as soon as possible to ensure your Vehicle stays reliable and functional." } },
                { "Blown fuses", new List<string> { "Usually a fuse can blow if there is a fault or increase in current on a circuit or device. In these instances, the fuse will need replacing." } },

                // AirCon Solutions
                { "Air Conditioning pumping out warm air", new List<string> { "Air Conditioning systems contain a special gas that cools and conditions the air from outside, this gas has a limited lifespan and it will eventually evaporate, or a leak can cause it to drain out of the system. This results in the air becoming warm and no longer cooled." } },
                { "Damp Smell", new List<string> { "Filters can often cause Damp Smells when they require replacement. We stock a range of filters and can replace these quickly to get you back on the road." } },
                { "Air Conditioning not working", new List<string> { "If your Air Conditioning is not working at all, not even pushing out warm air - then this can indicate a problem with the Electrical System or potentially the ducting has become blocked. We can diagnose the problem for you." } },

                // Wiper Solutions
                { "Wiper Blade Deteriorated/Defective", new List<string> { "Wiper Blades have a limited lifespan and require replacement once they become worn down or defective. We will replace your blades and carry out a test of your wiper motor to ensure it is still functioning correctly." } },
                { "Screenwash not working", new List<string> { "If you notice your screenwash is no longer working, it could be because the pipes or pump that feed it are damaged or defective." } },

                // Other Solutions
                { "Transmission Problems", new List<string> { "Transmission Problems create a wide range of issues, including noticing problems when you change gears or accelerate your Vehicle." } },
                { "Suspension Problems", new List<string> { "Suspension Problems are noticable when your Vehicle's handling or suspension seems different. Suspensions contain many parts which need replacing eventually. " } },
                { "Infotainment Problems", new List<string> { "In Infotainment Systems, there are many parts that can go wrong - we can diagnose these problems and assist you with your Infotainment System." } },
                { "Number Plate Replacement", new List<string> { "Number Plates deteroriate over time. We are a DVLA-registered number plate supplier and we are able to replace your Number Plates. Please note we require a V5C and document showing the rights to your registration so please bring these to your Appointment." } },
            };

            // Return any matching solutions
            return identifiedSolutions.ContainsKey(selectedOption) ? identifiedSolutions[selectedOption] : new List<string>();
        }
    }
}