using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class NewRouteDetailVm : IMapFrom<RouteDetail>
    {
       // public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Kilometers { get; set; }
        public int RouteRef { get; set; }
        public int? VehicleId { get; set; }
        public List<VehicleForListVm> Vehicles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRouteDetailVm, RouteDetail>().ReverseMap();
        }
    }
    public class NewRouteDetailValidation : AbstractValidator<NewRouteDetailVm>
    {
        public NewRouteDetailValidation()
        {
            RuleFor(r => r.RouteRef).NotNull();

            RuleFor(r => r.StartPoint).NotEmpty().WithMessage("Miejsce wyjazdu nie może pozostać puste")
                .MaximumLength(255).WithMessage("Maksymalna ilość znaków to 255");

            RuleFor(r => r.EndPoint).NotEmpty().WithMessage("Miejsce docelowe nie może pozostać puste")
                .MaximumLength(255).WithMessage("Maksymalna ilość znaków to 255")
                .NotEqual(r => r.StartPoint).WithMessage("Miejsce docelowe nie może być takie samo jak miejsce startowe");

            RuleFor(r => r.StartDate).NotEmpty().WithMessage("Data wyjazdu nie może pozostać pusta")
                .LessThan(r => r.EndDate).WithMessage("Data wyjazdu musi być wcześniej niż data przyjazdu");

            RuleFor(r => r.EndDate).NotEmpty().WithMessage("Data przyjazdu nie może pozostać pusta")
                .GreaterThan(r => r.StartDate).WithMessage("Data przyjazdu musi być poźniej niż data wyjazdu");

            RuleFor(r => r.Kilometers).NotEmpty().WithMessage("Ilość kilometrów nie może pozostać pusta");
        }
    }
}
