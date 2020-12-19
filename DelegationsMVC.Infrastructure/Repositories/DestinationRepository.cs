using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class DestinationRepository : IDestinationRepository
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

        public void UpdateDesintation(Destination dest)
        {
            _context.Attach(dest);
            _context.Entry(dest).Property("Name").IsModified = true;
            _context.Entry(dest).Property("ModifiedById").IsModified = true;
            _context.Entry(dest).Property("ModifiedDateTime").IsModified = true;
            _context.Entry(dest).Property("CountryId").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteDestination(int destinationToRemoveId)
        {
            var destination = _context.Destinations.Find(destinationToRemoveId);
            if(destination != null)
            {
                _context.Destinations.Remove(destination);
                _context.SaveChanges();
            }
        }

        public Destination GetDestinationById(int id)
        {
            var destination = _context.Destinations
                .Include(d => d.Country)
                .FirstOrDefault(d => d.Id == id);
            return destination;
        }

        public IQueryable<Destination> GetDestinationsByCountry(int countryId)
        {
            var destinations = _context.Destinations.Where(d => d.CountryId == countryId);
            return destinations;
        }

        public IQueryable<Destination> GetDestinations()
        {
            var destinations = _context.Destinations;
            return destinations;
        }

        /*Operations related to the project object      
         * project can't exist without destination object 
         * *******************************************/
        public int AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project.Id; 
        }

        public void UpdateProject(Project project)
        {
            _context.Attach(project);
            _context.Entry(project).Property("Name").IsModified = true;
            _context.Entry(project).Property("Number").IsModified = true;
            _context.Entry(project).Property("ProjectStatusId").IsModified = true;
            _context.Entry(project).Property("DestinationId").IsModified = true;
            _context.Entry(project).Property("ModifiedById").IsModified = true;
            _context.Entry(project).Property("ModifiedDateTime").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteProject(int projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if(project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }

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

        public Project GetProjectById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Destination)
                .FirstOrDefault(p => p.Id == id);
            return project;
        }
        public IQueryable<ProjectStatus> GetProjectStatuses()
        {
            var statuses = _context.ProjectStatuses;
            return statuses;
        }

        /*Operations related to the coutnry object 
         * *******************************************/
        public IQueryable<Country> GetAllCountries()
        {
            var countries = _context.Countries;
            return countries;
        }

        public IQueryable<Country> GetProjectsCountries()
        {
            var countries = _context.Destinations.Select(d => d.Country).Distinct();
            return countries;
        }
    }
}
