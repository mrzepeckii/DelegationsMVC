using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.RouteVm
{
    public class NewRouteVm : IMapFrom<Route>
    {
        public int Id { get; set; }
        public int DelegationId { get; set; }
        public int TypeOfTransportId { get; set; }
        public int RouteTypeId { get; set; }
        public NewRouteDetailVm RouteDetail { get; set; }
        public List<TransportTypeVm> TransportTypes { get; set; }
        public List<RouteTypeVm> RouteTypes { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewRouteVm, Route>().ReverseMap();
        }

        public class NewRouteValidation : AbstractValidator<NewRouteVm>
        {
            public NewRouteValidation()
            {
                RuleFor(r => r.Id).NotNull();

                RuleFor(r => r.DelegationId).NotNull();

                RuleFor(r => r.TypeOfTransportId).NotNull();

                RuleFor(r => r.RouteTypeId).NotNull();

                RuleForEach(r => r.RouteDetail).SetValidator(new NewRouteDetailValidation());
            }
        }
    }
}
