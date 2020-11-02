using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IDelegationService
    {
        int AddDelegation(NewDelegationVm delVm);
        ListDelegationForListVm GetAllDelegationsForListByStatus(int statusId);
        void DeleteDelegation(Delegation del);
        void UpdateDelegation(NewDelegationVm delVm);
        IQueryable<DestinationTypeVm> GetAllDestinations();
        void DeleteDelegationPermanently(int delId);
        Delegation GetDelegationById(int delId);
        int AddRoute(NewRouteVm model);
        IQueryable<RouteTypeVm> GetRouteTypes();
        IQueryable<TransportTypeVm> GetTransportTypes();
        List<NewRouteVm> CheckRoutes(NewDelegationVm delVm);
        NewRouteVm SetParametersToVm(NewRouteVm routeVm);
        NewDelegationVm SetParametersToVm(NewDelegationVm delVm);
    }
}
