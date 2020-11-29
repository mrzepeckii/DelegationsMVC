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
        void CancelDelegation(int delId);
        void UpdateDelegation(NewDelegationVm delVm);
        IQueryable<DestinationTypeVm> GetAllDestinations();
        void DeleteDelegationPermanently(int delId);
        Delegation GetDelegationById(int delId);
        int AddRoute(NewRouteVm model);
        IQueryable<RouteTypeVm> GetRouteTypes();
        ListDelegationForListVm GetAllDelegationsForList();
        IQueryable<TransportTypeVm> GetTransportTypes();
        List<NewRouteVm> CheckRoutes(NewDelegationVm delVm);
        NewRouteVm SetParametersToVm(NewRouteVm routeVm);
        NewDelegationVm SetParametersToVm(NewDelegationVm delVm);
        NewDelegationVm GetDelegationForEdit(int id);
        void DeleteRoute(int idRoute);
        NewRouteVm GetRouteForEdit(int id);
        void UpdateRoute(NewRouteVm routeVm);
        DelegationDetailVm GetDelegationDetails(int id);
        bool ChangeStatusOfDelegation(int delId, int delStatus);
        Route GetRouteById(int id);
        ListDelegationForListVm GetDelegationsByEmployee(int id);
    }
}
