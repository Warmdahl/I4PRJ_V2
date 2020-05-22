using System;
using System.Collections.Generic;
using System.Linq;
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

                var citizen1 = new Citizen
                {
                    FirstName = "Borger",
                    LastName = "Borgersen",
                    CPR = 1212194665
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

                var ri1 = new ResidenceInformation
                {
                    StartDate = DateTime.Now,
                    ReevaluationDate = DateTime.Now.AddDays(14),
                    PlannedDischargeDate = new DateTime(2021, 09, 11),
                    ProspectiveSituationStatusForCitizen = "Revurderingsbehov",
                    Citizen = citizen1
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
                    IsAvailable = false,
                    RespiteCareHome = respiteCareHome,
                    Citizen = citizen1
                };

                context.RespiteCareRooms.AddRange(respiteCareRoom, respiteCareRoom1);
                context.Relatives.Add(relative);
                context.ResidenceInformations.AddRange(ri, ri1);
                context.CitizenOverviews.Add(co);
                context.Citizens.Add(citizen);

                context.SaveChanges();
            }
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
