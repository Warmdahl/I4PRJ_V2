using System;
using System.Linq;
using RCCS.DatabaseUsers.Data;
using RCCS.DatabaseUsers.Model.Entities;
using static BCrypt.Net.BCrypt;

namespace RCCS.DatabaseUsers.Data
{
    public static class DataSeederUsers
    {
        public const int BcryptWorkfactor = 10;

        public static void SeedUsers(RCCSUsersContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
                SeedAccounts(context);
            if (!context.Admins.Any())
                SeedAdmins(context);
            if (!context.Coordinators.Any())
                SeedCoordinators(context);
            if (!context.NursingStaffs.Any())
                SeedNursingStaffs(context);
        }

        static void SeedAccounts(RCCSUsersContext context)
        {
            context.Users.AddRange(
            // Seed admin
            new EfUser
            {
                PersonaleId = "Admin",
                PwHash = HashPassword("Admin", BcryptWorkfactor),
                Role = Role.Admin
            },
            // Seed Coordinator
            new EfUser
            {
                PersonaleId = "Coordinator",
                PwHash = HashPassword("Coordinator", BcryptWorkfactor),
                Role = Role.Coordinator
            },
            // Seed NursingStaff
            new EfUser
            {
                PersonaleId = "NursingStaff",
                PwHash = HashPassword("NursingStaff", BcryptWorkfactor),
                Role = Role.NursingStaff
            }
            // TO DO: Seed other users
            );
            context.SaveChanges();
        }

        static void SeedAdmins(RCCSUsersContext context)
        {
            context.Admins.Add(
                new EfAdmin
                {
                    EfUserId = 1,
                    PersonaleId = "Admin",
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",

                });
            context.SaveChanges();
        }
        static void SeedCoordinators(RCCSUsersContext context)
        {
            context.Coordinators.Add(
                new EfCoordinator()
                {
                    EfUserId = 2,
                    PersonaleId = "Coordinator",
                    FirstName = "CoordinatorFirstName",
                    LastName = "CoordinatorLastName",

                });
            context.SaveChanges();
        }
        static void SeedNursingStaffs(RCCSUsersContext context)
        {
            context.NursingStaffs.Add(
                new EfNursingStaff()
                {
                    EfUserId = 3,
                    PersonaleId = "NursingStaff",
                    FirstName = "NursingStaffFirstName",
                    LastName = "NursingStaffLastName",

                });
            context.SaveChanges();
        }
    }
}
