using SensataApp.DTOs;
using SensataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SensataApp.Services
{
    public interface IVehicleService 
    {
        IEnumerable<Vehicle> GetVehicles();

        IEnumerable<LatestVehicleData> GetLatestVehicleData();
        IEnumerable<VehicleDataDTO> GetVehicleData(string id);
        void AddVehicle(VehicleDTO data);
        string AddVehicleData(string id, VehicleDataDTO data);
    }

    public class VehicleService : IVehicleService
    {
        private VehiclesContext _vehiclesContext;
        public VehicleService(VehiclesContext VehiclesContext)
        {
            _vehiclesContext = VehiclesContext;
        }


        // For testing
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _vehiclesContext.Vehicles;
        }


        public IEnumerable<LatestVehicleData> GetLatestVehicleData()
        {
            List<LatestVehicleData> latestVehicleData = new List<LatestVehicleData>();
            VehicleData vehicleData;

            // Iterate over all vehicles and get their latest data inputs.
            // Map that data to LatestVehicleData objects.
            foreach (Vehicle vehicle in _vehiclesContext.Vehicles)
            {
                vehicleData = _vehiclesContext.Data
                    .Where(vd => vd.VehicleId == vehicle.Id)
                    .OrderByDescending(vd => vd.Created)
                    .FirstOrDefault();

                latestVehicleData.Add(new LatestVehicleData() { 
                    VehicleName = vehicle.Name,
                    Latitude = vehicleData.Latitude,
                    Longitude = vehicleData.Longitude,
                    Speed = vehicleData.Speed
                });
            }

            return latestVehicleData;
        }

        
        public IEnumerable<VehicleDataDTO> GetVehicleData(string id)
        {
            // Check if vehicle with given ID exists.
            Vehicle vehicle = _vehiclesContext.Vehicles.Find(id);
            if (vehicle != null)
            {
                /*List<VehicleDataDTO> vehicleData = new List<VehicleDataDTO>();
                vehicleData =
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


        public string AddVehicleData(string id, VehicleDataDTO data)
        {
            // Check if vehicle with given ID exists.
            Vehicle vehicle = _vehiclesContext.Vehicles.Find(id);
            if (vehicle != null)
            {
                // Map received data to database entity.
                VehicleData vehicleData = new VehicleData
                {
                    VehicleId = id,
                    Latitude = data.Latitude,
                    Longitude = data.Longitude,
                    Speed = data.Speed
                };
                _vehiclesContext.Data.Add(vehicleData);
                _vehiclesContext.SaveChanges();
                return vehicle.Name;
            }

            return null;
        }
    }
}
