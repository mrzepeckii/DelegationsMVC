using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DestinationVm
{
    public class ProjectForListVm : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Client { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectForListVm>()
                .ForMember(s => s.Client, opt => opt.MapFrom(d => d.Destination.Name));
        }
    }
}
