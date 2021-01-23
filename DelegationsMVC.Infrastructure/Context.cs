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

            //add start data to database
            builder.Entity<ContactDetailType>()
                .HasData(new ContactDetailType { Id = 1, Name = "Numer telefonu" },
                        new ContactDetailType { Id = 2, Name = "Email" });

            builder.Entity<CostType>()
                .HasData(new CostType { Id = 1, Name = "Noclegi" },
                        new CostType { Id = 2, Name = "Dodatkowe wydatki" });

            builder.Entity<DelegationStatus>()
                .HasData(new DelegationStatus { Id = 1, Name = "Utworzona" },
                        new DelegationStatus { Id = 2, Name = "Akceptacja - prezes" },
                        new DelegationStatus { Id = 3, Name = "Akceptacja - księgowej" },
                        new DelegationStatus { Id = 4, Name = "Opłacona" },
                        new DelegationStatus { Id = 5, Name = "W edycji" },
                        new DelegationStatus { Id = 6, Name = "Anulowana" });

            builder.Entity<EmployeeType>()
                .HasData(new EmployeeType { Id = 1, Name = "Prezes" },
                        new EmployeeType { Id = 2, Name = "Księgowa" },
                        new EmployeeType { Id = 3, Name = "Programista" },
                        new EmployeeType { Id = 4, Name = "Projektant" });

            builder.Entity<MileageAllowence>()
                .HasData(new MileageAllowence { Id = 1, RatePerKm = 0.5214M },
                        new MileageAllowence { Id = 2, RatePerKm = 0.8358M });

            builder.Entity<EngineType>()
                .HasData(new EngineType { Id = 1, Name = "Do 900 cm3", MileageAllowenceId = 1 },
                        new EngineType { Id = 2, Name = "Pow 900 cm3", MileageAllowenceId = 2 });

            builder.Entity<ProjectStatus>()
                .HasData(new ProjectStatus { Id = 1, Name = "Otwarty" },
                        new ProjectStatus { Id = 2, Name = "Zamknięty" });

            builder.Entity<RouteType>()
                .HasData(new ProjectStatus { Id = 1, Name = "Jazda po mieście" },
                        new ProjectStatus { Id = 2, Name = "Międzymiastowa" });

            builder.Entity<SubsistanceAllowence>()
                .HasData(new SubsistanceAllowence { Id = 1, RatePerDay = 30M });

            builder.Entity<TransportType>()
                .HasData(new TransportType { Id = 1, Name = "Samochód osobowy" },
                        new TransportType { Id = 2, Name = "Transport publiczny" },
                        new TransportType { Id = 3, Name = "Taksówka" },
                        new TransportType { Id = 4, Name = "Pasażer" });

            builder.Entity<Country>()
                .HasData(new Country { Id = 1, SubsistanceAllowenceId = 1 });


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
