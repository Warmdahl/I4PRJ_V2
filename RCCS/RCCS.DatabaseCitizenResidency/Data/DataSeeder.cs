using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Model;

namespace RCCS.DatabaseCitizenResidency.Data
{
    public static class DataSeeder
    {
        public static void SeedCitizenResidencyDb(RCCSContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Citizens.Any() &&
                !context.CitizenOverviews.Any() &&
                !context.ProgressReports.Any() &&
                !context.Relatives.Any() &&
                !context.ResidenceInformations.Any() &&
                !context.RespiteCareHomes.Any() &&
                !context.RespiteCareRooms.Any())
            {
                SeedData1(context);
                SeedData2(context);
                SeedRespiteCareHomeData(context);
            }
        }

        static void SeedData1(RCCSContext context)
        {
            var citizen = new Citizen
            {
                FirstName = "Anker",
                LastName = "Larsen",
                CPR = 1203451679
            };

            var relative = new Relative
            {
                FirstName = "Lone",
                LastName = "Jensen",
                PhoneNumber = 78456598,
                Relation = "Datter",
                IsPrimary = true,
                Citizen = citizen
            };

            var citizenOverview = new CitizenOverview
            {
                PurposeOfStay = "Genoptræning",
                CareNeed = "Stort plejebehov",
                NumberOfReevaluations = 3,
                Citizen = citizen
            };

            var respiteCareHome = new RespiteCareHome
            {
                AvailableRespiteCareRooms = 4,
                RespiteCareRoomsTotal = 5,
                PhoneNumber = 45121225,
                Address = "Smørvej 14",
                Name = "Kærgården"
            };

            var respiteCareRoom = new RespiteCareRoom
            {
                RoomNumber = 1,
                Type = "Alm. plejebolig",
                IsAvailable = false,
                RespiteCareHome = respiteCareHome,
                Citizen = citizen
            };

            List<RespiteCareRoom> respiteCareRooms = new List<RespiteCareRoom>();

            for (var i = 1; i <= respiteCareHome.AvailableRespiteCareRooms; i++)
            {
                respiteCareRooms.Add(new RespiteCareRoom
                {
                    RoomNumber = i+1,
                    Type = "Demensbolig",
                    IsAvailable = true,
                    RespiteCareHome = respiteCareHome,
                    Citizen = null
                });
            }

            foreach (var careRoom in respiteCareRooms)
            {
                context.RespiteCareRooms.Add(careRoom);
            }

            var residentInfo = new ResidenceInformation
            {
                StartDate = new DateTime(2020, 3, 14),
                ReevaluationDate = new DateTime(2020, 4, 17),
                PlannedDischargeDate = new DateTime(2020, 4, 24),
                ProspectiveSituationStatusForCitizen = "Afklaret",
                Citizen = citizen
            };

            var progressReport = new ProgressReport
            {
                Title = "Ingen ændring",
                Date = new DateTime(2020, 4, 17),
                Report = "Anker har etc.",
                ResponsibleCaretaker = "Dorte Hansen",
                Citizen = citizen
            };


            //Adds citizen
            context.ProgressReports.Add(progressReport);
            context.Relatives.Add(relative);
            context.CitizenOverviews.Add(citizenOverview);
            context.ResidenceInformations.Add(residentInfo);

            //Adds room and home
            context.RespiteCareRooms.Add(respiteCareRoom);

            context.SaveChanges();
        }

        static void SeedData2(RCCSContext context)
        {
            var citizen1 = new Citizen
            {
                FirstName = "Jens",
                LastName = "Jensen",
                CPR = 3008378183
            };

            var relative1 = new Relative
            {
                FirstName = "Trine",
                LastName = "Sørensen",
                PhoneNumber = 85123298,
                Relation = "Kone",
                IsPrimary = true,
                Citizen = citizen1
            };

            var citizenOverview1 = new CitizenOverview
            {
                PurposeOfStay = "Genoptræning",
                CareNeed = "Stort plejebehov",
                NumberOfReevaluations = 3,
                Citizen = citizen1
            };

            var respiteCareHome1 = new RespiteCareHome
            {
                AvailableRespiteCareRooms = 14,
                RespiteCareRoomsTotal = 15,
                PhoneNumber = 45983256,
                Address = "Lindholmsvej 23",
                Name = "Lindholm"
            };

            var respiteCareRoom1 = new RespiteCareRoom
            {
                RoomNumber = 1,
                Type = "Demensbolig",
                IsAvailable = false,
                CitizenCPR = citizen1.CPR,
                RespiteCareHome = respiteCareHome1
            };

            List<RespiteCareRoom> respiteCareRooms = new List<RespiteCareRoom>();

            for (var i = 2; i <= respiteCareHome1.AvailableRespiteCareRooms; i++)
            {
                respiteCareRooms.Add(new RespiteCareRoom
                {
                    RoomNumber = i,
                    Type = "Alm. plejebolig",
                    IsAvailable = true,
                    RespiteCareHome = respiteCareHome1,
                    Citizen = null
                });
            }

            foreach (var careRoom in respiteCareRooms)
            {
                context.RespiteCareRooms.Add(careRoom);
            }

            var residentInfo1 = new ResidenceInformation
            {
                StartDate = new DateTime(2020, 5, 12),
                ReevaluationDate = new DateTime(2020, 5, 30),
                PlannedDischargeDate = new DateTime(2020, 7, 24),
                ProspectiveSituationStatusForCitizen = "Revurderingsbehov",
                Citizen = citizen1
            };

            var progressReport1 = new ProgressReport
            {
                Title = "I bedring",
                Date = new DateTime(2020, 5, 13),
                Report = "Jens er osv etc.",
                ResponsibleCaretaker = "Steen Steensen",
                Citizen = citizen1
            };

            //Adds citizen
            context.ProgressReports.Add(progressReport1);
            context.Relatives.Add(relative1);
            context.CitizenOverviews.Add(citizenOverview1);
            context.ResidenceInformations.Add(residentInfo1);

            //Adds room and home
            context.RespiteCareRooms.Add(respiteCareRoom1);

            context.SaveChanges();
        }

        static void SeedRespiteCareHomeData(RCCSContext context)
        {
            var respiteCareHome1 = new RespiteCareHome
            {
                AvailableRespiteCareRooms = 15,
                RespiteCareRoomsTotal = 15,
                PhoneNumber = 48327898,
                Address = "Magarinevej 23",
                Name = "Bakkedal"
            };

            List<RespiteCareRoom> repiteCareRooms = new List<RespiteCareRoom>();

            for (var i = 0; i < respiteCareHome1.RespiteCareRoomsTotal; i++)
            {
                if (i < 7)
                {
                    repiteCareRooms.Add(new RespiteCareRoom
                    {
                        RoomNumber = i + 1,
                        Type = "Demensbolig",
                        IsAvailable = true,
                        RespiteCareHome = respiteCareHome1,
                        Citizen = null
                    });
                }
                else
                {
                    repiteCareRooms.Add(new RespiteCareRoom
                    {
                        RoomNumber = i + 1,
                        Type = "Alm. plejebolig",
                        IsAvailable = true,
                        RespiteCareHome = respiteCareHome1,
                        Citizen = null
                    });
                }
            }

            context.RespiteCareHomes.Add(respiteCareHome1);

            foreach (var respiteCareRoom in repiteCareRooms)
            {
                context.RespiteCareRooms.Add(respiteCareRoom);
            }

            context.SaveChanges();
        }
    }
}
