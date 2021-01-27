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

namespace DelegationsMVC.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void ShouldReturnEmployeeWithVehicles()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var vehicles = new List<Vehicle>() { new Vehicle() { Id = 1, CreatedDateTime = DateTime.Now, EmployeeId = 1,
                EngineTypeId = 2, PlateNumbers = "DW33221" } };
            var emp = new Employee()
            {
                Id = 1,
                FirstName = "TestName",
                LastName = "TestSurname",
                UserId = "1",
                BankAccountCode = "12345",
                EmployeeTypeId = 1,
                CreatedDateTime = DateTime.Now,
                Vehicles = vehicles
            };
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
            var vehicles = new List<Vehicle>() { new Vehicle() { Id = 1, CreatedDateTime = DateTime.Now, EmployeeId = 1,
                EngineTypeId = 2, PlateNumbers = "DW33221" } };
            var contacts = new List<ContactDetail>() { new ContactDetail() { Id = 1, ContactDetailInformation = "123456789", ContactDetailTypeId = 1,
                EmployeeId = 1}, new ContactDetail() {Id = 2, ContactDetailInformation = "email@email.pl", ContactDetailTypeId = 2, EmployeeId = 1},
                 new ContactDetail() {Id = 3, ContactDetailInformation = "email@email.pl", ContactDetailTypeId = 2, EmployeeId = 1}};
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
            var empRepo = new Mock<IEmployeeRepository>();
            empRepo.Setup(e => e.GetEmployeeById(1)).Returns(emp);
            empRepo.Setup(e => e.GetEmployeeByUserId("1")).Returns(emp);
            var vehRepo = new Mock<IVehicleRepository>();
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
    }
}
