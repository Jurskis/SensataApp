using Microsoft.AspNetCore.Mvc;
using SensataApp.DTOs;
using SensataApp.Models;
using SensataApp.Services;
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
         * Get all vehicles.
         * 
         * Ši funkcija testavimui.
         * 
         */
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles = _vehicleService.GetVehicles();
            // Check if any vehicles were found.
            if (vehicles != null)
                return Ok(vehicles);
            else
                return NotFound("No vehicle data found.");
        }


        /**
         * GET api/<VehiclesController>/latest
         * Get the latest inputs of all vehicles.
         */
        [HttpGet("latest")]
        public ActionResult<IEnumerable<LatestVehicleInput>> GetLatestVehicleInputs()
        {
            var latestVehicleInputs = _vehicleService.GetLatestVehicleInputs();
            // Check if any vehicles were found.
            if (latestVehicleInputs != null)
                return Ok(latestVehicleInputs);
            else
                return NotFound("No vehicle data found.");
        }


        /**
         * GET api/<VehiclesController>/{id}
         * Get all inputs from the specified vehicle.
         */
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VehicleInputDTO>> GetVehicleInputs(string id)
        {
            var vehicleInputs = _vehicleService.GetVehicleInputs(id);
            if (vehicleInputs != null)
                return Ok(vehicleInputs);
            return NotFound($"Vehicle with ID: {id} was not found.");
        }

        /**
         * POST api/<VehiclesController>
         * Add a vehicle.
         */
        [HttpPost]
        public ActionResult AddVehicle([FromBody] VehicleDTO vehicle)
        {
            _vehicleService.AddVehicle(vehicle);
            return Ok($"Vehicle added. {vehicle}");
        }


        /**
         * POST api/<VehiclesController>/id
         * Add an instance of vehicle input.
         */
        [HttpPost("{id}")]
        public ActionResult AddVehicleInput(string id, [FromBody] VehicleInputDTO input)
        {
            string name = _vehicleService.AddVehicleInput(id, input);
            if (name != null)
                return Ok($"Data added to vehicle {name}. {input}");
            return NotFound($"Vehicle with ID: {id} not found.");
        }
    }
}
