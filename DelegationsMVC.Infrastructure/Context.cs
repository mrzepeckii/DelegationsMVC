using DelegationsMVC.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ContactDetailType> ContactDetailTypes { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Delegation> Delegations { get; set; }
        public DbSet<DelegationStatus> DelegationStatuses { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<MileageAllowence> MileageAllowences { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteDetail> RouteDetails { get; set; }
        public DbSet<RouteType> RouteTypes { get; set; }
        public DbSet<SubsistanceAllowence> SubsistanceAllowences { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Route>()
                .HasOne(rd => rd.RouteDetail).WithOne(r => r.Route)
                .HasForeignKey<RouteDetail>(rd => rd.RouteRef);

            builder.Entity<Cost>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,4)");

            builder.Entity<MileageAllowence>()
                .Property(p => p.RatePerKm)
                .HasColumnType("decimal(18,4)");

            builder.Entity<SubsistanceAllowence>()
                .Property(p => p.RatePerDay)
                .HasColumnType("decimal(18,4)");
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableModel && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
               ((AuditableModel)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                   ((AuditableModel)entityEntry.Entity).CreatedDateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
