﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Domain.Model
{
    public class Vehicle : AuditableModel
    {
        public int Id { get; set; }
        public string PlateNumbers { get; set; }
        public int EngineTypeId { get; set; }
        public int EmployeeId { get; set; }
        public virtual EngineType EngineType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
