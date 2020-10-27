using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
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
           // var listOfRoutes = _context.Routes.Where(r => r.Delegation == delegationToRemove);
            if(delegationToRemove != null)
            {
              //  _context.Costs.RemoveRange(delegationToRemove.Costs);
              //  _context.Routes.RemoveRange(listOfRoutes);
                _context.Delegations.Remove(delegationToRemove); 
            }
            _context.SaveChanges();
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
            _context.SaveChanges();
        }

        public Delegation GetDelegationById(int delegationId)
        {
            var delegation = _context.Delegations.FirstOrDefault(d => d.Id == delegationId);
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
            var delegations = _context.DelegationStatuses.FirstOrDefault(ds => ds.Id == delegationStatusId).Delegations.AsQueryable();
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
    }
}
