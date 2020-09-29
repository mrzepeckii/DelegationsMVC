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
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepo, IVehicleRepository vehicleRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _vehicleRepo = vehicleRepo;
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

            var emails = employee.ContactDetails.Where(cd => cd.ContactDetailTypeId == 1);
            var phoneNumbers = employee.ContactDetails.Where(cd => cd.ContactDetailTypeId == 2);
            var vehicles = employee.Vehicles;

            employeeVm.Emails = emails.AsQueryable().ProjectTo<ContactDetailsForListVm>(_mapper.ConfigurationProvider).ToList();
            employeeVm.PhoneNumbers = phoneNumbers.AsQueryable().ProjectTo<ContactDetailsForListVm>(_mapper.ConfigurationProvider).ToList();
            employeeVm.Vehicles = vehicles.AsQueryable().ProjectTo<VehicleForListVm>(_mapper.ConfigurationProvider).ToList();

            return employeeVm;
        }

        public IQueryable<EmployeeTypeVm> GetEmployeeTypes()
        {
            var empTypesVm = _employeeRepo.GetEmployeeTypes().ProjectTo<EmployeeTypeVm>(_mapper.ConfigurationProvider);
            return empTypesVm;
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepo.DeleteEmployee(id);
        }

        public IQueryable<ContactDetailTypeVm> GetConactDetailTypes()
        {
            var contactDetTypeVm = _employeeRepo.GetContactDetailTypes().ProjectTo<ContactDetailTypeVm>(_mapper.ConfigurationProvider);
            return contactDetTypeVm;
        }

        public IQueryable<EngineTypeVm> GetEngineTypes()
        {
            var engineTypesVm = _vehicleRepo.GetEngineTypes().ProjectTo<EngineTypeVm>(_mapper.ConfigurationProvider);
            return engineTypesVm;
        }
    }
}
