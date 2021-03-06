﻿using DelegationsMVC.Domain.Interfaces;
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

        public void UpdateEmployee(Employee emp)
        {
            _context.Attach(emp);
            _context.Entry(emp).Property("FirstName").IsModified = true;
            _context.Entry(emp).Property("LastName").IsModified = true;
            _context.Entry(emp).Property("BankAccountCode").IsModified = true;
            _context.Entry(emp).Reference("EmployeeType").IsModified = true;
            _context.Entry(emp).Collection("ContactDetails").IsModified = true;
            _context.Entry(emp).Collection("Vehicles").IsModified = true;
            _context.Entry(emp).Property("ModifiedDateTime").IsModified = true;
            _context.SaveChanges();
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
            var employees = _context.Employees.AsNoTracking();
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _context.Employees.AsNoTracking()
                .Include(e => e.EmployeeType)
                .Include(e => e.ContactDetails).ThenInclude(e => e.ContactDetailType)
                .Include(e => e.Vehicles).ThenInclude(e => e.EngineType)
                .FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public Employee GetEmployeeByUserId(string id)
        {
            var emp = _context.Employees.AsNoTracking()
                 .Include(e => e.EmployeeType)
                .Include(e => e.ContactDetails).ThenInclude(e => e.ContactDetailType)
                .Include(e => e.Vehicles).ThenInclude(e => e.EngineType)
                .FirstOrDefault(e => e.UserId == id);
            return emp;
        }

        public IQueryable<Employee> GetEmployeesByType(int typeId)
        {
            var employees = _context.EmployeeTypes.AsNoTracking().FirstOrDefault(et => et.Id == typeId).Employees.AsQueryable();
            return employees;
        }

        public IQueryable<EmployeeType> GetEmployeeTypes()
        {
            var empTypes = _context.EmployeeTypes.AsNoTracking();
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
            var contactDetails = _context.Employees.AsNoTracking().FirstOrDefault(e => e.Id == employeeId).ContactDetails.AsQueryable();
            return contactDetails;
        }

        public IQueryable<ContactDetailType> GetContactDetailTypes()
        {
            var contactDetails = _context.ContactDetailTypes.AsNoTracking();
            return contactDetails;
        }

        public ContactDetail GetContactDetailById(int id)
        {
            var contact = _context.ContactDetails.AsNoTracking().FirstOrDefault(c => c.Id == id);
            return contact;
        }

        public void UpdateContact(ContactDetail con)
        {
            _context.Attach(con);
            _context.Entry(con).Property("ContactDetailInformation").IsModified = true;
            _context.Entry(con).Property("ContactDetailTypeId").IsModified = true;
            _context.SaveChanges();
        }

    }
}
