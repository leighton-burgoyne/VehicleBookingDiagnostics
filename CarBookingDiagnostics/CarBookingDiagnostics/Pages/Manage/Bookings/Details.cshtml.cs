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

namespace CarBookingDiagnostics.Pages.Manage.Bookings
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CarBookingDiagnostics.Data.SystemData _context;

        public DetailsModel(CarBookingDiagnostics.Data.SystemData context)
        {
            _context = context;
        }
        [BindProperty]
        public Customer Customer { get; set; }
        public CarBookingDiagnostics.Models.Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                Booking = booking;
            }
            return Page();
        }
    }
}
