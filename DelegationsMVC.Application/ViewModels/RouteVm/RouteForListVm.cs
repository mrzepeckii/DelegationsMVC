using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class RouteForListVm : IMapFrom<RouteDetail>
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public int Kilometers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RouteDetail, RouteForListVm>()
                .ForMember(s => s.Id, opt => opt.MapFrom(d => d.RouteRef));
        }
    }
}
