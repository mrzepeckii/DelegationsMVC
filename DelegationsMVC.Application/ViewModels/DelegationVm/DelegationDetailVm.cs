using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Application.ViewModels.RouteVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class DelegationDetailVm : IMapFrom<Delegation>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DelegationNo { get; set; }
        public string Purpose { get; set; }
        public string EmployeeName { get; set; }
        public string Destination { get; set; }
        public string DelegationStatus { get; set; }
        public int DaysInDelegation { get; set; }
        public decimal SubsistenceAllowence { get; set; }
        public decimal MileageAllowence { get; set; }
        public decimal SummaryAllowence { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? AccoutantApprovedDate { get; set; }
        public DateTime? ChiefApprovedDate { get; set; }
        public DateTime? PaidDateDate { get; set; }
        public virtual List<RouteForListVm> Routes { get; set; }
        public virtual List<CostForListVm> Costs { get; set; }
        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delegation, DelegationDetailVm>()
                .ForMember(s => s.DelegationNo, opt => opt.MapFrom(d => d.Employee.FirstName.Substring(0, 1).ToUpper() + d.Employee.LastName.Substring(0, 1).ToUpper()
                    + d.Routes.OrderBy(r => r.RouteDetail.StartDate).First().RouteDetail.StartDate.Date.ToString("ddMMyyyy")))
                .ForMember(s => s.EmployeeName, opt => opt.MapFrom(d => d.Employee.FirstName + " " + d.Employee.LastName))
                .ForMember(s => s.Destination, opt => opt.MapFrom(d => d.Destination.Name))
                .ForMember(s => s.DelegationStatus, opt => opt.MapFrom(d => d.DelegationStatus.Name))
                .ForMember(s => s.UserId, opt => opt.MapFrom(d => d.Employee.UserId));
        }
    }
}
