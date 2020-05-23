using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.ViewModel;

namespace RCCS.DatabaseAPI.RCCSCitizenResidencyDbViewControllers
{
    [Route("rccsdb/[controller]")]
    [ApiController]
    [Authorize]
    public class CitizenListController : ControllerBase
    {
        private readonly RCCSContext _context;

        public CitizenListController(RCCSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenListViewModel>>> GetCitizenList()
        {
            Console.WriteLine("CitizenListController activated");
            List<CitizenListViewModel> clvmList = new List<CitizenListViewModel>();

            var citizens
                = await _context.Citizens
                    .AsNoTracking()
                    .Include(c => c.ResidenceInformation)
                    .Include(c => c.RespiteCareRoom)
                    .ToListAsync();

            foreach (var citizen in citizens)
            {
                var currentDate = DateTime.Now;
                var dischargeDate = citizen.ResidenceInformation.PlannedDischargeDate;
                var dischargeTimeSpan = dischargeDate - currentDate;
                var daysUntilDiscarge = (int) (dischargeTimeSpan.TotalDays);
                string timeUntilDiscarge = null;

                if (daysUntilDiscarge < 0)
                {
                    timeUntilDiscarge = "Udskrevet";
                }
                else if (daysUntilDiscarge < 7)
                {
                    timeUntilDiscarge = daysUntilDiscarge + " dage";
                }
                else
                {
                    timeUntilDiscarge = (daysUntilDiscarge / 7) + " uger";
                }

                CitizenListViewModel clvm = new CitizenListViewModel
                {
                    CPR = citizen.CPR.ToString(),
                    CitizenName = citizen.FirstName + " " + citizen.LastName,
                    RespiteCareHome = citizen.RespiteCareRoom.RespiteCareHomeName,
                    TimeUntilDischarge = timeUntilDiscarge,
                    ProspectiveSituationStatusForCitizen = citizen.ResidenceInformation.ProspectiveSituationStatusForCitizen
                };

                clvmList.Add(clvm);
            }

            return clvmList;
        }
    }
}
