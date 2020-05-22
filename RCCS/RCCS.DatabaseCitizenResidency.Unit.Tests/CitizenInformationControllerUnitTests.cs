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
    public class CitizenInformationControllerUnitTests
    {
        private readonly DbContextOptions<RCCSContext> _contextOptions;

        //Setup
        public CitizenInformationControllerUnitTests()
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
                    PhoneNumber = 12345678,
                };

                var respiteCareRoom = new RespiteCareRoom
                {
                    IsAvailable = true,
                    RespiteCareHomeName = "Kærgården",
                    RespiteCareHome = respiteCareHome,
                    Citizen = citizen
                };


                context.RespiteCareRooms.AddRange(respiteCareRoom);
                context.Relatives.Add(relative);
                context.ResidenceInformations.Add(ri);
                context.CitizenOverviews.Add(co);
                context.Citizens.Add(citizen);

                context.SaveChanges();
            }
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
                var taskResult = Assert.IsType<ActionResult<CitizenInformationViewModel>>(citizenInformationViewModel);
            }
        }
    }
}
