using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DestinationVm
{
    public class DestinationDetailVm : IMapFrom<Destination>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ListProjectForList Projects { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Destination, DestinationDetailVm>()
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country.Name))
                .ForMember(d => d.Projects, opt => opt.Ignore());
        }
    }
}
