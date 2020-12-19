using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Domain.Interfaces
{
    public interface IDestinationRepository
    {
        int AddDestination(Destination destinationToAdd);
        void UpdateDesintation(Destination dest);
        void DeleteDestination(int destinationToRemoveId);
        IQueryable<Destination> GetDestinations();
        Destination GetDestinationById(int id);
        IQueryable<Destination> GetDestinationsByCountry(int countryId);
        int AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int projectId);
        IQueryable<Project> GetProjectsByDestination(int destinationId);
        IQueryable<Project> GetProjectsByStatus(int statusId);
        Project GetProjectById(int id);
        IQueryable<ProjectStatus> GetProjectStatuses();
        IQueryable<Country> GetAllCountries();
        IQueryable<Country> GetProjectsCountries();
    }
}
