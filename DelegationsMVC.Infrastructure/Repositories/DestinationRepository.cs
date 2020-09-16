using DelegationsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class DestinationRepository
    {
        private readonly Context _context;

        public DestinationRepository(Context context)
        {
            _context = context;
        }

        /*Operations related to the destination object      
         * *******************************************/
        public int AddDestination(Destination destinationToAdd)
        {
            _context.Destinations.Add(destinationToAdd);
            _context.SaveChanges();
            return destinationToAdd.Id;
        }

        public void DeleteDestination(int destinationToRemoveId)
        {
            var destination = _context.Destinations.Find(destinationToRemoveId);
            var projects = _context.Projects.Where(p => p.Destination == destination);
            if(destination != null)
            {
                _context.Projects.RemoveRange(projects);
                _context.Destinations.Remove(destination);
                _context.SaveChanges();
            }
        }

        public Destination GetDestinationById(int id)
        {
            var destination = _context.Destinations.FirstOrDefault(d => d.Id == id);
            return destination;
        }

        public IQueryable<Destination> GetDestinationsByCountry(int countryId)
        {
            var destinations = _context.Destinations.Where(d => d.CountryId == countryId);
            return destinations;
        }

        /*Operations related to the project object      
         * project can't exist without destination object 
         * *******************************************/
        public IQueryable<Project> GetProjectsByDestination(int destinationId)
        {
            var projects = _context.Destinations.FirstOrDefault(d => d.Id == destinationId).Projects.AsQueryable();
            return projects;
        }

        public IQueryable<Project> GetProjectsByStatus(int statusId)
        {
            var projects = _context.ProjectStatuses.FirstOrDefault(ps => ps.Id == statusId).Projects.AsQueryable();
            return projects;
        }

        /*Operations related to the coutnry object 
         * *******************************************/
        public IQueryable<Country> GetAllCountries()
        {
            var countries = _context.Countries;
            return countries;
        }
    }
}
