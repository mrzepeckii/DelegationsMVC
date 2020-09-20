using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        int AddEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        Employee GetEmployeeById(int id);
        IQueryable<Employee> GetAllEmployees();
        IQueryable<Employee> GetEmployeesByType(int typeId);
        int AddContactDetail(ContactDetail contactDetail);
        void DeleteContactDetail(int contactDetailId);
        IQueryable<ContactDetail> GetEmployeeContactDetails(int employeeId);
    }
}
