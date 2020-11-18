using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Application.ViewModels.RouteVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class DelegationDetailVm : IMapFrom<Delegation>
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public string EmployeeName { get; set; }
        public string Destination { get; set; }
        public string DelegationStatus { get; set; }
        public DateTime? AccoutantApprovedDate { get; set; }
        public DateTime? ChiefApprovedDate { get; set; }
        public DateTime? PaidDateDate { get; set; }
        public virtual List<RouteForListVm> Routes { get; set; }
        public virtual List<CostForListVm> Costs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delegation, DelegationDetailVm>()
                .ForMember(s => s.EmployeeName, opt => opt.MapFrom(d => d.Employee.FirstName + " " + d.Employee.LastName))
                .ForMember(s => s.Destination, opt => opt.MapFrom(d => d.Destination.Name))
                .ForMember(s => s.DelegationStatus, opt => opt.MapFrom(d => d.DelegationStatus.Name))
        }
    }
}
