using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseAPI.RCCSCitizenResidencyDbViewControllers;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;
using RCCS.DatabaseCitizenResidency.Unit.Tests.Utilities;
using RCCS.DatabaseCitizenResidency.ViewModel;
using Xunit;

namespace RCCS.DatabaseCitizenResidency.Unit.Tests.Tests
{
    /*
     * Tests inspired from https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
     * Written by Steve Smith and its contributors
     */
    public class CitizenInformationControllerUnitTests
    {
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        //Setup
        public CitizenInformationControllerUnitTests()
        {
            _contextOptions = new CitizenInformationUnitTestDatabaseSetup().SetupDatabase();
        }

        [Fact]
        public async void GetCitizenInformation_ReturnsCitizenInformationViewModel()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var citizenInformationController = new CitizenInformationController(context);

                //Act
                var citizenInformationViewModel = 
                    await citizenInformationController
                    .GetCitizenInformation("2905891233");

                //Assert
                Assert.NotNull(citizenInformationViewModel);
                var actionResult = Assert.IsType<ActionResult<CitizenInformationViewModel>>(citizenInformationViewModel);
                var civm = Assert.IsType<CitizenInformationViewModel>(actionResult.Value);
                Assert.Equal("2905891233", civm.CPR);
            }
        }
    }
}
