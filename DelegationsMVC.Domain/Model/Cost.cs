namespace DelegationsMVC.Domain.Model
{
    public class Cost
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CostTypeId { get; set; }
        public int DelegationId { get; set; }
        public virtual CostType CostType { get; set; }
        public virtual Delegation Delegation { get; set; }
    }
}