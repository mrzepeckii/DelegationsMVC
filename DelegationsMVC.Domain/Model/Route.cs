namespace DelegationsMVC.Domain.Model
{
    public class Route : AuditableModel
    {
        public int Id { get; set; }
        public int DelegationId { get; set; }
        public int TypeOfTransportId { get; set; }
        public int RouteTypeId { get; set; }
        public RouteDetail RouteDetail { get; set; }
        public virtual Delegation Delegation { get; set; }
        public virtual TransportType TypeOfTransport { get; set; }
        public virtual RouteType RouteType { get; set; }

    }
}