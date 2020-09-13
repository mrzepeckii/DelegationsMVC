using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Domain.Model
{
    public class EngineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MileageAllowenceId { get; set; }
        public virtual MileageAllowence MileageAllowence { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
