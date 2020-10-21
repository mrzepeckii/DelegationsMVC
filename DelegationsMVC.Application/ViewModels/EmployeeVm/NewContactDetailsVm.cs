using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
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
        public int EmployeeId { get; set; }
        public List<ContactDetailTypeVm> ContactDetailTypes { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewContactDetailsVm, ContactDetail>().ReverseMap();
        }

    }
}
