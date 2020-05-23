using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;

namespace RCCS.DatabaseCitizenResidency.Unit.Tests.Utilities
{
    internal class CreateCitizenControllerDatabaseSetup
    {
        public DbContextOptions<RCCSContext> SetupDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<RCCSContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new RCCSContext(contextOptions))
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

            return contextOptions;
        }
    }
}
