using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class ContactDetailsForListVm : IMapFrom<ContactDetail>
    {
        public string ContactDetailType { get; set; }
        public string ContactDetailInformation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDetail, ContactDetailsForListVm>()
                .ForMember(s => s.ContactDetailType, opt => opt.MapFrom(d => d.ContactDetailType.Name))
        }
    }
}