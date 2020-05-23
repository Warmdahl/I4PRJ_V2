using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CitizenListControllerUnitTests
    {
        /*
         * Tests inspired from https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
         * Written by Steve Smith and its contributors and its contributors
         */
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        //Setup
        public CitizenListControllerUnitTests()
        {
            _contextOptions = new CitizenListControllerUnitTestDatabaseSetup().SetupDatabase();
        }

        [Fact]
        public async void GetCitizenList_ReturnsCitizenList()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var citizenListController = new CitizenListController(context);

                //Act
                var citizenListViewModel = await citizenListController.GetCitizenList();

                //Assert
                Assert.NotNull(citizenListViewModel);
                var actionResult = Assert.IsType<ActionResult<IEnumerable<CitizenListViewModel>>>(citizenListViewModel);
                Assert.IsAssignableFrom<IEnumerable<CitizenListViewModel>>(actionResult.Value);
                var citizenListViewModels = actionResult.Value.Count();
                Assert.True(!citizenListViewModels.Equals(0));
            }
        }
    }
}
