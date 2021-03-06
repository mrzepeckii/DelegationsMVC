﻿using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
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
            IQueryable<Vehicle> vehicles = null;
            var emp = _context.Employees.AsNoTracking().Where(e => e.Id == employeeId);
            if (emp != null)
            {
                vehicles = _context.Vehicles.AsNoTracking().Where(v => v.EmployeeId == employeeId);
            }
            return vehicles;
        }

        public Vehicle GetVehicleByPlateNumbers(string plateNumbers)
        {
            var vehicle =_context.Vehicles.AsNoTracking().FirstOrDefault(v => v.PlateNumbers == plateNumbers);
            return vehicle;
        }

        public IQueryable<Vehicle> GetVehiclesByEngineType(int engineTypeId)
        {
            var vehicles = _context.EngineTypes.AsNoTracking().FirstOrDefault(et => et.Id == engineTypeId).Vehicles.AsQueryable();
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
            var engineTypes = _context.EngineTypes.AsNoTracking();
            return engineTypes;
        }

        public Vehicle GetVehicleById(int id)
        {
            var vehicle = _context.Vehicles.AsNoTracking().FirstOrDefault(v => v.Id == id);
            return vehicle;
        }

        public void UpdateVehicle(Vehicle veh)
        {
            _context.Attach(veh);
            _context.Entry(veh).Property("PlateNumbers").IsModified = true;
            _context.Entry(veh).Property("EngineTypeId").IsModified = true;
            _context.Entry(veh).Property("ModifiedDateTime").IsModified = true;
            _context.SaveChanges();
        }

        public decimal GetMilleageAllowenceByVehicle(int? id)
        {
            var vehicle = _context.Vehicles.AsNoTracking()
                .Include(v => v.EngineType)
                    .ThenInclude(et => et.MileageAllowence)
                .FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return 0;
            }
            var allowence = vehicle.EngineType.MileageAllowence.RatePerKm;
            return allowence;
        }
    }
}
