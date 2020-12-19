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
using Microsoft.AspNetCore.Identity;

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
            var id = _employeeRepo.AddEmployee(emp);
            return id;
        }

        public Employee GetEmployeeById(int id)
        {
            var emp = _employeeRepo.GetEmployeeById(id);
            return emp;
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

        public EmployeeDetailVm GetEmployeeDetails(string userId)
        {
            var emp = _employeeRepo.GetEmployeeByUserId(userId);
            var empVm = GetEmployeeDetails(emp.Id);
            return empVm;
        }

        public EmployeeDetailVm GetEmployeeDetails(int employeeId)
        {
            var employee = _employeeRepo.GetEmployeeById(employeeId);
            var employeeVm = _mapper.Map<EmployeeDetailVm>(employee);
            if(employeeVm != null)
            {
                employeeVm.Emails = employee.ContactDetails.Where(cd => cd.ContactDetailTypeId == 1)
                    .AsQueryable().ProjectTo<ContactDetailsForListVm>(_mapper.ConfigurationProvider).ToList();

                employeeVm.PhoneNumbers = employee.ContactDetails.Where(cd => cd.ContactDetailTypeId == 2)
                .AsQueryable().ProjectTo<ContactDetailsForListVm>(_mapper.ConfigurationProvider).ToList();

                employeeVm.Vehicles = _vehicleRepo.GetVehiclesByEmployee(employeeId)
                    .AsQueryable().ProjectTo<VehicleForListVm>(_mapper.ConfigurationProvider).ToList();
            }
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

        public void DeleteContact(int id)
        {
            _employeeRepo.DeleteContactDetail(id);
        }

        public IQueryable<EngineTypeVm> GetEngineTypes()
        {
            var engineTypesVm = _vehicleRepo.GetEngineTypes().ProjectTo<EngineTypeVm>(_mapper.ConfigurationProvider);
            return engineTypesVm;
        }

        public void DeleteVehicle(int id)
        {
            _vehicleRepo.DeleteVehicle(id);
        }

        public NewEmployeeVm GetEmployeeForEdit(string id)
        {
            var user = _employeeRepo.GetEmployeeByUserId(id);
            var emp = _employeeRepo.GetEmployeeById(user.Id);
            var empVm = _mapper.Map<NewEmployeeVm>(emp);
            return empVm;
        }

        public void UpdateEmployee(NewEmployeeVm empVm)
        {
            var emp = _mapper.Map<Employee>(empVm);
            _employeeRepo.UpdateEmployee(emp);
        }

        public int AddVehicle(NewVehicleVm vehVm)
        {
            var veh = _mapper.Map<Vehicle>(vehVm);
            var id = _vehicleRepo.AddVehicle(veh);
            return id;
        }

        public Vehicle GetVehicleById(int id)
        {
            var veh = _vehicleRepo.GetVehicleById(id);
            return veh;
        }

        public NewVehicleVm GetVehicleForEdit(int id)
        {
            var veh = _vehicleRepo.GetVehicleById(id);
            var vehVm = _mapper.Map<NewVehicleVm>(veh);
            return vehVm;
        }

        public void UpdateVehicle(NewVehicleVm vehVm)
        {
            var veh = _mapper.Map<Vehicle>(vehVm);
            _vehicleRepo.UpdateVehicle(veh);
        }

        public int AddContact(NewContactDetailsVm conVm)
        {
            var con = _mapper.Map<ContactDetail>(conVm);
            var id = _employeeRepo.AddContactDetail(con);
            return id;
        }

        public ContactDetail GetContactDetailById(int id)
        {
            var con = _employeeRepo.GetContactDetailById(id);
            return con;
        }

        public NewContactDetailsVm GetContactForEdit(int id)
        {
            var con = _employeeRepo.GetContactDetailById(id);
            var conVm = _mapper.Map<NewContactDetailsVm>(con);
            return conVm;
        }

        public void UpdateContact(NewContactDetailsVm conVm)
        {
            var con = _mapper.Map<ContactDetail>(conVm);
            _employeeRepo.UpdateContact(con);
        }

        public IQueryable<VehicleForListVm> GetVehiclesByEmploee(int id)
        {
            var vehsVm = _vehicleRepo.GetVehiclesByEmployee(id).ProjectTo<VehicleForListVm>(_mapper.ConfigurationProvider);
            return vehsVm;
        }

        public List<NewVehicleVm> CheckVehiclesList(List<NewVehicleVm> newVehicles)
        {
            var vehicles = new List<NewVehicleVm>();
            vehicles = newVehicles.Where(v => v.PlateNumbers != null).ToList();
            return vehicles;
        }

        public List<NewContactDetailsVm> CheckContactsList(List<NewContactDetailsVm> newContacts)
        {
            var contacts = new List<NewContactDetailsVm>();
            contacts = newContacts.Where(c => c.ContactDetailInformation != null).ToList();
            return contacts;
        }

        public NewEmployeeVm SetParametersToVm(NewEmployeeVm model)
        {
            model.EmployeeTypes = GetEmployeeTypes().ToList();
            model.EngineTypes = GetEngineTypes().ToList();
            model.ContactDetailTypes = GetConactDetailTypes().ToList();
            return model;
        }

        public Employee GetEmployeeByUserId(string id)
        {
            var emp = _employeeRepo.GetEmployeeByUserId(id);
            return emp;
        }

        public bool CheckIfEmployeeExist(string id)
        {
            return _employeeRepo.GetEmployeeByUserId(id) == null;
        }
    }
}
