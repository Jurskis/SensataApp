using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensataApp.Models
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public ICollection<VehicleInput> Inputs { get; set; }

        public string Name { get; set; }
    }
}
