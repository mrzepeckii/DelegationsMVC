using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class NewRouteVm : IMapFrom<Route>
    {
        public int Id { get; set; }
        public int DelegationId { get; set; }
        public int TypeOfTransportId { get; set; }
        public int RouteTypeId { get; set; }
        public NewRouteDetailVm RouteDetail { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRouteVm, Route>().ReverseMap();

        }
    }
}
