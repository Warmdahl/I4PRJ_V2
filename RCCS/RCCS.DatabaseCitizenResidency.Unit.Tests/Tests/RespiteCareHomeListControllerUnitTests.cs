using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseAPI.RCCSCitizenResidencyDbViewControllers;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Unit.Tests.Utilities;
using RCCS.DatabaseCitizenResidency.ViewModel;
using Xunit;

namespace RCCS.DatabaseCitizenResidency.Unit.Tests.Tests
{
    /*
    * Tests inspired from https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
    * Written by Steve Smith and its contributors
    */
    public class RespiteCareHomeListControllerUnitTests
    {
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        public RespiteCareHomeListControllerUnitTests()
        {
           _contextOptions = new RespiteCareHomeListUnitTestDatabaseSetup().SetupDatabase();
        }

        [Fact]
        public async void GetRespiteCareHomeList()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var respiteCareHomeController = new RespiteCareHomeListController(context);

                //Act
                var respiteCareHomeListViewModel = await respiteCareHomeController.GetRespiteCareHomeList();

                //Assert
                Assert.NotNull(respiteCareHomeListViewModel);
                var actionResult = Assert.IsType<ActionResult<IEnumerable<RespiteCareHomeListViewModel>>>(respiteCareHomeListViewModel);
                Assert.IsAssignableFrom<IEnumerable<RespiteCareHomeListViewModel>>(actionResult.Value);
                var respiteCareHomeListViewModels = actionResult.Value.Count();
                Assert.True(!respiteCareHomeListViewModels.Equals(0));
            }
        }
    }
}
