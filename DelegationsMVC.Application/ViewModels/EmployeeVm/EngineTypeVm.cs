﻿using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class EngineTypeVm : IMapFrom<EngineType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EngineType, EngineTypeVm>();
        }
    }
}
