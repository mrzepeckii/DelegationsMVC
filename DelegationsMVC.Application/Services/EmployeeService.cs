using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        public int AddEmployee(NewEmployeeVm employee)
        {
            var emp = _mapper.Map<Employee>(employee);
            emp.CreateById = 1;
            emp.CreatedDateTime = DateTime.Now;
            var id = _employeeRepo.AddEmployee(emp);
            return id;
        }

        public ListEmployeeForListVm GetAllEmployeeForList()
        {
            var employees = _employeeRepo.GetAllEmployees().ProjectTo<EmployeeForLitstVm>(_mapper.ConfigurationProvider).ToList();
            var employeesVm = new ListEmployeeForListVm()
            {
                Employees = employees,
                Count = employees.Count
            };
            return employeesVm;
        }

        public EmployeeDetailVm GetEmployeeDetails(int employeeId)
        {
            var employee = _employeeRepo.GetEmployeeById(employeeId);
            var employeeVm = _mapper.Map<EmployeeDetailVm>(employee);
           // employeeVm.Emails = new List<ContactDetailsForListVm>();
           // employeeVm.PhoneNumbers = new List<ContactDetailsForListVm>();
           // employeeVm.Vehicles = new List<VehicleForListVm>();
            return employeeVm;
        }

        public IEnumerable<EmployeeTypeVm> GetEmployeeTypes()
        {
            var empTypesVm = _employeeRepo.GetEmployeeTypes().ProjectTo<EmployeeTypeVm>(_mapper.ConfigurationProvider).ToList();
            return empTypesVm;
        }
    }
}
