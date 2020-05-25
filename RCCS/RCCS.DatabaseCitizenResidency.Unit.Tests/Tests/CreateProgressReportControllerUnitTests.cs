using System;
using System.Collections.Generic;
using System.Text;
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
    public class CreateProgressReportControllerUnitTests
    {
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        public CreateProgressReportControllerUnitTests()
        {
            _contextOptions = new CreateProgressReportControllerUnitTestDatabaseSetup().SetupDatabase();
        }

        [Fact]
        public async void GetProgressReport_GetsProgressReportDataForCitizen()
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

        [Fact]
        public async void PostProgressReport_PostsProgressReport()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createProgressReportController = new CreateProgressReportController(context);
                var createProgressViewModel = new CreateProgressReportViewModel
                {
                    CPR = 2905891233,
                    Title = "Progressreport title",
                    Report = "Test report",
                    ResponsibleCaretaker = "Me"
                };

                //Act
                var progressReportViewModel = await createProgressReportController.PostProgressReport(createProgressViewModel);

                //Assert
                Assert.NotNull(progressReportViewModel);
                var actionResult = Assert.IsType<ActionResult<ProgressReport>>(progressReportViewModel);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.Equal("PostProgressReport", createdAtActionResult.ActionName);
                var progressReportCreated = Assert.IsType<ProgressReport>(createdAtActionResult.Value);

                Assert.True(createProgressViewModel.CPR.Equals(progressReportCreated.CitizenCPR));
                Assert.True(createProgressViewModel.Title.Equals(progressReportCreated.Title));
                Assert.True(createProgressViewModel.Report.Equals(progressReportCreated.Report));
                Assert.True(createProgressViewModel.ResponsibleCaretaker.Equals(progressReportCreated.ResponsibleCaretaker));
            }
        }
    }
}
