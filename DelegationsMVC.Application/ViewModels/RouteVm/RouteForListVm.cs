using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class RouteForListVm : IMapFrom<Route>
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public int Kilometers { get; set; }
        public string TypeOfTransport { get; set; }
        public string RouteType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MileageAllowence { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Route, RouteForListVm>()
                .ForMember(s => s.Id, opt => opt.MapFrom(d => d.RouteDetail.RouteRef))
                .ForMember(s => s.StartPoint, opt => opt.MapFrom(d => d.RouteDetail.StartPoint))
                .ForMember(s => s.EndPoint, opt => opt.MapFrom(d => d.RouteDetail.EndPoint))
                .ForMember(s => s.Kilometers, opt => opt.MapFrom(d => d.RouteDetail.Kilometers))
                .ForMember(s => s.StartDate, opt => opt.MapFrom(d => d.RouteDetail.StartDate))
                .ForMember(s => s.EndDate, opt => opt.MapFrom(d => d.RouteDetail.EndDate))
                .ForMember(s => s.TypeOfTransport, opt => opt.MapFrom(d => d.TypeOfTransport.Name))
                .ForMember(s => s.RouteType, opt => opt.MapFrom(d => d.RouteType.Name));
        }
    }
}
