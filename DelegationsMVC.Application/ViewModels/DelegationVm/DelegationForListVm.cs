using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Linq;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class DelegationForListVm : IMapFrom<Delegation>
    {
        public int Id { get; set; }
        public string DelegationNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; }
        public string DelegationStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delegation, DelegationForListVm>()
                .ForMember(s => s.DelegationNo, opt => opt.MapFrom(d => d.Employee.FirstName + d.Employee.LastName + d.CreatedDateTime))
                .ForMember(s => s.StartDate, opt => opt.MapFrom(d => d.Routes.OrderBy(r => r.RouteDetail.StartDate).First().RouteDetail.StartDate))
                .ForMember(s => s.EndDate, opt => opt.MapFrom(d => d.Routes.OrderByDescending(r => r.RouteDetail.EndDate).First().RouteDetail.EndDate))
                .ForMember(s => s.Destination, opt => opt.MapFrom(d => d.Destination.Name))
                .ForMember(s => s.DelegationStatus, opt => opt.MapFrom(d => d.DelegationStatus.Name));
        }
    }
}