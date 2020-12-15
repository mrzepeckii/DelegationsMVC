using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _destRepo;
        private readonly IMapper _mapper;

        public DestinationService(IDestinationRepository destRepo, IMapper mapper)
        {
            _destRepo = destRepo;
            _mapper = mapper;
        }

        public ListDestinationForListVm GetAllClients()
        {
            var clients = _destRepo.GetDestinations().ProjectTo<DestinationForListVm>(_mapper.ConfigurationProvider).ToList();
            var clientsVm = new ListDestinationForListVm()
            {
                Destinations = clients,
                Count = clients.Count
            };
            return clientsVm;
        }

        public IQueryable<CountryVm> GetProjectsCountries()
        {
            var countries = _destRepo.GetProjectsCountries().ProjectTo<CountryVm>(_mapper.ConfigurationProvider);
            return countries;
        }

        public ListProjectForList GetCurrentProjects()
        {
            var projects = _destRepo.GetProjectsByStatus(1).ProjectTo<ProjectForListVm>(_mapper.ConfigurationProvider).ToList();
            var projectsVm = new ListProjectForList()
            {
                Projects = projects,
                Count = projects.Count
            };
            return projectsVm;
        }

        public ListProjectForList GetClosedProjects()
        {
            var projects = _destRepo.GetProjectsByStatus(2).ProjectTo<ProjectForListVm>(_mapper.ConfigurationProvider).ToList();
            var projectsVm = new ListProjectForList()
            {
                Projects = projects,
                Count = projects.Count
            };
            return projectsVm;
        }        
    }
}
