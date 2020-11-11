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
        public DateTime? AccoutantApprovedDate { get; set; }
        public DateTime? ChiefApprovedDate { get; set; }
        public string Destination { get; set; }
        public string DelegationStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delegation, DelegationForListVm>()
                .ForMember(s => s.DelegationNo, opt => opt.MapFrom(d => d.Employee.FirstName.Substring(0,1).ToUpper() + d.Employee.LastName.Substring(0,1).ToUpper()
                    + d.Routes.OrderBy(r => r.RouteDetail.StartDate).First().RouteDetail.StartDate.Date.ToString("ddMMyyyy")))
                .ForMember(s => s.StartDate, opt => opt.MapFrom(d => d.Routes.OrderBy(r => r.RouteDetail.StartDate).First().RouteDetail.StartDate))
                .ForMember(s => s.EndDate, opt => opt.MapFrom(d => d.Routes.OrderByDescending(r => r.RouteDetail.EndDate).First().RouteDetail.EndDate))
                .ForMember(s => s.Destination, opt => opt.MapFrom(d => d.Destination.Name))
                .ForMember(s => s.DelegationStatus, opt => opt.MapFrom(d => d.DelegationStatus.Name));
        }
    }
}