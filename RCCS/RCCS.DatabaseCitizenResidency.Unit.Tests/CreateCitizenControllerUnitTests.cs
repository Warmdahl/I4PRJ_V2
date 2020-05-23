using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseAPI.RCCSCitizenResidencyDbViewControllers;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;
using RCCS.DatabaseCitizenResidency.ViewModel;
using Xunit;

namespace RCCS.DatabaseCitizenResidency.Unit.Tests
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
            _contextOptions = new DbContextOptionsBuilder<RCCSContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new RCCSContext(_contextOptions))
            {
                context.Database.EnsureCreated();

                var citizen = new Citizen
                {
                    FirstName = "Citizen",
                    LastName = "Citizensen",
                    CPR = 2905891233
                };

                var relative = new Relative
                {
                    FirstName = "Relative",
                    LastName = "Relativesen",
                    PhoneNumber = 12345678,
                    Relation = "Daughter",
                    IsPrimary = true,
                    Citizen = citizen
                };

                var ri = new ResidenceInformation
                {
                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 01, 29),
                    ProspectiveSituationStatusForCitizen = "Uafklaret",
                    Citizen = citizen
                };

                var co = new CitizenOverview
                {
                    PurposeOfStay = "Get better",
                    CareNeed = "Lots",
                    NumberOfReevaluations = 0,
                    Citizen = citizen
                };

                var respiteCareHome = new RespiteCareHome
                {
                    Address = "RespiteCareHome Vej 19",
                    Name = "Kærgården",
                    AvailableRespiteCareRooms = 1,
                    RespiteCareRoomsTotal = 2,
                    PhoneNumber = 12345678,
                };

                var respiteCareRoom = new RespiteCareRoom
                {
                    Type = "Demensbolig",
                    RoomNumber = 1,
                    IsAvailable = false,
                    RespiteCareHome = respiteCareHome,
                    Citizen = citizen
                };

                var respiteCareRoom1 = new RespiteCareRoom
                {
                    Type = "Alm. plejebolig",
                    RoomNumber = 2,
                    IsAvailable = true,
                    RespiteCareHome = respiteCareHome
                };

                context.RespiteCareRooms.AddRange(respiteCareRoom, respiteCareRoom1);
                context.Relatives.Add(relative);
                context.ResidenceInformations.Add(ri);
                context.CitizenOverviews.Add(co);
                context.Citizens.Add(citizen);

                context.SaveChanges();
            }
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
    }
}
