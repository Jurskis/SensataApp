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
         * Žinau jog nesaugu, ši funkcija testavimui.
         * 
         */
        [HttpGet]
        public ActionResult<List<Vehicle>> GetVehicles()
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
         * Get the latest data of all vehicles.
         */
        [HttpGet("latest")]
        public ActionResult<List<object>> GetLatestVehicleData()
        {
            var latestVehicleData = _vehicleService.GetLatestVehicleData();
            // Check if any vehicles were found.
            if (latestVehicleData != null)
                return Ok(latestVehicleData);
            else
                return NotFound("No vehicle data found.");
        }


        /**
         * GET api/<VehiclesController>/{id}
         * Get all data from the specified vehicle.
         */
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicleData(string id)
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
         * Add a vehicle.
         */
        [HttpPost]
        public void AddVehicle([FromBody] VehicleDTO vehicle)
        {
            _vehicleService.AddVehicle(vehicle);
        }


        [HttpPut("{id}")]
        public void AddVehicleData(string id, [FromBody] VehicleData data)
        {
            _vehicleService.AddVehicleData(id, data);
        }


        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(string id)
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
