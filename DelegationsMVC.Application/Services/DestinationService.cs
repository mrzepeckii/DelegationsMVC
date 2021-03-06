﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
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

        public int AddDestination(NewDestinationVm destinationVm)
        {
            var dest = _mapper.Map<Destination>(destinationVm);
            var id = _destRepo.AddDestination(dest);
            return id;
        }

        public void DeleteDestination(int id)
        {
            _destRepo.DeleteDestination(id);
        }

        public void UpdateDesination(NewDestinationVm destinationVm)
        {
            var dest = _mapper.Map<Destination>(destinationVm);
            _destRepo.UpdateDesintation(dest);
        }

        public int AddProject(NewProjectVm projectVm)
        {
            var project = _mapper.Map<Project>(projectVm);
            var id = _destRepo.AddProject(project);
            return id;
        }

        public void DeleteProject(int id)
        {
            _destRepo.DeleteProject(id);
        }

        public void UpdateProject(NewProjectVm projectVm)
        {
            var project = _mapper.Map<Project>(projectVm);
            _destRepo.UpdateProject(project);
        }

        public void CloseProject(int id)
        {
            var project = _destRepo.GetProjectById(id);
            if(project == null || project.ProjectStatusId == 2)
            {
                return;
            }
            project.ProjectStatusId = 2;
            _destRepo.UpdateProject(project);
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

        public IQueryable<DestinationTypeVm> GetDestinations()
        {
            var dests = _destRepo.GetDestinations().ProjectTo<DestinationTypeVm>(_mapper.ConfigurationProvider);
            return dests;
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

        public IQueryable<CountryVm> GetCountries()
        {
            var countries = _destRepo.GetAllCountries().ProjectTo<CountryVm>(_mapper.ConfigurationProvider);
            return countries;
        }

        public NewDestinationVm GetDestinationForEdit(int id)
        {
            var dest = _destRepo.GetDestinationById(id);
            var destVm = _mapper.Map<NewDestinationVm>(dest);
            if(destVm != null)
            {
                destVm.Countries = GetCountries().ToList();
            }
            return destVm;
        }

        public NewProjectVm GetProjectForEdit(int id)
        {
            var proj = _destRepo.GetProjectById(id);
            var projVm = _mapper.Map<NewProjectVm>(proj);
            if(projVm != null)
            {
                projVm.Destinations = GetDestinations().ToList();
                projVm.Statuses = GetProjectStatuses().ToList();
            }
            return projVm;
        }

        public Destination GetDestinationById(int id)
        {
            var dest = _destRepo.GetDestinationById(id);
            return dest;
        }

        public Project GetProjectById(int id)
        {
            var proj = _destRepo.GetProjectById(id);
            return proj;
        }

        public DestinationDetailVm GetDestinationDetail(int id)
        {
            var dest = _destRepo.GetDestinationById(id);
            var destVm = _mapper.Map<DestinationDetailVm>(dest);
            var projects =  dest.Projects.AsQueryable().ProjectTo<ProjectForListVm>(_mapper.ConfigurationProvider).ToList();
            var projectsVm = new ListProjectForList()
            {
                Projects = projects,
                Count = projects.Count
            };
            destVm.Projects = projectsVm;
            return destVm;
        }

        public ListProjectForList GetAllProjects()
        {
            var projects = _destRepo.GetProjects().ProjectTo<ProjectForListVm>(_mapper.ConfigurationProvider).ToList();
            var projectsVm = new ListProjectForList()
            {
                Projects = projects,
                Count = projects.Count
            };
            return projectsVm;
        }

        public IQueryable<ProjectStatusVm> GetProjectStatuses()
        {
            var statuses = _destRepo.GetProjectStatuses().ProjectTo<ProjectStatusVm>(_mapper.ConfigurationProvider);
            return statuses;
        }

        public bool CheckIfDestinationExist(int id)
        {
            var dest = _destRepo.GetDestinationById(id);
            if(dest == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfProjectExist(int id)
        {
            var proj = _destRepo.GetProjectById(id);
            if(proj == null)
            {
                return false;
            }
            return true;
        }
    }
}
