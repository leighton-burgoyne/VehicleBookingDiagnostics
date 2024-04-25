using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarBookingDiagnostics.Data;
using CarBookingDiagnostics.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarBookingDiagnostics.Pages.Manage.Vehicles
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CarBookingDiagnostics.Data.SystemData _context;

        public DetailsModel(CarBookingDiagnostics.Data.SystemData context)
        {
            _context = context;
        }

        public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                Vehicle = vehicle;
            }
            return Page();
        }
    }
}
