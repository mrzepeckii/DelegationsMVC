using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class DelegationRepository : IDelegationRepository
    {
        private readonly Context _context;

        public DelegationRepository(Context context)
        {
            _context = context;
        }

        /*Operations related to the delegation object
        * *******************************************/
        public int AddDelegation(Delegation delegationToAdd)
        {
            _context.Delegations.Add(delegationToAdd);
            _context.SaveChanges();
            return delegationToAdd.Id;
        }
        
        public void DeleteDelegation(int delegationId)
        {
            var delegationToRemove = _context.Delegations.Find(delegationId);
            if(delegationToRemove != null)
            {
                _context.Delegations.Remove(delegationToRemove);
                _context.SaveChanges();
            } 
        }

        public IQueryable<Delegation> GetAllDelegations()
        {
            var delegations = _context.Delegations;
            return delegations;
        }

        public void UpdateDelegation(Delegation del)
        {
            _context.Attach(del);
            _context.Entry(del).Property("Purpose").IsModified = true;
            _context.Entry(del).Property("DestinationId").IsModified = true;
            _context.Entry(del).Reference("DelegationStatus").IsModified = true;
            _context.Entry(del).Property("AccoutantApprovedDate").IsModified = true;
            _context.Entry(del).Property("ChiefApprovedDate").IsModified = true;
            _context.Entry(del).Property("PaidDateDate").IsModified = true;
            _context.Entry(del).Collection("Routes").IsModified = true;
            _context.Entry(del).Collection("Costs").IsModified = true;
            _context.Entry(del).Property("ModifiedDateTime").IsModified = true;
            foreach (var item in del.Costs)
            {
                _context.Entry(item).Property("Amount").IsModified = true;
            }
            _context.SaveChanges();
        }

        public Delegation GetDelegationById(int delegationId)
        {
            var delegation = _context.Delegations
                .Include(d => d.Employee)
                .Include(d => d.Destination)
                .Include(d => d.DelegationStatus)
                .Include(d => d.Costs)
                    .ThenInclude(d => d.CostType)
                .Include(d => d.Routes)
                    .ThenInclude(r => r.RouteDetail)
                .Include(d => d.Routes)
                    .ThenInclude(r => r.RouteType)
                .Include(d => d.Routes)
                    .ThenInclude(r => r.TypeOfTransport)
                .FirstOrDefault(d => d.Id == delegationId);
            return delegation;
        }

        public IQueryable<Delegation> GetDelegationsByEmployee(int employeeId)
        {
            var delegations = _context.Delegations.Where(d => d.EmployeeId == employeeId);
            return delegations;
        }

        public IQueryable<Delegation> GetDelegationsNotApprovedByAcc()
        {
            var delegations = _context.Delegations.Where(d => d.AccoutantApprovedDate == null);
            return delegations;
        }

        public IQueryable<Delegation> GetDelegationsNotApprovedByChief()
        {
            var delegations = _context.Delegations.Where(d => d.ChiefApprovedDate == null);
            return delegations;
        }

        public IQueryable<Delegation> GetDelegationsToPaid()
        {
            var delegations = _context.Delegations.Where(d => d.PaidDateDate == null);
            return delegations;
        }

        public IQueryable<Delegation> GetDelegationsByStatus(int delegationStatusId)
        {
            var delegations = _context.Delegations
                .Include(d => d.Routes)
                    .ThenInclude(r => r.RouteDetail)
                .Where(d => d.DelegationStatusId == delegationStatusId);  
            return delegations;
        }
        
        public void GetDelegationsByDate(DateTime date)
        {
            //to implement
        }

        /*Operations related to the route object      
         * route can't exist without delegation object 
         * *******************************************/
        public IQueryable<Route> GetRoutesByDelegation(int delegationId)
        {
            var delegation = GetDelegationById(delegationId);
            IQueryable<Route> routes = null;
            if (delegation != null)
            {
                routes = delegation.Routes.AsQueryable();
            }
            return routes;
        }
        
        public RouteDetail GetRouteDetails(int routeId)
        {
            var route = _context.Routes.FirstOrDefault(r => r.Id == routeId);
            var routeDetail = _context.RouteDetails.FirstOrDefault(rd => rd.Route == route);
            return routeDetail;
        }

        public void GetRoutesByDate(DateTime date)
        {
           //to implement 
        }

        public int AddRoute(Route route)
        {
            _context.Routes.Add(route);
            _context.SaveChanges();
            return route.Id;
        }

        public void DeleteRoute(int idRoute)
        {
            var route = _context.Routes.FirstOrDefault(r => r.Id == idRoute);
            if (route != null)
            {
                _context.Routes.Remove(route);
                _context.SaveChanges();
            }
        }

        public Route GetRouteById(int id)
        {
            var route = _context.Routes
                .Include(r => r.RouteDetail)
                .FirstOrDefault(r => r.Id == id);
            return route;
        }

        public void UpdateRoute(Route route)
        {
            _context.Attach(route);
            _context.Entry(route).Property("TypeOfTransportId").IsModified = true;
            _context.Entry(route).Property("RouteTypeId").IsModified = true;
            _context.Entry(route.RouteDetail).Property("StartPoint").IsModified = true;
            _context.Entry(route.RouteDetail).Property("EndPoint").IsModified = true;
            _context.Entry(route.RouteDetail).Property("StartDate").IsModified = true;
            _context.Entry(route.RouteDetail).Property("EndDate").IsModified = true;
            _context.Entry(route.RouteDetail).Property("Kilometers").IsModified = true;
            _context.Entry(route.RouteDetail).Property("VehicleId").IsModified = true;
            _context.Entry(route).Property("ModifiedDateTime").IsModified = true;
            _context.SaveChanges();
        }

        /*Operations related to getting all list of objects    
        * to chose the type in the form
        * *******************************************/
        public IQueryable<RouteType> GetAllRouteTypes()
        {
            var routeTypes = _context.RouteTypes;
            return routeTypes;
        }
        
        public IQueryable<TransportType> GetAllTransportTypes()
        {
            var transportTypes = _context.TransportTypes;
            return transportTypes;
        }

        public IQueryable<CostType> GetAllCostTypes()
        {
            var costTypes = _context.CostTypes;
            return costTypes;
        }

        public IQueryable<Destination> GetAllDestinations()
        {
            var dest = _context.Destinations;
            return dest;
        }

        public decimal GetSubsistanceAllowenceByDel(int delId)
        {
            var del = _context.Delegations
                .Include(d => d.Destination)
                    .ThenInclude(d => d.Country)
                        .ThenInclude(c => c.SubsistanceAllowence)
                .FirstOrDefault(d => d.Id == delId);
            if(del == null)
            {
                return 0;
            }
            var allowence = del.Destination.Country.SubsistanceAllowence.RatePerDay;
            return allowence;
        }
    }
}
