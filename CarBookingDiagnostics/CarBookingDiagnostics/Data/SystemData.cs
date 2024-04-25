using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarBookingDiagnostics.Models;

namespace CarBookingDiagnostics.Data
{
    public class SystemData : DbContext
    {
        public SystemData (DbContextOptions<SystemData> options)
            : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Staff> Staff { get; set; } = default!;
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
    }
}
