﻿using System;
using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class Delegation : AuditableModel
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public int EmployeeId { get; set; }
        public int DestinationId { get; set; }
        public int DelegationStatusId { get; set; }
        public DateTime? AccoutantApprovedDate { get; set; }
        public DateTime? ChiefApprovedDate { get; set; }
        public DateTime? PaidDateDate { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Destination Destination { get; set; }
        public virtual DelegationStatus DelegationStatus { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}