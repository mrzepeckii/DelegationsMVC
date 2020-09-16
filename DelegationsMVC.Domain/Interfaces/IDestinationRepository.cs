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
        void DeleteDestination(int destinationToRemoveId);
        Destination GetDestinationById(int id);
        IQueryable<Destination> GetDestinationsByCountry(int countryId);
        IQueryable<Project> GetProjectsByDestination(int destinationId);
        IQueryable<Project> GetProjectsByStatus(int statusId);
        IQueryable<Country> GetAllCountries();
    }
}
