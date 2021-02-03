using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation.TestHelper;

namespace DelegationsMVC.Tests.Validations
{
    public class DelegationValidatorsTests
    {
        private readonly NewDelegationValidation _delValidator = new NewDelegationValidation();
        private readonly NewRouteDetailValidation _routeDetValidator = new NewRouteDetailValidation();
        private NewDelegationVm _delVm = new NewDelegationVm();
        private NewRouteDetailVm _routeDetVm = new NewRouteDetailVm();

        //delegationVm
        [Fact]
        public void ShouldReturnErrorWithEmptyDelegationPurpose()
        {
            _delVm.Purpose = "";
            var result = _delValidator.TestValidate(_delVm);
            result.ShouldHaveValidationErrorFor("Purpose");
        }

        [Fact]
        public void ShouldNotReturnErrorWithDelegationPurpose()
        {
            _delVm.Purpose = "purpose";
            var result = _delValidator.TestValidate(_delVm);
            result.ShouldNotHaveValidationErrorFor("Purpose");
        }
        
        //routeDetailVm
        [Fact]
        public void ShouldReturnErrorWithEmptyRouteStartPoint()
        {
            _routeDetVm.StartPoint = "";
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldHaveValidationErrorFor("StartPoint");
        }

        [Fact]
        public void ShouldNotReturnErrorWithRouteStartPoint()
        {
            _routeDetVm.StartPoint = "StartPoint";
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldNotHaveValidationErrorFor("StartPoint");
        }

        [Fact]
        public void ShouldReturnErrorWithEmptyRouteEndPoint()
        {
            _routeDetVm.EndPoint = "";
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldHaveValidationErrorFor("StartPoint");
        }

        [Fact]
        public void ShouldReturnErrorWithTheSameRouteEndPoint()
        {
            _routeDetVm.StartPoint = "Point";
            _routeDetVm.EndPoint = "Point";
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldHaveValidationErrorFor("EndPoint");
        }

        [Fact]
        public void ShouldNotReturnErrorWithRouteEndPoint()
        {
            _routeDetVm.StartPoint = "StartPoint";
            _routeDetVm.EndPoint = "EndPoint";
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldNotHaveValidationErrorFor("EndPoint");
        }

        [Fact]
        public void ShouldReturnErrorWithTheSameRouteStartAndEndDate()
        {
            _routeDetVm.StartDate = DateTime.Now;
            _routeDetVm.EndDate = _routeDetVm.StartDate;
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldHaveValidationErrorFor("StartDate");
            result.ShouldHaveValidationErrorFor("EndDate");
        }

        [Fact]
        public void ShouldNotReturnErrorWithTheSameRouteStartAndEndDate()
        {
            _routeDetVm.StartDate = DateTime.Now;
            _routeDetVm.EndDate = DateTime.Now.AddMinutes(1);
            var result = _routeDetValidator.TestValidate(_routeDetVm);
            result.ShouldNotHaveValidationErrorFor("StartDate");
            result.ShouldNotHaveValidationErrorFor("EndDate");
        }
    }
}
