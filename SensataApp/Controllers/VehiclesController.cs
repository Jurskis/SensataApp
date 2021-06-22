using Microsoft.AspNetCore.Mvc;
using SensataApp.DTOs;
using SensataApp.Models;
using System.Collections.Generic;

namespace SensataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private VehiclesContext _vehiclesContext;

        public VehiclesController(VehiclesContext vehiclesContext)
        {
            _vehiclesContext = vehiclesContext;
        }


        /**
         * GET api/vehicles
         * Get all vehicles.
         * 
         * Ši funkcija testavimui.
         * 
         */
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles = _vehiclesContext.Vehicles;
            // Check if any vehicles were found.
            if (vehicles != null)
                return Ok(vehicles);
            else
                return NotFound("No vehicle data found.");
        }


        /**
         * POST api/vehicles
         * Add a vehicle.
         */
        [HttpPost]
        public ActionResult AddVehicle([FromBody] VehicleDTO vehicleDTO)
        {
            // Create and add to the database a new vehicle with a newly generated ID.
            Vehicle vehicle = new Vehicle
            {
                Name = vehicleDTO.Name
            };
            _vehiclesContext.Vehicles.Add(vehicle);
            _vehiclesContext.SaveChanges();

            return Ok(vehicleDTO);
        }
    }
}
