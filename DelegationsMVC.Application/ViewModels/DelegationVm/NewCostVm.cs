using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class NewCostVm : IMapFrom<Cost>
    {
        public int CostId { get; set; }

        public decimal Amount { get; set; }
        public int CostTypeId { get; set; }
        public int DelegationId { get; set; }
        public List<CostTypeVm> CostTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewCostVm, Cost>()
                .ForMember(s => s.Id, opt => opt.MapFrom(d => d.CostId))
                .ReverseMap();
        }
    }
    public class NewCostValidation : AbstractValidator<NewCostVm>
    {
        public NewCostValidation()
        {
            RuleFor(c => c.CostId).NotNull();

            RuleFor(c => c.CostTypeId).NotNull();

            RuleFor(c => c.DelegationId).NotNull();

            RuleFor(c => c.Amount).NotEmpty().WithMessage("Koszty nie mogą pozostać puste");
                
        }
    }
}
