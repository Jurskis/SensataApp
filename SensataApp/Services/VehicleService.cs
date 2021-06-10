using SensataApp.DTOs;
using SensataApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SensataApp.Services
{
    public interface IVehicleService 
    {
        IEnumerable<Vehicle> GetVehicles();

        IEnumerable<LatestVehicleInput> GetLatestVehicleInputs();
        IEnumerable<VehicleInputDTO> GetVehicleInputs(string id);
        void AddVehicle(VehicleDTO vehicleDTO);
        string AddVehicleInput(string id, VehicleInputDTO input);
    }

    public class VehicleService : IVehicleService
    {
        private VehiclesContext _vehiclesContext;
        public VehicleService(VehiclesContext vehiclesContext)
        {
            _vehiclesContext = vehiclesContext;
        }


        // Ši funkcija testavimui.
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _vehiclesContext.Vehicles;
        }


        public IEnumerable<LatestVehicleInput> GetLatestVehicleInputs()
        {
            List<LatestVehicleInput> latestVehicleInputs = new List<LatestVehicleInput>();
            VehicleInput vehicleInput;

            // Iterate over all vehicles and get their latest inputs.
            // Map that data to LatestVehicleInput objects.
            foreach (Vehicle vehicle in _vehiclesContext.Vehicles)
            {
                vehicleInput = _vehiclesContext.VehicleInputs
                    .Where(vd => vd.VehicleId == vehicle.Id)
                    .OrderByDescending(vd => vd.Created)
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
                    latestVehicleInputs.Add(new LatestVehicleInput() { 
                        VehicleId = vehicle.Id,
                        VehicleName = vehicle.Name,
                        Latitude = vehicleInput.Latitude,
                        Longitude = vehicleInput.Longitude,
                        Speed = vehicleInput.Speed
                    });
                }
            }

            return latestVehicleInputs;
        }

        
        public IEnumerable<VehicleInputDTO> GetVehicleInputs(string id)
        {
            // Check if vehicle with given ID exists.
            Vehicle vehicle = _vehiclesContext.Vehicles.Find(id);
            if (vehicle != null)
            {
                /*List<VehicleInputDTO> vehicleInput = new List<VehicleInputDTO>();
                vehicleInput =
                return _vehiclesContext.Data
                    .Where(vd => vd.VehicleId == id);*/
            }
            return null;
        }


        public void AddVehicle(VehicleDTO vehicleDTO)
        {
            // Create and add to the database a new vehicle with a newly generated ID.
            Vehicle vehicle = new Vehicle {
                Name = vehicleDTO.Name
            };
            _vehiclesContext.Vehicles.Add(vehicle);
            _vehiclesContext.SaveChanges();
        }


        public string AddVehicleInput(string id, VehicleInputDTO input)
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
                return vehicle.Name;
            }
            
            return null;
        }
    }
}
