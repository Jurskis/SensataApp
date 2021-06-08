using Microsoft.EntityFrameworkCore;
using SensataApp.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensataApp.Db.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
