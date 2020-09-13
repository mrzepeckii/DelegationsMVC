using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class CostType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}