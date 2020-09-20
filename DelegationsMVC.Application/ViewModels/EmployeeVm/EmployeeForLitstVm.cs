using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class EmployeeForLitstVm : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeForLitstVm>()
                .ForMember(s => s.FullName, opt => opt.MapFrom(d => d.FirstName + " " + d.LastName))
                .ForMember(s => s.Position, opt => opt.MapFrom(d => d.EmployeeType.Name));
        }
    }
}
