using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class NewVehicleVm : IMapFrom<Vehicle>
    {
        public string PlateNumbers { get; set; }
        public int EngineTypeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewVehicleVm, Vehicle>();
        }
    }
}
