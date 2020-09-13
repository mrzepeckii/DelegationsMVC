namespace DelegationsMVC.Domain.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubsistanceAllowenceId { get; set; }
        public virtual SubsistanceAllowence SubsistanceAllowence { get; set; }
    }
}