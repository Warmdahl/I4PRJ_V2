using System;
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
    public class CitizenInformationController : ControllerBase
    {
        private readonly RCCSContext _context;

        public CitizenInformationController(RCCSContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenInformationViewModel>> GetCitizenInformation(string id)
        {
            var citizenInfo = new CitizenInformationViewModel();

            var citizen
                = await _context.Citizens
                    .AsNoTracking()
                    .Include(c => c.ResidenceInformation)
                    .Include(c => c.RespiteCareRoom)
                        .ThenInclude(c => c.RespiteCareHome)
                    .Include(c => c.Relatives)
                    .Include(c => c.ProgressReports)
                    .Include(c => c.CitizenOverview)
                    .SingleAsync(c => c.CPR.ToString() == id);

            //Calculate Time until discharge for citizen
            var currentDate = DateTime.Now;
            var dischargeDate = citizen.ResidenceInformation.PlannedDischargeDate;
            var dischargeTimeSpan = dischargeDate - currentDate;
            var daysUntilDiscarge = (int)(dischargeTimeSpan.TotalDays);
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

            //Calculate age for citizen
            long birthday = citizen.CPR;

            static int[] Long2ManyInt(long birthday)
            {
                int days = (int) (birthday / 100000000);
                int tempMonth = (int) (birthday / 1000000);
                int month = tempMonth - (days * 100);
                int year = ((int) (birthday / 10000)) - (tempMonth * 100);

                return new int[]{days, month, year};
            }

            int[] happy = Long2ManyInt(birthday);
            DateTime birthdate = new DateTime((1900+happy[2]),happy[1],happy[0]);
            var ageintime = currentDate.Date - birthdate;
            int age = (int) ((ageintime.Days / 365.25) - 0.5);

            //Building the citizen
            citizenInfo.CPR = citizen.CPR.ToString();
            citizenInfo.CitizenName = citizen.FirstName + " " + citizen.LastName;
            citizenInfo.Age = age.ToString(); //Needs to be implemented
            citizenInfo.RespiteCareHome = citizen.RespiteCareRoom.RespiteCareHomeName;
            citizenInfo.CareHomeAddress = citizen.RespiteCareRoom.RespiteCareHome.Address;
            citizenInfo.CareHomeType = citizen.RespiteCareRoom.Type;
            citizenInfo.DateOfAdmission = citizen.ResidenceInformation.StartDate.Date;
            citizenInfo.EvaluationDate = citizen.ResidenceInformation.ReevaluationDate.Date;

            citizenInfo.TimeUntilDischarge = timeUntilDiscarge;
            citizenInfo.ProspectiveSituationStatusForCitizen =
            citizen.ResidenceInformation.ProspectiveSituationStatusForCitizen;
            citizenInfo.AmountOfEvaluations = citizen.CitizenOverview.NumberOfReevaluations;
            citizenInfo.CareNeed = citizen.CitizenOverview.CareNeed;
            citizenInfo.PurposeOfStay = citizen.CitizenOverview.PurposeOfStay;
            citizenInfo.ProgressReports = citizen.ProgressReports; //List
            citizenInfo.Relatives = citizen.Relatives; //List

            //citizenInfo = temp;
            return citizenInfo;
        }
    }
}
