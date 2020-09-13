using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}