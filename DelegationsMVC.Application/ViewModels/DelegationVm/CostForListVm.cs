using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class CostForListVm : IMapFrom<Cost>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string CostType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cost, CostForListVm>()
                .ForMember(s => s.CostType, opt => opt.MapFrom(d => d.CostType.Name));
        }
    }
}
