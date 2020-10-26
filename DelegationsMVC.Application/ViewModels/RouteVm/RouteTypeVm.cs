using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class RouteTypeVm : IMapFrom<RouteType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RouteType, RouteTypeVm>();
        }
    }
}
