using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensataApp.Models
{
    public class VehicleInput
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Speed { get; set; }
    }
}
