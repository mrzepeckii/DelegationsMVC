using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    
    public class NewEmployeeVm : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeTypeId { get; set; }
        public string BankAccountCode { get; set; }
        public virtual List<NewContactDetailsVm> ContactDetails { get; set; }
        public virtual List<NewVehicleVm> Vehicles { get; set; }
        public List<EmployeeTypeVm> EmployeeTypes { get; set; }
        public List<ContactDetailTypeVm> ContactDetailTypes { get; set; }
        public List<EngineTypeVm> EngineTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewEmployeeVm, Employee>().ReverseMap();
        }
    }

    public class NewEmployeeValidation : AbstractValidator<NewEmployeeVm>
    {
        public NewEmployeeValidation()
        {
            RuleFor(e => e.Id).NotNull();

            RuleFor(e => e.FirstName).NotEmpty().WithMessage("Imie nie może pozostać puste")
                .MaximumLength(255).WithMessage("Maksymalna dlugość imienia to 255");

            RuleFor(e => e.LastName).NotEmpty().WithMessage("Naziwsko nie może pozostać puste")
                .NotEqual(e => e.FirstName).WithMessage("Nazwisko musi się różnić od imienia")
                .MaximumLength(255).WithMessage("Maksymalna dlugość nazwiska to 255");

            RuleFor(e => e.BankAccountCode).NotEmpty().WithMessage("Konto bankowe nie może pozostać puste")
                .Length(26).WithMessage("Długość numeru konta bankowego musi wynosić 26")
                .Matches(@"^[0-9]+$").WithMessage("Konto bankowe może zawierać wyłącznie cyfry");

            RuleForEach(e => e.Vehicles).SetValidator(new NewVehicleValidation());
        }
    }
}
