using SensataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SensataApp.Core
{
    public interface IVehicleService 
    {
        IEnumerable<Vehicle> GetLatestVehiclesData();
        Vehicle GetVehicle(Guid id);
        void AddVehicle(VehicleData data);
        void AddVehicleData(Guid id, VehicleData data);
        public void DeleteVehicle(Vehicle vehicle);
    }

    public class VehicleService : IVehicleService
    {
        private VehiclesContext _vehiclesContext;
        public VehicleService(VehiclesContext VehiclesContext)
        {
            _vehiclesContext = VehiclesContext;
        }

        private List<Vehicle> vehicles = new List<Vehicle>()
        {
            new Vehicle()
            {
                Id = Guid.NewGuid(),
                Data = new List<VehicleData>()
                    {
                        new VehicleData()
                        {
                            Latitude = 54.123456,
                            Longitude = 21.654321,
                            Speed = 55
                        },
                        new VehicleData()
                        {
                            Latitude = 55.456789,
                            Longitude = 20.987654,
                            Speed = 106
                        }
                    }
            },
            new Vehicle()
            {
                Id = Guid.NewGuid(),
                Data = new List<VehicleData>()
                    {
                        new VehicleData()
                        {
                            Latitude = 46.741852,
                            Longitude = 32.963852,
                            Speed = 20
                        },
                        new VehicleData()
                        {
                            Latitude = 47.258147,
                            Longitude = 33.258369,
                            Speed = 850
                        }
                    }
            }
        };

        public IEnumerable<Vehicle> GetLatestVehiclesData()
        {
            return _vehiclesContext.Vehicles;
        }

        public Vehicle GetVehicle(Guid id)
        {
            return vehicles.Find(vehicle => vehicle.Id == id);
        }






        public void AddVehicle(VehicleData data)
        {
            // Create and add to the database a new vehicle with a newly generated ID.
            Vehicle vehicle = new Vehicle() { Id = Guid.NewGuid() };
            _vehiclesContext.Vehicles.Add(vehicle);

            // Check if the new vehicle has any default starting data and add that too.
            if (data != null)
            {
                data.VehicleId = vehicle.Id;
                _vehiclesContext.Add(data);
            }

            _vehiclesContext.SaveChanges();
        }







        public void AddVehicleData(Guid id, VehicleData data)
        {
            // Add data to vehicle with the specified ID.
            var index = vehicles.FindIndex(vehicle => vehicle.Id == id);
            vehicles[index].Data.Add(data);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
        }
    }
}
