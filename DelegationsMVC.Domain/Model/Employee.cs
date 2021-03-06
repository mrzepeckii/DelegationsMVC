﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class Employee : AuditableModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BankAccountCode { get; set; }
        public int EmployeeTypeId { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Delegation> Delegations { get; set; }
    }
}