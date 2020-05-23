using System;
using System.Collections.Generic;
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
    public class CreateProgressReportControllerUnitTests
    {
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        public CreateProgressReportControllerUnitTests()
        {
            _contextOptions = new CreateProgressReportControllerUnitTestDatabaseSetup().SetupDatabase();
        }

        [Fact]
        public async void GetProgressReport_GetsProgressReportForCitizen()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createProgressReportController = new CreateProgressReportController(context);
                var id = 2905891233;

                //Act
                var progressReportViewModel = await createProgressReportController.GetProgressReport(id);

                //Assert
                Assert.NotNull(progressReportViewModel);
                var actionResult = Assert.IsType<ActionResult<ProgressReportViewModel>>(progressReportViewModel);
                var prvm = Assert.IsType<ProgressReportViewModel>(actionResult.Value);

                Assert.Equal(id, prvm.CPR);
                Assert.Equal("Citizen Citizensen", prvm.Name);
                Assert.Equal("Kærgården", prvm.RespiteCareHomeName);
                Assert.Equal(new DateTime(2021, 01, 29), prvm.PlannedDischargeDate);
            }
        }
    }
}
