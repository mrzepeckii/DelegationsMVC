using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class EmployeeDetailVm : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string BankAccountCode { get; set; }
     //   public List<VehicleForListVm> Vehicles { get; set; }
      //  public List<ContactDetailsForListVm> Emails { get; set; }
     //   public List<ContactDetailsForListVm> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailVm>()
                .ForMember(s => s.FullName, opt => opt.MapFrom(d => d.FirstName + " " + d.LastName))
                .ForMember(s => s.Position, opt => opt.MapFrom(d => d.EmployeeType.Name));
             //   .ForMember(s => Emails, opt => opt.Ignore())
             //   .ForMember(s => Vehicles, opt => opt.Ignore())
             //   .ForMember(s => PhoneNumbers, opt => opt.Ignore());
        }
    }
}
