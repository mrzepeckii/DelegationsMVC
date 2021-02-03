using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AutoMapper;
using DelegationsMVC.Domain.Model;
using DelegationsMVC.Application.Mapping;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Application.Services;
using FluentAssertions;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using System.Linq;
using DelegationsMVC.Application.ViewModels.DestinationVm;

namespace DelegationsMVC.Tests
{
    public class DelegationServiceTests
    {
        private Delegation SetDelegation()
        {
            var routes = new List<Route>() { new Route { Id = 1, CreatedDateTime = DateTime.Now, DelegationId = 1, RouteTypeId = 1, TypeOfTransportId = 1, 
                RouteDetail = new RouteDetail() { Id = 1, RouteRef = 1, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now,
                    StartPoint = "start", EndPoint = "end", Kilometers = 35 } } };
            var costs = new List<Cost>() { new Cost() { Id = 1, Amount = 300, CostTypeId = 1, DelegationId = 1 }, new Cost() { Id = 2, Amount = 100, DelegationId = 1, CostTypeId = 2 } };
            var delegation = new Delegation()
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                DelegationStatusId = 1,
                DelegationStatus = new DelegationStatus() { Id = 1, Name = "open" },
                DestinationId = 1,
                Destination = new Destination() { Id = 1, Name = "testDestination"},
                EmployeeId = 1,
                Employee = new Employee() { Id = 1, FirstName = "testName", LastName = "testSurname"},
                Purpose = "purpose",
                Routes = routes,
                Costs = costs
            };
            return delegation;
        }

        [Fact]
        public void ShouldReturnDelegation()
        {
            //arrange
            var delegation = SetDelegation();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetDelegationById(1)).Returns(delegation);
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);
            //act
            var delResult = delServ.GetDelegationById(1);
            //assert
            delResult.Should().BeOfType(typeof(Delegation));
            delResult.Should().NotBeNull();
            delResult.Should().BeSameAs(delegation);
        }

        [Fact]
        public void ShouldReturnDelegationVmToEdit()
        {
            var delegation = SetDelegation();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetDelegationById(1)).Returns(delegation);
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);
            var delVmResult = delServ.GetDelegationForEdit(1);
            delVmResult.Should().BeOfType(typeof(NewDelegationVm));
            delVmResult.Should().NotBeNull();
            delVmResult.Routes.Should().HaveCount(1);
            delVmResult.Routes.Should().AllBeOfType(typeof(NewRouteVm));
            delVmResult.Costs.Should().HaveCount(2);
            delVmResult.Costs.Should().AllBeOfType(typeof(NewCostVm));
            delVmResult.Costs.Should().OnlyHaveUniqueItems(c => c.CostId);
            delVmResult.Costs.ForEach(c => c.DelegationId.Should().Be(delegation.Id));
        }

        [Fact]
        public void ShouldReturnRoute()
        {
            var route = new Route
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                DelegationId = 1,
                RouteTypeId = 1,
                TypeOfTransportId = 1,
                RouteDetail = new RouteDetail()
                {
                    Id = 1,
                    RouteRef = 1,
                    VehicleId = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    StartPoint = "start",
                    EndPoint = "end",
                    Kilometers = 35
                }
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetRouteById(1)).Returns(route);
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);
            var routeResult = delServ.GetRouteById(1);
            routeResult.Should().BeOfType(typeof(Route));
            routeResult.Should().NotBeNull();
            routeResult.Should().BeSameAs(route);
        }

        [Fact]
        public void ShouldReturnRouteForEdit()
        {
            var route = new Route
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                DelegationId = 1,
                RouteTypeId = 1,
                TypeOfTransportId = 1,
                RouteDetail = new RouteDetail()
                {
                    Id = 1,
                    RouteRef = 1,
                    VehicleId = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    StartPoint = "start",
                    EndPoint = "end",
                    Kilometers = 35
                }
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetRouteById(1)).Returns(route);
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);
            var routeResult = delServ.GetRouteForEdit(1);
            routeResult.Should().BeOfType(typeof(NewRouteVm));
            routeResult.Should().NotBeNull();
            routeResult.RouteDetail.Should().BeOfType(typeof(NewRouteDetailVm));
            routeResult.RouteDetail.Should().NotBeNull();
        }

        [Fact]
        public void ShouldReturnDelegationDetailsVm()
        {
            var del = SetDelegation();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetDelegationById(1)).Returns(del);
            delRepo.Setup(d => d.GetRouteById(1)).Returns(del.Routes.FirstOrDefault(r => r.Id == 1));
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);
            var delDetailsVmResult = delServ.GetDelegationDetails(1);
            delDetailsVmResult.Should().BeOfType(typeof(DelegationDetailVm));
            delDetailsVmResult.Should().NotBeNull();
            delDetailsVmResult.Costs.Should().AllBeOfType(typeof(CostForListVm));
            delDetailsVmResult.Costs.Should().NotBeNullOrEmpty();
            delDetailsVmResult.Routes.Should().AllBeOfType(typeof(RouteForListVm));
            delDetailsVmResult.Routes.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ShouldChangeStatusOfDelegation()
        {
            var del = new Delegation() { Id = 1, DelegationStatusId = 1 };
            var delWithAccounant = new Delegation() { Id = 2, DelegationStatusId = 3, AccoutantApprovedDate = DateTime.Now };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetDelegationById(1)).Returns(del);
            delRepo.Setup(d => d.GetDelegationById(2)).Returns(delWithAccounant);
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);
            var delShouldBeChanged = delServ.ChangeStatusOfDelegation(1, 2);
            var delShouldNotBeChanged = delServ.ChangeStatusOfDelegation(1, 1); // the same status 
            var delShouldNotBeChangeAccountantDate = delServ.ChangeStatusOfDelegation(1, 4); // try to change status to paid without accountant acceptance
            var delShouldBeChangedAccounantDate = delServ.ChangeStatusOfDelegation(2, 4); // try to change status to  paid with accounant accpetnace 

            delShouldBeChanged.Should().BeTrue();
            delShouldNotBeChanged.Should().BeFalse();
            delShouldNotBeChangeAccountantDate.Should().BeFalse();
            delShouldBeChangedAccounantDate.Should().BeTrue();
        }

        [Fact]
        public void ShouldCaluclateAndSetAllowence()
        {
            var delWith13H = SetDelegation();
            var delWith9H = SetDelegation();
            var delWith3H = SetDelegation();
           
            var routes13H = new List<Route>() { new Route { Id = 1, CreatedDateTime = DateTime.Now, DelegationId = 1, RouteTypeId = 1, TypeOfTransportId = 1,
                RouteDetail = new RouteDetail() { Id = 1, RouteRef = 1, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now,
                    StartPoint = "start", EndPoint = "end", Kilometers = 35 } },
            new Route { Id = 11, CreatedDateTime = DateTime.Now, DelegationId = 1, RouteTypeId = 2, TypeOfTransportId = 1,
                RouteDetail = new RouteDetail() { Id = 11, RouteRef = 11, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(13),
                    StartPoint = "start", EndPoint = "end", Kilometers = 35 } }};
            delWith13H.Routes = routes13H;
            var routes9h = new List<Route>() { new Route { Id = 2, CreatedDateTime = DateTime.Now, DelegationId = 2, RouteTypeId = 1, TypeOfTransportId = 1,
                RouteDetail = new RouteDetail() { Id = 2, RouteRef = 2, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now,
                    StartPoint = "start", EndPoint = "end", Kilometers = 100 } },
            new Route { Id = 22, CreatedDateTime = DateTime.Now, DelegationId = 2, RouteTypeId = 2, TypeOfTransportId = 1,
                RouteDetail = new RouteDetail() { Id = 22, RouteRef = 22, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(9),
                    StartPoint = "start", EndPoint = "end", Kilometers = 100 } }};
            delWith9H.Id = 2;
            delWith9H.Routes = routes9h;
            var routes3h = new List<Route>() { new Route { Id = 3, CreatedDateTime = DateTime.Now, DelegationId = 3, RouteTypeId = 1, TypeOfTransportId = 1,
                RouteDetail = new RouteDetail() { Id = 3, RouteRef = 3, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now,
                    StartPoint = "start", EndPoint = "end", Kilometers = 23 } },
            new Route { Id = 33, CreatedDateTime = DateTime.Now, DelegationId = 3, RouteTypeId = 2, TypeOfTransportId = 1,
                RouteDetail = new RouteDetail() { Id = 33, RouteRef = 33, VehicleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(3),
                    StartPoint = "start", EndPoint = "end", Kilometers = 43 } }};
            delWith3H.Id = 3;
            delWith3H.Routes = routes3h;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetDelegationById(1)).Returns(delWith13H);
            delRepo.Setup(d => d.GetRouteById(1)).Returns(delWith13H.Routes.FirstOrDefault(r => r.Id == 1));
            delRepo.Setup(d => d.GetDelegationById(2)).Returns(delWith9H);
            delRepo.Setup(d => d.GetRouteById(2)).Returns(delWith9H.Routes.FirstOrDefault(r => r.Id == 2));
            delRepo.Setup(d => d.GetDelegationById(3)).Returns(delWith3H);
            delRepo.Setup(d => d.GetRouteById(3)).Returns(delWith3H.Routes.FirstOrDefault(r => r.Id == 3));
            delRepo.Setup(d => d.GetRouteById(11)).Returns(delWith13H.Routes.FirstOrDefault(r => r.Id == 11));
            delRepo.Setup(d => d.GetRouteById(22)).Returns(delWith9H.Routes.FirstOrDefault(r => r.Id == 22));
            delRepo.Setup(d => d.GetRouteById(33)).Returns(delWith3H.Routes.FirstOrDefault(r => r.Id == 33));
            delRepo.Setup(d => d.GetSubsistanceAllowenceByDel(1)).Returns(30M);
            delRepo.Setup(d => d.GetSubsistanceAllowenceByDel(2)).Returns(30M);
            delRepo.Setup(d => d.GetSubsistanceAllowenceByDel(3)).Returns(30M);
            var vehRepo = new Mock<IVehicleRepository>();
            vehRepo.Setup(v => v.GetMilleageAllowenceByVehicle(2)).Returns(0.8395M);
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);

            var delWith13HResult = delServ.GetDelegationDetails(1);
            var delWith9HResult = delServ.GetDelegationDetails(2);
            var delWith3HResult = delServ.GetDelegationDetails(3);

            delWith13HResult.SubsistenceAllowence.Should().Be(30);
            delWith13HResult.MileageAllowence.Should().BeGreaterThan(50);
            delWith9HResult.SubsistenceAllowence.Should().Be(15);
            delWith9HResult.MileageAllowence.Should().BeGreaterThan(167);
            delWith3HResult.SubsistenceAllowence.Should().Be(0);
            delWith3HResult.MileageAllowence.Should().BeGreaterThan(55);
        }

        [Fact]
        public void ShouldReturnAllClients()
        {
            var country = new Country() { Id = 1, Name = "testCountry", SubsistanceAllowenceId = 1 };
            var clients = new List<Destination>() { new Destination() { Id = 1, CountryId = 1, Name = "first", CreatedDateTime = DateTime.Now, Country = country},
                                                    new Destination() {Id = 2, CountryId = 1, Name = "second", CreatedDateTime = DateTime.Now, Country = country}};
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetAllDestinations()).Returns(clients.AsQueryable());
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);

            var resultList = delServ.GetAllDestinations();

            resultList.Should().AllBeOfType(typeof(DestinationTypeVm));
            resultList.Should().NotBeEmpty();
            resultList.Should().HaveCount(2);
        }

        [Fact]
        public void ShouldReturnRouteTypes()
        {
            var types = new List<RouteType>() { new RouteType() { Id = 1, Name = "firstType" }, 
                                                new RouteType() { Id = 2, Name = "secondType" } };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetAllRouteTypes()).Returns(types.AsQueryable());
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);

            var resultList = delServ.GetRouteTypes();

            resultList.Should().AllBeOfType(typeof(RouteTypeVm));
            resultList.Should().NotBeEmpty();
            resultList.Should().HaveCount(2);
        }

        [Fact]
        public void ShouldReturnTransportTypes()
        {
            var types = new List<TransportType> { new TransportType() { Id = 1, Name = "firstType"},
                                                    new TransportType(){Id = 2, Name= "secondType"}};
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetAllTransportTypes()).Returns(types.AsQueryable());
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);

            var resultList = delServ.GetTransportTypes();

            resultList.Should().AllBeOfType(typeof(TransportTypeVm));
            resultList.Should().NotBeEmpty();
            resultList.Should().HaveCount(2);
        }

        [Fact]
        public void ShouldReturnAllDelegations()
        {
            var delegations = new List<Delegation>() { SetDelegation(), SetDelegation(), SetDelegation() };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetAllDelegations()).Returns(delegations.AsQueryable());
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);

            var resultList = delServ.GetAllDelegationsForList();

            resultList.Should().BeOfType(typeof(ListDelegationForListVm));
            resultList.Should().NotBeNull();
            resultList.Count.Should().Be(3);
            resultList.Delegations.Should().AllBeOfType(typeof(DelegationForListVm));
            resultList.Delegations.Should().HaveCount(3);
        }

        [Fact]
        public void ShouldReturnEmployeesDelegations()
        {
            var delegations = new List<Delegation>() { SetDelegation(), SetDelegation(), SetDelegation() };
            var emp = new Employee() { Id = 1, Delegations = delegations };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var delRepo = new Mock<IDelegationRepository>();
            delRepo.Setup(d => d.GetDelegationsByEmployee(1)).Returns(emp.Delegations.AsQueryable());
            var vehRepo = new Mock<IVehicleRepository>();
            var empRepo = new Mock<IEmployeeRepository>();
            var delServ = new DelegationService(delRepo.Object, empRepo.Object, vehRepo.Object, mapper);

            var resultList = delServ.GetDelegationsByEmployee(1);

            resultList.Should().BeOfType(typeof(ListDelegationForListVm));
            resultList.Should().NotBeNull();
            resultList.Count.Should().Be(3);
            resultList.Delegations.Should().AllBeOfType(typeof(DelegationForListVm));
            resultList.Delegations.Should().HaveCount(3);
        }
    }
}
