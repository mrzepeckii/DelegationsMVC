using DelegationsMVC.Application.ViewModels.DestinationVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Application.Interfaces
{
    public interface IDestinationService
    {
        public ListDestinationForListVm GetAllClients();
        public IQueryable<CountryVm> GetProjectsCountries();
    }
}
