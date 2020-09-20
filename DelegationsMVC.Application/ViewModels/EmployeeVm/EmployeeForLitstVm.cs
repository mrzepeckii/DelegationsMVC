using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class EmployeeForLitstVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
