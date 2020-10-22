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
            RuleFor(e => e.FirstName).NotEmpty().MaximumLength(255);
            RuleFor(e => e.LastName).NotEmpty().MaximumLength(255).NotEqual(e => e.FirstName);
            RuleFor(e => e.BankAccountCode).NotEmpty().Length(26).Custom((e, context) =>
            {
                if (e.Any(c => !char.IsLetter(c)))
                {
                    context.AddFailure("Numer konta powinien zawierać wyłącznie cyfry");
                }
            });
        }
    }
}
