namespace DelegationsMVC.Domain.Model
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string ContactDetailInformation { get; set; }
        public int ContactDetailTypeId { get; set; }
        public int EmployeeId { get; set; }
        public virtual ContactDetailType ContactDetailType { get; set; }
        public virtual Employee Employee { get; set; }
    }
}