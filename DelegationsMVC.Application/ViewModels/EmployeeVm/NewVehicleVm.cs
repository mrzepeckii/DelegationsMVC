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
        public int Id { get; set; }
        public string PlateNumbers { get; set; }
        public int EngineTypeId { get; set; }
        public string EngineType { get; set; }
        public int EmployeeId { get; set; }
        public List<EngineTypeVm> EngineTypes { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewVehicleVm, Vehicle>().ReverseMap()
                .ForMember(s => s.EngineType, opt => opt.MapFrom(d => d.EngineType.Name));
        }
    }
}
