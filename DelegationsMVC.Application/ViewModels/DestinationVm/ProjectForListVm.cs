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
        public string Status { get; set; }
        public int ProjectStatusId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectForListVm>()
                .ForMember(s => s.Client, opt => opt.MapFrom(d => d.Destination.Name))
                .ForMember(s => s.Status, opt => opt.MapFrom(d => d.ProjectStatus.Name));
        }
    }
}
