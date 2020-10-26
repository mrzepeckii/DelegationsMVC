using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Model;

namespace DelegationsMVC.Application.ViewModels.DelegationVm
{
    public class CostTypeVm : IMapFrom<CostType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CostTypeVm, CostType>();
        }
    }
}