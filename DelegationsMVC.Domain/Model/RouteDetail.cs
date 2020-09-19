using System;

namespace DelegationsMVC.Domain.Model
{
    public class RouteDetail
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Kilometers { get; set; }
        public int RouteRef { get; set; }
        public int? VehicleId { get; set; }
        public Route Route { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}