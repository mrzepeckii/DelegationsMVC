using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Services
{
    public class DelegationService : IDelegationService
    {
        private readonly IDelegationRepository _delegationRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IVehicleRepository _vehRepo;
        private readonly IMapper _mapper;

        public DelegationService(IDelegationRepository delegRepo, IEmployeeRepository empRepo, IVehicleRepository vehRepo, IMapper mapper)
        {
            _delegationRepo = delegRepo;
            _employeeRepo = empRepo;
            _vehRepo = vehRepo;
            _mapper = mapper;
        }

        public int AddDelegation(NewDelegationVm delVm)
        {
            var deleg = _mapper.Map<Delegation>(delVm);
            deleg.EmployeeId = 10;
            deleg.CreateById = 1;
            deleg.CreatedDateTime = DateTime.Now;
            deleg.DelegationStatusId = 1;
            var id =_delegationRepo.AddDelegation(deleg);
            return id;
        }

        public ListDelegationForListVm GetAllDelegationsForListByStatus(int statusId)
        {
            var delegations = _delegationRepo.GetDelegationsByStatus(statusId).ProjectTo<DelegationForListVm>(_mapper.ConfigurationProvider).ToList();
            var delegationsVm = new ListDelegationForListVm()
            {
                Delegations = delegations,
                Count = delegations.Count
            };
            return delegationsVm;
        }

        public void DeleteDelegation(Delegation del)
        {
            del.DelegationStatusId = 6;
            _delegationRepo.UpdateDelegation(del);
        }

        public void DeleteDelegationPermanently(int delId)
        {
            _delegationRepo.DeleteDelegation(delId);
        }

        public void UpdateDelegation(NewDelegationVm delVm)
        {
            var del = _mapper.Map<Delegation>(delVm);
            _delegationRepo.UpdateDelegation(del);
        }

        public Delegation GetDelegationById(int delId)
        {
           var del = _delegationRepo.GetDelegationById(delId);
            return del;
        }

        public IQueryable<DestinationTypeVm> GetAllDestinations()
        {
            var destinations = _delegationRepo.GetAllDestinations().ProjectTo<DestinationTypeVm>(_mapper.ConfigurationProvider);
            return destinations;
        }

        public int AddRoute(NewRouteVm model)
        {
            var route = _mapper.Map<Route>(model);
            var id = _delegationRepo.AddRoute(route);
            return id;
        }

        public IQueryable<RouteTypeVm> GetRouteTypes()
        {
            var types = _delegationRepo.GetAllRouteTypes().ProjectTo<RouteTypeVm>(_mapper.ConfigurationProvider);
            return types;
        }

        public IQueryable<TransportTypeVm> GetTransportTypes()
        {
            var types = _delegationRepo.GetAllTransportTypes().ProjectTo<TransportTypeVm>(_mapper.ConfigurationProvider);
            return types;
        }

        public List<NewRouteVm> CheckRoutes(NewDelegationVm delVm)
        {
            if (delVm.IsAdditionalRoute)
            {
                return delVm.Routes;
            }
            var routes = delVm.Routes;
            routes.RemoveAt(2);
            return routes;
        }

        public NewRouteVm SetParametersToVm(NewRouteVm routeVm)
        {
            var del = GetDelegationById(routeVm.DelegationId);
            routeVm.TransportTypes = GetTransportTypes().ToList();
            routeVm.RouteTypes = GetRouteTypes().ToList();
            routeVm.RouteDetail.Vehicles = _vehRepo.GetVehiclesByEmployee(del.EmployeeId)
                .ProjectTo<VehicleForListVm>(_mapper.ConfigurationProvider).ToList();
            return routeVm;
        }

        public NewDelegationVm SetParametersToVm(NewDelegationVm delVm)
        {
            delVm.Destinations = GetAllDestinations().ToList();
            delVm.RouteTypes = GetRouteTypes().ToList();
            delVm.TransportTypes = GetTransportTypes().ToList();
            delVm.Vehicles = _vehRepo.GetVehiclesByEmployee(delVm.EmployeeId)
                .ProjectTo<VehicleForListVm>(_mapper.ConfigurationProvider).ToList();
            if(delVm.Id != 0)
            {
                delVm.Routes = _delegationRepo.GetRoutesByDelegation(delVm.Id)
                .ProjectTo<NewRouteVm>(_mapper.ConfigurationProvider).ToList();
            }
            return delVm;
        }

        public NewDelegationVm GetDelegationForEdit(int id)
        {
            var del = _delegationRepo.GetDelegationById(id);
            var delVm = _mapper.Map<NewDelegationVm>(del);
            return delVm;
        }

        public void DeleteRoute(int idRoute)
        {
            _delegationRepo.DeleteRoute(idRoute);
        }

        public NewRouteVm GetRouteForEdit(int id)
        {
            var route = _delegationRepo.GetRouteById(id);
            var routeVm = _mapper.Map<NewRouteVm>(route);
            return routeVm;
        }

        public void UpdateRoute(NewRouteVm routeVm)
        {
            var route = _mapper.Map<Route>(routeVm);
            _delegationRepo.UpdateRoute(route);
        }

        public DelegationDetailVm GetDelegationDetails(int id)
        {
            var del = _delegationRepo.GetDelegationById(id);
            var model = _mapper.Map<DelegationDetailVm>(del);
            return model;
        }
    }
}
