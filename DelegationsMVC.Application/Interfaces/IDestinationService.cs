using DelegationsMVC.Application.ViewModels.DestinationVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IDestinationService
    {
        ListDestinationForListVm GetAllClients();
        IQueryable<CountryVm> GetProjectsCountries();
        ListProjectForList GetCurrentProjects();
        ListProjectForList GetClosedProjects();
    }
}
