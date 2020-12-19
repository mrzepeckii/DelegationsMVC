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

        public void CancelDelegation(int delId)
        {
            var del = _delegationRepo.GetDelegationById(delId);
            if (del == null || del.DelegationStatusId != 1)
            {
                return;
            }
            del.DelegationStatusId = 6;
            _delegationRepo.UpdateDelegation(del);
        }

        public void DeleteDelegationPermanently(int delId)
        {
            _delegationRepo.DeleteDelegation(delId);
        }

        public void UpdateDelegation(NewDelegationVm delVm)
        {
            if (delVm.DelegationStatusId != 1)
            {
                return;
            }
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
            if (!IsDelegationEditable(model.DelegationId))
            {
                return 0;
            }
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
            var route = _delegationRepo.GetRouteById(idRoute);
            if (!IsDelegationEditable(route.DelegationId))
            {
                return;
            }
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
            if (IsDelegationEditable(routeVm.DelegationId))
            {
                return;
            }
            var route = _mapper.Map<Route>(routeVm);
            _delegationRepo.UpdateRoute(route);
        }

        public DelegationDetailVm GetDelegationDetails(int id)
        {
            var del = _delegationRepo.GetDelegationById(id);
            var model = _mapper.Map<DelegationDetailVm>(del);
            CalculateAndSetAllowences(model);
            return model;
        }

        public bool ChangeStatusOfDelegation(int delId, int delStatus)
        {
            var del = _delegationRepo.GetDelegationById(delId);
            if (del == null || del.DelegationStatusId == delStatus)
            {
                return false;
            }
            del.DelegationStatusId = delStatus;
            if (delStatus == 2)
            {
                del.ChiefApprovedDate = DateTime.Now;
            }
            else if (delStatus == 3)
            {
                del.AccoutantApprovedDate = DateTime.Now;
            }
            else
            {
                if (del.AccoutantApprovedDate == null)
                {
                    return false;
                }
                del.PaidDateDate = DateTime.Now;
            }
            _delegationRepo.UpdateDelegation(del);
            return true;
        }

        public ListDelegationForListVm GetAllDelegationsForList()
        {
            var delegations = _delegationRepo.GetAllDelegations().ProjectTo<DelegationForListVm>(_mapper.ConfigurationProvider).ToList();
            var delegationsVm = new ListDelegationForListVm()
            {
                Delegations = delegations,
                Count = delegations.Count
            };
            return delegationsVm;
        }

        public ListDelegationForListVm GetDelegationsByEmployee(int id)
        {
            var delegations = _delegationRepo.GetDelegationsByEmployee(id).ProjectTo<DelegationForListVm>(_mapper.ConfigurationProvider).ToList();
            var delegationsVm = new ListDelegationForListVm()
            {
                Delegations = delegations,
                Count = delegations.Count
            };
            return delegationsVm;
        }

        public Route GetRouteById(int id)
        {
            var route = _delegationRepo.GetRouteById(id);
            return route;
        }
        private void CalculateAndSetAllowences(DelegationDetailVm delVm)
        {
            CalculateMileageAllowence(delVm.Routes);
            CalculateSubsistenceAllowence(delVm);
            delVm.MileageAllowence = delVm.Routes.Sum(r => r.MileageAllowence);
            delVm.SummaryAllowence = delVm.MileageAllowence + delVm.SubsistenceAllowence
                + delVm.Costs.Sum(c => c.Amount);
        }

        private void CalculateMileageAllowence(List<RouteForListVm> routes)
        {
            if (routes == null)
            {
                return;
            }
            routes.ForEach(r => r.MileageAllowence = CalculateMileageAllowence(r.Id));
        }

        private decimal CalculateMileageAllowence(int routeId)
        {
            var route = _delegationRepo.GetRouteById(routeId);
            if(route.TypeOfTransportId != 1)
            {
                return 0;
            }
            var allowence = _vehRepo.GetMilleageAllowenceByVehicle(route.RouteDetail.VehicleId);
            var mileageAllowenceForRoute = route.RouteDetail.Kilometers * allowence;
            return mileageAllowenceForRoute;
        }

        private double CalculateDaysInDelegation(DelegationDetailVm delVm)
        {
            double days;
            //calucalte how many days or hours employee was in delegation
            TimeSpan timeInDelegation = delVm.Routes.OrderByDescending(r => r.EndDate).First().EndDate
                - delVm.Routes.OrderBy(r => r.StartDate).First().StartDate;

            days = timeInDelegation.TotalDays;
            //if deicmal part is bigger or equal than 0,33 (8h) and lower than 0.5 (12h)
            if (days - Math.Truncate(days) >= 0.33 && days - Math.Truncate(days) < 0.5)
            {
                //get int of day and add half of day
                days = timeInDelegation.Days + 0.5;
            }
            else if (days - Math.Truncate(days) >= 0.5)
            {
                //get int of day and add day
                days = timeInDelegation.Days + 1;
            }
            else
            {
                days = timeInDelegation.Days;
            }
            delVm.DaysInDelegation = (Int32)days;
            return days;
        }

        private void CalculateSubsistenceAllowence(DelegationDetailVm delVm)
        {
            var del = _delegationRepo.GetDelegationById(delVm.Id);
            if(del == null)
            {
                return;
            }
            var allowence = _delegationRepo.GetSubsistanceAllowenceByDel(delVm.Id);
            delVm.SubsistenceAllowence = (decimal)CalculateDaysInDelegation(delVm) * allowence;
        }

       private bool IsDelegationEditable(int id)
        {
            var del = _delegationRepo.GetDelegationById(id);
            return del.DelegationStatusId == 1;
        }
    }
}
