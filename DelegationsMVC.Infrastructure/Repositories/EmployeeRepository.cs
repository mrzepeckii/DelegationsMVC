using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public int AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee.Id;
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);
            if(employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public IQueryable<Employee> GetEmployeesByType(int typeId)
        {
            var employees = _context.EmployeeTypes.FirstOrDefault(et => et.Id == typeId).Employees.AsQueryable();
            return employees;
        }

        public IQueryable<ContactDetail> GetEmployeeContactDetails(int employeeId)
        {
            var contactDetails = _context.Employees.FirstOrDefault(e => e.Id == employeeId).ContactDetails.AsQueryable();
            return contactDetails;
        }
    }
}
