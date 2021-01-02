using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Domain.Model
{
    public class AuditableModel
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
