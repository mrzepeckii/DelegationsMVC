using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class AcceptanceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Acceptance> Acceptances { get; set; }
    }
}