using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DestinationVm
{
    public class NewDestinationVm : IMapFrom<Destination>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public List<CountryVm> Countries {get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Destination, NewDestinationVm>().ReverseMap();
        }
    }

    public class NewDestinationValidation : AbstractValidator<NewDestinationVm>
    {
        public NewDestinationValidation()
        {
            RuleFor(d => d.Id).NotNull();
            RuleFor(d => d.Name).NotEmpty().WithMessage("Nazwa nie może pozostać pusta")
                .MaximumLength(255).WithMessage("Maksymalna dlugosc to 255 znaków");
            RuleFor(d => d.CountryId).NotNull().WithMessage("Wybierz kraj");
        }
    }
}
