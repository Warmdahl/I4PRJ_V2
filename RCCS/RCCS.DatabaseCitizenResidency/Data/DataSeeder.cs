using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Model;

namespace RCCS.DatabaseCitizenResidency.Data
{
    public class DataSeeder
    {
        private readonly RCCSContext _context;

        public DataSeeder(RCCSContext context)
        {
            _context = context;
        }

        public void SeedData1()
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
                _context.RespiteCareRooms.Add(careRoom);
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
            _context.ProgressReports.Add(progressReport);
            _context.Relatives.Add(relative);
            _context.CitizenOverviews.Add(citizenOverview);
            _context.ResidenceInformations.Add(residentInfo);

            //Adds room and home
            _context.RespiteCareRooms.Add(respiteCareRoom);

            _context.SaveChanges();
        }

        public void SeedData2()
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
                _context.RespiteCareRooms.Add(careRoom);
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
            _context.ProgressReports.Add(progressReport1);
            _context.Relatives.Add(relative1);
            _context.CitizenOverviews.Add(citizenOverview1);
            _context.ResidenceInformations.Add(residentInfo1);

            //Adds room and home
            _context.RespiteCareRooms.Add(respiteCareRoom1);

            _context.SaveChanges();
        }

        public void SeedRespiteCareHomeData()
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

            _context.RespiteCareHomes.Add(respiteCareHome1);

            foreach (var respiteCareRoom in repiteCareRooms)
            {
                _context.RespiteCareRooms.Add(respiteCareRoom);
            }

            _context.SaveChanges();
        }
    }
}
