using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class TransportType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}