using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensataApp.Db.Models
{
    [Keyless]
    public class VehicleData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Speed { get; set; }
        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
