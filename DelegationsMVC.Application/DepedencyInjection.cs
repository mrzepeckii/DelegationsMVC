using AutoMapper;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.Services;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DelegationsMVC.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IDelegationService, DelegationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDestinationService, DestinationService>();

            services.AddTransient<IValidator<NewVehicleVm>, NewVehicleValidation>();
            services.AddTransient<IValidator<NewEmployeeVm>, NewEmployeeValidation>();
            services.AddTransient<IValidator<NewContactDetailsVm>, NewContactDetailsValidation>();
            services.AddTransient<IValidator<NewDelegationVm>, NewDelegationValidation>();
            services.AddTransient<IValidator<NewRouteVm>, NewRouteValidation>();
            services.AddTransient<IValidator<NewRouteDetailVm>, NewRouteDetailValidation>();
            services.AddTransient<IValidator<NewCostVm>, NewCostValidation>();
            services.AddTransient<IValidator<NewProjectVm>, NewProjectValidation>();
            services.AddTransient<IValidator<NewDestinationVm>, NewDestinationValidation>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
