using SensataApp.Db.Data;
using SensataApp.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensataApp.Core
{
    public interface IVehicleService 
    {
        IEnumerable<Vehicle> GetLatestVehiclesData();
        Vehicle GetVehicle(Guid id);
        void AddVehicle(List<VehicleData> vehicleData);
        void AddVehicleData(Guid id, VehicleData data);
        public void DeleteVehicle(Vehicle vehicle);
    }

    public class VehicleService : IVehicleService
    {
        private VehicleContext _vehicleContext;
        public VehicleService(VehicleContext vehicleContext)
        {
            _vehicleContext = vehicleContext;
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
            return _vehicleContext.Vehicles.ToList();
        }

        public Vehicle GetVehicle(Guid id)
        {
            return vehicles.Find(vehicle => vehicle.Id == id);
        }

        public void AddVehicle(List<VehicleData> data)
        {
            // Create a new vehicle with a newly generated ID.
            var vehicle = new Vehicle() { Id = Guid.NewGuid(), Data = data };
            vehicles.Add(vehicle);
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
