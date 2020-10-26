using AutoMapper;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
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
            deleg.CreateById = 1;
            deleg.CreatedDateTime = DateTime.Now;
            deleg.DelegationStatusId = 1;
            var id =_delegationRepo.AddDelegation(deleg);
            return id;
        }
    }
}
