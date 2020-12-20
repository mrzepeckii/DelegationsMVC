﻿using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DestinationVm
{
    public class NewProjectVm : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int ProjectStatusId { get; set; }
        public int DestinationId { get; set; }
        public List<ProjectStatusVm> Statuses { get; set; }
        public List<DestinationTypeVm> Destinations { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, NewProjectVm>().ReverseMap();
        }
    }
}
