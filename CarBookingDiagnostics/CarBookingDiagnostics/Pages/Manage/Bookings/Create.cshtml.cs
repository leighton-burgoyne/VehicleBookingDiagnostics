using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarBookingDiagnostics.Data;
using CarBookingDiagnostics.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarBookingDiagnostics.Pages.Manage.Bookings
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CarBookingDiagnostics.Data.SystemData _context;

        public CreateModel(CarBookingDiagnostics.Data.SystemData context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "AddressLine1");
        ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId");
            return Page();
        }

        [BindProperty]
        public CarBookingDiagnostics.Models.Booking Booking { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
