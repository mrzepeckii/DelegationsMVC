using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDelegationRepository, DelegationRepository>();
            services.AddTransient<IDestinationRepository, DestinationRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            return services;
        }
    }
}
