using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SensataApp.DTOs;
using SensataApp.Hubs;
using SensataApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SensataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInputsController : ControllerBase
    {
        private readonly VehiclesContext _vehiclesContext;
        private readonly IHubContext<VehicleInputHub> _vehiclesHubContext;

        public VehicleInputsController(VehiclesContext vehiclesContext, IHubContext<VehicleInputHub> vehiclesHubContext)
        {
            _vehiclesContext = vehiclesContext;
            _vehiclesHubContext = vehiclesHubContext;
        }

        /**
         * GET api/vehicleinputs/latest
         * Get the latest inputs of all vehicles.
         */
        [HttpGet("latest")]
        public ActionResult<IEnumerable<LatestVehicleInput>> GetLatestVehicleInputs()
        {
            List<LatestVehicleInput> latestVehicleInputs = new List<LatestVehicleInput>();
            VehicleInput vehicleInput;

            // Iterate over all vehicles and get their latest inputs.
            // Map that data to LatestVehicleInput objects.
            foreach (Vehicle vehicle in _vehiclesContext.Vehicles)
            {
                vehicleInput = _vehiclesContext.VehicleInputs
                    .Where(_vehicleInput => _vehicleInput.VehicleId == vehicle.Id)
                    .OrderByDescending(_vehicleInput => _vehicleInput.Created)
                    .FirstOrDefault();

                // If vehicle doesn't have inputs then set default(empty) values.
                if (vehicleInput == null)
                {
                    latestVehicleInputs.Add(new LatestVehicleInput()
                    {
                        VehicleId = vehicle.Id,
                        VehicleName = vehicle.Name,
                        Latitude = null,
                        Longitude = null,
                        Speed = null
                    });
                }
                else
                {
                    latestVehicleInputs.Add(new LatestVehicleInput()
                    {
                        VehicleId = vehicle.Id,
                        VehicleName = vehicle.Name,
                        Latitude = vehicleInput.Latitude,
                        Longitude = vehicleInput.Longitude,
                        Speed = vehicleInput.Speed
                    });
                }
            }

            // Check if any vehicles were found.
            if (latestVehicleInputs != null)
                return Ok(latestVehicleInputs);
            else
                return NotFound("No vehicle data found.");
        }


        /**
         * GET api/vehicleinputs/{id}
         * Get all inputs from the specified vehicle.
         */
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VehicleInputDTO>> GetVehicleInputs(string id)
        {
            List<VehicleInputDTO> vehicleInputDTOs = new List<VehicleInputDTO>();

            // Check if vehicle with given ID exists.
            Vehicle vehicle = _vehiclesContext.Vehicles.Find(id);
            if (vehicle != null)
            {
                // Get list of vehicle inputs, return empty list on database query error.
                try
                {
                    vehicleInputDTOs = _vehiclesContext.VehicleInputs
                        .Where(_vehicleInput => _vehicleInput.VehicleId == id)
                        .OrderBy(_vehicleInput => _vehicleInput.Created)
                        .Select(_vehicleInput => new VehicleInputDTO { Latitude = _vehicleInput.Latitude, Longitude = _vehicleInput.Longitude, Speed = _vehicleInput.Speed })
                        .DefaultIfEmpty().ToList();
                }
                catch
                {
                    return Ok(new List<VehicleInputDTO>());
                }

                return Ok(vehicleInputDTOs);
            }
            return NotFound($"Vehicle with ID: {id} was not found.");
        }


        /**
         * POST api/vehicleinputs/id
         * Add an instance of vehicle input.
         */
        [HttpPost("{id}")]
        public ActionResult AddVehicleInput(string id, [FromBody] VehicleInputDTO input)
        {
            // Check if vehicle with given ID exists.
            Vehicle vehicle = _vehiclesContext.Vehicles.Find(id);
            if (vehicle != null)
            {
                // Map received data to database entity.
                VehicleInput vehicleInput = new VehicleInput
                {
                    VehicleId = id,
                    Latitude = input.Latitude,
                    Longitude = input.Longitude,
                    Speed = input.Speed
                };
                _vehiclesContext.VehicleInputs.Add(vehicleInput);
                _vehiclesContext.SaveChanges();

                _vehiclesHubContext.Clients.All.SendAsync("vehicleinputadded");

                return Ok(input);
            }

            return NotFound($"Vehicle with ID: {id} not found.");
        }
    }
}
