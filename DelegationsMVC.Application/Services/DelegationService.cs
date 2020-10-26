using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.Services
{
    public class DelegationService : IDelegationService
    {
        private readonly IDelegationRepository _delegationRepo;
        private readonly IEmployeeRepository _employeeRepo;
    }
}
