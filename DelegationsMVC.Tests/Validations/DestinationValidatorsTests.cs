using DelegationsMVC.Application.ViewModels.DestinationVm;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DelegationsMVC.Tests.Validations
{
    public class DestinationValidatorsTests
    {
        private readonly NewDestinationValidation _destValidator = new NewDestinationValidation();
        private readonly NewProjectValidation _projValidator = new NewProjectValidation();
        private NewDestinationVm _destVm = new NewDestinationVm();
        private NewProjectVm _projVm = new NewProjectVm();

        //destination
        [Fact]
        public void ShouldReturnErrorWithEmptyDestinationName()
        {
            _destVm.Name = "";
            var result = _destValidator.TestValidate(_destVm);
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Fact]
        public void ShouldNotReturnErrorInDestination()
        {
            _destVm.Name = "name";
            var result = _destValidator.TestValidate(_destVm);
            result.ShouldNotHaveValidationErrorFor("Name");
        }

        //project
        [Fact]
        public void ShouldReturnErrorWithEmptyProjectName()
        {
            _projVm.Name = "";
            var result = _projValidator.TestValidate(_projVm);
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Fact]
        public void ShouldNotReturnErrorWithProjectName()
        {
            _projVm.Name = "name";
            var result = _projValidator.TestValidate(_projVm);
            result.ShouldNotHaveValidationErrorFor("Name");
        }

        [Fact]
        public void ShouldReturnErrorWithEmptyProjectNumber()
        {
            _projVm.Number = "";
            var result = _projValidator.TestValidate(_projVm);
            result.ShouldHaveValidationErrorFor("Number");
        }

        [Fact]
        public void ShouldNotReturnErrorWithProjectNumber()
        {
            _projVm.Number = "number";
            var result = _projValidator.TestValidate(_projVm);
            result.ShouldNotHaveValidationErrorFor("Number");
        }
    }
}
