using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using FluentAssertions;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using FluentValidation.TestHelper;

namespace DelegationsMVC.Tests.Validations
{
    public class EmployeeValidatorsTests
    {
        private readonly NewEmployeeValidation _empValidator = new NewEmployeeValidation();
        private readonly NewContactDetailsValidation _contactValidator = new NewContactDetailsValidation();
        private readonly NewVehicleValidation _vehValidator = new NewVehicleValidation();
        private NewEmployeeVm _empVm = new NewEmployeeVm();
        private NewContactDetailsVm _contactVm = new NewContactDetailsVm();
        private NewVehicleVm _vehVm = new NewVehicleVm();

        //employeeVm
        [Fact]
        public void ShouldReturnErrorWithoutFirstName()
        {
            _empVm.FirstName = null;
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("FirstName");
        }

        [Fact]
        public void ShouldReturnErrorWithEmptyFirstName()
        {
            _empVm.FirstName = "";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("FirstName");
        }

        [Fact]
        public void ShouldNotReturnErrorWithFirstName()
        {
            _empVm.FirstName = "First Name";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldNotHaveValidationErrorFor("FirstName");
        }

        [Fact]
        public void ShouldReturnErrorWithoutLastName()
        {
            _empVm.LastName = null;
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("LastName");
        }

        [Fact]
        public void ShouldReturnErrorWithEmptyLastName()
        {
            _empVm.LastName = "";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("LastName");
        }

        [Fact]
        public void ShouldNotReturnErrorWithLastName()
        {
            _empVm.LastName = "Last Name";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldNotHaveValidationErrorFor("LastName");
        }

        [Fact]
        public void ShouldReturnErrorWithEmptyBankAccount()
        {
            _empVm.BankAccountCode = "";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("BankAccountCode");
        }

        [Fact]
        public void ShouldReturnErrorWithLessLetterBankAccount()
        {
            _empVm.BankAccountCode = "123";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("BankAccountCode");
        }

        [Fact]
        public void ShouldReturnErrorWithMoreLetterBankAccount()
        {
            _empVm.BankAccountCode = "123456789123456789123456789";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("BankAccountCode");
        }

        [Fact]
        public void ShouldReturnErrorWithLettersInBankAccount()
        {
            _empVm.BankAccountCode = "12345678912345A78912345678";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldHaveValidationErrorFor("BankAccountCode");
        }

        [Fact]
        public void ShouldNotReturnErrorInBankAccount()
        {
            _empVm.BankAccountCode = "12345678912345678912345678";
            var result = _empValidator.TestValidate(_empVm);
            result.ShouldNotHaveValidationErrorFor("BankAccountCode");
        }

        //contactVm
        [Fact]
        public void ShouldRetrurnErrorWithEmptyInformation()
        {
            _contactVm.ContactDetailInformation = "";
            var result = _contactValidator.TestValidate(_contactVm);
            result.ShouldHaveValidationErrorFor("ContactDetailInformation");
        }

        [Fact]
        public void ShouldNotReturnErrorInInformation()
        {
            _contactVm.ContactDetailInformation = "123";
            var result = _contactValidator.TestValidate(_contactVm);
            result.ShouldNotHaveValidationErrorFor("ContactDetailInformation");
        }

        //vehcileVm
        [Fact]
        public void ShouldReturnErrorWithEmptyPlateNumbers()
        {
            _vehVm.PlateNumbers = "";
            var result = _vehValidator.TestValidate(_vehVm);
            result.ShouldHaveValidationErrorFor("PlateNumbers");
        }

        [Fact]
        public void ShouldReturnErrorWithManyLettersInPlateNumbers()
        {
            _vehVm.PlateNumbers = "123456789";
            var result = _vehValidator.TestValidate(_vehVm);
            result.ShouldHaveValidationErrorFor("PlateNumbers");
        }

        [Fact]
        public void ShouldNotReturnErrorInPlateNumbers()
        {
            _vehVm.PlateNumbers = "12345678";
            var result = _vehValidator.TestValidate(_vehVm);
            result.ShouldNotHaveValidationErrorFor("PlateNumbers");
        }
    }
}
