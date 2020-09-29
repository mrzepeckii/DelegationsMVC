using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly Context _context;
        public VehicleRepository(Context context)
        {
            _context = context;
        }

        /*Operations related to the vehicle object      
        * *******************************************/
        public int AddVehicle(Vehicle vehicleToAdd)
        {
            _context.Vehicles.Add(vehicleToAdd);
            _context.SaveChanges();
            return vehicleToAdd.Id;
        }

        public void DeleteVehicle(int vehicleId)
        {
            var vehicleToRemove = _context.Vehicles.Find(vehicleId);
            if(vehicleToRemove != null)
            {
                _context.Vehicles.Remove(vehicleToRemove);
                _context.SaveChanges();
            }
        }

        public IQueryable<Vehicle> GetVehiclesByEmployee(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            IQueryable<Vehicle> vehicles = null;
            if (employee != null)
            {
                vehicles = employee.Vehicles.AsQueryable();
            }
            return vehicles;
        }

        public Vehicle GetVehicleByPlateNumbers(string plateNumbers)
        {
            var vehicle =_context.Vehicles.FirstOrDefault(v => v.PlateNumbers == plateNumbers);
            return vehicle;
        }

        public IQueryable<Vehicle> GetVehiclesByEngineType(int engineTypeId)
        {
            var vehicles = _context.EngineTypes.FirstOrDefault(et => et.Id == engineTypeId).Vehicles.AsQueryable();
            return vehicles;
        }

        public IQueryable<Route> GetRoutesByVehicle(string plateNumbers)
        {
            var vehicle = GetVehicleByPlateNumbers(plateNumbers);
            IQueryable<Route> routes = null;
            if (vehicle != null)
            {
                routes = vehicle.Routes.AsQueryable();
            }
            return routes;
        }

        public IQueryable<EngineType> GetEngineTypes()
        {
            var engineTypes = _context.EngineTypes;
            return engineTypes;
        }
    }
}
