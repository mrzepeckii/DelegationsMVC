using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Application.ViewModels.EmployeeVm
{
    public class EmployeeDetailVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string BankAccountCode { get; set; }
        public List<VehicleForListVm> Vehicles { get; set; }
        public List<ContactDetailsForListVm> Emails { get; set; }
        public List<ContactDetailsForListVm> PhoneNumbers { get; set; }
    }
}
