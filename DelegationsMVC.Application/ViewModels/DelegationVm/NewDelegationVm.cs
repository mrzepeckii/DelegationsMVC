using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class NewDelegationVm : IMapFrom<Delegation>
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public int DestinationId { get; set; }
        public int EmployeeId { get; set; }
        public int DelegationStatusId { get; set; }
        public virtual List<NewRouteVm> Routes { get; set; }
        public virtual List<NewCostVm> Costs { get; set; }
        public List<DestinationTypeVm> Destinations { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewDelegationVm, Delegation>().ReverseMap();
        }
    }
}
