using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Domain.Interfaces
{
    public interface IDelegationRepository
    {
        int AddDelegation(Delegation delegationToAdd);
        void DeleteDelegation(int delegationId);
        Delegation GetDelegationById(int delegationId);
        IQueryable<Delegation> GetDelegationsByEmployee(int employeeId);
        IQueryable<Delegation> GetDelegationsNotApprovedByAcc();
        IQueryable<Delegation> GetDelegationsNotApprovedByChief();
        IQueryable<Delegation> GetDelegationsToPaid();
        IQueryable<Delegation> GetDelegationsByStatus(int delegationStatusId);
        void GetDelegationsByDate(DateTime date);
        IQueryable<Route> GetRoutesByDelegation(int delegationId);
        RouteDetail GetRouteDetails(int routeId);
        void GetRoutesByDate(DateTime date);
        IQueryable<RouteType> GetAllRouteTypes();
        IQueryable<TransportType> GetAllTransportTypes();
        IQueryable<CostType> GetAllCostTypes();
        void UpdateDelegation(Delegation del);
        IQueryable<Destination> GetAllDestinations();
        int AddRoute(Route route);
    }
}
