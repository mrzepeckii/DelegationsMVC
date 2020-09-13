using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class SubsistanceAllowence
    {
        public int Id { get; set; }
        public decimal RatePerDay { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}