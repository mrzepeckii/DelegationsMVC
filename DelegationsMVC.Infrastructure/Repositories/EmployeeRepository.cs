using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        /*Operations related to the employee object      
         * *******************************************/
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

        public IQueryable<Employee> GetAllEmployees()
        {
            var employees = _context.Employees;
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeType)
                .Include(e => e.Vehicles)
                .Include(e => e.ContactDetails).ThenInclude(e => e.ContactDetailType)
                .FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public IQueryable<Employee> GetEmployeesByType(int typeId)
        {
            var employees = _context.EmployeeTypes.FirstOrDefault(et => et.Id == typeId).Employees.AsQueryable();
            return employees;
        }

        public IQueryable<EmployeeType> GetEmployeeTypes()
        {
            var empTypes = _context.EmployeeTypes;
            return empTypes;
        }

        /*Operations related to the contactDetail object      
         * contactDetail can't exist without employee object 
         * *******************************************/
        public int AddContactDetail(ContactDetail contactDetail)
        {
            _context.ContactDetails.Add(contactDetail);
            _context.SaveChanges();
            return contactDetail.Id;
        }

        public void DeleteContactDetail(int contactDetailId)
        {
            var contactDetail = _context.ContactDetails.FirstOrDefault(cd => cd.Id == contactDetailId);
            if(contactDetail != null)
            {
                _context.ContactDetails.Remove(contactDetail);
                _context.SaveChanges();
            }
        }

        public IQueryable<ContactDetail> GetEmployeeContactDetails(int employeeId)
        {
            var contactDetails = _context.Employees.FirstOrDefault(e => e.Id == employeeId).ContactDetails.AsQueryable();
            return contactDetails;
        }

        public IQueryable<ContactDetailType> GetContactDetailTypes()
        {
            var contactDetails = _context.ContactDetailTypes;
            return contactDetails;
        }
    }
}
