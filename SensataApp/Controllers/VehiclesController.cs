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
         * Žinau kad neturėjau grąžint info tiesiai iš DB, ši funkcija testavimui.
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
         * Get the latest data of all vehicles.
         */
        [HttpGet("latest")]
        public ActionResult<IEnumerable<LatestVehicleData>> GetLatestVehicleData()
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
        public ActionResult<IEnumerable<VehicleDataDTO>> GetVehicleData(string id)
        {
            var vehicleData = _vehicleService.GetVehicleData(id);
            if (vehicleData != null)
                return Ok(vehicleData);
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
         * Add an instance of vehicle data input.
         */
        [HttpPost("{id}")]
        public ActionResult AddVehicleData(string id, [FromBody] VehicleDataDTO data)
        {
            string name = _vehicleService.AddVehicleData(id, data);
            if (name != null)
                return Ok($"Data added to vehicle {name}. {data}");
            return NotFound($"Vehicle with ID: {id} not found.");
        }
    }
}
