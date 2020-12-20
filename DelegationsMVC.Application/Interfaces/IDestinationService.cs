using DelegationsMVC.Application.ViewModels.DestinationVm;
using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IDestinationService
    {
        NewDestinationVm GetDestinationForEdit(int id);
        NewProjectVm GetProjectForEdit(int id);
        int AddDestination(NewDestinationVm destination);
        void DeleteDestination(int id);
        void UpdateDesination(NewDestinationVm destination);
        int AddProject(NewProjectVm project);
        void DeleteProject(int id);
        void UpdateProject(NewProjectVm project);
        void CloseProject(int id);
        Destination GetDestinationById(int id);
        DestinationDetailVm GetDestinationDetail(int id);
        Project GetProjectById(int id);
        ListDestinationForListVm GetAllClients();
        IQueryable<CountryVm> GetProjectsCountries();
        ListProjectForList GetAllProjects();
        ListProjectForList GetCurrentProjects();
        ListProjectForList GetClosedProjects();
        IQueryable<DestinationTypeVm> GetDestinations();
        IQueryable<CountryVm> GetCountries();
    }
}
