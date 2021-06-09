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
        List<LatestVehicleData> GetLatestVehicleData();
        Vehicle GetVehicle(string id);
        void AddVehicle(VehicleDTO data);
        void AddVehicleData(string id, VehicleData data);
        void DeleteVehicle(Vehicle vehicle);
    }

    public class VehicleService : IVehicleService
    {
        private VehiclesContext _vehiclesContext;
        public VehicleService(VehiclesContext VehiclesContext)
        {
            _vehiclesContext = VehiclesContext;
        }
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _vehiclesContext.Vehicles;
        }

        public List<LatestVehicleData> GetLatestVehicleData()
        {
            //List<VehicleData> vehicleData = new List<VehicleData>();
            List<LatestVehicleData> latestVehicleData = new List<LatestVehicleData>();
            VehicleData vehicleData;

            // Iterate over all vehicles and get their latest data inputs.
            // Map that required data to LatestVehicleData objects.
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


        public Vehicle GetVehicle(string id)
        {
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


        public void AddVehicleData(string id, VehicleData data)
        {
            // Add data to vehicle with the specified ID.
            
        }


        public void DeleteVehicle(Vehicle vehicle)
        {
            
        }
    }
}
