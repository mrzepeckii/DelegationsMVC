using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class VehicleForListVm : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public string PlateNumbers { get; set; }
        public string EngineType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleForListVm>()
                .ForMember(s => s.EngineType, opt => opt.MapFrom(d => d.EngineType.Name));
        }
    }
}