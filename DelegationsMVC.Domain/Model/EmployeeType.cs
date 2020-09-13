using System.Collections.Generic;

namespace DelegationsMVC.Domain.Model
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}