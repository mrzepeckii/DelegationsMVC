using System;

namespace DelegationsMVC.Domain.Model
{
    public class Acceptance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AcceptanceTypeId { get; set; }
        public int DelegationId { get; set; }
        public virtual AcceptanceType AcceptanceType { get; set; }
        public virtual Delegation Delegation { get; set; }
    }
}