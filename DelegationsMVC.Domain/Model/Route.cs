namespace DelegationsMVC.Domain.Model
{
    public class Route : AuditableModel
    {
        public int Id { get; set; }
        public int DelegationId { get; set; }
        public int TypeOfTransportId { get; set; }
        public int RouteTypeId { get; set; }
        public int RouteDetailId { get; set; }
        public virtual Delegation Delegation { get; set; }
        public virtual TypeOfTransport TypeOfTransport { get; set; }
        public virtual RouteType RouteType { get; set; }
        public virtual RouteDetail RouteDetail { get; set; }
    }
}