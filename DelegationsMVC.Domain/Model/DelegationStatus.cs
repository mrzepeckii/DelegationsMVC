using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class DelegationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Delegation> Delegations { get; set; }
    }
}