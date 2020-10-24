using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class NewVehicleValidation : AbstractValidator<NewVehicleVm>
    {
        public NewVehicleValidation()
        {
            RuleFor(v => v.Id).NotNull();
            RuleFor(v => v.EmployeeId).NotNull();
            RuleFor(v => v.EngineTypeId).NotNull();
            RuleFor(v => v.PlateNumbers).NotEmpty().WithMessage("Numer rejestracyjny nie może pozostać pusty")
                .MaximumLength(8).WithMessage("Maksymalna długość numeru rejestracyjnego wynosi 8");
        }
    }
}
