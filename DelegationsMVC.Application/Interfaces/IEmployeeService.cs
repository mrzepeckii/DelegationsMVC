﻿using DelegationsMVC.Application.ViewModels.EmployeeVm;
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
    }
}