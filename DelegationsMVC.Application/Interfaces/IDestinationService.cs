using DelegationsMVC.Application.ViewModels.DestinationVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IDestinationService
    {
        int AddDestination(NewDestinationVm destination);
        void RemoveDestination(int id);
        void UpdateDesination(NewDestinationVm destination);
        int AddProject(NewProjectVm project);
        void RemoveProject(int id);
        void UpdateProject(NewProjectVm project);
        void CloseProject(int id);
        ListDestinationForListVm GetAllClients();
        IQueryable<CountryVm> GetProjectsCountries();
        ListProjectForList GetCurrentProjects();
        ListProjectForList GetClosedProjects();
    }
}
