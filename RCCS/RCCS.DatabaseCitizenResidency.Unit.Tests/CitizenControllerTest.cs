using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseAPI.RCCSDbControllers;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;
using Xunit;

namespace RCCS.DatabaseCitizenResidency.Unit.Tests
{
    /*
     * Code inspired from https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1#unit-testing-controllers
     * Written by Steve Smith
     */
    public class CitizenControllerTest
    {
        [Fact]
        public async void GetCitizens_Returns_List_Of_Citizens()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<RCCSContext>()
                .UseSqlite(connection)
                .Options;


            using (var context = new RCCSContext(options))
            {
                context.Database.EnsureCreated();

                context.Citizens.AddRange(
                    new Citizen 
                    {
                        FirstName = "Jens",
                        LastName = "Jensen",
                        CPR = 2020201987
                    },
                    new Citizen
                    {
                        FirstName = "Anker",
                        LastName = "Sørensen",
                        CPR = 2121211947
                    });

                context.SaveChanges();
            }

            using (var context = new RCCSContext(options))
            {
                var citizenController = new CitizenController(context);

                var citizenList = await citizenController.GetCitizens();

                Assert.NotNull(citizenList);
                var actionResult = Assert.IsType<ActionResult<List<Citizen>>>(citizenList);
                var returnValue = Assert.IsAssignableFrom<IEnumerable<Citizen>>(actionResult.Value);
            }
        }
    }
}