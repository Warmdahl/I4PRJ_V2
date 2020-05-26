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

    public class CreateCitizenControllerUnitTests
    {
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        public CreateCitizenControllerUnitTests()
        {
            _contextOptions = new CreateCitizenControllerUnitTestDatabaseSetup().SetupDatabase();
        }

        [Fact]
        public async void PostCitizen_PostsCitizenToDatabase()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 1204115947,

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = "Kærgården",
                    Type = 0
                };

                //Act
                var postedCitizen = await createCitizenController.PostCitizen(ccvm);

                //Assert
                Assert.NotNull(postedCitizen);
                var actionResult = Assert.IsType<ActionResult<Citizen>>(postedCitizen);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.Equal("PostCitizen", createdAtActionResult.ActionName);
                var citizenViewModelCreated = Assert.IsType<CreateCitizenViewModel>(createdAtActionResult.Value);


                Assert.True(ccvm.FirstName.Equals(citizenViewModelCreated.FirstName));
                Assert.True(ccvm.LastName.Equals(citizenViewModelCreated.LastName));
                Assert.True(ccvm.CPR.Equals(citizenViewModelCreated.CPR));

                Assert.True(ccvm.RelativeFirstName.Equals(citizenViewModelCreated.RelativeFirstName));
                Assert.True(ccvm.RelativeLastName.Equals(citizenViewModelCreated.RelativeLastName));
                Assert.True(ccvm.PhoneNumber.Equals(citizenViewModelCreated.PhoneNumber));
                Assert.True(ccvm.Relation.Equals(citizenViewModelCreated.Relation));
                Assert.True(ccvm.IsPrimary.Equals(citizenViewModelCreated.IsPrimary));

                Assert.True(ccvm.StartDate.Equals(citizenViewModelCreated.StartDate));
                Assert.True(ccvm.ReevaluationDate.Equals(citizenViewModelCreated.ReevaluationDate));
                Assert.True(ccvm.PlannedDischargeDate.Equals(citizenViewModelCreated.PlannedDischargeDate));
                Assert.True(ccvm.ProspectiveSituationStatusForCitizen.Equals(citizenViewModelCreated.ProspectiveSituationStatusForCitizen));

                Assert.True(ccvm.CareNeed.Equals(citizenViewModelCreated.CareNeed));
                Assert.True(ccvm.PurposeOfStay.Equals(citizenViewModelCreated.PurposeOfStay));

                Assert.True(ccvm.RespiteCareHomeName.Equals(citizenViewModelCreated.RespiteCareHomeName));
                Assert.True(ccvm.Type.Equals(citizenViewModelCreated.Type));
            }
        }

        [Fact]
        public async void PostCitizen_TryPostCitizenWithNoCPR_ReturnsBadRequest()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = null, // With null CPR

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = "Kærgården",
                    Type = 0
                };

                //Act
                var postedCitizen = await createCitizenController.PostCitizen(ccvm);

                //Assert
                var actionResult = Assert.IsType<ActionResult<Citizen>>(postedCitizen);
                Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }

        [Fact]
        public async void PostCitizen_TryPostCitizenWithNoChosenRespiteCareHome_ReturnsBadRequest()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 1204115947,

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = null,
                    Type = 0
                };

                //Act
                var postedCitizen = await createCitizenController.PostCitizen(ccvm);

                //Assert
                var actionResult = Assert.IsType<ActionResult<Citizen>>(postedCitizen);
                Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }

        [Fact]
        public async void PostCitizen_TryPostCitizenToNoAvailableRespiteCareRoom()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 1204115947,

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = "Kærgården",
                    Type = 1
                };

                //Act
                var postedCitizen = await createCitizenController.PostCitizen(ccvm);

                //Assert
                var actionResult = Assert.IsType<ActionResult<Citizen>>(postedCitizen);
                Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }

        [Fact]
        public async void PostCitizen_TryPostAlreadyExistingCitizen()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 2905891233,

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = "Kærgården",
                    Type = 0
                };

                //Act & Assert
                //Use ArgumentException in tests and not DbUpdateException as InMemory seems to be
                //using Collections as database
                //Correctly:
                //await Assert.ThrowsAsync<DbUpdateException>(() => createCitizenController.PostCitizen(ccvm));
                await Assert.ThrowsAsync<ArgumentException>(() => createCitizenController.PostCitizen(ccvm));
            }
        }

        [Fact]
        public async void PutCitizen_UpdatesCitizenInDatabase()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 2905891233,

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = "Kærgården",
                    Type = 0
                };

                //Act
                var postedCitizen = await createCitizenController.PutCitizen(ccvm);

                //Assert
                Assert.NotNull(postedCitizen);
                var actionResult = Assert.IsType<ActionResult<Citizen>>(postedCitizen);
                Assert.IsType<NoContentResult>(actionResult.Result);
            }
        }

        [Fact]
        public async void PutCitizen_TryPutExistingCitizen()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);

                CreateCitizenViewModel ccvm = new CreateCitizenViewModel
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 1204115947,

                    RelativeFirstName = "Borger Jr.",
                    RelativeLastName = "Borgersen",
                    PhoneNumber = 12345678,
                    Relation = "Søn",
                    IsPrimary = true,

                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 12, 24),
                    ProspectiveSituationStatusForCitizen = "Afklaret",

                    CareNeed = "Behov for hjælp i køkken",
                    PurposeOfStay = "Få det bedre",

                    RespiteCareHomeName = "Kærgården",
                    Type = 0
                };

                //Act
                var postedCitizen = await createCitizenController.PutCitizen(ccvm);

                //Assert
                Assert.NotNull(postedCitizen);
                var actionResult = Assert.IsType<ActionResult<Citizen>>(postedCitizen);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }
        }

        [Fact]
        public async void GetCreateCitizin_GetsCreateCitizenViewModel()
        {
            using (var context = new RCCSContext(_contextOptions))
            {
                //Arrange
                var createCitizenController = new CreateCitizenController(context);
                var id = 2905891233;

                //Act
                var createCitizenViewModel = await createCitizenController.GetCreateCitizin(id);

                //Assert
                Assert.NotNull(createCitizenViewModel);
                var actionResult = Assert.IsType<ActionResult<CreateCitizenViewModel>>(createCitizenViewModel);
                var ccvm = Assert.IsType<CreateCitizenViewModel>(actionResult.Value);

                Assert.Equal(id, ccvm.CPR);
                Assert.Equal("Citizen Citizensen", ccvm.FirstName + " " + ccvm.LastName);
                Assert.Equal("Kærgården", ccvm.RespiteCareHomeName);
                Assert.Equal(new DateTime(2021, 01, 29), ccvm.PlannedDischargeDate);
            }
        }
    }
}
