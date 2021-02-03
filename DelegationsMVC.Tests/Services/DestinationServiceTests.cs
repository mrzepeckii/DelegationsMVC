using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using FluentAssertions;
using DelegationsMVC.Domain.Model;
using AutoMapper;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Application.Services;
using System.Linq;
using DelegationsMVC.Application.ViewModels.DestinationVm;

namespace DelegationsMVC.Tests.Services
{
    public class DestinationServiceTests
    {
        [Fact]
        public void ShouldChangeStatusOfProject()
        {
            //arrange
            var projectShouldChange = new Project() { Id = 1, DestinationId = 1, Name = "example", ProjectStatusId = 1 };
            var projectShouldNotChange = new Project() { Id = 2, DestinationId = 1, Name = "example2", ProjectStatusId = 2 };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetProjectById(1)).Returns(projectShouldChange);
            destRepo.Setup(d => d.GetProjectById(2)).Returns(projectShouldNotChange);
            var destServ = new DestinationService(destRepo.Object, mapper);
            //act
            destServ.CloseProject(1);
            destServ.CloseProject(2);
            //assert
            projectShouldChange.ProjectStatusId.Should().Be(2);
            projectShouldNotChange.ProjectStatusId.Should().Be(2);
        }

        [Fact]
        public void ShouldRetrunDestinationVmForEdit()
        {
            var destination = new Destination()
            {
                Id = 1,
                CountryId = 1,
                Name = "test",
                CreatedDateTime = DateTime.Now
            };
            var countries = new List<Country>() { new Country() { Id = 1, Name = "test", SubsistanceAllowenceId = 1 } };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetDestinationById(1)).Returns(destination);
            destRepo.Setup(d => d.GetAllCountries()).Returns(countries.AsQueryable());
            var destServ = new DestinationService(destRepo.Object, mapper);

            var destVmForEdit = destServ.GetDestinationForEdit(1);

            destVmForEdit.Should().BeOfType(typeof(NewDestinationVm));
            destVmForEdit.Should().NotBeNull();
            destVmForEdit.Countries.Should().AllBeOfType(typeof(CountryVm));
            destVmForEdit.Countries.Should().HaveCount(1);
        }

        [Fact]
        public void ShouldReturnProjectVmForEdit()
        {
            var project = new Project()
            {
                Id = 1,
                ProjectStatusId = 1,
                Number = "12345",
                Name = "test",
                DestinationId = 1,
                CreatedDateTime = DateTime.Now
            };
            var destList = new List<Destination>()
            {
                new Destination() { Id = 1, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "x", CreatedDateTime = DateTime.Now },
                new Destination() { Id = 2, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "y", CreatedDateTime = DateTime.Now },
                new Destination() { Id = 3, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "z", CreatedDateTime = DateTime.Now }
            };
            var projStatusList = new List<ProjectStatus>()
            {
                new ProjectStatus() {Id = 1, Name = "Opened"},
                new ProjectStatus() {Id = 2, Name = "Closed"}
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetDestinations()).Returns(destList.AsQueryable());
            destRepo.Setup(d => d.GetProjectStatuses()).Returns(projStatusList.AsQueryable());
            destRepo.Setup(d => d.GetProjectById(1)).Returns(project);
            var destServ = new DestinationService(destRepo.Object, mapper);

            var projVmForEdit = destServ.GetProjectForEdit(1);

            projVmForEdit.Should().BeOfType(typeof(NewProjectVm));
            projVmForEdit.Should().NotBeNull();
            projVmForEdit.Destinations.Should().AllBeOfType(typeof(DestinationTypeVm));
            projVmForEdit.Destinations.Should().HaveCount(3);
            projVmForEdit.Destinations.Should().OnlyHaveUniqueItems(d => d.Id);
            projVmForEdit.Statuses.Should().AllBeOfType(typeof(ProjectStatusVm));
            projVmForEdit.Statuses.Should().HaveCount(2);
            projVmForEdit.Statuses.Should().OnlyHaveUniqueItems(s => s.Id);

        }

        [Fact]
        public void ShouldReturnAllDestinations()
        {
            var destList = new List<Destination>()
            {
                new Destination() { Id = 1, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "x", CreatedDateTime = DateTime.Now },
                new Destination() { Id = 2, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "y", CreatedDateTime = DateTime.Now },
                new Destination() { Id = 3, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "z", CreatedDateTime = DateTime.Now }
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetDestinations()).Returns(destList.AsQueryable());
            var destServ = new DestinationService(destRepo.Object, mapper);

            var resultList = destServ.GetAllClients();

            resultList.Should().BeOfType(typeof(ListDestinationForListVm));
            resultList.Should().NotBeNull();
            resultList.Destinations.Should().HaveCount(3);
            resultList.Destinations.Should().OnlyHaveUniqueItems(d => d.Id);
            resultList.Count.Should().Be(3);
        }

        [Fact]
        public void ShouldReturnDestinationDetails()
        {
            var destination = new Destination()
            {
                Id = 1,
                CountryId = 1,
                Name = "test",
                CreatedDateTime = DateTime.Now
            };

            var projects = new List<Project>() { 
                new Project() { Id = 1, Name = "project1", Number = "111", Destination = destination, ProjectStatusId = 1, 
                    ProjectStatus = new ProjectStatus() { Id = 1, Name = "open" } } 
            };
            destination.Projects = projects;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetDestinationById(1)).Returns(destination);
            var destServ = new DestinationService(destRepo.Object, mapper);

            var destDetailsVm = destServ.GetDestinationDetail(1);

            destDetailsVm.Should().BeOfType(typeof(DestinationDetailVm));
            destDetailsVm.Should().NotBeNull();
            destDetailsVm.Projects.Should().BeOfType(typeof(ListProjectForList));
            destDetailsVm.Projects.Should().NotBeNull();
            destDetailsVm.Projects.Projects.Should().AllBeOfType(typeof(ProjectForListVm));
            destDetailsVm.Projects.Projects.Should().HaveCount(1);
            destDetailsVm.Projects.Count.Should().Be(1);
        }

        [Fact]
        public void ShouldReturnDestinations()
        {
            var destList = new List<Destination>()
            {
                new Destination() { Id = 1, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "x", CreatedDateTime = DateTime.Now },
                new Destination() { Id = 2, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "y", CreatedDateTime = DateTime.Now },
                new Destination() { Id = 3, CountryId = 1, Country = new Country() { Id = 1, Name ="country", SubsistanceAllowenceId = 1 }, Name = "z", CreatedDateTime = DateTime.Now }
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetDestinations()).Returns(destList.AsQueryable());
            var destServ = new DestinationService(destRepo.Object, mapper);

            var resultList = destServ.GetDestinations();

            resultList.Should().NotBeNull();
            resultList.Should().AllBeOfType(typeof(DestinationTypeVm));
            resultList.Should().HaveCount(3);
            resultList.Should().OnlyHaveUniqueItems(d => d.Id);
        }

        [Fact]
        public void ShouldReturnProjectStatuses()
        {
            var statuses = new List<ProjectStatus>()
            {
                new ProjectStatus() { Id = 1, Name = "open" },
                new ProjectStatus() { Id = 2, Name = "closed" }
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetProjectStatuses()).Returns(statuses.AsQueryable());
            var destServ = new DestinationService(destRepo.Object, mapper);

            var resultList = destServ.GetProjectStatuses();

            resultList.Should().NotBeNullOrEmpty();
            resultList.Should().AllBeOfType(typeof(ProjectStatusVm));
            resultList.Should().HaveCount(2);
            resultList.Should().OnlyHaveUniqueItems(ps => ps.Id);
        }

        [Fact]
        public void ShouldReturnCountries()
        {
            var countries = new List<Country>()
            {
                new Country() { Id = 1, Name = "country1", SubsistanceAllowenceId = 1},
                new Country() { Id = 2, Name = "country2", SubsistanceAllowenceId = 1},
                new Country() { Id = 3, Name = "country3", SubsistanceAllowenceId = 1}
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var destRepo = new Mock<IDestinationRepository>();
            destRepo.Setup(d => d.GetAllCountries()).Returns(countries.AsQueryable());
            var destServ = new DestinationService(destRepo.Object, mapper);

            var resultList = destServ.GetCountries();

            resultList.Should().NotBeNullOrEmpty();
            resultList.Should().AllBeOfType(typeof(CountryVm));
            resultList.Should().HaveCount(3);
            resultList.Should().OnlyHaveUniqueItems(ps => ps.Id);
        }
    }
}
