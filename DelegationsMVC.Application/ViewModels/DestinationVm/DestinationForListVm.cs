using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DestinationVm
{
    public class DestinationForListVm : IMapFrom<Destination>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime CooperationSince { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Destination, DestinationForListVm>()
                .ForMember(s => s.Country, opt => opt.MapFrom(d => d.Country.Name))
                .ForMember(s => s.CooperationSince, opt => opt.MapFrom(d => d.CreatedDateTime));
        }
    }
}
