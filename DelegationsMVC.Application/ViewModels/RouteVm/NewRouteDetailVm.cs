using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class NewRouteDetailVm : IMapFrom<RouteDetail>
    {
       // public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Kilometers { get; set; }
        public int RouteRef { get; set; }
        public int? VehicleId { get; set; }
        public List<VehicleForListVm> Vehicles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRouteDetailVm, RouteDetail>().ReverseMap();
        }
    }
}
