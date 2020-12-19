namespace DelegationsMVC.Domain.Model
{
    public class Project : AuditableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int ProjectStatusId { get; set; }
        public int DestinationId { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual Destination Destination { get; set; }
    }
}