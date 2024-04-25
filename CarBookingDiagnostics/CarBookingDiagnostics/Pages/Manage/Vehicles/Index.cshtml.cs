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
    public class IndexModel : PageModel
    {
        private readonly CarBookingDiagnostics.Data.SystemData _context;

        public IndexModel(CarBookingDiagnostics.Data.SystemData context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Vehicle = await _context.Vehicles.ToListAsync();
        }
    }
}
