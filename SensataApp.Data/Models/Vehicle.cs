using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SensataApp.Db.Models
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<VehicleData> Data { get; set; }
    }
}
