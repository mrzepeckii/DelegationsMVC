using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        int AddVehicle(Vehicle vehicleToAdd);
        void DeleteVehicle(int vehicleId);
        IQueryable<Vehicle> GetVehiclesByEmployee(int employeeId);
        Vehicle GetVehicleByPlateNumbers(string plateNumbers);
        IQueryable<Vehicle> GetVehiclesByEngineType(int engineTypeId);
        IQueryable<Route> GetRoutesByVehicle(string plateNumbers);
        IQueryable<EngineType> GetEngineTypes();
    }
}
