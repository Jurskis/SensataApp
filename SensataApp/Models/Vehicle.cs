using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensataApp.Models
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public ICollection<VehicleData> Data { get; set; }
        public string Name { get; set; }
    }
}
