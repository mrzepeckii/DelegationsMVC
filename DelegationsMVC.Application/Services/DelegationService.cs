using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Services
{
    public class DelegationService : IDelegationService
    {
        private readonly IDelegationRepository _delegationRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public DelegationService(IDelegationRepository delegRepo, IEmployeeRepository empRepo, IMapper mapper)
        {
            _delegationRepo = delegRepo;
            _employeeRepo = empRepo;
            _mapper = mapper;
        }

        public int AddDelegation(NewDelegationVm delVm)
        {
            var deleg = _mapper.Map<Delegation>(delVm);
            deleg.EmployeeId = 10;
            deleg.CreateById = 1;
            deleg.CreatedDateTime = DateTime.Now;
            deleg.DelegationStatusId = 1;
            var id =_delegationRepo.AddDelegation(deleg);
            return id;
        }

        public ListDelegationForListVm GetAllDelegationsForListByStatus(int statusId)
        {
            var delegations = _delegationRepo.GetDelegationsByStatus(statusId).ProjectTo<DelegationForListVm>(_mapper.ConfigurationProvider).ToList();
            var delegationsVm = new ListDelegationForListVm()
            {
                Delegations = delegations,
                Count = delegations.Count
            };
            return delegationsVm;
        }

        public void DeleteDelegation(Delegation del)
        {
            del.DelegationStatusId = 6;
            _delegationRepo.UpdateDelegation(del);
        }

        public void UpdateDelegation(NewDelegationVm delVm)
        {
            var del = _mapper.Map<Delegation>(delVm);
            _delegationRepo.UpdateDelegation(del);
        }

        public IQueryable<DestinationTypeVm> GetAllDestinations()
        {
            var destinations = _delegationRepo.GetAllDestinations().ProjectTo<DestinationTypeVm>(_mapper.ConfigurationProvider);
            return destinations;
        }
    }
}
