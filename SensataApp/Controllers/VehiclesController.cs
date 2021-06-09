using Microsoft.AspNetCore.Mvc;
using SensataApp.Core;
using SensataApp.Models;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        /**
         * GET api/<VehiclesController>
         * Get the latest data of all vehicles.
         */
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetLatestVehiclesData()
        {
            var vehicles = _vehicleService.GetLatestVehiclesData();
            // Check if any vehicles were found.
            if (vehicles != null)
                return Ok(vehicles);
            else
                return NotFound("No vehicle data found.");
        }


        /**
         * GET api/<VehiclesController>/{id}
         * Get all data from the specified vehicle.
         */
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicleData(Guid id)
        {
            var vehicle = _vehicleService.GetVehicle(id);
            // Check if vehicle with the given ID was found.
            if (vehicle != null)
                return Ok(vehicle);
            else
                return NotFound($"Vehicle with id: {id} was not found.");
        }

        /**
         * POST api/<VehiclesController>
         * Add a vehicle with possible default data.
         */
        [HttpPost]
        public ActionResult AddVehicle([FromBody] VehicleData data)
        {
            _vehicleService.AddVehicle(data);
            return Ok("Received post");
        }


        [HttpPut("{id}")]
        public void AddVehicleData(Guid id, [FromBody] VehicleData data)
        {
            _vehicleService.AddVehicleData(id, data);
        }


        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(Guid id)
        {
            var vehicle = _vehicleService.GetVehicle(id);
            // Check if vehicle with the given ID was found.
            if (vehicle != null)
            {
                _vehicleService.DeleteVehicle(vehicle);
                return Ok();
            } 

            return NotFound($"Vehicle with id: {id} was not found.");
        }
    }
}
