using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Domain.Model
{
    public class AuditableModel
    {
        public string CreateById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
