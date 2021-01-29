using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using FluentAssertions;
using DelegationsMVC.Domain.Interfaces;
using DelegationsMVC.Application.Services;
using AutoMapper;
using DelegationsMVC.Domain.Model;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Application.Mapping;
using System.Linq;

namespace DelegationsMVC.Tests
{
    public class EmployeeServiceTests
    {
        private Employee SetEmployee()
        {
            var vehicles = new List<Vehicle>() { new Vehicle() { Id = 1, CreatedDateTime = DateTime.Now, EmployeeId = 1,
                EngineTypeId = 2, PlateNumbers = "DW33221" } };
            var contacts = new List<ContactDetail>() { new ContactDetail() { Id = 1, ContactDetailInformation = "123456789", ContactDetailTypeId = 2,
                EmployeeId = 1}, new ContactDetail() {Id = 2, ContactDetailInformation = "email@email.pl", ContactDetailTypeId = 1, EmployeeId = 1},
                 new ContactDetail() {Id = 3, ContactDetailInformation = "email@email.pl", ContactDetailTypeId = 1, EmployeeId = 1}};

            var emp = new Employee()
            {
                Id = 1,
                FirstName = "TestName",
                LastName = "TestSurname",
                UserId = "1",
                BankAccountCode = "12345",
                EmployeeTypeId = 1,
                CreatedDateTime = DateTime.Now,
                Vehicles = vehicles,
                ContactDetails = contacts
            };
            return emp;
        }


        [Fact]
        public void ShouldReturnEmployeeWithVehicles()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var vehicles = new List<Vehicle>() { new Vehicle() { Id = 1, CreatedDateTime = DateTime.Now, EmployeeId = 1,
                EngineTypeId = 2, PlateNumbers = "DW33221" } };
            var emp = SetEmployee();
            var empRepo = new Mock<IEmployeeRepository>();
            empRepo.Setup(e => e.GetEmployeeById(1)).Returns(emp);
            var vehRepo = new Mock<IVehicleRepository>();
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper.Object);

            //Act
            var empResultGetById = empServ.GetEmployeeById(1);
            var empResultGetByUserId = empServ.GetEmployeeByUserId("1");

            //Assert
            empResultGetById.Should().BeOfType(typeof(Employee));
            empResultGetById.Should().NotBeNull();
            empResultGetById.Vehicles.Should().HaveCount(1);
            empResultGetById.Should().BeSameAs(emp);

            empResultGetById.Should().BeSameAs(empResultGetById);
        }

        [Fact]
        public void ShouldReturnEmployeeVmWithDetails()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();

            var emp = SetEmployee();

            var empRepo = new Mock<IEmployeeRepository>();
            empRepo.Setup(e => e.GetEmployeeById(1)).Returns(emp);
            empRepo.Setup(e => e.GetEmployeeByUserId("1")).Returns(emp);
            var vehRepo = new Mock<IVehicleRepository>();
            vehRepo.Setup(v => v.GetVehiclesByEmployee(1)).Returns(emp.Vehicles.AsQueryable());
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);


            //Act
            var empDetailsGetById = empServ.GetEmployeeDetails(1);
            var empDetailsGetByUserId = empServ.GetEmployeeDetails("1");

            //Assert 
            empDetailsGetById.Should().BeOfType(typeof(EmployeeDetailVm));
            empDetailsGetById.Should().NotBeNull();
            empDetailsGetById.Vehicles.Should().HaveCount(1);
            empDetailsGetById.Emails.Should().HaveCount(2);
            empDetailsGetById.Emails.Should().AllBeOfType(typeof(ContactDetailsForListVm));
            empDetailsGetById.Emails.Should().OnlyHaveUniqueItems(e => e.Id);
            empDetailsGetById.PhoneNumbers.Should().HaveCount(1);
            empDetailsGetById.PhoneNumbers.Should().AllBeOfType(typeof(ContactDetailsForListVm));
            empDetailsGetById.Position.Should().Be("Prezes");
            empDetailsGetById.FullName.Should().Be("TestName TestSurname");

            empDetailsGetByUserId.Should().BeSameAs(empDetailsGetById);
        }

        [Fact]
        public void ShouldReturnEmployeeForEditVm()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();

            var emp = SetEmployee();

            var empRepo = new Mock<IEmployeeRepository>();
            empRepo.Setup(e => e.GetEmployeeById(1)).Returns(emp);
            empRepo.Setup(e => e.GetEmployeeByUserId("1")).Returns(emp);
            var vehRepo = new Mock<IVehicleRepository>();
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);

            //Act
            var empForEdit = empServ.GetEmployeeForEdit("1");

            //Assert
            empForEdit.Should().BeOfType(typeof(NewEmployeeVm));
            empForEdit.Should().NotBeNull();
            empForEdit.ContactDetails.Should().HaveCount(3);
            empForEdit.ContactDetails.Should().AllBeOfType(typeof(NewContactDetailsVm));
            empForEdit.ContactDetails.Should().OnlyHaveUniqueItems(cd => cd.Id);
            empForEdit.Vehicles.Should().HaveCount(1);
            empForEdit.Vehicles.Should().AllBeOfType(typeof(NewVehicleVm));
            empForEdit.FirstName.Should().BeSameAs(emp.FirstName);
            empForEdit.LastName.Should().BeSameAs(emp.LastName);
            empForEdit.BankAccountCode.Should().BeSameAs(emp.BankAccountCode);
            empForEdit.EmployeeTypeId.Should().Be(emp.EmployeeTypeId);
        }

        [Fact]
        public void ShouldReturnVehicle()
        {
            //arrange
            var vehicle = new Vehicle()
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                EmployeeId = 1,
                EngineTypeId = 2,
                PlateNumbers = "DW33221"
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var empRepo = new Mock<IEmployeeRepository>();
            var vehRepo = new Mock<IVehicleRepository>();
            vehRepo.Setup(v => v.GetVehicleById(1)).Returns(vehicle);
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);
            //act
            var vehResult = empServ.GetVehicleById(1);

            //assert
            vehResult.Should().BeOfType(typeof(Vehicle));
            vehResult.Should().NotBeNull();
            vehResult.Should().BeSameAs(vehicle);
        }

        [Fact]
        public void ShouldRetrunVehicleForEditVm()
        {
            //arrange
            var vehicle = new Vehicle()
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                EmployeeId = 1,
                EngineTypeId = 2,
                PlateNumbers = "DW33221"
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var empRepo = new Mock<IEmployeeRepository>();
            var vehRepo = new Mock<IVehicleRepository>();
            vehRepo.Setup(v => v.GetVehicleById(1)).Returns(vehicle);
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);
            //act
            var vehForEdit = empServ.GetVehicleForEdit(1);

            //assert
            vehForEdit.Should().BeOfType(typeof(NewVehicleVm));
            vehForEdit.Should().NotBeNull();
            vehForEdit.PlateNumbers.Should().BeSameAs(vehicle.PlateNumbers);
            vehForEdit.EngineTypeId.Should().Be(vehicle.EngineTypeId);
            vehForEdit.EmployeeId.Should().Be(vehicle.EmployeeId);
        }

        [Fact]
        public void ShouldReturnContactDetails()
        {
            var contact = new ContactDetail()
            {
                Id = 1,
                EmployeeId = 1,
                ContactDetailTypeId = 2,
                ContactDetailInformation = "12345"
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var empRepo = new Mock<IEmployeeRepository>();
            empRepo.Setup(e => e.GetContactDetailById(1)).Returns(contact);
            var vehRepo = new Mock<IVehicleRepository>();
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);

            var conResult = empServ.GetContactDetailById(1);

            conResult.Should().BeOfType(typeof(ContactDetail));
            conResult.Should().NotBeNull();
            conResult.Should().BeSameAs(contact);
        }

        [Fact]
        public void ShouldReturnContactDetailsForEditVm()
        {
            var contact = new ContactDetail()
            {
                Id = 1,
                EmployeeId = 1,
                ContactDetailTypeId = 2,
                ContactDetailInformation = "12345"
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var empRepo = new Mock<IEmployeeRepository>();
            empRepo.Setup(e => e.GetContactDetailById(1)).Returns(contact);
            var vehRepo = new Mock<IVehicleRepository>();
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);

            var conForEdit = empServ.GetContactForEdit(1);

            conForEdit.Should().BeOfType(typeof(NewContactDetailsVm));
            conForEdit.Should().NotBeNull();
            conForEdit.EmployeeId.Should().Be(contact.EmployeeId);
            conForEdit.ContactDetailTypeId.Should().Be(contact.ContactDetailTypeId);
            conForEdit.ContactDetailInformation.Should().BeSameAs(contact.ContactDetailInformation);
        }

        [Fact]
        public void ShouldRemoveVehicleWithNullPlatesFromList()
        {
            var vehicles = new List<NewVehicleVm>() { new NewVehicleVm() { Id = 1,  EmployeeId = 1,
                EngineTypeId = 2, PlateNumbers = "DW33221" },
            new NewVehicleVm() { Id = 2,  EmployeeId = 1,
                EngineTypeId = 2, PlateNumbers = null } };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var empRepo = new Mock<IEmployeeRepository>();
            var vehRepo = new Mock<IVehicleRepository>();
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);

            var resultList = empServ.CheckVehiclesList(vehicles);

            resultList.ForEach(v => v.PlateNumbers.Should().NotBeNullOrEmpty());
            resultList.Should().HaveCount(1); 
        }

        [Fact]
        public void ShouldRemoveContactsWithNullDetailsFromList()
        {
            var contacts = new List<NewContactDetailsVm>() { new NewContactDetailsVm() { Id = 1, ContactDetailInformation = "123456789", ContactDetailTypeId = 2,
                EmployeeId = 1}, new NewContactDetailsVm() {Id = 2, ContactDetailInformation = "email@email.pl", ContactDetailTypeId = 1, EmployeeId = 1},
                 new NewContactDetailsVm() {Id = 3, ContactDetailInformation = null, ContactDetailTypeId = 1, EmployeeId = 1}};
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var empRepo = new Mock<IEmployeeRepository>();
            var vehRepo = new Mock<IVehicleRepository>();
            var empServ = new EmployeeService(empRepo.Object, vehRepo.Object, mapper);

            var resultList = empServ.CheckContactsList(contacts);

            resultList.ForEach(v => v.ContactDetailInformation.Should().NotBeNullOrEmpty());
            resultList.Should().HaveCount(2);
        }
    }
}
