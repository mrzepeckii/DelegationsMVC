using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class ListEmployeeForListVm
    {
        public List<EmployeeForLitstVm> Employees { get; set; }
        public int Count { get; set; }
    }
}
