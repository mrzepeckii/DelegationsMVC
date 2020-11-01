using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class NewContactDetailsVm : IMapFrom<ContactDetail>
    {
        public int Id { get; set; }
        public string ContactDetailInformation { get; set; }
        public int ContactDetailTypeId { get; set; }
        public string ContactDetailType { get; set; }
        public int EmployeeId { get; set; }
        public List<ContactDetailTypeVm> ContactDetailTypes { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewContactDetailsVm, ContactDetail>().ReverseMap()
                .ForMember(s => s.ContactDetailType, opt => opt.MapFrom(d => d.ContactDetailType.Name));
        }
    }

    public class NewContactDetailsValidation : AbstractValidator<NewContactDetailsVm>
    {
        public NewContactDetailsValidation()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.EmployeeId).NotNull();
            RuleFor(c => c.ContactDetailTypeId).NotNull();
            RuleFor(c => c.ContactDetailInformation).NotEmpty().WithMessage("Pole kontaktu nie może pozosotać puste")
                .MaximumLength(255).WithMessage("Maksymalna długość wynosi 255 znaków");
        }
    }
}
