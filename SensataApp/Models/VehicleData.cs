using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensataApp.Models
{
    public class VehicleData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Speed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
