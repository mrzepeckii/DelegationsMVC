using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class Destination : AuditableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}