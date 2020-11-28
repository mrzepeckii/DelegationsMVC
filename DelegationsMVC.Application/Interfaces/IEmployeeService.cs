using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IEmployeeService
    {
        ListEmployeeForListVm GetAllEmployeeForList();
        int AddEmployee(NewEmployeeVm employee);
        EmployeeDetailVm GetEmployeeDetails(int employeeId);
        IQueryable<EmployeeTypeVm> GetEmployeeTypes();
        void DeleteEmployee(int id);
        IQueryable<ContactDetailTypeVm> GetConactDetailTypes();
        IQueryable<EngineTypeVm> GetEngineTypes();
        void DeleteVehicle(int id);
        void DeleteContact(int id);
        NewEmployeeVm GetEmployeeForEdit(string id);
        void UpdateEmployee(NewEmployeeVm empVm);
        int AddVehicle(NewVehicleVm vehVm);
        NewVehicleVm GetVehicleForEdit(int id);
        void UpdateVehicle(NewVehicleVm vehVm);
        int AddContact(NewContactDetailsVm conVm);
        NewContactDetailsVm GetContactForEdit(int id);
        void UpdateContact(NewContactDetailsVm con);
        IQueryable<VehicleForListVm> GetVehiclesByEmploee(int id);
        List<NewVehicleVm> CheckVehiclesList(List<NewVehicleVm> newVehicles);
        List<NewContactDetailsVm> CheckContactsList(List<NewContactDetailsVm> newContacts);
        Employee GetEmployeeById(int id);
        NewEmployeeVm SetParametersToVm(NewEmployeeVm model);
        Employee GetEmployeeByUserId(string id);
    }
}
